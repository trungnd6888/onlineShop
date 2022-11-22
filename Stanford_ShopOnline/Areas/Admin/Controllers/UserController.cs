using Stanford_ShopOnline.Common;
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
    public class UserController : BaseController
    {
        // GET: Product
        public ActionResult list(string keyword, string Role_Id)
        {
            //convert String to Int32
            int roleIdInt32 = 0;
            Int32.TryParse(Role_Id, out roleIdInt32);

            UserBusiness userBus = new UserBusiness();
            List<User> userList = userBus.GetList(keyword, roleIdInt32);
            ViewBag.keyword = keyword;

            DisplayRoleList(roleIdInt32);

            return View(userList);
        }

        private void DisplayRoleList(int roleId)
        {
            RoleBusiness roleBus = new RoleBusiness();
            List<Role> roleList = roleBus.GetList("");

            if (roleId > 0)
            {
                ViewBag.roleList = new SelectList(roleList, "Id", "Name", roleId);
            }
            else
            {
                ViewBag.roleList = new SelectList(roleList, "Id", "Name");
            }
        }

        public ActionResult add()
        {
            DisplayRoleList(-1);
          
            return View();
        }

        [HttpPost]
        public ActionResult add(User objUser, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                if (objUser != null)
                {
                    //file upload
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(fileUpload.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                        fileUpload.SaveAs(_path);

                        objUser.ImageUrl = Path.Combine("/Content/admin/images", _FileName);
                    }

                    objUser.CreateDate = DateTime.Now;
                    objUser.Password = Encryptor.MD5Hash(objUser.Password);
                    objUser.PasswordConfirm = Encryptor.MD5Hash(objUser.PasswordConfirm);

                    UserBusiness userBusiness = new UserBusiness();
                    bool result = userBusiness.Add(objUser);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            DisplayRoleList(-1);
         
            return View(objUser);
        }

        public ActionResult update(int id)
        {
            //get object by id
            UserBusiness userBusiness = new UserBusiness();
            User objUser = userBusiness.GetById(id);

            int roleId = Convert.ToInt32(objUser.Role_Id);
            DisplayRoleList(roleId);

            return View(objUser);
        }

        [HttpPost]
        public ActionResult update(User objUser, HttpPostedFileBase fileUpload)
        {
            if (objUser == null)
            {
                return View();
            }

            //Xử lý upload file, nếu không có file upload thì giữ lại file cũ đã lưu trong DB
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string _FileName = fileUpload.FileName;
                string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                fileUpload.SaveAs(_path);

                objUser.ImageUrl = Path.Combine("/Content/admin/images", _FileName);
            }

            UserBusiness userBusiness = new UserBusiness();

            //Lấy thông tin cũ 
            User objUserOld = userBusiness.GetById(objUser.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objUserOld != null)
                {
                    DataProvider.Entities.Entry(objUserOld).CurrentValues.SetValues(objUser);
                }

                //Gọi hàm cập nhật
                bool result = userBusiness.Update(objUserOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            int roleId = Convert.ToInt32(objUser.Role_Id);
            DisplayRoleList(roleId);

            return View(objUser);
        }

        public ActionResult remove(int id)
        {
            UserBusiness userBusiness = new UserBusiness();

            //Lấy thông tin văn bản cần xóa 
            User objUser = userBusiness.GetById(id);

            if (objUser != null)
            {
                bool result = userBusiness.Remove(objUser);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}