
namespace eShopping.Exceptions
{
    public class MaximumAddressException : Exception
    {
        public MaximumAddressException() : base("You have reached the maximum addresses allowed per user.")
        {
        }

        public MaximumAddressException(string? message) : base(message)
        {
        }

        public MaximumAddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}