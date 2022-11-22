using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Stanford_ShopOnline.Models.Business
{
    public class NewsBusiness
    {
        public List<News> GetList(string keyword, int userId)
        {
            IQueryable<News> newsList = DataProvider.Entities.News;

            if (!string.IsNullOrEmpty(keyword))
            {
                newsList = newsList.Where(n => n.Title.Contains(keyword) || n.Summary.Contains(keyword));
            }

            if (userId > 0)
            {
                newsList = newsList.Where(n => n.User_Id == userId);
            }

            return newsList.ToList();
        }

        public News GetById(int id)
        {
            return DataProvider.Entities.News.Where(n => n.Id == id).First();
        }

        public bool Add(News objNews)
        {
            if (objNews == null) return false;

            DataProvider.Entities.News.Add(objNews);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(News objNews)
        {
            if (objNews == null) return false;

            DataProvider.Entities.Entry(objNews).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(News objNews)
        {
            if (objNews == null) return false;

            DataProvider.Entities.News.Remove(objNews);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}