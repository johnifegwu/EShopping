namespace Users.Core.Models
{
    public class DefaultConfig
    {
        public string Environment { get; set; } = default!;
        public string RedisUrl { get; set; } = default!;
        public string SecretKey { get; set; } = default!;
        public string DefaultUserName { get; set; } = default!;
        public string DefaultUserEmail { get; set; } = default!;
        public string defaultUserPassword { get; set; } = default!;
        public int PaswordExpiryMonths { get; set; } = 6;
        public int? MaxAddressPerUser { get; set; } = 5;
    }
}
