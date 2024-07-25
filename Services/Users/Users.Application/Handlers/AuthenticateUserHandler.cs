
using Data.Repositories;
using eShopping.Models;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthenticateUserHandler> _logger;
        public AuthenticateUserHandler(IUnitOfWorkCore unitOfWork, IOptions<DefaultConfig> config, ILogger<AuthenticateUserHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._defaultConfig = config.Value;
            this._logger = logger;
        }

        public async Task<UserLoginResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.AuthenticateUser(_defaultConfig, _logger, request.Payload.UserName, request.Payload.Password);
        }
    }
}
