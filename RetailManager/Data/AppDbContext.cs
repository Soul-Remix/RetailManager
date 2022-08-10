using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailManager.Models;

namespace RetailManager.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(eb =>
            {
                eb.Property(p => p.CreatedAt).HasDefaultValueSql("getutcdate()");
                eb.Property(p => p.UpdatedAt).HasDefaultValueSql("getutcdate()");
            });
            
            builder.Entity<Inventory>(eb =>
            {
                eb.Property(p => p.PurchaseDate).HasDefaultValueSql("getutcdate()");
            });
            
            builder.Entity<Sale>(eb =>
            {
                eb.Property(p => p.SaleDate).HasDefaultValueSql("getutcdate()");
            });
            base.OnModelCreating(builder);
        }
    }
}
