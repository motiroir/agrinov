using Microsoft.EntityFrameworkCore;
using AgriNov.Models.ProductionModel;
using AgriNov.Models.ProductModel;

namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;}
        public DbSet<User> Users { get; set;}
        public DbSet<CorporateUser> CorporateUsers {get; set;}
        public DbSet<Supplier> Suppliers { get; set;}
        public DbSet<Production> Productions { get; set;}
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}