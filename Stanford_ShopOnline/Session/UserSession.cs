using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Session
{
    [Serializable]
    public class UserSession
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
    }
}