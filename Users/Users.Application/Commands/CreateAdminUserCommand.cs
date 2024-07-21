
using MediatR;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class CreateAdminUserCommand : IRequest<UserLoginResponse>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
