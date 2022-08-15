using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailManager.Models;

namespace RetailManager.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(eb =>
            {
                eb.Property(p => p.CreatedAt).HasDefaultValueSql("timezone('utc', now())");
                eb.Property(p => p.UpdatedAt).HasDefaultValueSql("timezone('utc', now())");
            });

            builder.Entity<Profile>(eb =>
            {
                eb.Property(p => p.CreatedAt).HasDefaultValueSql("timezone('utc', now())");
                eb.Property(p => p.UpdatedAt).HasDefaultValueSql("timezone('utc', now())");
            });

            builder.Entity<Inventory>(eb =>
            {
                eb.Property(p => p.PurchaseDate).HasDefaultValueSql("timezone('utc', now())");
            });

            builder.Entity<Sale>(eb => { eb.Property(p => p.SaleDate).HasDefaultValueSql("timezone('utc', now())"); });
            base.OnModelCreating(builder);
        }
    }
}