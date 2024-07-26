
using Data.Repositories;
using eShopping.Constants;
using eShopping.Exceptions;
using eShopping.Models;
using eShopping.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Mappers;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly DefaultConfig _config;

        public CreateUserHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config)
        {
            this._unitOfWork = unitOfWork;
            this._config = config.Value;
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            //Fluent Validation is failing for email and other regex
            //So we revalidate here just incase it fails.
            request.Payload.UserEmail.ValidateEmail();
            request.Payload.Password.ValidatePassword();

            //check if UserName or Email already exist.
            var user = await _unitOfWork.Repository<User>().Read().Where(x => x.UserName == request.Payload.UserName || x.UserEmail == request.Payload.UserEmail).FirstOrDefaultAsync();

            if(user != null)
            {
                if ((user.UserEmail != request.Payload.UserEmail))
                {
                    throw new DuplicateRecordException("UserName already exist.");
                }
                else
                {
                    throw new DuplicateRecordException("Email already exist.");
                }
            }

            //Check Max User Addresses
            if(request.Payload.Addresses != null && request.Payload.Addresses.Count > _config.MaxAddressPerUser)
            {
                throw new MaximumAddressException($"You are only allowed to have not more than {_config.MaxAddressPerUser} addresses.");
            }

            //Fetch roles and address type
            var roles = await _unitOfWork.Repository<Role>().Read().ToListAsync();
            var addressTypes = await _unitOfWork.Repository<AddressType>().Read().ToListAsync();

            //Create new user

            user = new User();
            string createdBy = (request.IsAdminUser && request.CurrentUser != null && request.CurrentUser.UserName != null) ? request.CurrentUser.UserName : request.Payload.UserName;
            user.CreateUser(request.Payload.UserName, request.Payload.UserEmail, request.Payload.Password, _config.PaswordExpiryMonths, createdBy);
            await _unitOfWork.Repository<User>().AddAsync(user, cancellationToken);

            //Add user to roles
            var userroles = new List<UserRoleJoin>();

            //Customer role
            var customerrole = new UserRoleJoin();
            customerrole.UserId = user.Id;
            customerrole.RoleId = roles.FirstOrDefault(x => x.RoleName == NameConstants.CustomerRoleName).Id;
            userroles.Add(customerrole);

            if (request.IsAdminUser && request.CurrentUser is not null && request.CurrentUser.IsInRole(NameConstants.AdminRoleName))
            {
                //Admin role
                var adminrole = new UserRoleJoin();
                adminrole.UserId = user.Id;
                adminrole.RoleId = roles.FirstOrDefault(x => x.RoleName == NameConstants.AdminRoleName).Id;
                userroles.Add(adminrole);
            }

            await _unitOfWork.Repository<UserRoleJoin>().AddRangeAsync(userroles);

            //Add user addresses if available
            if(request.Payload.Addresses != null && request.Payload.Addresses.Count > 0)
            {
                var addresses = UsersMapper.Mapper.Map<List<UserAddress>>(request.Payload.Addresses);

                foreach (var item in addresses)
                {
                    item.UserId = user.Id;
                    item.AddressTypeId = (addressTypes.FirstOrDefault(x => x.Id == item.AddressTypeId) != null) ? item.AddressTypeId : addressTypes.FirstOrDefault().Id;
                    item.CreatedBy = user.UserName; 
                    item.CreatedDate = DateTime.UtcNow;
                }

                await _unitOfWork.Repository<UserAddress>().AddRangeAsync(addresses);

                user.Addresses = addresses;
            }

            return UsersMapper.Mapper.Map<UserResponse>(user);
        }
    }
}
