
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class DeleteUserAddressHandler : IRequestHandler<DeleteUserAddressCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public DeleteUserAddressHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.CurrentUser.UserName);

            if (user == null)
            {
                throw new NotAuthorizedException();
            }

            var address = await _unitOfWork.GetUserAddressById(user.Id, request.AddressId);

            if (address == null)
            {
                throw new NotFoundException("Address not found.");
            }

            await _unitOfWork.Repository<UserAddress>().DeleteAsync(address);

            return true;
        }
    }
}
