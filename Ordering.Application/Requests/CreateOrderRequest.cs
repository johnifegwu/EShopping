
using Ordering.Core.Entities;

namespace Ordering.Application.Requests
{
    public class CreateOrderRequest
    {
        public string? UserName { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailAddress { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? CardType { get; set; }
        public string? Expiration { get; set; }
        public string? CVV { get; set; }
        public int? PaymentMethod { get; set; }
        public List<CreateOrderDetail> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetail
    {
        public int OrderId { get; set; }
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
