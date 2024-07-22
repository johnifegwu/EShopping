
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Configurations
{
    internal partial class UserRoleJoinConfiguration : IEntityTypeConfiguration<UserRoleJoin>
    {
        public void Configure(EntityTypeBuilder<UserRoleJoin> entity)
        {
            entity.ToTable("userrolejoin");
            entity.HasKey(t => t.Id).HasName("PK_userrolejoin");
            entity.Property(t => t.Id).HasColumnName("user_role_join_id").ValueGeneratedOnAdd();
            entity.Property(t => t.UserId).HasColumnName("user_id");
            entity.Property(t => t.RoleId).HasColumnName("role_id");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<UserRoleJoin> builder);
    }
}
