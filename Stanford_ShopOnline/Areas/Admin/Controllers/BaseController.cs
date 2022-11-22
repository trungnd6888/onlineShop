using Stanford_ShopOnline.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(SessionHelper.GetSession() == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller= "Login", action= "login" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}