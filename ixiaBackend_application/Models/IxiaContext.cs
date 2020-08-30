using ixiaBackend_application.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ixiaBackend_application.Models
{
    public class IxiaContext : IdentityDbContext
    {
        public IxiaContext(DbContextOptions<IxiaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>();

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Company).WithMany(e => e.Products);
                entity.HasOne(e => e.Category).WithMany(e => e.Products);
            });
        
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        
            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId });
                entity.HasOne(e => e.User).WithMany(e => e.Purchases);
                entity.HasOne(e => e.Product).WithMany(e => e.Purchases);
            });
        
            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId });
                entity.HasOne(e => e.User).WithMany(e => e.Favorites);
                entity.HasOne(e => e.Product).WithMany(e => e.Favorites);
            });
        }
    }
}
