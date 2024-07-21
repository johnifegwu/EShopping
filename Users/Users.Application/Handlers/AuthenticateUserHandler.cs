
using Data.Repositories;
using eShopping.Models;
using MediatR;
using Microsoft.Extensions.Options;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Application.Responses;

namespace Users.Application.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, UserLoginResponse>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly DefaultConfig _defaultConfig;
        public AuthenticateUserHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config)
        {
            this._unitOfWork = unitOfWork;
            this._defaultConfig = config.Value;
        }

        public async Task<UserLoginResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthenticateUser(_defaultConfig, request.Payload.UserName, request.Payload.Password);
        }
    }
}
