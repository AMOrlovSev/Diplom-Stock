using Microsoft.EntityFrameworkCore;
using Stock.Models;

namespace Stock.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<BrokerageAccount> BrokerageAccounts { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<StockActive> StockActives { get; set; }
        public DbSet<StockInformation> StockInformations { get; set; }
    }
}
