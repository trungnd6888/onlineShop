using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class DistributorController : BaseController
    {
        // GET: Product
        public ActionResult list(string keyword)
        {
            DistributorBusiness distributorBus = new DistributorBusiness();
            List<Distributor> distributorList = distributorBus.GetList(keyword);
            ViewBag.keyword = keyword;

            return View(distributorList);
        }

        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Distributor objDistributor)
        {
            if (ModelState.IsValid)
            {
                if (objDistributor != null)
                {
                    DistributorBusiness distributorBusiness = new DistributorBusiness();
                    bool result = distributorBusiness.Add(objDistributor);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            return View(objDistributor);
        }

        public ActionResult update(int id)
        {
            //get object by id
            DistributorBusiness distributorBusiness = new DistributorBusiness();
            Distributor objDistributor = distributorBusiness.GetById(id);

            return View(objDistributor);
        }

        [HttpPost]
        public ActionResult update(Distributor objDistributor)
        {
            if (objDistributor == null)
            {
                return View();
            }

            DistributorBusiness distributorBusiness = new DistributorBusiness();

            //Lấy thông tin cũ 
            Distributor objDistributorOld = distributorBusiness.GetById(objDistributor.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objDistributorOld != null)
                {
                    DataProvider.Entities.Entry(objDistributorOld).CurrentValues.SetValues(objDistributor);
                }

                //Gọi hàm cập nhật
                bool result = distributorBusiness.Update(objDistributorOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View(objDistributor);
        }

        public ActionResult remove(int id)
        {
            DistributorBusiness distributorBusiness = new DistributorBusiness();

            //Lấy thông tin văn bản cần xóa 
            Distributor objDistributor = distributorBusiness.GetById(id);

            if (objDistributor != null)
            {
                bool result = distributorBusiness.Remove(objDistributor);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}