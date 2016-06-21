using QuanLiDoAn.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using QuanLiDoAn.Common;
using QuanLiDoAn.Areas.Admin.Code;
using System.Web.Security;
using Models.ViewModel;

namespace QuanLiDoAn.Areas.User.Controllers
{
 
    public class HomeController : Controller
    {
        //
        // GET: /User/DOCGIA/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DOANDetails(string searchString,int page = 1,int pageSize = 10)
        {
            var iplDOAN = new DOANModel();
            var model = iplDOAN.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

    }
}
