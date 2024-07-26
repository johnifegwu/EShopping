
namespace Users.Application.Responses
{
    public class AddressTypeResponse
    {
        public int Id {  get; set; }
        public string AddressTypeName { get; set; } = default!;
        public int? MaxAddressPerUser { get; set; }
    }
}
