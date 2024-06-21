using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
    {
    }
}
