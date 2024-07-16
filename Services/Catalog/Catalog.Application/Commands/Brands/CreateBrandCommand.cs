
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Brands
{
    public class CreateBrandCommand : IRequest<BrandResponse>
    {
        public CreateBrandRequest Payload { get; set; } = default!;
    }
}
