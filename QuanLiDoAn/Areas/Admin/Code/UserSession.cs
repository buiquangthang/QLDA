using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLiDoAn.Areas.Admin.Code
{
    [Serializable]
    public class UserSession
    {
        public string UserName { set; get; }
        public string ID { set; get; }
    }
}