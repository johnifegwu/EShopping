
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Mappers;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class AddUserAddressHandler : IRequestHandler<AddUserAddressCommand, UserAddressResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly DefaultConfig _config;

        public AddUserAddressHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config)
        {
            this._unitOfWork = unitOfWork;
            this._config = config.Value;
        }
        public async Task<UserAddressResponse> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.CurrentUser.UserName);

            if (user == null)
            {
                throw new NotAuthorizedException();
            }

            var addresses = await _unitOfWork.GetUserAddresses(user.Id, 1, _config.MaxAddressPerUser.GetValueOrDefault());

            if ((addresses is not null && _config.MaxAddressPerUser is not null && ((addresses.Count + 1) > _config.MaxAddressPerUser)))
            {
                throw new MaximumAddressException($"You are only allowed to have not more than {_config.MaxAddressPerUser} addresses.");
            }

            var address = UsersMapper.Mapper.Map<UserAddress>(request.Payload);
            var addressTypes = await _unitOfWork.Repository<AddressType>().Read().ToListAsync();
            
            address.UserId = user.Id;
            address.AddressTypeId = (addressTypes.FirstOrDefault(x => x.Id == address.AddressTypeId) != null) ? address.AddressTypeId : addressTypes.FirstOrDefault().Id;
            address.CreatedBy = user.UserName;
            address.CreatedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<UserAddress>().AddAsync(address);

            return UsersMapper.Mapper.Map<UserAddressResponse>(address);
        }
    }
}
