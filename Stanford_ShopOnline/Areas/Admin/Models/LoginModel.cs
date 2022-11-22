using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Areas.Admin.Models
{
    public class LoginModel
    {
        [Display(Name="Tài khoản")]
        [Required(ErrorMessage =("Bạn cần nhập tài khoản"))]
        public string UserName { get; set; }
        [Display(Name="Mật khẩu")]
        [Required(ErrorMessage="Bạn cần nhập mật khẩu")]
        public string Password { get; set; }

        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}