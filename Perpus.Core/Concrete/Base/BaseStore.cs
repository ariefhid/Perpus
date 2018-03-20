using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Perpus.Core.Entity.Base;
using Perpus.Core.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Perpus.Core.Abstract;

namespace Perpus.Core.Concrete.Base
{
    /// <summary>
    /// base store, use this object when saving entity object to trigger the persistence event
    /// </summary>
    public abstract class BaseStore<T> : IPersistenceEvent where T : BaseContext, new()
    {
        #region private declaration
        private IOutboundManager _outboundManager;
        private IAuditProcessor<string> _auditProcessor;

        private async Task exportOutbound(IEnumerable<Outbound> outboundMessages)
        {
            try
            {
                if (outboundMessages.Count() > 0)
                {
                    bool exportResult = false;
                    List<Outbound> exportedMessages = new List<Outbound>();
                    foreach (var outboundMessage in outboundMessages)
                    {
                        exportResult = await this._outboundManager.ExportOutboundAsync(outboundMessage);

                        if (exportResult)
                        {
                            exportedMessages.Add(outboundMessage);
                        }
                    }
                    await this._outboundManager.CleanupExportedAsync(exportedMessages);
                    await this.context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
        }

        private IEnumerable<Outbound> generateOutboundMessages()
        {
            var savedEntries = this.GetSavedEntries();
            var deletedEntries = this.GetDeletedEntries();
            //Generate outbound data
            IEnumerable<Outbound> outboundMessages = this._outboundManager.CreateOutboundData(savedEntries, deletedEntries);
            if (outboundMessages != null && outboundMessages.Count() > 0)
            {

                this.Create(outboundMessages);
            }
            return outboundMessages;
        }
        #endregion

        #region protected method
        protected T context { get; private set; }

        protected IEnumerable<EntityEntry> GetSavedEntries()
        {
            return context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
        }

        protected IEnumerable<EntityEntry> GetDeletedEntries()
        {
            return context.ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Deleted);
        }

        /// <summary>
        /// use this method when you create or update entity
        /// This method will invoked the integration routine & trigger  persistence event
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected async Task<int> SaveAsync(IEnumerable<object> objects)
        {
            //trigger persistence event before save
            await this.BeforeSaveAsync(objects);

            IEnumerable<Outbound> outboundMessages = generateOutboundMessages();
            
            ////audit the entity
            //var auditEntities = this._auditProcessor.AuditSavedEntities(this.GetSavedEntries());
            //if (auditEntities.Item1.Any())
            //{
            //    await this.context.Audits.AddRangeAsync(auditEntities.Item1);
            //}

            int result = await this.context.SaveChangesAsync();

            //Audit
            //if (auditEntities != null && auditEntities.Item2.Any())
            //{
            //    var generatedDbValues = this._auditProcessor.AuditGeneratedDbValues(auditEntities.Item2);
            //    await this.context.Audits.AddRangeAsync(generatedDbValues);
            //    result += await this.context.SaveChangesAsync();
            //}

            //trigger persistence event before save
            await this.AfterSaveAsync(objects);

            // no need to wait for it to finish because it usually call a remote server
            Task process = Task.Factory.StartNew(async () =>
            {
                await this.exportOutbound(outboundMessages);
            });
            return result;
        }

        /// <summary>
        /// Use this method when you delete an entity
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected async Task<int> SaveDeleteAsync(IEnumerable<object> objects)
        {
            await this.BeforeDeleteAsync(objects);
            IEnumerable<Outbound> outboundMessages = generateOutboundMessages();

            ////TODO:check the audit is enable
            //var auditEntities = this._auditProcessor.AuditSavedEntities(this.GetDeletedEntries());
            //await this.context.Audits.AddRangeAsync(auditEntities.Item1);

            int result = await this.context.SaveChangesAsync();

            await this.AfterDeleteAsync();

            Task process = Task.Factory.StartNew(async () =>
            {
                await this.exportOutbound(outboundMessages);
            });
            return result;
        }

        protected void SetEntityState<TEntity>(IEnumerable<TEntity> TEntities, EntityState entityState) where TEntity : class
        {
            foreach (TEntity tObject in TEntities)
            {
                this.context.Entry<TEntity>(tObject).State = entityState;
            }
        }

        protected void Create<TEntity>(IEnumerable<TEntity> tObjects) where TEntity : class
        {
            this.context.Set<TEntity>().AddRange(tObjects);
        }
        protected void Delete<TEntity>(params TEntity[] tObjects) where TEntity : class
        {
            this.context.Set<TEntity>().RemoveRange(tObjects);
        }

        #region IPersistenceStore
        /// <summary>
        /// Get the original value
        /// </summary>
        /// <typeparam name="T1">Object type</typeparam>
        /// <typeparam name="T2">Column type</typeparam>
        /// <param name="entity"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public T2 GetOriginalValue<T1, T2>(T1 entity, string propertyName) where T1 : class
        {
            return this.context.Entry<T1>(entity).OriginalValues.GetValue<T2>(propertyName);
        }
        #endregion


        protected TEntity FindTrackedLocalEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return this.context.Set<TEntity>().Local.SingleOrDefault(t => t.Id == entity.Id);
        }

        #endregion

        #region constructor
        //public BaseStore(IOutboundManager outboundManager)
        //{
        //    this.context = new T();
        //    this._outboundManager = outboundManager;
        //}
        //public BaseStore(T context, IOutboundManager outboundManager)
        //{
        //    this.context = context;
        //    this._outboundManager = outboundManager;
        //}

        /// <summary>
        /// Use this constructor on your store object to utilize integration and audit flow
        /// </summary>
        /// <param name="context"></param>
        /// <param name="outboundManager"></param>
        /// <param name="auditProcessor"></param>
        public BaseStore(T context, IOutboundManager outboundManager, IAuditProcessor<string> auditProcessor)
        {
            this.context = context;
            this._outboundManager = outboundManager;
            this._auditProcessor = auditProcessor;
        }
        /// <summary>
        /// Use this constructor on your store object to utilize integration and without audit flow
        /// </summary>
        /// <param name="context"></param>
        /// <param name="outboundManager"></param>
        public BaseStore(T context, IOutboundManager outboundManager)
        {
            this.context = context;
            this._outboundManager = outboundManager;
        }
        /// <summary>
        /// This constructor is only used for non auditable object Eg: outbound data
        /// </summary>
        public BaseStore()
        {
            this.context = new T();
        }
        #endregion

        #region IPersistenceEvent
        public virtual Task BeforeSaveAsync(IEnumerable<object> objects)
        {
            return Task.Factory.StartNew(() => { });
        }
        public virtual Task AfterSaveAsync(IEnumerable<object> objects)
        {
            return Task.Factory.StartNew(() => { });
        }
        public virtual Task BeforeDeleteAsync(IEnumerable<object> objects)
        {
            return Task.Factory.StartNew(() => { });
        }
        public virtual Task AfterDeleteAsync()
        {
            return Task.Factory.StartNew(() => { });
        }
        #endregion
    }
}