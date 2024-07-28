
namespace eShopping.Exceptions
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException() : base("Card payment was not successful.")
        {
        }

        public PaymentFailedException(string message)
            : base(message)
        {
        }

        public PaymentFailedException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
