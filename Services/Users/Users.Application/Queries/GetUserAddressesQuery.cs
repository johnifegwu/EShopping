
using eShopping.Security;
using MediatR;
using Users.Application.Responses;

namespace Users.Application.Queries
{
    public class GetUserAddressesQuery : IRequest<IList<UserAddressResponse>>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 15;
    }
}
