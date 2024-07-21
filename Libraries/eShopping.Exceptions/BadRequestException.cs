
namespace eShopping.Exceptions
{
    [Serializable]
    public class BadRequestException : Exception
    {
        public BadRequestException() : base("Bad request.")
        {
        }

        public BadRequestException(string message)
            : base(message)
        {
        }

        public BadRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
