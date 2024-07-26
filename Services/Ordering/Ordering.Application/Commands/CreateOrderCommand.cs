
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
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public CreateOrderRequest Payload { get; set; } = default!;
    }
}
