
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
    public class AddUserToAdminRoleHandler : IRequestHandler<AddUserToAdminRoleCommand, bool>
    {
        private readonly IUnitOfWorkCore _unitOfWork;
        private readonly ILogger<AuthenticateUserHandler> _logger;

        public AddUserToAdminRoleHandler(IUnitOfWorkCore unitOfWork, ILogger<AuthenticateUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(AddUserToAdminRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.GetUser(request.UserName);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            //Check this user already in the Admin role.
            var userAdminRole = await _unitOfWork.Repository<Role>().Get()
                .Where(r => r.RoleName == NameConstants.AdminRoleName)
                .Join(_unitOfWork.Repository<UserRoleJoin>().Get().Where(x => x.UserId == user.Id), r => r.Id, ur => ur.RoleId, (r, ur) => ur).FirstOrDefaultAsync();

            //Add this user to admin role.
            if (userAdminRole == null)
            {
                //Get admin role
                var adminrole = await _unitOfWork.Repository<Role>().Get().Where(r => r.RoleName == NameConstants.AdminRoleName).FirstOrDefaultAsync();

                if (adminrole != null)
                {
                    await _unitOfWork.Repository<UserRoleJoin>().AddAsync(new UserRoleJoin
                    {
                        RoleId = adminrole.Id,
                        UserId = user.Id
                    });
                }

                _logger.LogInformation($"{user.UserName} was added to admin role by {request.CurrentUser.UserName} at {DateTime.UtcNow}");

            }

            return true;
        }
    }
}
