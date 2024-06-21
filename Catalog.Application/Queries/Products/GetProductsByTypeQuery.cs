using Catalog.Application.Responses;
using MediatR;
using MongoDB.Bson;

namespace Catalog.Application.Queries.Products
{
    public class GetProductsByTypeQuery : IRequest<IList<ProductResponse>>
    {
        public string TypeId { get; set; } = default!;
    }
}
