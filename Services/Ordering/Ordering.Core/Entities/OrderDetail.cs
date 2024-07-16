
namespace Ordering.Core.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
    }
}
