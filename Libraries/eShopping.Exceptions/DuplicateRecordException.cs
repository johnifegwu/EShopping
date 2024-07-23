
namespace eShopping.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        public DuplicateRecordException() : base("Duplicate record exception.")
        {
        }

        public DuplicateRecordException(string message)
            : base(message)
        {
        }

        public DuplicateRecordException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
