
using Ordering.Core.Common;

namespace Ordering.Core.Entities
{
    public class Order : BaseEntity
    {
        public string? UserName {  get; set; }
        public decimal? TotalPrice { get; set; }
        public string? FirstName {  get; set; }
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
        public string? Expiration {  get; set; }
        public string? CVV { get; set; }
        public int? PaymentMethod { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }

        public void UpdateChildWithId()
        {
            if(OrderDetails != null && OrderDetails.Count > 0)
            {
                foreach(var item in OrderDetails)
                {
                    item.OrderId = this.Id;
                }
            }
        }
    }
}
