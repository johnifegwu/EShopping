
using Data.Repositories;
using eShopping.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;

        public ForgotPasswordHandler(IUnitOfWorkCore unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<ForgotPasswordResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.Email);

            if (user == null)
            {
                throw new NotFoundException("The email provided was not found in our records.");
            }

            //Generate GUID and update user.
            var guid = Guid.NewGuid().ToString();
            var expiryDate = DateTime.UtcNow.AddDays(1);

            user.PasswordRecoveryUID = guid;
            user.PasswordRecoveryUIDExpiry = expiryDate;

            await _unitOfWork.Repository<User>().UpdateAsync(user);

            return new ForgotPasswordResponse()
            {
                GUID = guid,
                GUIDExpiryDate = expiryDate,
            };
        }
    }
}
