using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace eatklik.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<RiderRating> RiderRatings { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuExtraItem> MenuExtraItems { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<RestaurantContact> RestaurantContacts { get; set; }
        public DbSet<RestaurantLocation> RestaurantLocations { get; set; }
        public DbSet<RestaurantTiming> RestaurantTimings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CouponCode> CouponCodes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<RestaurantExtraItem> RestaurantExtraItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RestaurantCuisine>()
                .HasKey(bc => new { bc.RestaurantId, bc.CuisineId });
            modelBuilder.Entity<OrderItem>()
           .HasKey(bc => new { bc.Id });
        }
    }
}