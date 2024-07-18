
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrdersByFlagsQuery : IRequest<IList<OrderResponse>>
    {
        public string? OptionalUserName {  get; set; }
        public bool IsShipped {  get; set; } 
        public bool IsPaid {  get; set; }
        public bool IsCanceled { get; set; }
        public bool IsDeleted {  get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
