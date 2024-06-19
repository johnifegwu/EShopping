
using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Catalog.Infrastructure.Seeders.CatalogSeeder;

namespace Catalog.Core.Configurations
{
    internal partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(entity => entity.Name);
            entity.Property(entity => entity.Summary);
            entity.Property(entity=> entity.Description);
            entity.Property(entity=> entity.ImageFile);
            entity.OwnsOne(entity => entity.Brands);
            entity.OwnsOne(entity => entity.Types);
            entity.Property(entity => entity.Price);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> modelBuilder);
    }
}
