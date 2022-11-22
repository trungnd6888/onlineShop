using Stanford_ShopOnline.Models;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using Stanford_ShopOnline.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Controllers
{
    public class CartItemController : Controller
    {
        public ActionResult list()
        {
            var cart = SessionHelper.GetCartSession();

            List<CartItem> cartList = new List<CartItem>();

            if (cart != null)
            {
                cartList = cart.Select(p => new CartItem { Product = p.Product, Quantity = p.Quantity }).ToList();
            }

            return View(cartList);
        }

        [HttpPost]
        public ActionResult addCart(int productId, int quantity) 
        {
            var cart = SessionHelper.GetCartSession();

            List<CartItem> cartList = new List<CartItem>();

            if (cart != null)
            {
                cartList = cart.Select(p => new CartItem() { Product = p.Product, Quantity = p.Quantity }).ToList();

                bool isHas = cartList.Exists(p => p.Product.Id == productId);

                if (isHas)
                {
                    foreach (var item in cartList)
                    {
                        if (item.Product.Id == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    ProductBusiness productBus = new ProductBusiness();

                    Product objProduct = productBus.GetById(productId);

                    ProductViewModel objProductViewModel = new ProductViewModel()
                    {
                        Id = objProduct.Id,
                        Name = objProduct.Name,
                        Code = objProduct.Code,
                        Price = objProduct.Price,
                        ImageUrl = objProduct.ImageUrl
                    };

                    CartItem objCartItem = new CartItem() { Product = objProductViewModel, Quantity = quantity };

                    cartList.Add(objCartItem);
                }

                SessionHelper.SetCartSession(cartList.Select(p => new CartSession() { Product = p.Product, Quantity = p.Quantity}).ToList());
            }
            else 
            {
                ProductBusiness productBus = new ProductBusiness();

                Product objProduct = productBus.GetById(productId);

                ProductViewModel objProductViewModel = new ProductViewModel()
                {
                    Id = objProduct.Id,
                    Name = objProduct.Name,
                    Code = objProduct.Code,
                    Price = objProduct.Price,
                    ImageUrl = objProduct.ImageUrl
                };

                CartItem objCartItem = new CartItem() { Product = objProductViewModel, Quantity = quantity };

                cartList.Add(objCartItem);

                SessionHelper.SetCartSession(cartList.Select(p => new CartSession() { Product = p.Product, Quantity = p.Quantity }).ToList());
            }
           
            return new EmptyResult();
        }
        
        [HttpPost]
        public ActionResult updateCart(int productId, int quantity)
        {
            var cart = SessionHelper.GetCartSession();

            if(cart != null)
            {
                List<CartItem> cartList = new List<CartItem>();

                cartList = cart.Select(p => new CartItem() { Product = p.Product, Quantity = p.Quantity }).ToList();

                foreach(var item in cartList)
                {
                    if (item.Product.Id == productId)
                    {
                        if(quantity == 0)
                        {
                            cartList.Remove(item);
                            break;
                        }

                        item.Quantity = quantity;
                    }
                }

                SessionHelper.SetCartSession(cartList.Select(p => new CartSession() { Product = p.Product, Quantity = p.Quantity }).ToList());
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult removeCart(int productId) 
        {
            var cart = SessionHelper.GetCartSession();

            if (cart != null)
            {
                List<CartItem> cartList = new List<CartItem>();

                cartList = cart.Select(p => new CartItem() { Product = p.Product, Quantity = p.Quantity }).ToList();

                foreach(var item in cartList)
                {
                    if(item.Product.Id == productId)
                    {
                        cartList.Remove(item);
                        break;
                    }
                }

                SessionHelper.SetCartSession(cartList.Select(p => new CartSession() { Product = p.Product, Quantity = p.Quantity }).ToList());
            }

            return new EmptyResult();
        }

        public JsonResult loadData()
        {
            var cart = SessionHelper.GetCartSession();

            List<CartItem> cartList = new List<CartItem>();

            if(cart != null)
            {
                cartList = cart.Select(p => new CartItem() { Product = p.Product, Quantity = p.Quantity }).ToList();
            }

            return Json(cartList, JsonRequestBehavior.AllowGet);
        }
    }
}