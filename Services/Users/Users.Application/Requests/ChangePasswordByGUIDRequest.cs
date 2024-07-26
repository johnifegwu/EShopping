
namespace Users.Application.Requests
{
    public class ChangePasswordByGUIDRequest
    {
        public string GUID { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
