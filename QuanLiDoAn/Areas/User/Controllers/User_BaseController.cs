using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiDoAn.Common;
using QuanLiDoAn.Areas.Admin.Code;
using System.Web.Routing;
namespace QuanLiDoAn.Areas.User.Controllers
{
    public class User_BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession) Session[CommonConstants.USER_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login", Area = "User" }));
            }
            base.OnActionExecuting(filterContext);
            return;
        }
    }
}
