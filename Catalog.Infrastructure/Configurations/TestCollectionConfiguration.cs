
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Catalog.Infrastructure.Seeders.CatalogSeeder;

namespace Catalog.Infrastructure.Configurations
{
    internal partial class TestCollectionConfiguration : IEntityTypeConfiguration<TestCollection>
    {
        public void Configure(EntityTypeBuilder<TestCollection> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(entity => entity.Name).HasMaxLength(255);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<TestCollection> modelBuilder);
    }
}
