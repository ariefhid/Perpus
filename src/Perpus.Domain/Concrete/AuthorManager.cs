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
    public class AuthorManager : BaseManager, IAuthorManager
    {
        private IAuthorStore _authorStore;
        private IAuthorValidator _authorValidator;
        public AuthorManager(IAuthorStore authorStore, IAuthorValidator authorValidator)
        {
            this._authorStore = authorStore;
            this._authorValidator = authorValidator;
        }
        public IQueryable<Author> Authors => this._authorStore.Entities;

        public async Task<PerpusResult> DeleteAsync(params Author[] Authors)
        {
            await this._authorStore.DeleteAsync(Authors);
            return PerpusResult.Success;
        }

        public async Task<Author> FindByIdAsync(int id) => await this.Authors.SingleOrDefaultAsync(l => l.Id == id);


        public Task<IEnumerable<Author>> GetByActiveLocationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetByCodeAsync(string code)
        {
            return await this.Authors
               .Where(s => s.Code.ToUpper().Contains(code.ToUpper())).ToListAsync();
        }

        public Task<IEnumerable<Author>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PerpusResult> UpdateAsync(params Author[] Authors)
        {
            var result = await this._authorValidator.UpdateValidatorAsync(Authors);
            if (result.Succeeded)
            {
                await this._authorStore.UpdateAsync(Authors);
            }
            return result;
        }

        public async Task<PerpusResult> CreateAsync(params Author[] Authors)
        {
            var result = this._authorValidator.CreateValidator(Authors);
            if (result.Succeeded)
            {
                await this._authorStore.CreateAsync(Authors);
            }
            return result;
        }
    }
}
