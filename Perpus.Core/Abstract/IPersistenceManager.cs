using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{ 
    /// <summary>
    /// This interface is intended to use for persistance method
    /// </summary>
    /// <typeparam name="T">class</typeparam>
    public interface IPersistenceManager<T> where T:class
    {     

        /// <summary>
        /// create the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns></returns>
        Task<PerpusResult> CreateAsync(params T [] entities);

        /// <summary>
        /// Update the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns></returns>
        Task<PerpusResult> UpdateAsync(params T[] entities);

        /// <summary>
        /// Delete the entities
        /// </summary>
        /// <param name="entities">object</param>
        /// <returns></returns>
        Task<PerpusResult> DeleteAsync(params T[] entities);
    }
}
