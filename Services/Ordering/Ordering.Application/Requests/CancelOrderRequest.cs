
namespace Ordering.Application.Requests
{
    public class CancelOrderRequest
    {
        /// <summary>
        /// Order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Username of the owner of this order.
        /// </summary>
        public string OwnerUserName { get; set; } = default!;

        /// <summary>
        /// Email address of the owner of this order.
        /// </summary>
        public string OwnerEmail { get; set; } = default!;
    }
}
