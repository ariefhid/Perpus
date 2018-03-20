using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Perpus.Domain.Entity
{
    public class Author : PerpusEntity
    {
        public Author() { }
        [Required]
        public string Code { get; set; }
        public string AuthorName { get; set; }
        public bool IsActive { get; set; }
    }
}
