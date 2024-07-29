
using eShopping.Security;
using MediatR;
using Ordering.Application.Requests;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class CancelOrderCommand : IRequest<OrderResponse>
    {
        /// <summary>
        /// Current User.
        /// </summary>
        public UserClaims CurrentUser { get; set; } = default!;
        public CancelOrderRequest Payload { get; set; } = default!;
    }
}
