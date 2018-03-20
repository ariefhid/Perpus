using Perpus.Core.Abstract;
using Perpus.Core.Concrete.Base;
using Perpus.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Concrete
{
    public class OutboundStore<T> : BaseStore<T>, IOutboundStore where T : BaseContext, new()
    {
        #region Constructor
        public OutboundStore()
        {

        }
        #endregion
        #region IOutboundStore
        public IQueryable<Outbound> Outbounds
        {
            get { return this.context.Set<Outbound>(); }
        }

        public IQueryable<Outbound> Entities { get { return this.context.Set<Outbound>(); } }

        public async Task<Outbound> FindByIdAsync(int Id)
        {
            return await this.context
                 .Set<Outbound>().SingleOrDefaultAsync(o => o.Id == Id);
        }

        public async Task<int> CreateAsync(IEnumerable<Outbound> entities)
        {
            this.Create(entities);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(IEnumerable<Outbound> entities)
        {
            this.SetEntityState(entities,EntityState.Modified);
            return await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(IEnumerable<Outbound> entities)
        {
            this.SetEntityState(entities, EntityState.Deleted);
            return await this.context.SaveChangesAsync();
        }

     
        #endregion
    }
}
