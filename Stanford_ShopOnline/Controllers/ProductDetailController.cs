using Stanford_ShopOnline.Models;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: Product
        public ActionResult detail(int id)
        {
            ProductBusiness productBus = new ProductBusiness();
            Product objProduct = productBus.GetById(id);

            //Convert to Int32
            int categoryIdInt32 = 0;
            Int32.TryParse(objProduct.Category_Id.ToString(), out categoryIdInt32);

            CategoryBusiness categoryBus = new CategoryBusiness();
            ProductViewModel objProductViewMode =  new ProductViewModel()
            {
                Id = objProduct.Id,
                Name = objProduct.Name,
                Code = objProduct.Code,
                Detail = objProduct.Detail,
                CategoryName = categoryBus.GetById(categoryIdInt32).Name,
                Price = objProduct.Price,
                ImageUrl = objProduct.ImageUrl,
                ImageUrl1 = objProduct.ImageUrl1,
                ImageUrl2 = objProduct.ImageUrl2,
                ImageUrl3 = objProduct.ImageUrl3
            }; 

            return View(objProductViewMode);
        }

        public ActionResult ProductBestSale()
        {
            ProductBusiness productBus = new ProductBusiness();
            List<Product> productList = productBus.GetBestSaleList(5);

            return PartialView(productList);
        }
    }
}