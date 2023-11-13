using Microsoft.EntityFrameworkCore;

namespace MiniAccounting.Infrastructure
{
    public class MiniAccountingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TransactionInfo> Transactions { get; set; }
        
        public MiniAccountingContext(DbContextOptions<MiniAccountingContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User("Manrikez", 100));
        }
    }
}