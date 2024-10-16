using AgriNov.Models.ProductionModel;
using Microsoft.EntityFrameworkCore;


namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;}

		public DbSet<BoxContract> BoxContracts { get; set; }

        public DbSet<User> Users { get; set;}
        public DbSet<CorporateUser> CorporateUsers {get; set;}
        public DbSet<Supplier> Suppliers { get; set;}
        public DbSet<Production> Productions { get; set;}
        public DbSet<Activity> Activities { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}