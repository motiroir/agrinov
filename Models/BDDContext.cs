using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;}
		public DbSet<BoxContract> BoxContracts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}