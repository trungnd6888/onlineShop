using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Models.Business
{
    public class ProductBusiness
    {
        public List<Product> GetList(string keyword, int categoryId, DateTime dateFrom, DateTime dateTo, 
            double priceFrom, double priceTo, bool isApproved , bool isCheckApproved)
        {
            IQueryable<Product> productList = DataProvider.Entities.Products;

            if (!string.IsNullOrEmpty(keyword)) {
                productList = productList.Where(p => p.Name.Contains(keyword) || p.Code.Contains(keyword));
            }

            if (categoryId > 0)
            {
                productList = productList.Where(p => p.Category_Id == categoryId);
            }

            if (priceTo > 0 && priceTo >= priceFrom )
            {
                productList = productList.Where(p => p.Price >= priceFrom && p.Price <= priceTo);
            }

            if (dateTo > (new DateTime()) && dateTo >= dateFrom)
            {
                productList = productList.Where(p => p.CreateDate >= dateFrom && p.CreateDate <= dateTo);
            }

            if(isCheckApproved == true)
            {
                productList = productList.Where(p => p.IsApproved == isApproved);
            }

            return productList.ToList();
        }

        public Product GetById(int id)
        {
            return DataProvider.Entities.Products.Where(p => p.Id == id).First();
        }

        public Product GetByCode(string code)
        {
            return DataProvider.Entities.Products.Where(p => p.Code == code).FirstOrDefault();
        }

        public bool Add(Product objProduct) 
        {
            if (objProduct == null) return false;

            DataProvider.Entities.Products.Add(objProduct);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;            
        }

        public bool Update(Product objProduct)
        {
            if (objProduct == null) return false;

            DataProvider.Entities.Entry(objProduct).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Product objProduct)
        {
            if (objProduct == null) return false;

            DataProvider.Entities.Products.Remove(objProduct);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public List<Product> GetNewList(int top)
        {
            return DataProvider.Entities.Products.Where(p => p.IsApproved == true && p.IsNew == true).Take(top).ToList();
        }

        public List<Product> GetBestSaleList(int top)
        {
            return DataProvider.Entities.Products.Where(p => p.IsApproved == true && p.IsBestSale == true).Take(top).ToList();
        }
    }
}