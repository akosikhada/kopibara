using Kopibara.Models;
using Microsoft.EntityFrameworkCore;

namespace Kopibara.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<CoffeeOrder> Cart { get; set; }

        public DbSet<Kopi_products> products { get; set; }

        public DbSet<Accounts> Accounts { get; set; }

        public DbSet<Checkout> Checkout { get; set; }

        public DbSet<OrderList> Orders { get; set; }
    }
}
