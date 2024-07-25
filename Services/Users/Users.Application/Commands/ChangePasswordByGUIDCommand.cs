
using MediatR;

namespace Users.Application.Commands
{
    public class ChangePasswordByGUIDCommand : IRequest<bool>
    {
        public string GUID { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
