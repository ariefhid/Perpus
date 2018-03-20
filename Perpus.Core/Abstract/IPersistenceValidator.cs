using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{
    /// <summary>
    /// Validator for persistence method
    /// </summary>
    /// <typeparam name="T">entities</typeparam>
    public interface IPersistenceValidator<T> where T :class
    {
        /// <summary>
        /// Convert error message list to erpresult
        /// </summary>
        /// <param name="errorMessages">list of error message</param>
        /// <returns>validation result</returns>
        PerpusResult ValidationResult(IEnumerable<string> errorMessages);

        /// <summary>
        /// create the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns>validation result</returns>
        Task<PerpusResult> ValidateCreateAsync(params T[] entities);

        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns>validation result</returns>
        Task<PerpusResult> ValidateUpdateAsync(params T[] entities);

        /// <summary>
        /// Delete the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns>validation result</returns>
        Task<PerpusResult> ValidateDeleteAsync(params T[] entities);
    }
}
