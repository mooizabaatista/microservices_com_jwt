using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.Product.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Microservices.Services.Product.Models.Product> Products { get; set; }
    }
}
