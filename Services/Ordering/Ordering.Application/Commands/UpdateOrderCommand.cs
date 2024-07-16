
using MediatR;
using Ordering.Application.Requests;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class UpdateOrderCommand : IRequest<OrderResponse>
    {
        public string UserName { get; set; } = default!;
        public UpdateOrderRequest Payload { get; set; } = default!;
    }
}
