
using eShopping.Security;
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrderByGuidQuery : IRequest<OrderResponse>
    {
        public UserClaims CurrentUser { get; set; } = default!;
        public string OrderGuid { get; set; } = default!;
    }
}
