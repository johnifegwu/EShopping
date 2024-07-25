
namespace Users.Application.Requests
{
    public class NewUserRequest
    {
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
        public List<CreateUserAddressRequest>? Addresses { get; set; }
    }
}
