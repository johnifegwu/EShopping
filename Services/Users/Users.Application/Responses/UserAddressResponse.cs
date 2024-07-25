
namespace Users.Application.Responses
{
    public class UserAddressResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressLine1 { get; set; } = default!;
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
    }
}
