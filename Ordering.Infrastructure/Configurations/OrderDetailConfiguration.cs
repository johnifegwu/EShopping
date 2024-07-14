
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Configurations
{
    internal partial class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> entity)
        {
            entity.ToTable("orderdetail");
            entity.HasKey(x => x.Id).HasName("PK_orderdetail");
            entity.Property(x => x.Id).HasColumnName("oderdetail_id").ValueGeneratedOnAdd();
            entity.Property(x => x.OrderId).HasColumnName("order_id");
            entity.Property(x => x.ProductId).HasColumnName("product_id").HasMaxLength(255);
            entity.Property(x => x.ProductName).HasColumnName("product_name").HasMaxLength(255);
            entity.Property(x => x.Quantity).HasColumnName("quantity");
            entity.Property(x => x.Price).HasColumnName("price").HasColumnType("decimal(18,4)");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<OrderDetail> typeBuilder);

    }
}
