
using eShopping.Security;
using MediatR;
using Users.Application.Requests;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class UpdateUserAddressCommand : IRequest<UserAddressResponse>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public UpdateUserAddressRequest Payload { get; set; } = default!;
    }
}
