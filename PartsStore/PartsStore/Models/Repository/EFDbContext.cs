using System.Data.Entity;

namespace PartsStore.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<Detail> Details { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}