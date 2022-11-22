using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class MenuBusiness
    {
        public List<Menu> GetList()
        {
            return DataProvider.Entities.Menus.Where(p => p.IsApproved == true).OrderBy(p => p.Order).ToList();
        }
    }
}