using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuanLiDoAn.Areas.Admin.Models
{
    public class ChangePassModel
    {
        [Required]
        public string passOld { get; set; }

        [Required]
        public string passNew { get; set; }

        [Required]
        public string reType { get; set; }

        

        
    }
}