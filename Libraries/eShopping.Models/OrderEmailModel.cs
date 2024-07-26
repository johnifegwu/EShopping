
namespace eShopping.Models
{
    public class OrderEmailModel
    {
        public int OrderId {  get; set; }
        public string CustomerEmail { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string ShippingDetails { get; set; } = default!;
    }
}
