using Microservices.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<UserExtended>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserExtended> UserExtended { get; set; }
    }
}
