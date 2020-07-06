using System.Data.Entity;

namespace PartsStore.Models.Repository
{
    public class EFDbContext : DbContext
    {
        public DbSet<Details> Details { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}