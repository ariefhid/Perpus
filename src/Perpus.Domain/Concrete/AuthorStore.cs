using Perpus.Core.Concrete.Base;
using Perpus.Domain.Abstract;
using Perpus.Domain.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Domain.Entity;
using System.Threading.Tasks;
using Perpus.Core.Abstract;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Perpus.Domain.Concrete
{
    public class AuthorStore : BaseStore<PerpusContext>, IAuthorStore
    {
        public AuthorStore(PerpusContext context, IOutboundManager outboundManager) : base(context, outboundManager)
        {

        }        

        IQueryable<Author> IPersistenceStore<Author>.Entities => this.context.Authors;

        public async Task<int> CreateAsync(IEnumerable<Author> authors)
        {
            await this.context.AddRangeAsync(authors);
            return await this.SaveAsync(authors);
        }
        public async Task<Author> FindByIdAsync(int Id)
        {
            return await this.context.Authors.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<int> DeleteAsync(IEnumerable<Author> authors)
        {
            IEnumerable<int> AuthorID = authors.Select(e => e.Id).ToList();
            IEnumerable<Author> deletedAuthors = await this.context
                .Authors
                .Where(i => AuthorID.Contains(i.Id))
                .ToListAsync();
            this.context.Authors.RemoveRange(authors);
            return await this.SaveDeleteAsync(deletedAuthors);
        }

        public async Task<int> UpdateAsync(IEnumerable<Author> authors)
        {
            HashSet<Author> updateAuthors = new HashSet<Author>();
            foreach (var author in authors)
            {
                var authorDb = await this.FindByIdAsync(author.Id);

                authorDb.Code = author.Code;
                authorDb.AuthorName = author.AuthorName;
                authorDb.IsActive = author.IsActive;
                updateAuthors.Add(authorDb);

            }
            return await this.SaveAsync(updateAuthors);
        }
    }
}
