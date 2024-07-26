
using eShopping.MailMan.Interfaces;
using eShopping.MailMan.Models;
using eShopping.MailMan.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;
using System.Reflection;

namespace eShopping.MailMan.Extensions
{
    public static class MailManExtention
    {
        public static void AddEmailService(this IServiceCollection services, IConfiguration configuration, string EmailSettingsSectionName, Assembly executingAssembly)
        {
            services.Configure<EmailSettings>(configuration.GetSection(EmailSettingsSectionName));
            services.AddScoped<IEmailService, EmailService>();

            //Configure Razorlight with embeded resources
            var razorlightEngine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(executingAssembly)
                .UseMemoryCachingProvider()
                .EnableDebugMode(true)
                .Build();
            services.AddSingleton<IRazorLightEngine>(razorlightEngine);
        }
    }
}
