
using Ordering.Core.Common;

namespace Ordering.Core.Entities
{
    public class Order : BaseEntity
    {
        public string UserName { get; set; } = default!;
        public string OrderGuid { get; set; } = default!;
        public decimal TotalPrice {  get; set; } = default!;

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
        public string Expiration {  get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int? PaymentMethod { get; set; }
        public bool? IsPaid { get; set; } = false;
        public string? PaymentReference {  get; set; }
        public string? PaymentProviderUsed {  get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool? IsCanceled { get; set; } = false;
        public bool? IsShipped { get; set; } = false;
        public string? ShippingDetails {  get; set; }
        public List<OrderDetail>? OrderDetails { get; set; } = new List<OrderDetail>();

        public Order()
        {
            this.OrderGuid = new Guid().ToString();
        }

        public decimal GetTotalPrice()
        {
            decimal result = 0;

            foreach (var item in OrderDetails)
            {
                result += (item.Price * item.Quantity);
            }

            this.TotalPrice = result;

            return result;
        }
        public void UpdateChildWithId()
        {
            foreach (var item in OrderDetails)
            {
                item.OrderId = this.Id;
            }
        }
    }
}
