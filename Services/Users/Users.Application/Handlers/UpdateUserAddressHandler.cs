
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Mappers;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class UpdateUserAddressHandler : IRequestHandler<UpdateUserAddressCommand, UserAddressResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public UpdateUserAddressHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<UserAddressResponse> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.CurrentUser.UserName);

            if (user == null)
            {
                throw new NotAuthorizedException();
            }

            var address = await _unitOfWork.GetUserAddressById(user.Id, request.Payload.Id);

            if (address == null)
            {
                throw new NotFoundException("Address not found.");
            }

            //Update address
            address.LastModifiedDate = DateTime.UtcNow;
            address.LastModifiedBy = user.UserName;
            address.AddressLine1 = request.Payload.AddressLine1;
            address.AddressLine2 = request.Payload.AddressLine1;
            address.Country = request.Payload.Country;
            address.City = request.Payload.City;
            address.State = request.Payload.State;
            address.ZipCode = request.Payload.ZipCode;

            await _unitOfWork.Repository<UserAddress>().UpdateAsync(address);

            return UsersMapper.Mapper.Map<UserAddressResponse>(address);
        }
    }
}
