using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Domain.Entity
{
    public class BookType : PerpusEntity
    {
        public BookType() { }
        public string Code { get; set; }
        public string BookTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
