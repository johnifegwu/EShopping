
using MediatR;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class ChangePasswordCommand : IRequest<UserLoginResponse>
    {
        public string UserName { get; set; } = default!;
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
