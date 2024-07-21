
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Configurations
{
    internal partial class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable("roles");
            entity.HasKey(x => x.Id).HasName("PK_roles");
            entity.Property(x => x.Id).HasColumnName("role_id").ValueGeneratedOnAdd();
            entity.Property(x => x.RoleName).HasColumnName("role_name").HasMaxLength(255);
            entity.Property(x => x.RoleDescription).HasColumnName("role_description").HasMaxLength(1500);

            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(255);
            entity.Property(x => x.CreatedDate).HasColumnName("created_date");
            entity.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by").HasMaxLength(255);
            entity.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Role> builder);
    }
}
