using Microsoft.AspNetCore.Identity;

namespace QuanLyCF.DAL.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; }
        public List<User> Users { get; set; }
    }
}
