using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        public ActionResult list(string categoryId)
        {
            int categoryIdInt32 = 0;
            Int32.TryParse(categoryId, out categoryIdInt32);

            CategoryBusiness categoryBus = new CategoryBusiness();
            List<Category> categoryList = categoryBus.GetList("");

            ProductBusiness productBus = new ProductBusiness();
            List<Product> productList = productBus.GetList("", categoryIdInt32, new DateTime(), new DateTime(), 0, 0, false, false);
            ViewBag.productList = productList;

            return View(categoryList);
        }


    }
}