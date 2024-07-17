
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Commands
{
    public class CancelOrderCommand : IRequest<OrderResponse>
    {
        /// <summary>
        /// Order id.
        /// </summary>
        public int OrderId {  get; set; }

        /// <summary>
        /// Current User.
        /// </summary>
        public string UserName { get; set; } = default!;

        /// <summary>
        /// Username of the owner of this order.
        /// </summary>
        public string OwnerUserName {  get; set; } = default!;
    }
}
