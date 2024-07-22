
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class AuthenticateUserCommand : IRequest<UserLoginResponse>
    {
        public AuthenticateUserRequest Payload { get; set; } = default!;
    }
}
