using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult list(string keyword)
        {
            CategoryBusiness categoryBus = new CategoryBusiness();
            List<Category> categoryList = categoryBus.GetList(keyword);
            ViewBag.keyword = keyword;

            return View(categoryList);
        }

        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Category objCategory, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (objCategory != null)
                {
                    //file upload
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(fileUpload.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                        fileUpload.SaveAs(_path);

                        objCategory.ImageUrl = Path.Combine("/Content/admin/images", _FileName); ;
                    }

                    CategoryBusiness categoryBus = new CategoryBusiness();
                    bool result = categoryBus.Add(objCategory);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            return View(objCategory);
        }

        public ActionResult update(int id)
        {
            //get object by id
            CategoryBusiness categoryBus = new CategoryBusiness();
            Category objCategory = categoryBus.GetById(id);
        
            return View(objCategory);
        }

        [HttpPost]
        public ActionResult update(Category objCategory, HttpPostedFileBase fileUpload)
        {
            if (objCategory == null)
            {
                return View();
            }

            //Xử lý upload file, nếu không có file upload thì giữ lại file cũ đã lưu trong DB
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string _FileName = fileUpload.FileName;
                string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                fileUpload.SaveAs(_path);

                objCategory.ImageUrl = Path.Combine("/Content/admin/images", _FileName);
            }

            CategoryBusiness categoryBus = new CategoryBusiness();

            //Lấy thông tin cũ 
            Category objCategoryOld = categoryBus.GetById(objCategory.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objCategoryOld != null)
                {
                    DataProvider.Entities.Entry(objCategoryOld).CurrentValues.SetValues(objCategory);
                }

                //Gọi hàm cập nhật
                bool result = categoryBus.Update(objCategoryOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View(objCategory);
        }

        public ActionResult remove(int id)
        {
            CategoryBusiness categoryBus = new CategoryBusiness();

            //Lấy thông tin văn bản cần xóa 
            Category objCategory = categoryBus.GetById(id);

            if (objCategory != null)
            {
                bool result = categoryBus.Remove(objCategory);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}