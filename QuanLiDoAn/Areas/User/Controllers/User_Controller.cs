using Models;
using QuanLiDoAn.Areas.Admin.Code;
using QuanLiDoAn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLiDoAn.Areas.User.Controllers
{
    public class User_Controller : User_BaseController
    {
        //
        // GET: /User/User_/

        public ActionResult User_Details(string searchString, int page = 1, int pageSize = 10)
        {
            var iplDOAN = new DOANModel();
            var model = iplDOAN.ListAll(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Borrow()
        {
            UserSession user = (UserSession)Session[CommonConstants.USER_SESSION];
            var dg = new DOCGIAModel();
            var model = dg.Borrow(user.ID);
            return View(model);
        }

        public ActionResult User_Index()
        {
            return View();
        }

        public ActionResult User_Profile()
        {
            //UserSession nv = (UserSession)Session[CommonConstants.USER_SESSION];
            return View();
        }

    }
}
