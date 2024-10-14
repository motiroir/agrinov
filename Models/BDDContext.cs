using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using AgriNov.Models.ProductionModel;

namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set;}

        public DbSet<Production> Productions { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}