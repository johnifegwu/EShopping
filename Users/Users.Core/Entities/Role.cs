
using Users.Core.Common;

namespace Users.Core.Entities
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; } = default!;
        public string RoleDescription { get; set; } = default!;
    }
}
