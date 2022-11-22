using Stanford_ShopOnline.Areas.Admin.Models;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class OrderDetailController : BaseController
    {
        // GET: OrderDetail
        public JsonResult jsonList(int orderId)
        {
            OrderDetailBusiness orderDetailBus = new OrderDetailBusiness();
            List<OrderDetail> orderDetailList =  orderDetailBus.GetList(orderId);

            OrderBusiness orderBus = new OrderBusiness();
            List<Order> orderList = orderBus.GetList(new DateTime(), new DateTime(), 0);

            ProductBusiness productBus = new ProductBusiness();
            List<Product> productList = productBus.GetList("", 0, new DateTime(), new DateTime(), 0, 0, true, false);

            //Sử dụng lớp OrderDetailViewModel để trả về kết quả khi dùng ajax
            IEnumerable<OrderDetailViewModel> orderDetailViewModelList =
                from product in productList
                join orderDetail in orderDetailList 
                on product.Id  equals orderDetail.Product_Id
                select new OrderDetailViewModel()
                {
                Id = orderDetail.Id,
                Order_Id = orderDetail.Order_Id,
                Product_Id = orderDetail.Product_Id,
                Quantity = orderDetail.Quantity,
                ProductCode = product.Code,
                ProductName = product.Name,
                ProductPrice = product.Price,
                DetailTotal = product.Price * orderDetail.Quantity
                };

            JsonResult jsonResult = Json(orderDetailViewModelList.ToList(), JsonRequestBehavior.AllowGet);
            
            return jsonResult;
        }
    }
}