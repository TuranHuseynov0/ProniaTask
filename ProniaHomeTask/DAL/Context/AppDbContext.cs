using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProniaHomeTask.Models;

namespace ProniaHomeTask.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // StringLength yalnız validasiya üçündür; DB sütunu nvarchar(max) qalır
            builder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).HasColumnType("nvarchar(max)");
                entity.Property(p => p.Description).HasColumnType("nvarchar(max)");
            });
        }
    }
}
