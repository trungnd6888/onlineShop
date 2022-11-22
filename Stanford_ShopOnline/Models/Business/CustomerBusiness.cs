using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class CustomerBusiness
    {
        public List<Customer> GetList(string keyword)
        {
            IQueryable<Customer> customerList = DataProvider.Entities.Customers;

            if (!string.IsNullOrEmpty(keyword))
            {
                customerList = customerList.Where(c => c.Name.Contains(keyword));
            }

            return customerList.ToList();
        }

        public Customer GetById(int id)
        {
            return DataProvider.Entities.Customers.Where(c => c.Id == id).First();
        }

        public Customer GetByTel(string tel)
        {     
            return DataProvider.Entities.Customers.Where(p => p.Tel == tel).FirstOrDefault();
        }

        public bool Add(Customer objCustomer)
        {
            if (objCustomer == null) return false;

            DataProvider.Entities.Customers.Add(objCustomer);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public Customer AddReturnObject(Customer objCustomer)
        {
            if (objCustomer == null) return null;

            DataProvider.Entities.Customers.Add(objCustomer);
            DataProvider.Entities.SaveChanges();

            return objCustomer;
        }

        public bool Update(Customer objCustomer)
        {
            if (objCustomer == null) return false;

            DataProvider.Entities.Entry(objCustomer).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Customer objCustomer)
        {
            if (objCustomer == null) return false;

            DataProvider.Entities.Customers.Remove(objCustomer);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}