
using eShopping.Security;
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class AddUserAddressCommand : IRequest<UserAddressResponse>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public CreateUserAddressRequest Payload { get; set; } = default!;
    }
}
