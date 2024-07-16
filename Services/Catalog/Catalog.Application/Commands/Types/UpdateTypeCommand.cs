
using Catalog.Application.Requests;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Commands.Types
{
    public class UpdateTypeCommand : IRequest<TypeResponse>
    {
        public UpdateTypeRequest Payload { get; set; } = default!;
    }
}
