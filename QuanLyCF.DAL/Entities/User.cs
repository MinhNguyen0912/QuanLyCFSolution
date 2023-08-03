using Microsoft.AspNetCore.Identity;

namespace QuanLyCF.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
