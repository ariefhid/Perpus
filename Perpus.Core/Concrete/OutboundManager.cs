using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perpus.Core.Concrete.Base;
using Perpus.Core.Abstract;
using Perpus.Core.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Perpus.Core;

namespace Perpus.Core.Concrete
{
    /// <summary>
    /// The default outbound manager class, override this to control application integration
    /// </summary>
    public class OutboundManager : BaseManager, IOutboundManager
    {
        #region Private Declaration
        private IOutboundStore outboundStore;
        #endregion
        #region Constructor
        /// <summary>
        /// outbound constructor
        /// </summary>
        /// <param name="outboundStore">IOutboundStore</param>
        public OutboundManager(IOutboundStore outboundStore)
        {
            this.outboundStore = outboundStore;
        }
        #endregion
        #region IOutboundManager

        /// <summary>
        /// outbound entities
        /// </summary>
        public IQueryable<Outbound> Outbounds
        {
            get { return this.outboundStore.Entities; }
        }
        /// <summary>
        /// Generate outbound date based on save/deleted entries
        /// The returned data will be saved by the core runtime
        /// </summary>
        /// <param name="savedEntities"></param>
        /// <param name="deletedEntities"></param>
        /// <returns></returns>
        public  virtual IEnumerable<Outbound> CreateOutboundData(IEnumerable<EntityEntry> savedEntities, IEnumerable<EntityEntry> deletedEntities)
        {
            var outbounds = new List<Outbound>();
            return outbounds;
        }

        /// <summary>
        /// export the outbound data to third party application
        /// nothing todo on base
        /// </summary>
        /// <param name="outbound"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExportOutboundAsync(Outbound outbound)
        {
            return await Task.Factory.StartNew(() => false);
        }

        /// <summary>
        /// nothing todo on base
        /// </summary>
        /// <returns></returns>
        public virtual Task ExportOutboundAsync()
        {
            return Task.Factory.StartNew(() => {});
        }
        /// <summary>
        /// Find the outbound entity by it's Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Outbound> FindByIdAsync(int Id)
        {
            return await this.outboundStore
                .Entities
                .SingleOrDefaultAsync(o => o.Id == Id);
        }

        /// <summary>
        /// Create outbound
        /// </summary>
        /// <param name="entities">outbound entities</param>
        /// <returns></returns>
        public async Task<PerpusResult> CreateAsync(params Outbound [] entities)
        {
            await this.outboundStore.CreateAsync(entities);
            
            return PerpusResult.Success;
        }

        /// <summary>
        /// update outbound
        /// </summary>
        /// <param name="entities">outbound entities</param>
        /// <returns></returns>
        public async Task<PerpusResult> UpdateAsync(params Outbound[] entities)
        {
            await this.outboundStore.UpdateAsync(entities);
            return PerpusResult.Success;
        }

        /// <summary>
        /// delete outbound
        /// </summary>
        /// <param name="entities">outbound entities</param>
        /// <returns></returns>
        public async Task<PerpusResult> DeleteAsync(params Outbound[] entities)
        {
            await this.outboundStore.DeleteAsync(entities);
            return PerpusResult.Success;
        }

        /// <summary>
        /// remove succesfully exported outbound data
        /// </summary>
        /// <param name="exportedOutbounds">outbound entities</param>
        /// <returns></returns>
        public async Task CleanupExportedAsync(IEnumerable<Outbound> exportedOutbounds)
        {
            await this.DeleteAsync(exportedOutbounds.ToArray());
        }
        #endregion





    }
}
