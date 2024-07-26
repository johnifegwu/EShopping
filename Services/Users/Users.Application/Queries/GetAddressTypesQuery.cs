
using MediatR;
using Users.Application.Responses;

namespace Users.Application.Queries
{
    public class GetAddressTypesQuery : IRequest<IList<AddressTypeResponse>>
    {
    }
}
