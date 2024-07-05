
namespace Basket.Application.Responses
{
    public class ShoppingCartItemResponse
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public string ImageFile { get; set; } = default!;
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; } = default!;
    }
}
