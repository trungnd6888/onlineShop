using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;

namespace Stanford_ShopOnline.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult index()
        {
            CategoryBusiness categoryBus = new CategoryBusiness();
            List<Category> categoryList = categoryBus.GetList("");

            ProductBusiness productBus = new ProductBusiness();
            List<Product> productNewList = productBus.GetNewList(16);
            ViewBag.productNewList = productNewList;

            BannerBusiness bannerBus = new BannerBusiness();
            List<Banner> bannerList = bannerBus.GetList("");
            ViewBag.bannerList = bannerList;

            return View(categoryList);
        }

        public ActionResult MainMenu()
        {
            MenuBusiness menuBus = new MenuBusiness();
            List<Menu> menuList = menuBus.GetList();

            MenuChildBusiness menuChildBus = new MenuChildBusiness();
            List<MenuChild> menuChildList = menuChildBus.GetList();
            ViewBag.menuChildList = menuChildList;

            return PartialView(menuList); 
        }

        public ActionResult ProductBestSale()
        {
            ProductBusiness productBus = new ProductBusiness();
            List<Product> productBestSale = productBus.GetBestSaleList(5);

            return PartialView(productBestSale);
        }
    }
}