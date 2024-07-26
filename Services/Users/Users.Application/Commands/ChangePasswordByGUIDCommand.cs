
using MediatR;
using Users.Application.Requests;

namespace Users.Application.Commands
{
    public class ChangePasswordByGUIDCommand : IRequest<bool>
    {
        public ChangePasswordByGUIDRequest Payload { get; set; } = default!;
    }
}
