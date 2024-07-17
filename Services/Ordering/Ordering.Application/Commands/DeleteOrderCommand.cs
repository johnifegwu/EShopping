
using MediatR;

namespace Ordering.Application.Commands
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        /// <summary>
        /// Current User.
        /// </summary>
        public string UserName { get; set; } = default!;
        public int OrderId { get; set; }
    }
}
