
using eShopping.MessageBrocker.Endpoints;
using eShopping.MessageBrocker.Models;
using eShopping.MessageBrocker.Services;
using MassTransit;
using Microsoft.Extensions.Options;

namespace Ordering.Application.Services
{
    internal class RabbitMqService : IRabbitMqService
    {
        private readonly IBusControl _busControl;
        private readonly MessageBrokerConfig _config;
        private string _serviceName;

        public RabbitMqService(IBusControl busControl, IOptions<MessageBrokerConfig> config)
        {
            this._busControl = busControl;
            this._config = config.Value;
            this._serviceName = NamedEndpoints.OrderBasketQueue;
        }

        public void SetEndPoint(string endpoint)
        {
            _serviceName = endpoint;
        }

        public async Task SendMessage<T>(T message) where T : class
        {
            var port = Environment.GetEnvironmentVariable("RABBITMQ_PORT") ?? _config.Port;
            var host = Environment.GetEnvironmentVariable("RABBITMQ_HOST")?? _config.Host;

            var endpoint = await _busControl.GetSendEndpoint(new Uri($"rabbitmq://{host}:{port}/{_serviceName}"));
            await endpoint.Send<T>(message);
        }

        public async Task StartAsync()
        {
            await _busControl.StartAsync();
        }

        public async Task StopAsync()
        {
            await _busControl.StopAsync();
        }
    }
}
