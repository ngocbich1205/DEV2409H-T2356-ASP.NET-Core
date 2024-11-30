using System.ComponentModel.DataAnnotations;

namespace lesson10CustomAuthorize.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Email không để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không để trống")]
        public string Password { get; set; }
    }

}

