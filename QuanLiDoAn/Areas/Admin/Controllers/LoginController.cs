using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiDoAn.Areas.Admin.Models;
using Models;
using QuanLiDoAn.Areas.Admin.Code;
using System.Web.Security;
using QuanLiDoAn.Common;

namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            var context = new AccountModel();
            if (Membership.ValidateUser(model.UserName,model.Password)  && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                var user = context.getByName(model.UserName);
                var userSession = new UserSession();
                userSession.UserName = user.TenDangNhap;
                userSession.ID = user.MaNhanVien;

                Session.Add(CommonConstants.ADMIN_SESSION, userSession);
                return RedirectToAction("Index", "DOAN");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
