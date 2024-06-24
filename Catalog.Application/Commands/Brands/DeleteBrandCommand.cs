
using MediatR;

namespace Catalog.Application.Commands.Brands
{
    public class DeleteBrandCommand : IRequest<bool>
    {
        public string BrandId { get; set; } = default!;
    }
}
