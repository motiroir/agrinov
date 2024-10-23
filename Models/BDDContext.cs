using AgriNov.Models;
using Microsoft.EntityFrameworkCore;


namespace AgriNov.Models
{
    public class BDDContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<User> Users { get; set;}
        public DbSet<CorporateUser> CorporateUsers {get; set;}
        public DbSet<Supplier> Suppliers { get; set;}
        public DbSet<Admin> Admins { get; set;}
        public DbSet<Volunteer> Volunteers { get; set;}
        public DbSet<Production> Productions { get; set;}
        public DbSet<Activity>Activities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BoxContract> BoxContracts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MemberShipFee> MembershipFees { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders {get; set;}
        public DbSet<OrderItem> OrderItems {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        }

    }
}