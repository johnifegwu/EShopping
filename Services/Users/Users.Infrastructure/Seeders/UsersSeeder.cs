﻿
using Data.Repositories;
using eShopping.Models;
using Microsoft.Extensions.Options;
using Users.Application.Extensions;
using Users.Core.Entities;
using eShopping.Constants;

namespace Users.Infrastructure.Seeders
{
    internal class UsersSeeder(IUnitOfWorkCore context, IOptions<DefaultConfig> config) : IUsersSeeder
    {
        public async Task Seed()
        {
            ////==================================================================================
            var canconnect = await context.GetContext().Database.CanConnectAsync();
            var _config = config.Value;

            if (canconnect)
            {
                var roles = context.Repository<Role>().Read().ToList();

                if (roles == null || roles.Count == 0)
                {
                    roles = GetRoles();
                    await context.Repository<Role>().AddRangeAsync(roles);
                }

                var addresstype = context.Repository<AddressType>().Read().FirstOrDefault();

                if (addresstype == null)
                {
                    var addresstypes = GetAddressTypes();
                    await context.Repository<AddressType>().AddRangeAsync(addresstypes);
                    addresstype = addresstypes.Where(x => x.AddressTypeName == NameConstants.BillingAddressType).FirstOrDefault();
                }

                var defaultUser = context.Repository<User>().Read().Where(x => x.UserName == _config.DefaultUserName).FirstOrDefault();

                if(defaultUser == null)
                {
                    //Create default user
                    var user = new User();
                    user.CreateUser(_config.DefaultUserName, _config.DefaultUserEmail, _config.defaultUserPassword, _config.PaswordExpiryMonths, "System.");
                    await context.Repository<User>().AddAsync(user);
                    
                    var adminrole = roles.Where(x => x.RoleName == NameConstants.AdminRoleName).FirstOrDefault();
                    var customerrole = roles.Where(x => x.RoleName == NameConstants.CustomerRoleName).FirstOrDefault();
                    
                    var userroles = new List<UserRoleJoin>() 
                    { 
                        new UserRoleJoin()
                        {
                            UserId = user.Id,
                            RoleId = adminrole.Id
                        },
                        new UserRoleJoin()
                        {
                            UserId = user.Id,
                            RoleId = customerrole.Id
                        }
                    };

                    await context.Repository<UserRoleJoin>().AddRangeAsync(userroles);
                }
            }

            List<AddressType> GetAddressTypes()
            {
                return new List<AddressType>
                {
                    new AddressType()
                    {
                        AddressTypeName = NameConstants.BillingAddressType,
                        MaxAddressPerUser = config.Value.MaxAddressPerUser,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    },
                    new AddressType()
                    {
                        AddressTypeName = NameConstants.ShippingAddressType,
                        MaxAddressPerUser = config.Value.MaxAddressPerUser,
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    }
                };
            }

            List<Role> GetRoles()
            {
                return new List<Role>
                {
                    new Role()
                    {
                        RoleName = NameConstants.AdminRoleName,
                        RoleDescription = "Role for Administrators.",
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    },
                    new Role()
                    {
                        RoleName = NameConstants.CustomerRoleName,
                        RoleDescription = "Role for Customers.",
                        CreatedBy = "System",
                        CreatedDate = DateTime.UtcNow
                    }
                };
            }
        }
    }
}