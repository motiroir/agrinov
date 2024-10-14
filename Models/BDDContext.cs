using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}