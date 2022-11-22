using Stanford_ShopOnline.Session;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopOnline.Areas.Admin.Models;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(LoginModel objLoginModel)
        {
            if (ModelState.IsValid)
            {
                UserBusiness userBus = new UserBusiness();
                bool result = userBus.Login(objLoginModel.UserName, objLoginModel.Password);

                if (result)
                {
                    User objUserLogin = userBus.GetByUserName(objLoginModel.UserName);  

                    SessionHelper.SetSession(new UserSession() { UserName = objLoginModel.UserName, Email = objUserLogin.Email, ImageUrl = objUserLogin.ImageUrl });
              
                    return RedirectToAction("list", "Product");
                }

                ModelState.AddModelError("", "Sai thông tin đăng nhập");
            }

            return View(objLoginModel);
        }

        public ActionResult logout()
        {
            SessionHelper.SetSession(null);

            return RedirectToAction("login", "Login");
        }
    }
}