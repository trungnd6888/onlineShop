using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class RoleBusiness
    {
        public List<Role> GetList(string keyword)
        {
            IQueryable<Role> roleList = DataProvider.Entities.Roles;

            if(!string.IsNullOrEmpty(keyword))
            {
                roleList = roleList.Where(p => p.Name.Contains(keyword));
            }

            return roleList.ToList();
        }

        public Role GetById(int id)
        {
            return DataProvider.Entities.Roles.Where(n => n.Id == id).First();
        }

        public bool Add(Role objRole)
        {
            if (objRole == null) return false;

            DataProvider.Entities.Roles.Add(objRole);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(Role objRole)
        {
            if (objRole == null) return false;

            DataProvider.Entities.Entry(objRole).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(Role objRole)
        {
            if (objRole == null) return false;

            DataProvider.Entities.Roles.Remove(objRole);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }
    }
}