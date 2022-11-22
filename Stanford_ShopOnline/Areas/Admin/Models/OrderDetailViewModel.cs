using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Areas.Admin.Models
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public Nullable<int> Order_Id { get; set; }
        public Nullable<int> Product_Id { get; set; }
        public string ProductCode { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string ProductName { get; set; }
        public Nullable<double> ProductPrice { get; set; }
        public Nullable<double> DetailTotal { get; set; }
    }
}