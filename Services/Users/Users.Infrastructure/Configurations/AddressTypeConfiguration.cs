
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Configurations
{
    internal partial class AddressTypeConfiguration : IEntityTypeConfiguration<AddressType>
    {
        public void Configure(EntityTypeBuilder<AddressType> entity)
        {
            entity.ToTable("addresstypes");
            entity.HasKey(x => x.Id).HasName("PK_addresstypes");
            entity.Property(x => x.Id).HasColumnName("address_type_id").ValueGeneratedOnAdd();
            entity.Property(x => x.AddressTypeName).HasColumnName("address_type_name").HasMaxLength(255);
            entity.Property(x => x.MaxAddressPerUser).HasColumnName("max_address_per_user");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(255);
            entity.Property(x => x.CreatedDate).HasColumnName("created_date");
            entity.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by").HasMaxLength(255);
            entity.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AddressType> builder);
    }

}
