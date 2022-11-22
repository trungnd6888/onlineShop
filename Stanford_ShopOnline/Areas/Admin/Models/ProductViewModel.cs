using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
       
        public string Code { get; set; }
     
        public string Name { get; set; }
  
        public Nullable<double> Price { get; set; }
 
        public string ImageUrl { get; set; }
 
    }
}