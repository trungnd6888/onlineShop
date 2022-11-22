using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stanford_ShopOnline.Session
{
    public class SessionHelper
    {
        //UserSession
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["loginSession"] = session;
        }
        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];

            if(session != null)
            {
                return (UserSession)session;
              
            }
          
            return null;
        }

        //CartSession
        public static void SetCartSession(List<CartSession> session)
        {
            HttpContext.Current.Session["cartSession"] = session;
        }

        public static List<CartSession> GetCartSession()
        {
            var session = HttpContext.Current.Session["cartSession"];

            if(session != null)
            {
                return (List<CartSession>)session;
            }

            return null;
        }
    }
}