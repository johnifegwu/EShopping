using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Brands
{
    public class GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
