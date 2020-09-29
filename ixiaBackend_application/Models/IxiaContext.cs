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
        public DbSet<Price> Prices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Sub_Category> Sub_Categories { get; set; }
        public DbSet<Type> Types { get; set; }

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
                entity.HasOne(e => e.Type).WithMany(e => e.Products);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User).WithMany(e => e.Purchases);
                entity.HasOne(e => e.Product).WithMany(e => e.Purchases);
                entity.HasOne(e => e.Currency).WithMany(e => e.PurchasesCurrency);
                entity.HasOne(e => e.Country).WithMany(e => e.PurchasesCountry);
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId, e.CountryId, e.CurrencyId });
                entity.HasOne(e => e.User).WithMany(e => e.Favorites);
                entity.HasOne(e => e.Product).WithMany(e => e.Favorites);
                entity.HasOne(e => e.Country).WithMany(e => e.FavoritesCountry);
                entity.HasOne(e => e.Currency).WithMany(e => e.FavoritesCurrency);
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CountryId, e.CurrencyId });
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Company).WithMany(e => e.Brands);
            });

            modelBuilder.Entity<Sub_Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Category).WithMany(e => e.Sub_Categories);
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Sub_Category).WithMany(e => e.Types);
            });
        }
    }
}