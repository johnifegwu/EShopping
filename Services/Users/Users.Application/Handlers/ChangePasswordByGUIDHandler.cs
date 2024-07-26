
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.Models;
using eShopping.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class ChangePasswordByGUIDHandler : IRequestHandler<ChangePasswordByGUIDCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly DefaultConfig _config;

        public ChangePasswordByGUIDHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config)
        {
            this._unitOfWork = unitOfWork;
            this._config = config.Value;
        }
        public async Task<bool> Handle(ChangePasswordByGUIDCommand request, CancellationToken cancellationToken)
        {
            //Fluent Validation is failing for email and other regex
            //So we revalidate here just incase it fails.
            request.Payload.NewPassword.ValidatePassword();

            var user = await _unitOfWork.Repository<User>().Get().Where(x => x.PasswordRecoveryUID == request.Payload.GUID).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new NotFoundException("GUID not not found.");
            }

            //Check if GUID has expired.
            if(user.PasswordRecoveryUIDExpiry < DateTime.UtcNow)
            {
                throw new ExpiredPasswordException("GUID has expired.");
            }

            user.PasswordSalt = request.Payload.NewPassword.GenerateSalt();
            user.PasswordHash = request.Payload.NewPassword.HashPassword(user.PasswordSalt);
            user.PasswordExpiryDate = DateTime.UtcNow.AddMonths(_config.PaswordExpiryMonths);
            user.CreatedBy = user.UserName;
            user.CreatedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<User>().UpdateAsync(user);

            return true;
        }
    }
}
