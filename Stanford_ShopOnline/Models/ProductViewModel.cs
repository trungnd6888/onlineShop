using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        public string Detail { get; set; }

        public Nullable<double> Price { get; set; }

        public string ImageUrl { get; set; }
        public string ImageUrl1 { get; set; }
        public string ImageUrl2 { get; set; }
        public string ImageUrl3 { get; set; }

        public string CategoryName { get; set; }
    }
}