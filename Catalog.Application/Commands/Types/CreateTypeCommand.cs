
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Types
{
    public class CreateTypeCommand : IRequest<TypeResponse>
    {
        public CreateTypeRequest Payload { get; set; } = default!;
    }
}
