using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RazorLight;
using Microsoft.Extensions.Options;
using eShopping.MailMan.Interfaces;
using eShopping.MailMan.Models;

namespace eShopping.MailMan.Services
{
    internal class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly RazorLightEngine _razorLightEngine;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
            _razorLightEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(EmailService))
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task SendEmailAsync(string toEmail, string subject, string templateName, object model)
        {
            var templatePath = $"{_emailSettings.EmailTemplatesFolder}/{templateName}.cshtml";
            var emailBody = await _razorLightEngine.CompileRenderAsync(templatePath, model);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = emailBody };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
