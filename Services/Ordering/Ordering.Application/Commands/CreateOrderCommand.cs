
using eShopping.Security;
using MediatR;
using Ordering.Application.Requests;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class CreateOrderCommand : IRequest<OrderResponse>
    {
        /// <summary>
        /// Current user.
        /// </summary>
        public UserClaims CurrentUser { get; set; } = default!;
        public CreateOrderRequest Payload { get; set; } = default!;
    }
}
