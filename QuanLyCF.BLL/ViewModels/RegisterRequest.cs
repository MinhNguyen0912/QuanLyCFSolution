using System.ComponentModel.DataAnnotations;

namespace QuanLyCF.BLL.ViewModels
{
    public class RegisterRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        [Required]
        public Guid RoleId { get; set; }

    }
}
