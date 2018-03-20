using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Core.Entity
{
    /// <summary>
    /// ERP configuration class
    /// </summary>
    public class PerpusConfig
    {
        /// <summary>
        /// constructor
        /// </summary>
        public PerpusConfig()
        {

        }
        /// <summary>
        /// The application name
        /// </summary>
        public string BrandName { get; set; }
        /// <summary>
        /// The administrator name
        /// </summary>
        public string AdminName { get; set; }
        /// <summary>
        /// Determine wheter the maintenance mode is up
        /// </summary>
        public bool IsMaintenanceMode { get; set; }
        /// <summary>
        /// Determine wheter auditing is running
        /// </summary>
        public bool IsAuditEnable { get; set; }
        /// <summary>
        /// When this mode is running, the field createdbyid/modifiedbyid will be hardcoded to dev-mode
        /// </summary>
        public bool IsDeveloperMode { get; set; }

        /// <summary>
        /// determine wheter sample data will be imported
        /// </summary>
        public bool ImportSampleData { get; set; }

    }
}
