﻿
using Data.Repositories;
using eShopping.Exceptions;
using eShopping.Models;
using eShopping.Security;
using MediatR;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Responses;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, UserLoginResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly DefaultConfig _config;

        public ChangePasswordHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config)
        {
            this._unitOfWork = unitOfWork;
            this._config = config.Value;
        }
        public async Task<UserLoginResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            //Fluent Validation is failing for email and other regex
            //So we revalidate here just incase it fails.
            request.Payload.NewPassword.ValidatePassword();

            var user = await _unitOfWork.GetUser(request.CurrentUser.UserName);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var userRoles = await _unitOfWork.GetUserRoles(user.Id);

            //Validate password
            if (user.PasswordHash != request.Payload.OldPassword.HasStringValue(user.PasswordSalt))
            {
                throw new NotAuthorizedException("Invalid password.");
            }

            user.PasswordSalt = request.Payload.NewPassword.GenerateSalt();
            user.PasswordHash = request.Payload.NewPassword.HasStringValue(user.PasswordSalt);
            user.PasswordExpiryDate = DateTime.UtcNow.AddMonths(_config.PaswordExpiryMonths);
            user.CreatedBy = user.UserName;
            user.CreatedDate = DateTime.UtcNow;

            await _unitOfWork.Repository<User>().UpdateAsync(user);

            var result = new UserLoginResponse
            {
                UserId = user.Id,
                UserEmail = user.UserEmail,
                UserName = user.UserName,
                Roles = userRoles
            };

            result.GenerateToken(_config);

            return result;
        }
    }
}
