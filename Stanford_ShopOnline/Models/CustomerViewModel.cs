using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [Display(Name="Họ tên")]
        [Required(ErrorMessage ="Bạn cần nhập họ tên")]
        public string Name { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Bạn cần nhập điện thoại")]
        [Display(Name = "Điện thoại")]
        public string Tel { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}