using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Core.Concrete.Base
{
    /// <summary>
    /// The root validator class
    /// </summary>
    public abstract class BaseValidator
    {
        /// <summary>
        /// return failed if the error messages is not empty
        /// </summary>
        /// <param name="errorMessages">the error messages</param>
        /// <returns></returns>
        public PerpusResult ValidationResult(IEnumerable<string> errorMessages)
        {
            if (errorMessages.Count() > 0)
            {
                return PerpusResult.Failed(errorMessages);
            }
            else
            {
                return PerpusResult.Success;
            }
        }
    }
}
