namespace eShopping.MessageBrocker.Models
{
    public class MessageBrokerConfig
    {
        public string Host { get; set; } = default!;
        public string Port { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
