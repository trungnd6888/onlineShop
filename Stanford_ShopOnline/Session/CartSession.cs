using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Stanford_ShopOnline.Models;

namespace Stanford_ShopOnline.Session
{
    [Serializable]
    public class CartSession
    {
        public ProductViewModel Product { get; set; }

        public Nullable<int> Quantity { get; set; }
    }
}