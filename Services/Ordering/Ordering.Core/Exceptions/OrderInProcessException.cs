
namespace Ordering.Core.Exceptions
{
    public class OrderInProcessException : Exception
    {
        public OrderInProcessException(): base("Your order is currently being processed, try canceling it first and try again.")
        {

        }

        public OrderInProcessException(int OrderId) : base($"Order number {OrderId} is currently being processed, try canceling it first and try again.")
        {

        }

        public OrderInProcessException(int OrderId, bool IsShipped) : base($"Order number {OrderId} is marked as shipped, the operation has been canceled as a result.")
        {
        }
    }
}
