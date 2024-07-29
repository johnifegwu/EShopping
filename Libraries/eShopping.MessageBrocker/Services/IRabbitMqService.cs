
namespace eShopping.MessageBrocker.Services
{
    public interface IRabbitMqService
    {
        void SetEndPoint(string endpoint);

        Task StartAsync();

        Task StopAsync();

        Task SendMessage<T>(T message) where T : class;
    }
}
