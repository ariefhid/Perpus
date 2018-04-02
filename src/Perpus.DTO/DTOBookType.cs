using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.DTO
{
    public class DTOBookType
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string BookTypeName { get; set; }
        public bool IsActive { get; set; }
    }
}
