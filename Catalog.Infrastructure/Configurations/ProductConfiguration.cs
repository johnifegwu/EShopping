
using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Core.Configurations
{
    internal partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(p => p.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name);
            entity.Property(e => e.Summary);
            entity.Property(e => e.Description);
            entity.Property(e => e.ImageFile);
            entity.Property(e => e.ProductTypeId);
            entity.Property(e => e.ProductBrandId);
            entity.Property(e => e.Price);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> modelBuilder);
    }
}
