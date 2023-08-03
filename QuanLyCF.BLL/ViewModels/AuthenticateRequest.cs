using System.ComponentModel.DataAnnotations;

namespace QuanLyCF.BLL.ViewModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
