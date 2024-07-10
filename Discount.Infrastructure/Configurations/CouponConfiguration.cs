
using Discount.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.Infrastructure.Configurations
{
    internal partial class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> entity)
        {
            entity.ToTable(nameof(Coupon));
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.ProductId);
            entity.Property(x => x.ProductName);
            entity.Property(x => x.Description);
            entity.Property(x => x.Amount);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Coupon> typeBuilder);
    }
}
