
using eShopping.Security;
using MediatR;
using Ordering.Application.Requests;

namespace Ordering.Application.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// Current User.
        /// </summary>
        public UserClaims CurrentUser { get; set; } = default!;
        public DeleteOrderRequest Payload { get; set; } = default!;
    }
}
