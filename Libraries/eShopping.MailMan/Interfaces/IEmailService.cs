
namespace eShopping.MailMan.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string templateName, object model);
    }

}
