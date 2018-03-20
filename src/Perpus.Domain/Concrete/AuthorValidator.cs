using Perpus.Core.Concrete.Base;
using Perpus.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Core;
using Perpus.Domain.Entity;
using System.Threading.Tasks;

namespace Perpus.Domain.Concrete
{
    public class AuthorValidator : BaseValidator, IAuthorValidator
    {
        private IAuthorStore _authorStore;
        public AuthorValidator(IAuthorStore authorStore)
        {
            this._authorStore = authorStore;
        }
        public PerpusResult CreateValidator(IEnumerable<Author> authors)
        {
            List<string> errors = new List<string>();
            foreach (var author in authors)
            {

                if (string.IsNullOrWhiteSpace(author.Code))
                {
                    errors.Add("Author code is mandatory");
                }
                if (string.IsNullOrWhiteSpace(author.AuthorName))
                {
                    errors.Add("Author name is mandatory");
                }
            }
            return this.ValidationResult(errors);
        }

        public async Task<PerpusResult> UpdateValidatorAsync(IEnumerable<Author> authors)
        {
            List<string> errors = new List<string>();
            foreach (var author in authors)
            {
                if (string.IsNullOrWhiteSpace(author.Code))
                {
                    errors.Add("Author code is mandatory");
                }
                if (string.IsNullOrWhiteSpace(author.AuthorName))
                {
                    errors.Add("Author name is mandatory");
                }
                var isAuthorExist = await this._authorStore.FindByIdAsync(author.Id);

                if (isAuthorExist == null)
                {
                    errors.Add($"Author {author.Id} does not exist");
                }

            }
            return this.ValidationResult(errors);
        }
    }
}
