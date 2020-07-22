using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class MyContext : DbContext
    {
        public MyContext( DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(i => !i.IsDeleted);
        }
    }
}
