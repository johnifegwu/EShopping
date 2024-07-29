
using eShopping.MessageBrocker.Models;

namespace eShopping.MessageBrocker.Messages
{
    public class OrderBasketMessage
    {
        public OrderBasketModel Payload { get; set; } = default!;

    }
}
