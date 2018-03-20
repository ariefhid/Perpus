using Perpus.Core;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Domain.Abstract
{
    public interface IAuthorValidator
    {
        PerpusResult CreateValidator(IEnumerable<Author> authors);
        Task<PerpusResult> UpdateValidatorAsync(IEnumerable<Author> authors);
    }
}
