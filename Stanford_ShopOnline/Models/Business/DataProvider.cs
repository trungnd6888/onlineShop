using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    /// <summary>
    /// Khai báo 1 đối tượng để làm việc vói db bằng Entity Framework
    /// </summary>
    public class DataProvider
    {
        private static ShopOnlineEntities shopOnlineEntities = null;

        public static ShopOnlineEntities Entities 
        {
            get
            {
                if(shopOnlineEntities == null)
                {
                    shopOnlineEntities = new ShopOnlineEntities();
                }

                return shopOnlineEntities;
            }
        }
    }
}