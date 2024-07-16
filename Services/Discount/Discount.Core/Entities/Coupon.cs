
namespace Discount.Core.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Amount {  get; set; }
        public string ProductId { get; set; } = default!;
    }
}
