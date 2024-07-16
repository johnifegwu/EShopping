
namespace Basket.Core.Entities
{
    public class ShoppingCartItem
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string ImageFile {  get; set; } = default!;
        public int Quantity {  get; set; } = 1;
        public decimal Price {  get; set; } = default!;
    }
}
