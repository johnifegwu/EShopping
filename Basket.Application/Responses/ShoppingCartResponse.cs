
namespace Basket.Application.Responses
{
    public class ShoppingCartResponse
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItemResponse> Items { get; set; } = new List<ShoppingCartItemResponse>();
        public decimal TotalPrice
        {
            get
            {
                decimal result = 0;

                foreach (var item in Items)
                {
                    result += (item.Price * item.Quantity);
                }

                return result;
            }
        }
    }
}
