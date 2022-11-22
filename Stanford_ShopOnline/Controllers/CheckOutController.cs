using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopOnline.Models;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using Stanford_ShopOnline.Session;

namespace Stanford_ShopOnline.Controllers
{
    public class CheckOutController : Controller
    {
        public ActionResult checkOut()
        {
            getCartList();

            return View();
        }

        [HttpPost]
        public ActionResult checkOut(CustomerViewModel customerViewModel)
        {
            getCartList();

            if (ModelState.IsValid)
            {
                /*Add Customer*/
                Customer objCustomer = null;
                CustomerBusiness customerBus = new CustomerBusiness();

                if (customerBus.GetByTel(customerViewModel.Tel) != null)
                {
                    objCustomer = customerBus.GetByTel(customerViewModel.Tel);
                }
                else 
                {
                    objCustomer = new Customer()
                    {
                        Name = customerViewModel.Name,
                        Address = customerViewModel.Address,
                        Tel = customerViewModel.Tel,
                        Email = customerViewModel.Email,
                    };

                    objCustomer = customerBus.AddReturnObject(objCustomer);
                }

                /*Add Order*/
                OrderBusiness orderBus = new OrderBusiness();
                Order objOrder = new Order()
                {
                    Customer_Id = objCustomer.Id,
                    Date = DateTime.Now,
                    Status = 1
                };

                objOrder = orderBus.AddReturnObject(objOrder);

                /*Add OrderDetail*/
                OrderDetailBusiness orderDetailBus = new OrderDetailBusiness();

                //get Session
                List<CartItem> cartList = SessionHelper.GetCartSession().Select(p => new CartItem() { 
                    Product = p.Product,
                    Quantity = p.Quantity,
                }).ToList();

                foreach(CartItem cartItem in cartList)
                {
                    OrderDetail objOrderDetail = new OrderDetail() { 
                        Order_Id = objOrder.Id,
                        Product_Id = cartItem.Product.Id,
                        Quantity = cartItem.Quantity
                    };

                    orderDetailBus.Add(objOrderDetail);
                }

                //Set Session is null
                SessionHelper.SetCartSession(null);

                return RedirectToAction("index", "Home");
            }

            return View(customerViewModel);
        }

        public void getCartList()
        {
            var cart = SessionHelper.GetCartSession();

            List<CartItem> cartList = new List<CartItem>();

            if (cart != null)
            {
                cartList = cart.Select(p => new CartItem { Product = p.Product, Quantity = p.Quantity }).ToList();
            }

            ViewBag.cartList = cartList;
        }
    }
}