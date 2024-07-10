
namespace eShopping.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException():base("Record not found.")
        {
        }

        public RecordNotFoundException(string message)
            : base(message)
        {
        }

        public RecordNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
