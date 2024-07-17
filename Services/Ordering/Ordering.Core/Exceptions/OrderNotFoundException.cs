
using eShopping.Exceptions;

namespace Ordering.Core.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(int OrderId, string OwnerUserName):base($"Order number {OrderId} was not found in the system for user {OwnerUserName}.")
        {

        }
    }
}
