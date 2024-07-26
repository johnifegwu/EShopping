
using eShopping.Security;
using MediatR;

namespace Users.Application.Commands
{
    public class AddUserToAdminRoleCommand : IRequest<bool>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public string UserName { get; set; } = default!;
    }
}
