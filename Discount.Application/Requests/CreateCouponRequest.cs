
namespace Discount.Application.Requests
{
    public class CreateCouponRequest
    {
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
        public string ProductId { get; set; } = default!;
    }
}
