using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AplicationDBContext : IdentityDbContext<AppUser>
    {
        public AplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
        }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockPortifolio> StockPortifolios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StockPortifolio>(x => x.HasKey(p => new {p.AppUserId, p.StockId}));
            builder.Entity<StockPortifolio>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.StockPortifolios)
                .HasForeignKey(u => u.AppUserId);

            builder.Entity<StockPortifolio>()
                .HasOne(u => u.Stock)
                .WithMany(u => u.StockPortifolios)
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