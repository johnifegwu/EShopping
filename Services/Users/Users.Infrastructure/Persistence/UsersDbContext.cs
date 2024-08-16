
using EFCore.UnitOfWorkCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;
using Users.Infrastructure.Configurations;

namespace Users.Infrastructure.Persistence
{
    internal sealed class UsersDbContext : DbContext, IJayDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        public DbSet<User> Users { get; init; }
        public DbSet<UserRoleJoin> UserRoles { get; init; }
        public DbSet<AddressType> AddressTypes { get; init; }
        public DbSet<Role> Roles { get; init; }
        public DbSet<UserAddress> UserAddresses { get; init; }

        //This for data migration only.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL("server=localhost;port=3306;database=eshopping;user=eshop_admin;password=eshop_admin_password");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        }
    }
}
