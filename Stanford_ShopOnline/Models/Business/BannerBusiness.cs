using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class BannerBusiness
    {
        public List<Banner> GetList(string keyword)
        {
            IQueryable<Banner> bannerList = DataProvider.Entities.Banners;

            if (!string.IsNullOrEmpty(keyword))
            {
                bannerList = bannerList.Where(n => n.Title.Contains(keyword));
            }

            return bannerList.ToList();
        }

        public Banner GetById(int id)
        {
            return DataProvider.Entities.Banners.Where(n => n.Id == id).First();
        }

        public bool Add(Banner objBanner)
        {
            if (objBanner == null) return false;

            DataProvider.Entities.Banners.Add(objBanner);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(Banner objBanner)
        {
            if (objBanner == null) return false;

            DataProvider.Entities.Entry(objBanner).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Banner objBanner)
        {
            if (objBanner == null) return false;

            DataProvider.Entities.Banners.Remove(objBanner);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}