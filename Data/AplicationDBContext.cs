using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<StockDB> Stock { get; set; }
        public DbSet<StockPortfolio> StockPortfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StockPortfolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            builder.Entity<StockPortfolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Portfolio)
                .HasForeignKey(u => u.AppUserId);

            builder.Entity<StockPortfolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.Portfolio)
                .HasForeignKey(u => u.StockId);
                
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"    
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}