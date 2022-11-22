using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class CategoryBusiness
    {
        public List<Category> GetList(string keyword)
        {
            IQueryable<Category> categoryList = DataProvider.Entities.Categories;
            
            if(!string.IsNullOrEmpty(keyword))
            {
                categoryList = categoryList.Where(p => p.Name.Contains(keyword));
            }

            return categoryList.ToList();
        }

        public Category GetById(int id)
        {
            return DataProvider.Entities.Categories.Where(n => n.Id == id).First();
        }

        public bool Add(Category objCategory)
        {
            if (objCategory == null) return false;

            DataProvider.Entities.Categories.Add(objCategory);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(Category objCategory)
        {
            if (objCategory == null) return false;

            DataProvider.Entities.Entry(objCategory).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Category objCategory)
        {
            if (objCategory == null) return false;

            DataProvider.Entities.Categories.Remove(objCategory);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}