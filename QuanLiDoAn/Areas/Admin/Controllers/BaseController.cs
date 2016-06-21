using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLiDoAn.Common;
using QuanLiDoAn.Areas.Admin.Code;
using System.Web.Routing;
namespace QuanLiDoAn.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (UserSession) Session[CommonConstants.ADMIN_SESSION];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Index", Area = "Admin"}));
            }
            base.OnActionExecuting(filterContext);
        }


    }
}
