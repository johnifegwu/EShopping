
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Core.Entities;
using System.Data;

namespace Ordering.Infrastructure.Configurations
{
    internal partial class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.ToTable("order");
            entity.HasKey(x => x.Id).HasName("PK_order");
            entity.Property(x => x.Id).HasColumnName("order_id").ValueGeneratedOnAdd();
            entity.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(255);
            entity.Property(x => x.OrderGuid).HasColumnName("order_guid").HasMaxLength(255);
            entity.Property(x => x.TotalPrice).HasColumnName("total_price").HasColumnType("decimal(18,4)");
            entity.Property(x => x.Currency).HasColumnName("currency").HasDefaultValue("USD").HasMaxLength(3);
            entity.Property(x => x.FirstName).HasColumnName("first_name").HasMaxLength(255);
            entity.Property(x => x.LastName).HasColumnName("last_name").HasMaxLength(255);
            entity.Property(x => x.EmailAddress).HasColumnName("email_address").HasMaxLength(255);
            entity.Property(x => x.AddressLine1).HasColumnName("address_line1").HasColumnType("nvarchar(max)");
            entity.Property(x => x.AddressLine2).HasColumnName("address_line2").HasColumnType("nvarchar(max)");
            entity.Property(x => x.City).HasColumnName("city").HasMaxLength(255);
            entity.Property(x => x.State).HasColumnName("state").HasMaxLength(255);
            entity.Property(x => x.ZipCode).HasColumnName("zip_code").HasMaxLength(20);
            entity.Property(x => x.Country).HasColumnName("country").HasMaxLength(255);
            entity.Property(x => x.CardName).HasColumnName("card_name").HasMaxLength(255);
            entity.Property(x => x.CardNumber).HasColumnName("card_number").HasMaxLength(255);
            entity.Property(x => x.CardType).HasColumnName("card_type").HasMaxLength(30);
            entity.Property(x => x.Expiration).HasColumnName("expiration").HasMaxLength(20);
            entity.Property(x => x.CVV).HasColumnName("cvv").HasMaxLength(255);
            entity.Property(x => x.PaymentMethod).HasColumnName("payment_method");
            entity.Property(x => x.IsPaid).HasColumnName("is_paid").HasDefaultValue(false);
            entity.Property(x => x.PaymentReference).HasColumnName("payment_reference").HasColumnType("nvarchar(max)");
            entity.Property(x => x.PaymentProviderUsed).HasColumnName("payment_provider_used").HasColumnType("nvarchar(max)");
            entity.Property(x => x.IsCanceled).HasColumnName("is_canceled").HasDefaultValue(false);
            entity.Property(x => x.IsShipped).HasColumnName("is_shipped").HasDefaultValue(false);
            entity.Property(x => x.ShippingDetails).HasColumnName("shipping_details").HasColumnType("nvarchar(max)");
            entity.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("nvarchar(max)");
            entity.Property(x => x.CreatedDate).HasColumnName("created_date");
            entity.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by").HasColumnType("nvarchar(max)");
            entity.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Order> typeBuilder);
    }
}
