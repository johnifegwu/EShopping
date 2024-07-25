
using MediatR;
using Users.Application.Responses;

namespace Users.Application.Commands
{
    public class ForgotPasswordCommand : IRequest<ForgotPasswordResponse>
    {
        public string Email { get; set; } = default!;
    }
}
