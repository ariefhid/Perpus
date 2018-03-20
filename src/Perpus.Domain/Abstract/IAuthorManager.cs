using Perpus.Core;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Domain.Abstract
{
    public interface IAuthorManager
    {
        IQueryable<Author> Authors { get; }

        Task<IEnumerable<Author>> GetByNameAsync(string name);
        Task<Author> FindByIdAsync(int id);
        Task<PerpusResult> CreateAsync(params Author[] Authors);
        Task<PerpusResult> UpdateAsync(params Author[] Authors);
        Task<PerpusResult> DeleteAsync(params Author[] Authors);
        Task<IEnumerable<Author>> GetByActiveLocationsAsync();
        Task<IEnumerable<Author>> GetByCodeAsync(string code);
    }
}
