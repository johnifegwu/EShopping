
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Configurations
{
    internal partial class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> entity)
        {
            entity.ToTable("useraddresses");
            entity.HasKey(x => x.Id).HasName("PK_useraddresses");
            entity.Property(x => x.Id).HasColumnName("User_address_id").ValueGeneratedOnAdd();
            entity.Property(x => x.UserId).HasColumnName("user_id");
            entity.Property(x => x.AddressTypeId).HasColumnName("address_type_id");
            entity.Property(x => x.AddressLine1).HasColumnName("address_line1").HasMaxLength(2000);
            entity.Property(x => x.AddressLine2).HasColumnName("address_line2").HasMaxLength(2000);
            entity.Property(x => x.City).HasColumnName("city").HasMaxLength(255);
            entity.Property(x => x.State).HasColumnName("state").HasMaxLength(255);
            entity.Property(x => x.ZipCode).HasColumnName("zip_code").HasMaxLength(20);
            entity.Property(x => x.Country).HasColumnName("country").HasMaxLength(255);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(255);
            entity.Property(x => x.CreatedDate).HasColumnName("created_date");
            entity.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by").HasMaxLength(255);
            entity.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder builder);
    }
}
