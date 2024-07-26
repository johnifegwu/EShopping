
using eShopping.MailMan.Interfaces;
using eShopping.MailMan.Models;
using eShopping.MailMan.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShopping.MailMan.Extensions
{
    public static class MailManExtention
    {
        public static void AddEmailService(this IServiceCollection services, IConfiguration configuration, string EmailSettingsSectionName)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettingsSectionName));
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
