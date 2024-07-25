
namespace eShopping.MailMan.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; } = default!;
        public int SmtpPort { get; set; } = default!;
        public string SenderName { get; set; } = default!;
        public string SenderEmail { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
