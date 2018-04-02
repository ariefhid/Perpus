using Perpus.Core.Concrete.Base;
using Perpus.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Core;
using Perpus.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Perpus.Domain.Concrete
{
    public class BookTypeManager : BaseManager, IBookTypeManager
    {

        private IBookTypeStore _bookTypeStore;
        private IBookTypeValidator _bookTypeValidator;

        public BookTypeManager(IBookTypeStore bookTypeStore, IBookTypeValidator bookTypeValidator)
        {
            this._bookTypeStore = bookTypeStore;
            this._bookTypeValidator = bookTypeValidator;
        }

        public IQueryable<BookType> BookTypes => this._bookTypeStore.Entities;

        public async Task<PerpusResult> CreateAsync(params BookType[] BookTypes)
        {
            var result = this._bookTypeValidator.CreateValidator(BookTypes);
            if (result.Succeeded)
            {
                await this._bookTypeStore.CreateAsync(BookTypes);
            }
            return result;
        }

        public async Task<PerpusResult> DeleteAsync(params BookType[] BookTypes)
        {
            await this._bookTypeStore.DeleteAsync(BookTypes);
            return PerpusResult.Success;
        }

        public async Task<BookType> FindByIdAsync(int id) => await this.BookTypes.SingleOrDefaultAsync(l => l.Id == id);
        
        public Task<IEnumerable<BookType>> GetByActiveLocationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookType>> GetByCodeAsync(string code)
        {
            return await this.BookTypes
               .Where(s => s.Code.ToUpper().Contains(code.ToUpper())).ToListAsync();
        }

        public Task<IEnumerable<BookType>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PerpusResult> UpdateAsync(params BookType[] BookTypes)
        {
            var result = await this._bookTypeValidator.UpdateValidatorAsync(BookTypes);
            if (result.Succeeded)
            {
                await this._bookTypeStore.UpdateAsync(BookTypes);
            }
            return result;
        }
    }
}
