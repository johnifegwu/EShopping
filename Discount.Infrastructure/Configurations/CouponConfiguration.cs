
using Discount.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Infrastructure.Configurations
{
    internal partial class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> entity)
        {
            entity.ToTable("coupon");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("coupon_id").ValueGeneratedOnAdd();
            entity.Property(x => x.ProductId).HasColumnName("product_id").HasMaxLength(255);
            entity.Property(x => x.ProductName).HasColumnName("product_name").HasMaxLength(255);
            entity.Property(x => x.Description).HasColumnName("description").HasMaxLength(255);
            entity.Property(x => x.Amount).HasColumnName("amount");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Coupon> typeBuilder);
    }
}
