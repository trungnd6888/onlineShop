using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class BannerController: BaseController
    {
        public ActionResult list(string keyword)
        {
            BannerBusiness bannerBus = new BannerBusiness();
            List<Banner> bannerList = bannerBus.GetList(keyword);
            ViewBag.keyword = keyword;

            return View(bannerList);
        }

        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Banner objBanner, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (objBanner != null)
                {
                    //file upload
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(fileUpload.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                        fileUpload.SaveAs(_path);

                        objBanner.ImageUrl = Path.Combine("/Content/admin/images", _FileName); ;
                    }

                    objBanner.IsApproved = false;

                    BannerBusiness bannerBus = new BannerBusiness();
                    bool result = bannerBus.Add(objBanner);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            return View(objBanner);
        }

        public ActionResult update(int id)
        {
            //get object by id
            BannerBusiness bannerBus = new BannerBusiness();
            Banner objBanner = bannerBus.GetById(id);
            
            return View(objBanner);
        }

        [HttpPost]
        public ActionResult update(Banner objBanner, HttpPostedFileBase fileUpload)
        {
            if (objBanner == null)
            {
                return View();
            }

            //Xử lý upload file, nếu không có file upload thì giữ lại file cũ đã lưu trong DB
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string _FileName = fileUpload.FileName;
                string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                fileUpload.SaveAs(_path);

                objBanner.ImageUrl = Path.Combine("/Content/admin/images", _FileName);
            }

            BannerBusiness bannerBus = new BannerBusiness();

            //Lấy thông tin cũ 
            Banner objBannerOld = bannerBus.GetById(objBanner.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objBannerOld != null)
                {
                    DataProvider.Entities.Entry(objBannerOld).CurrentValues.SetValues(objBanner);
                }

                //Gọi hàm cập nhật
                bool result = bannerBus.Update(objBannerOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View(objBanner);
        }

        public ActionResult remove(int id)
        {
            BannerBusiness bannerBus = new BannerBusiness();

            //Lấy thông tin đối tượng cần xóa 
            Banner objBanner = bannerBus.GetById(id);

            if (objBanner != null)
            {
                bool result = bannerBus.Remove(objBanner);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}