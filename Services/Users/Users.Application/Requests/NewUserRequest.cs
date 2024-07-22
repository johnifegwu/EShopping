
namespace Users.Application.Requests
{
    public class NewUserRequest
    {
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public DateTime? PasswordExpiryDate { get; set; }
        public string? PasswordRecoveryUID { get; set; }
        public DateTime? PasswordRecoveryUIDExpiry { get; set; }
        public List<AddressRequest>? Addresses { get; set; }
    }
}
