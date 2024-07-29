
namespace eShopping.MessageBrocker.Models
{
    public class OrderBasketModel
    {
        public string UserName { get; set; } = default!;
        public IList<string> Products { get; set; } = new List<string>();

    }
}
