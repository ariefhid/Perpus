using Perpus.Core.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Perpus.Core.Entity
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }
        public AuditActions Action { get; set; }
        public string   CreatedById { get; set; }
        public string EntityType { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }

}
