
namespace Users.Application.Requests
{
    public class AuthenticateUserRequest
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
