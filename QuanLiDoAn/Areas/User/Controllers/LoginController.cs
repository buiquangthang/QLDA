using Models;
using QuanLiDoAn.Areas.Admin.Code;
using QuanLiDoAn.Areas.Admin.Models;
using QuanLiDoAn.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLiDoAn.Areas.User.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /User/Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            var context = new DOCGIAModel();
            if (ModelState.IsValid)
            {
                int res = context.Login(model.UserName, model.Password);
                if (res > 0)
                {
                    var user = context.getByID(model.UserName);
                    var userSession = new UserSession();
                    userSession.UserName = "";
                    userSession.ID = user.MaDocGia;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return RedirectToAction("User_Index", "User_");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}
