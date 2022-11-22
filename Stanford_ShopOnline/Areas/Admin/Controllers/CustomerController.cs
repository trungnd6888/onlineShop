using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Product
        public ActionResult list(string keyword)
        {
            CustomerBusiness customerBus = new CustomerBusiness();
            List<Customer> customerList = customerBus.GetList(keyword);
            ViewBag.keyword = keyword;

            return View(customerList);
        }

        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Customer objCustomer)
        {
            if (ModelState.IsValid)
            {
                    CustomerBusiness customerBusiness = new CustomerBusiness();
                    bool result = customerBusiness.Add(objCustomer);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
            }

            return View(objCustomer);
        }

        public ActionResult update(int id)
        {
            //get object by id
            CustomerBusiness customerBusiness = new CustomerBusiness();
            Customer objCustomer = customerBusiness.GetById(id);

            return View(objCustomer);
        }

        [HttpPost]
        public ActionResult update(Customer objCustomer)
        {
            if (objCustomer == null)
            {
                return View();
            }

            CustomerBusiness customerBusiness = new CustomerBusiness();

            //Lấy thông tin cũ 
            Customer objCustomerOld = customerBusiness.GetById(objCustomer.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objCustomerOld != null)
                {
                    DataProvider.Entities.Entry(objCustomerOld).CurrentValues.SetValues(objCustomer);
                }

                //Gọi hàm cập nhật
                bool result = customerBusiness.Update(objCustomerOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View(objCustomer);
        }

        public ActionResult remove(int id)
        {
            CustomerBusiness customerBusiness = new CustomerBusiness();

            //Lấy thông tin cần xóa 
            Customer objCustomer = customerBusiness.GetById(id);

            if (objCustomer != null)
            {
                bool result = customerBusiness.Remove(objCustomer);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}