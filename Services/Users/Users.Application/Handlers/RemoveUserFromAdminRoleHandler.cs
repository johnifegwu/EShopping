
using Data.Repositories;
using eShopping.Constants;
using eShopping.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Users.Application.Commands;
using Users.Application.Extensions;
using Users.Core.Entities;

namespace Users.Application.Handlers
{
    public class RemoveUserFromAdminRoleHandler : IRequestHandler<RemoveUserFromAdminRoleCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly ILogger<AuthenticateUserHandler> _logger;

        public RemoveUserFromAdminRoleHandler(IUnitOfWorkCore unitOfWork, ILogger<AuthenticateUserHandler> logger)
        {
            this._unitOfWork = unitOfWork;
            this._logger = logger;
        }

        public async Task<bool> Handle(RemoveUserFromAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.UserName);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var userAdminRole = await _unitOfWork.Repository<Role>().Get()
                .Where(r => r.RoleName == NameConstants.AdminRoleName)
                .Join(_unitOfWork.Repository<UserRoleJoin>().Get().Where(x => x.UserId == user.Id), r => r.Id, ur => ur.RoleId, (r, ur) => ur).FirstOrDefaultAsync();

            if (userAdminRole == null)
            {
                throw new NotFoundException("User not in Admin role.");
            }

            await _unitOfWork.Repository<UserRoleJoin>().DeleteAsync(userAdminRole);

            _logger.LogInformation($"{user.UserName} was removed from admin role by {request.CurrentUser.UserName} at {DateTime.UtcNow}");

            return true;
        }
    }
}
