
using eShopping.Security;
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class ChangePasswordCommand : IRequest<UserLoginResponse>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public ChangePasswordRequest Payload { get; set; } = default!;
    }
}
