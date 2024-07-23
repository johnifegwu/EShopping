
using eShopping.Security;
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class CreateUserCommand : IRequest<UserResponse>
    {
        public UserClaims? CurrentUser { get; set; }
        public bool IsAdminUser { get; set; } = false;
        public NewUserRequest Payload { get; set; } = default!;
    }
}
