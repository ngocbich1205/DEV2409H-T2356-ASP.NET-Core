using System.ComponentModel.DataAnnotations;

namespace lab09.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Mã sinh viên không để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mật khẩu không để trống")]
        public string Password { get; set; }
        public object? UserOrEmail { get; internal set; }
    }
}
