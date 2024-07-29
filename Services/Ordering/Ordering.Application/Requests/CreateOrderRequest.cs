
namespace Ordering.Application.Requests
{
    public class CreateOrderRequest
    {
        public string UserName { get; set; } = default!;
        public string OrderGuid { get; set; } = default!;
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = "USD";
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string AddressLine1 { get; set; } = default!;
        public string? AddressLine2 { get; set; }
        public string City { get; set; } = default!;
        public string? State { get; set; }
        public string ZipCode { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string CardType { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public List<CreateOrderDetail> OrderDetails { get; set; } = new();
    }

    public class CreateOrderDetail
    {
        public string ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
