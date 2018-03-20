using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Perpus.Core.Entity.Base
{
    /// <summary>
    /// this must be set as the root base entity object
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateCreated { get; protected set; }

        [Required]
        public DateTime DateModified { get; set; }

        [Required]
        [MaxLength(255)]
        public string CreatedById { get; set; }

        [Required]
        [MaxLength(255)]
        public string ModifiedById { get; set; }

        public BaseEntity()
        {
            this.DateModified = this.DateCreated = DateTime.Now;
            this.CreatedById = "DevMode";
            this.ModifiedById = "DevMode";
        }
    }
}
