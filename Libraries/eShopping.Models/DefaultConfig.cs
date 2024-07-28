namespace eShopping.Models
{
    public class DefaultConfig
    {
        public string Environment { get; set; } = default!;
        public string RedisUrl { get; set; } = default!;
        public string JWTSecretKey { get; set; } = default!;
        public string JWTIssuer { get; set; } = default!;
        public string JWTAudience { get; set; } = default!;
        public string DefaultUserName { get; set; } = default!;
        public string DefaultUserEmail { get; set; } = default!;
        public string defaultUserPassword { get; set; } = default!;
        public string DiscountRpcHost { get; set; } = default!;
        public int PaswordExpiryMonths { get; set; } = 6;
        public int BearerTokenExpiryMonths { get; set; } = 1;
        public string StripeApiKey {  get; set; } = default!;
        public int? MaxAddressPerUser { get; set; } = 5;
        public string EncryptionKey {  get; set; } = default!;
        public string EncryptionSecret { get; set;} = default!;
    }
}
