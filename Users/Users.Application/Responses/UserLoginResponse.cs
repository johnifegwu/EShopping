
using Users.Core.Entities;

namespace Users.Application.Responses
{
    public class UserLoginResponse
    {
        public int UserId {  get; set; }
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string BearerToken { get; set; } = default!;
        public List<Role>? Roles { get; set; } = new();
    }
}
