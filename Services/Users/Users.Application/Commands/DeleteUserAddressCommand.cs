
using eShopping.Security;
using MediatR;

namespace Users.Application.Commands
{
    public class DeleteUserAddressCommand : IRequest<bool>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public int AddressId { get; set; }
    }
}
