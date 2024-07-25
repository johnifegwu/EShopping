
namespace eShopping.MailMan.Models
{
    public class ForgotPasswordModel
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Guid { get; set; } = default!;
        public string ChangePasswordUrl { get; set; } = default!;
    }
}
