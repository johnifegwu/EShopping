
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrdersByUserNameQuery : IRequest<IList<OrderResponse>>
    {
        public string UserName { get; set; } = default!;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
