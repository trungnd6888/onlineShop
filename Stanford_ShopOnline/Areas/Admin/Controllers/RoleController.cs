using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        // GET: Role
        public ActionResult list(string keyword)
        {
            RoleBusiness roleBus = new RoleBusiness();
            List<Role> roleList = roleBus.GetList(keyword);
            ViewBag.keyword = keyword;

            return View(roleList);
        }

        public ActionResult add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult add(Role objRole)
        {
            if (ModelState.IsValid)
            {
                if (objRole != null)
                {
                    RoleBusiness roleBus = new RoleBusiness();
                    bool result = roleBus.Add(objRole);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            return View(objRole);
        }

        public ActionResult update(int id)
        {
            //get object by id
            RoleBusiness roleBus = new RoleBusiness();
            Role objRole = roleBus.GetById(id);

            return View(objRole);
        }

        [HttpPost]
        public ActionResult update(Role objRole)
        {
            if (objRole == null)
            {
                return View();
            }

            RoleBusiness roleBus = new RoleBusiness();

            //Lấy thông tin cũ 
            Role objRoleOld = roleBus.GetById(objRole.Id);

            if (ModelState.IsValid)
            {
                //Gán giá trị mới 
                if (objRoleOld != null)
                {
                    DataProvider.Entities.Entry(objRoleOld).CurrentValues.SetValues(objRole);
                }

                //Gọi hàm cập nhật
                bool result = roleBus.Update(objRoleOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View(objRole);
        }

        public ActionResult remove(int id)
        {
            RoleBusiness roleBus = new RoleBusiness();

            //Lấy thông tin văn bản cần xóa 
            Role objRole = roleBus.GetById(id);

            if (objRole != null)
            {
                bool result = roleBus.Remove(objRole);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }
    }
}