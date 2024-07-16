
using MediatR;

namespace Catalog.Application.Commands.Types
{
    public class DeleteTypeCommand : IRequest<bool>
    {
        public string TypeId { get; set; } = default!;
    }
}
