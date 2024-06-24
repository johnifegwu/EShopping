using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Products
{

    /// <summary>
    /// Search Products by Name.
    /// </summary>
    public class GetProductsByNameQuery : IRequest<IList<ProductResponse>>
    {
        public string ProductName { get; set; } = default!;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set;} = 20;
    }

}
