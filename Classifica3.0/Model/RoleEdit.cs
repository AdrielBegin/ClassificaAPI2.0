using Microsoft.AspNetCore.Identity;

namespace Classifica3._0.Model
{
    public class RoleEdit
    {
        public IdentityRole? Role{ get; set; }
        public IEnumerable<IdentityUser> Members { get; set; }
        public IEnumerable<IdentityUser> NonMembers { get; set; }
    }
}
