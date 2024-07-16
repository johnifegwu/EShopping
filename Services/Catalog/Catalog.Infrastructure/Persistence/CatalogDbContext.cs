using Catalog.Core.Configurations;
using Catalog.Core.Entities;
using Catalog.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using static Catalog.Infrastructure.Seeders.CatalogSeeder;

namespace Catalog.Infrastructure.Persistence
{
    internal class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; init; }
        public DbSet<ProductBrand> ProductBrands { get; init; }
        public DbSet<ProductType> ProductTypes { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

    }
}
