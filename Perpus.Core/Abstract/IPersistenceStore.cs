using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{
    public interface IPersistenceStore<T> where T:class
    {
        /// <summary>
        /// Find the entity by Id
        /// </summary>
        /// <param name="Id">Entity Id</param>
        /// <returns></returns>
        Task<T> FindByIdAsync(int Id);
        /// <summary>
        /// Get the original value
        /// </summary>
        /// <typeparam name="T1">The entity</typeparam>
        /// <typeparam name="T2">The column name</typeparam>
        /// <param name="entity">The entity</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The original value in the database</returns>
        T2 GetOriginalValue<T1, T2>(T1 entity, string propertyName) where T1 : class;
        
        /// <summary>
        /// Get the original value before it is modified
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>saved entity</returns>
        Task<int> CreateAsync(IEnumerable<T> entities);
        
        /// <summary>
        /// Update the entity
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IEnumerable<T> entities);
     
        /// <summary>
        /// Delete the entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IEnumerable<T> entities);
    
        /// <summary>
        /// Return the queryable entities that is related to the store
        /// </summary>
        IQueryable<T> Entities { get; }
       
    }
}
