using Microsoft.EntityFrameworkCore;
using Perpus.Core.Concrete.Base;
using Perpus.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Perpus.Domain.Concrete.Context
{
    public class PerpusContext : BaseContext
    {
        public PerpusContext() { }
        public PerpusContext(DbContextOptions opt) : base(opt) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>()
              .HasIndex(i => i.Code)
              .IsUnique();
        }
        public DbSet<Author> Authors { get; set; }
    }
}
