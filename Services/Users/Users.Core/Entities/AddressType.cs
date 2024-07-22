
using Users.Core.Common;

namespace Users.Core.Entities
{
    public class AddressType : BaseEntity
    {
        public string AddressTypeName { get; set; } = default!;
        public int? MaxAddressPerUser {  get; set; }
    }
}
