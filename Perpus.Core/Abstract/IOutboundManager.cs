using Microsoft.EntityFrameworkCore.ChangeTracking;
using Perpus.Core.Abstract;
using Perpus.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perpus.Core.Abstract
{

    /// <summary>
    /// Manage the outbound data from the apps
    /// </summary>
    public interface IOutboundManager:IPersistenceManager<Outbound>
    {
        /// <summary>
        /// The outbound entities
        /// </summary>
        IQueryable<Outbound> Outbounds { get; }
        
        /// <summary>
        /// Create the outbound data
        /// </summary>
        /// <param name="savedEntities">The add/modified entity</param>
        /// <param name="deletedEntities">The deleted entity</param>
        /// <returns></returns>
        IEnumerable<Outbound> CreateOutboundData(IEnumerable<EntityEntry> savedEntities, IEnumerable<EntityEntry> deletedEntities);

        /// <summary>
        ///  Export a specific outbound data
        /// </summary>
        /// <param name="outbound"></param>
        /// <returns></returns>
        Task<bool> ExportOutboundAsync(Outbound outbound);

        /// <summary>
        ///  Export all outbound data
        /// </summary>
        /// <returns></returns>
        Task ExportOutboundAsync();

        /// <summary>
        /// method to clean up the succesffully exported data
        /// </summary>
        /// <returns></returns>
        Task CleanupExportedAsync(IEnumerable<Outbound> exportedOutbounds);
    }
}
