using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class DistributorBusiness
    {
        public List<Distributor> GetList(string keyword)
        {
            IQueryable<Distributor> distributorList = DataProvider.Entities.Distributors;

            if(!string.IsNullOrEmpty(keyword))
            {
              distributorList =  distributorList.Where(d => d.Name.Contains(keyword));
            }

            return distributorList.ToList();
        }

        public Distributor GetById(int id)
        {
            return DataProvider.Entities.Distributors.Where(d => d.Id == id).First();
        }

        public bool Add(Distributor objDistributor)
        {
            if (objDistributor == null) return false;

            DataProvider.Entities.Distributors.Add(objDistributor);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(Distributor objDistributor)
        {
            if (objDistributor == null) return false;

            DataProvider.Entities.Entry(objDistributor).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Distributor objDistributor)
        {
            if (objDistributor == null) return false;

            DataProvider.Entities.Distributors.Remove(objDistributor);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}