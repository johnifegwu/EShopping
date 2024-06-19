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

        public DbSet<TestCollection> TestCollections { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new TestCollectionConfiguration());
        }

    }
}
