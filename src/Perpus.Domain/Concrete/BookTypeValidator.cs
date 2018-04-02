using Perpus.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using Perpus.Core;
using Perpus.Domain.Entity;
using System.Threading.Tasks;
using Perpus.Core.Concrete.Base;

namespace Perpus.Domain.Concrete
{
    public class BookTypeValidator : BaseValidator, IBookTypeValidator
    {
        private IBookTypeStore _bookTypeStore;
        public BookTypeValidator(IBookTypeStore bookTypeStore)
        {
            this._bookTypeStore = bookTypeStore;
        }
        public PerpusResult CreateValidator(IEnumerable<BookType> BookTypes)
        {
            List<string> errors = new List<string>();
            foreach (var bookType in BookTypes)
            {

                if (string.IsNullOrWhiteSpace(bookType.Code))
                {
                    errors.Add("Book Type code is mandatory");
                }
                if (string.IsNullOrWhiteSpace(bookType.BookTypeName))
                {
                    errors.Add("Book Type name is mandatory");
                }
            }
            return this.ValidationResult(errors);
        }

        public async Task<PerpusResult> UpdateValidatorAsync(IEnumerable<BookType> BookTypes)
        {
            List<string> errors = new List<string>();
            foreach (var bookType in BookTypes)
            {
                if (string.IsNullOrWhiteSpace(bookType.Code))
                {
                    errors.Add("Book Type code is mandatory");
                }
                if (string.IsNullOrWhiteSpace(bookType.BookTypeName))
                {
                    errors.Add("Book Type name is mandatory");
                }
                var isBookTypeExist = await this._bookTypeStore.FindByIdAsync(bookType.Id);

                if (isBookTypeExist == null)
                {
                    errors.Add($"Book Type {bookType.Id} does not exist");
                }

            }
            return this.ValidationResult(errors);
        }
    }
}
