
namespace Users.Application.Responses
{
    public class ForgotPasswordResponse
    {
        public string GUID { get; set; } = default!;
        public DateTime GUIDExpiryDate { get; set; }
    }
}
