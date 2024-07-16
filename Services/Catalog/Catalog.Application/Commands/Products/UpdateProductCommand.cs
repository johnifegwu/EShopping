using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public UpdateProductRequest Payload { get; set; } = default!;
    }
}
