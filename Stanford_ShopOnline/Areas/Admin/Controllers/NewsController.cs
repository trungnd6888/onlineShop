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
    public class NewsController : BaseController
    {
        // GET: Product
        public ActionResult list(string keyword, string User_Id)
        {
            //convert String to Int32
            int userIdInt32 = 0;
            Int32.TryParse(User_Id, out userIdInt32);

            NewsBusiness newsBus = new NewsBusiness();
            List<News> newsList = newsBus.GetList(keyword, userIdInt32);
            ViewBag.keyword = keyword;

            DisplayUserList(userIdInt32);

            return View(newsList);
        }

        private void DisplayUserList(int userId)
        {
            UserBusiness userBus = new UserBusiness();
            List<User> userList = userBus.GetList("", -1);

            if (userId > 0)
            {
                ViewBag.userList = new SelectList(userList, "Id", "Name", userId);
            }
            else
            {
                ViewBag.userList = new SelectList(userList, "Id", "Name");
            }
        }

        public ActionResult add()
        {
            DisplayUserList(-1);
            return View();
        }

        [HttpPost]
        public ActionResult add(News objNews, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (objNews != null)
                {
                    //file upload
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(fileUpload.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                        fileUpload.SaveAs(_path);

                        objNews.ImageUrl = Path.Combine("/Content/admin/images", _FileName); ;
                    }

                    objNews.CreateDate = DateTime.Now;
                    objNews.UpdateDate = DateTime.Now;
                    objNews.ApprovedDate = DateTime.Now;
                    objNews.User_Id = 1;
                    objNews.Approved_Id = 1;

                    NewsBusiness newsBusiness = new NewsBusiness();
                    bool result = newsBusiness.Add(objNews);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            DisplayUserList(-1);
            return View(objNews);
        }

        public ActionResult update(int id)
        {
            //get object by id
            NewsBusiness newsBusiness = new NewsBusiness();
            News objNews = newsBusiness.GetById(id);

            int userId = Convert.ToInt32(objNews.User_Id);
            DisplayUserList(userId);

            return View(objNews);
        }

        [HttpPost]
        public ActionResult update(News objNews, HttpPostedFileBase fileUpload)
        {
            if (objNews == null)
            {
                return View();
            }

            //Xử lý upload file, nếu không có file upload thì giữ lại file cũ đã lưu trong DB
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string _FileName = fileUpload.FileName;
                string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                fileUpload.SaveAs(_path);

                objNews.ImageUrl = Path.Combine("/Content/admin/images", _FileName);
            }

            NewsBusiness newsBusiness = new NewsBusiness();

            //Lấy thông tin cũ 
            News objNewsOld = newsBusiness.GetById(objNews.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objNewsOld != null)
                {
                    DataProvider.Entities.Entry(objNewsOld).CurrentValues.SetValues(objNews);
                }

                //Gọi hàm cập nhật
                bool result = newsBusiness.Update(objNewsOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            int userId = Convert.ToInt32(objNews.User_Id);
            DisplayUserList(userId);

            return View(objNews);
        }

        public ActionResult remove(int id)
        {
            NewsBusiness newsBusiness = new NewsBusiness();

            //Lấy thông tin văn bản cần xóa 
            News objNews = newsBusiness.GetById(id);

            if (objNews != null)
            {
                bool result = newsBusiness.Remove(objNews);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }

        [HttpPost]
        public bool updateIsApproved(int newsId, bool isApproved)
        {
            NewsBusiness newsBus = new NewsBusiness();

            News objNews = newsBus.GetById(newsId);

            objNews.IsApproved = isApproved;

            bool result = newsBus.Update(objNews);

            return result;
        }
    }
}