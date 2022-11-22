using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class OrderDetailBusiness
    {
        public List<OrderDetail> GetList( int orderId)
        {
            IQueryable<OrderDetail> orderdetailList = DataProvider.Entities.OrderDetails;
            orderdetailList = orderdetailList.Where(o => o.Order_Id == orderId);
            
            return orderdetailList.ToList();
        }

        public List<OrderDetail> GetByOrderId(int orderId)
        {
            return DataProvider.Entities.OrderDetails.Where(p => p.Order_Id == orderId).ToList();
        }

        public bool Add(OrderDetail objOrderDetail)
        {
            if (objOrderDetail == null) return false;

            DataProvider.Entities.OrderDetails.Add(objOrderDetail);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }


    }
}