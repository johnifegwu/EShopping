
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public NewUserRequest Payload { get; set; } = default!;
    }
}
