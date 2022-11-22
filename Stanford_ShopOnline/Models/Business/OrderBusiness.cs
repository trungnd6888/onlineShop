using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class OrderBusiness
    {
        public List<Order> GetList(DateTime dateFrom, DateTime dateTo, int customerId)
        {
            IQueryable<Order> orderList = DataProvider.Entities.Orders;

            if(dateTo > (new DateTime()) && dateTo >= dateFrom) 
            {
                orderList = orderList.Where(o => o.Date >= dateFrom && o.Date <= dateTo);
            }
               
            if (customerId > 0)
            {
                orderList = orderList.Where(o =>o.Customer_Id == customerId);
            }

            return orderList.ToList();
        }

        public Order GetById(int id)
        {
            return DataProvider.Entities.Orders.Where(o => o.Id == id).First();
        }

        public bool Add(Order objOrder)
        {
            if (objOrder == null) return false;

            DataProvider.Entities.Orders.Add(objOrder);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public Order AddReturnObject(Order objOrder)
        {
            if (objOrder == null) return null;

            DataProvider.Entities.Orders.Add(objOrder);
            DataProvider.Entities.SaveChanges();

            return objOrder;
        }

        public bool Update(Order objOrder)
        {
            if (objOrder == null) return false;

            DataProvider.Entities.Entry(objOrder).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Order objOrder)
        {
            if (objOrder == null) return false;

            DataProvider.Entities.Orders.Remove(objOrder);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}