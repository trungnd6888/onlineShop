using Stanford_ShopOnline.Common;
using Stanford_ShopOnline.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Models.Business
{
    public class UserBusiness
    {
        public List<User> GetList(string keyword, int roleId)
        {
            IQueryable<User> userList = DataProvider.Entities.Users;

            if (!string.IsNullOrEmpty(keyword))
            {
                userList = userList.Where(u => u.Name.Contains(keyword) || u.UserName.Contains(keyword));
            }

            if (roleId > 0)
            {
                userList = userList.Where(p => p.Role_Id == roleId);
            }

            return userList.ToList();
        }

        public User GetByUserName(string userName)
        {
            return DataProvider.Entities.Users.Where(p => p.UserName == userName).First();
        } 

        public User GetById(int id)
        {
            return DataProvider.Entities.Users.Where(u => u.Id == id).First();
        }

        public bool Add(User objUser)
        {
            if (objUser == null) return false;

            DataProvider.Entities.Users.Add(objUser);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Update(User objUser)
        {
            if (objUser == null) return false;

            DataProvider.Entities.Entry(objUser).State = System.Data.Entity.EntityState.Modified;
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Remove(User objUser)
        {
            if (objUser == null) return false;

            DataProvider.Entities.Users.Remove(objUser);
            bool result = DataProvider.Entities.SaveChanges() > 0;

            return result;
        }

        public bool Login(string userName, string password)
        {
            IQueryable<User> userList = DataProvider.Entities.Users;

            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                string md5Password = Encryptor.MD5Hash(password);

                userList =  userList.Where(p => p.UserName == userName && p.Password == md5Password);
            }

            bool result = userList.ToList().Count > 0;

            return result;
        } 
    }
}