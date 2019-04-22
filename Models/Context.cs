using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace eatklik.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Setting> Settings { get; set; }


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlite("Data Source=blogging.db");
        // }
    }
}