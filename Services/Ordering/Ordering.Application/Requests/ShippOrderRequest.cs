
namespace Ordering.Application.Requests
{
    public class ShippOrderRequest
    {
        public int OrderId { get; set; }

        /// <summary>
        /// Username of the owner of this order.
        /// </summary>
        public string OwnerUserName { get; set; } = default!;

        /// <summary>
        /// Email of the owner of this order.
        /// </summary>
        public string OwnerEmail { get; set; } = default!;

        /// <summary>
        /// Shipping details may include:
        /// 1. Courier service name.
        /// 2. Trancking details, etc.
        /// </summary>
        public string ShiipingDetails { get; set; } = default!;
    }
}
