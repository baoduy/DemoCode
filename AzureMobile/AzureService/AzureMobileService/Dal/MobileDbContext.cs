using AzureMobileService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AzureMobileService.Dal
{
    public class MobileDbContext : DbContext
    {
        public MobileDbContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<User> Users { get; set; }
    }
}
