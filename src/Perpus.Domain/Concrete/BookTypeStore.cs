using Perpus.Core.Concrete.Base;
using Perpus.Domain.Abstract;
using Perpus.Domain.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;
using Perpus.Core.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Perpus.Domain.Concrete
{
    public class BookTypeStore : BaseStore<PerpusContext>, IBookTypeStore
    {
        public BookTypeStore(PerpusContext context, IOutboundManager outboundManager) : base(context, outboundManager) { }
        IQueryable<BookType> IPersistenceStore<BookType>.Entities => this.context.BookTypes;
        public async Task<int> CreateAsync(IEnumerable<BookType> entities)
        {
            await this.context.AddRangeAsync(entities);
            return await this.SaveAsync(entities);
        }
        public async Task<int> DeleteAsync(IEnumerable<BookType> entities)
        {
            IEnumerable<int> BookTypeID = entities.Select(e => e.Id).ToList();
            IEnumerable<BookType> deletedBookTypes = await this.context
                .BookTypes
                .Where(i => BookTypeID.Contains(i.Id))
                .ToListAsync();
            this.context.BookTypes.RemoveRange(entities);
            return await this.SaveDeleteAsync(deletedBookTypes);
        }
        public async Task<BookType> FindByIdAsync(int Id)
        {
            return await this.context.BookTypes.SingleOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<int> UpdateAsync(IEnumerable<BookType> entities)
        {
            HashSet<BookType> updateBookTypes = new HashSet<BookType>();
            foreach (var bookType in entities)
            {
                var bookTypeDb = await this.FindByIdAsync(bookType.Id);

                bookTypeDb.Code = bookType.Code;
                bookTypeDb.BookTypeName = bookType.BookTypeName;
                bookTypeDb.IsActive = bookType.IsActive;
                updateBookTypes.Add(bookTypeDb);

            }
            return await this.SaveAsync(updateBookTypes);
        }
    }
}
