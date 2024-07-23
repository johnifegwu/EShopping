
using Users.Core.Entities;

namespace Users.Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; } = default!;
        public string UserEmail { get; set; } = default!;
        public DateTime? PasswordExpiryDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<UserAddress>? Addresses { get; set; }
    }
}
