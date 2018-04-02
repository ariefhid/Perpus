using Perpus.Core;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Domain.Abstract
{
    public interface IBookTypeManager
    {
        IQueryable<BookType> BookTypes { get; }

        Task<IEnumerable<BookType>> GetByNameAsync(string name);
        Task<BookType> FindByIdAsync(int id);
        Task<PerpusResult> CreateAsync(params BookType[] BookTypes);
        Task<PerpusResult> UpdateAsync(params BookType[] BookTypes);
        Task<PerpusResult> DeleteAsync(params BookType[] BookTypes);
        Task<IEnumerable<BookType>> GetByActiveLocationsAsync();
        Task<IEnumerable<BookType>> GetByCodeAsync(string code);
    }
}
