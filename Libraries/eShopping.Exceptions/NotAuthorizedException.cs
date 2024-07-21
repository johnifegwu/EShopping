
namespace eShopping.Exceptions
{
    [Serializable]
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Un authorized access.")
        {
        }

        public NotAuthorizedException(string? message) : base(message)
        {
        }

        public NotAuthorizedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}