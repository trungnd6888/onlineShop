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
    public class OrderController : BaseController
    {
        // GET: Product
        public ActionResult list(string dateFrom, string dateTo, string Customer_Id)
        {
            //convert String to Int32
            int customerIdInt32 = 0;
            Int32.TryParse(Customer_Id, out customerIdInt32);

            DateTime dateFromDateTime = new DateTime();
            DateTime.TryParse(dateFrom, out dateFromDateTime);

            DateTime dateToDateTime = new DateTime();
            DateTime.TryParse(dateTo, out dateToDateTime);

            //Sử dụng lớp giả OrderViewModel thay cho lớp Order để hiển thị 
            OrderBusiness orderBus = new OrderBusiness();
            List<Order> orderList = orderBus.GetList(dateFromDateTime, dateToDateTime, customerIdInt32);

            CustomerBusiness customerBus = new CustomerBusiness();
            List<Customer> customerList = customerBus.GetList("");

            IEnumerable<OrderViewModel> orderViewModelList =
                from order in orderList
                join customer in customerList
                on order.Customer_Id equals customer.Id
                select new OrderViewModel()
                {
                    Id = order.Id,
                    CustomerName = customer.Name,
                    TotalMoney = getTotalMoneyOrderId(order.Id),
                    Date = order.Date,
                    Status = order.Status,
                };

            ViewBag.dateFrom = dateFrom;
            ViewBag.dateTo = dateTo;

            DisplayCustomerList(customerIdInt32);

            return View(orderViewModelList.ToList());
        }

        public double getTotalMoneyOrderId(int orderId)
        {
            OrderDetailBusiness orderDetailBus = new OrderDetailBusiness();

            List<OrderDetail> orderDetailList = orderDetailBus.GetByOrderId(orderId);

            double total = 0;
          
            foreach (var item in orderDetailList)
            {
                double price = (double)item.Product.Price;
                int quantity = (int)item.Quantity;
                total += price * quantity;
            }

            return total;
        }

        private void DisplayCustomerList(int customerId)
        {
            CustomerBusiness customerBus = new CustomerBusiness();
            List<Customer> customerList = customerBus.GetList("");

            if (customerId > 0)
            {
                ViewBag.customerList = new SelectList(customerList, "Id", "Name", customerId);
            }
            else
            {
                ViewBag.customerList = new SelectList(customerList, "Id", "Name");
            }
        }

        public ActionResult add()
        {
            DisplayCustomerList(-1);
            return View();
        }

        [HttpPost]
        public ActionResult add(Order objOrder)
        {
            if (ModelState.IsValid)
            {
                OrderBusiness orderBusiness = new OrderBusiness();
                bool result = orderBusiness.Add(objOrder);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            DisplayCustomerList(-1);
            return View(objOrder);
        }

        public ActionResult update(int id)
        {
            //get object by id
            OrderBusiness orderBusiness = new OrderBusiness();
            Order objOrder = orderBusiness.GetById(id);

            int customerId = Convert.ToInt32(objOrder.Customer_Id);
            DisplayCustomerList(customerId);

            return View(objOrder);
        }

        [HttpPost]
        public ActionResult update(Order objOrder)
        {
            if (objOrder == null)
            {
                return View();
            }
           
            OrderBusiness orderBusiness = new OrderBusiness();

            //Lấy thông tin cũ 
            Order objOrderOld = orderBusiness.GetById(objOrder.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objOrderOld != null)
                {
                    DataProvider.Entities.Entry(objOrderOld).CurrentValues.SetValues(objOrder);
                }

                //Gọi hàm cập nhật
                bool result = orderBusiness.Update(objOrderOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            int customerId = Convert.ToInt32(objOrder.Customer_Id);
            DisplayCustomerList(customerId);

            return View(objOrder);
        }

        public ActionResult remove(int id)
        {
            OrderBusiness orderBusiness = new OrderBusiness();

            //Lấy thông tin văn bản cần xóa 
            Order objOrder = orderBusiness.GetById(id);

            if (objOrder != null)
            {
                bool result = orderBusiness.Remove(objOrder);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}