using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetProductByBrandQuery : IRequest<IList<ProductResponse>>
    {
        public string BrandId { get; set; } = default!;
    }
}
