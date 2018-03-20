using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Perpus.Core.Entity
{
    public class Outbound
    {
        public Outbound()
        {
            this.DateCreated = DateTime.Now;
        }

        #region Column Property
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateCreated { get; private set; }
        [Required]
        public string SourceIdentifier { get; set; }

        [Required]
        public string Content { get; set; }
        [Required]
        [MaxLength(50)]
        public string SchemaName { get; set; }

        public OutboundFormats OutboundFormat { get; set; }

        public string ErrorMessage { get; set; }
        #endregion
    }
}
