using MediatR;

namespace Catalog.Application.Commands.Products
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string ProductId { get; set; } = default!;
    }
}
