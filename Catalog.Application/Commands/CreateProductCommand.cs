
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductResponse>
    {
        public CreateProductRequest Payload { get; set; } = default!;
    }
}
