using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDoAn.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool RememberMe { set; get; }
    }
}