using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries.Types
{
    public class GetAllProductTypesQuery : IRequest<IList<TypeResponse>>
    {
    }
}
