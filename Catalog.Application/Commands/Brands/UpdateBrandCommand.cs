
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Brands
{
    public class UpdateBrandCommand : IRequest<BrandResponse>
    {
        public UpdateBrandRequest Payload { get; set; } = default!;
    }
}
