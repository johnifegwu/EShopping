using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductsByTypeQuery : IRequest<IList<ProductResponse>>
    {
        public string TypeId { get; set; } = default!;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
