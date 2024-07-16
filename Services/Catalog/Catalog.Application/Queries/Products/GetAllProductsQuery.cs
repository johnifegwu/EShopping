using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{
    /// <summary>
    /// Gets all the Products from the system.
    /// </summary>
    public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
