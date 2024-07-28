
using eShopping.Security;
using MediatR;

namespace Ordering.Application.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// Current User.
        /// </summary>
        public UserClaims CurrentUser { get; set; } = default!;
        public int OrderId { get; set; }
        public string OrderUserName { get; set; } = default!;
    }
}
