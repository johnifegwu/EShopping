
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Core.Entities;

namespace Users.Infrastructure.Configurations
{
    internal partial class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("users");
            entity.HasKey(x => x.Id).HasName("PK_users");
            entity.Property(x => x.Id).HasColumnName("user_id").ValueGeneratedOnAdd();
            entity.Property(x => x.UserName).HasColumnName("user_name").HasMaxLength(255);
            entity.Property(x => x.UserEmail).HasColumnName("user_email").HasMaxLength(255);
            entity.Property(x => x.PasswordSalt).HasColumnName("password_salt").HasMaxLength(255);
            entity.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(1000);
            entity.Property(x => x.PasswordExpiryDate).HasColumnName("password_expiry_date");
            entity.Property(x => x.PasswordRecoveryUID).HasColumnName("password_recovery_uid").HasMaxLength(255);
            entity.Property(x => x.PasswordRecoveryUIDExpiry).HasColumnName("recovery_expiry");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(255);
            entity.Property(x => x.CreatedDate).HasColumnName("created_date");
            entity.Property(x => x.LastModifiedBy).HasColumnName("last_modified_by").HasMaxLength(255);
            entity.Property(x => x.LastModifiedDate).HasColumnName("last_modified_date");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<User> builder);
    }
}
