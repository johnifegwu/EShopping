
using eShopping.MessageBrocker.Models;
using eShopping.MessageBrocker.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Services;

namespace eShopping.MessageBrocker.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services, IConfigurationSection MessageBrockerSection)
        {
            services.AddScoped<IRabbitMqService, RabbitMqService>();
            //Add config
            services.Configure<MessageBrokerConfig>(MessageBrockerSection);

        }
    }
}
