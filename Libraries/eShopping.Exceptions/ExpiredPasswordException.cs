
namespace eShopping.Exceptions
{
    public class ExpiredPasswordException : Exception
    {
        public ExpiredPasswordException() : base("Your password has expired, go to forgot password section to initialize password renewal.")
        {
        }

        public ExpiredPasswordException(string message)
            : base(message)
        {
        }

        public ExpiredPasswordException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
