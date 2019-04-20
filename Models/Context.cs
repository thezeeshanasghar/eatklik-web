using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Rider> Riders { get; set; }
    }
}