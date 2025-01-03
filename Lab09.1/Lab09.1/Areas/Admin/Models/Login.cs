﻿using System.ComponentModel.DataAnnotations;

namespace Lab09._1.Areas.Admin.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Địa chỉ Email không để trống")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không để trống")]
        public string Password { get; set; }
        public bool Remember { get; set; }
        
    }
}
