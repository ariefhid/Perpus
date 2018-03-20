using Perpus.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Core.Concrete.Base
{
    public abstract class BaseContext:DbContext
    {
        public BaseContext(DbContextOptions opt):base(opt)
        {

        }
        public BaseContext()
        {

        }

        public virtual DbSet<Outbound> Outbounds { get; set; }
        public virtual DbSet<Audit> Audits { get; set; }

    }
}
