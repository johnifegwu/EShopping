
using MediatR;

namespace Users.Application.Commands
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; } = default!;
    }
}
