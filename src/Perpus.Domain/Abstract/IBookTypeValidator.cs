using Perpus.Core;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Domain.Abstract
{
    public interface IBookTypeValidator
    {

        PerpusResult CreateValidator(IEnumerable<BookType> BookTypes);
        Task<PerpusResult> UpdateValidatorAsync(IEnumerable<BookType> BookTypes);
    }
}
