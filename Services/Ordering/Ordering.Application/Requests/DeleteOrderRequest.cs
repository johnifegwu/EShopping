
namespace Ordering.Application.Requests
{
    public class DeleteOrderRequest
    {
        public int OrderId { get; set; }
        public string OrderUserName { get; set; } = default!;
    }
}
