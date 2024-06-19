
using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Core.Configurations
{
    internal partial class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name);
            entity.Property(e => e.Summary);
            entity.Property(e => e.Description);
            entity.Property(e => e.ImageFile);
            entity.Property(e => e.BrandId);
            entity.HasOne(e => e.Brands).WithMany().HasForeignKey(e => e.BrandId);
            entity.Property(e => e.TypeId);
            entity.HasOne(e => e.Types).WithMany().HasForeignKey(e => e.TypeId);
            entity.Property(e => e.Price);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Product> modelBuilder);
    }
}
