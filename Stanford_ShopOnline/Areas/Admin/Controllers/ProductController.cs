using Stanford_ShopOnline.Areas.Admin.Models;
using Stanford_ShopOnline.Models.Business;
using Stanford_ShopOnline.Models.EF;
using Stanford_ShopOnline.Session;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult list(string keyword, string Category_Id, string createDateFrom, 
            string createDateTo, string priceFrom, string priceTo, string isApproved)
        {
            //convert String to Int32
            int categoryIdInt32 = 0;
            Int32.TryParse(Category_Id, out categoryIdInt32);

            double priceFromDouble = 0;
            Double.TryParse(createDateFrom, out priceFromDouble);

            double priceToDouble = 0;
            Double.TryParse(priceTo, out priceToDouble);

            DateTime createDateFromDateTime ;
            DateTime.TryParse(createDateFrom, out createDateFromDateTime);

            DateTime createDateToDateTime;
            DateTime.TryParse(createDateTo, out createDateToDateTime);

            bool  isCheckApproved = true;

            if (string.IsNullOrEmpty(isApproved))
            {
                isCheckApproved = false;
            }

            bool isApprovedBool = false;
            Boolean.TryParse(isApproved, out isApprovedBool);

            ProductBusiness productBus = new ProductBusiness();
            List<Product> productList = productBus.GetList(keyword, categoryIdInt32, createDateFromDateTime,
            createDateToDateTime, priceFromDouble, priceToDouble, isApprovedBool, isCheckApproved);

            ViewBag.keyword = keyword;
            ViewBag.createDateFrom = createDateFrom;
            ViewBag.createDateTo = createDateTo;
            ViewBag.priceFrom = priceFrom;
            ViewBag.priceTo = priceTo;
            ViewBag.isApproved = isApproved;

            DisplayCategoryList(categoryIdInt32);
            DisplayIsApprovedList();

            return View(productList);
        }

        public JsonResult jsonList(string keyword)
        {
            ProductBusiness productBus = new ProductBusiness();
            List<Product> productList = productBus.GetList(keyword, 0, new DateTime(), new DateTime(), 0, 0, true, false);

            IEnumerable<ProductViewModel> productViewModelList = productList.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
            });

            JsonResult jsonResult = Json(productViewModelList.ToList(), JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        private void DisplayCategoryList(int categoryId)
        {
            CategoryBusiness categoryBus = new CategoryBusiness();
            List<Category> categoryList = categoryBus.GetList("");

            if (categoryId > 0)
            {
                ViewBag.categoryList = new SelectList(categoryList, "Id", "Name", categoryId);
            }
            else
            {
                ViewBag.categoryList = new SelectList(categoryList, "Id", "Name");
            }
        }

        private void DisplayIsApprovedList()
        {
            List<SelectListItem> isApprovedList = new List<SelectListItem>();

            isApprovedList.Add(new SelectListItem()
            {
                Text = "Chưa duyệt",
                Value = "false",
                Selected = false
            });

            isApprovedList.Add(new SelectListItem() { 
                Text = "Đã duyệt",
                Value = "true",
                Selected = false
            });

            ViewBag.isApprovedList = new SelectList(isApprovedList, "Value", "Text");
        }

        private void DisplayDistributorList(int distributorId)
        {
            DistributorBusiness distributorBus = new DistributorBusiness();
            List<Distributor> distributorList = distributorBus.GetList("");

            if (distributorId > 0)
            {
                ViewBag.distributorList = new SelectList(distributorList, "Id", "Name", distributorId);
            }
            else
            {
                ViewBag.distributorList = new SelectList(distributorList, "Id", "Name");
            }
        }

        public ActionResult add() 
        {
            DisplayCategoryList(-1);
            DisplayDistributorList(-1);
            return View();
        }

        public void handleFileUpload(Product objProduct ,HttpPostedFileBase fileUpload, string propName)
        {
            //file upload
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                string _FileName = Path.GetFileName(fileUpload.FileName);
                string _path = Path.Combine(Server.MapPath("~/Content/admin/images"), _FileName);
                fileUpload.SaveAs(_path);

                objProduct.GetType().GetProperty(propName).SetValue(objProduct, Path.Combine("/Content/admin/images", _FileName));
            }
        }

        [HttpPost]
        public ActionResult add(Product objProduct, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            if (isDuplicateCode(objProduct.Code)){
                ModelState.AddModelError("Code", "Mã sản phẩm đã tồn tại");
            } 

            if (ModelState.IsValid)
            {
               
                if (objProduct != null)
                {
                    //file upload
                    handleFileUpload(objProduct, fileUpload, "ImageUrl");
                    handleFileUpload(objProduct, fileUpload1, "ImageUrl1");
                    handleFileUpload(objProduct, fileUpload2, "ImageUrl2");
                    handleFileUpload(objProduct, fileUpload3, "ImageUrl3");

                    //set default value
                    objProduct.CreateDate = DateTime.Now;
                    objProduct.UpdateDate = DateTime.Now;
                    objProduct.ApprovedDate = DateTime.Now;
                    objProduct.ViewCount = 0;
                    
                    objProduct.User_Id = 35;
                    objProduct.Approved_Id = 1;

                    ProductBusiness productBusiness = new ProductBusiness();
                    bool result = productBusiness.Add(objProduct);

                    if (result)
                    {
                        return RedirectToAction("list");
                    }
                }
            }

            DisplayCategoryList(-1);
            DisplayDistributorList(-1);
            return View(objProduct);
        }

        public ActionResult update(int id)
        {
            //get object by id
            ProductBusiness productBusiness = new ProductBusiness();
            Product objProduct = productBusiness.GetById(id);

            int categoryId = Convert.ToInt32(objProduct.Category_Id);
            DisplayCategoryList(categoryId);

            int distributorId = Convert.ToInt32(objProduct.Distributor_Id);
            DisplayDistributorList(distributorId);

            return View(objProduct);  
        }

        [HttpPost]
        public ActionResult update(Product objProduct, HttpPostedFileBase fileUpload, HttpPostedFileBase fileUpload1, HttpPostedFileBase fileUpload2, HttpPostedFileBase fileUpload3)
        {
            if (objProduct == null)
            {
                return View();
            }

            //Xử lý upload file, nếu không có file upload thì giữ lại file cũ đã lưu trong DB
            handleFileUpload(objProduct, fileUpload, "ImageUrl");
            handleFileUpload(objProduct, fileUpload1, "ImageUrl1");
            handleFileUpload(objProduct, fileUpload2, "ImageUrl2");
            handleFileUpload(objProduct, fileUpload3, "ImageUrl3");

            ProductBusiness productBusiness = new ProductBusiness();

            //Lấy thông tin cũ 
            Product objProductOld = productBusiness.GetById(objProduct.Id);

            if (isDuplicateCode(objProduct.Code))
            {
                ModelState.AddModelError("Code", "Mã sản phẩm đã tồn tại");
            }

            if (ModelState.IsValid)
            {

                //Gán giá trị mới 
                if (objProductOld != null)
                {
                    DataProvider.Entities.Entry(objProductOld).CurrentValues.SetValues(objProduct);
                }

                //Gọi hàm cập nhật
                bool result = productBusiness.Update(objProductOld);
                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            int categoryId = Convert.ToInt32(objProduct.Category_Id);
            DisplayCategoryList(categoryId);

            int distributorId = Convert.ToInt32(objProduct.Distributor_Id);
            DisplayDistributorList(distributorId);

            DisplayCategoryList(categoryId);
            DisplayDistributorList(distributorId);

            return View(objProduct);
        }

        [HttpPost]
        public bool updateIsApproved(int productId, bool isApproved)
        {
            ProductBusiness productBus = new ProductBusiness();

            Product objProduct = productBus.GetById(productId);

            objProduct.IsApproved = isApproved;

            bool result = productBus.Update(objProduct);

            return result;
        }

        public ActionResult remove(int id)
        {
            ProductBusiness productBusiness = new ProductBusiness();

            //Lấy thông tin văn bản cần xóa 
            Product objProduct = productBusiness.GetById(id);

            if (objProduct != null)
            {
                bool result = productBusiness.Remove(objProduct);

                if (result)
                {
                    return RedirectToAction("list");
                }
            }

            return View();
        }

        public bool isDuplicateCode(string code)
        {
            ProductBusiness productBus = new ProductBusiness();

            Product objProduct = productBus.GetByCode(code);

            return objProduct != null;  
        }
    }
}