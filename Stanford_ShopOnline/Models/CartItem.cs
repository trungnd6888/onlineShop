using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models
{
    public class CartItem
    {
        public ProductViewModel Product { get; set; }

        public Nullable<int> Quantity { get; set; }
    }
}