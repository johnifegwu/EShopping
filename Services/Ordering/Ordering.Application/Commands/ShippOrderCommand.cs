
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class ShippOrderCommand : IRequest<OrderResponse>
    {
        public int OrderId { get; set; }

        /// <summary>
        /// Current User.
        /// </summary>
        public string UserName { get; set; } = default!;

        /// <summary>
        /// Username of the owner of this order.
        /// </summary>
        public string OwnerUserName { get; set; } = default!;

        /// <summary>
        /// Shipping details may include:
        /// 1. Courier service name.
        /// 2. Trancking details, etc.
        /// </summary>
        public string ShiipingDetails { get; set; } = default!;
    }
}
