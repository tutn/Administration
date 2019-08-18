using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Administration.Model.Utilities
{
    public static class IdentityHelper
    {
        //private static IUserManager repo = new UserManager();
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    return HttpContext.Current.User.Identity.Name.Split('|')[0];
                }
                else
                    throw new Exception("User not logged in.");
            }
        }

        public static string GroupName
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    return HttpContext.Current.User.Identity.Name.Split('|')[1];
                }
                else
                    throw new Exception("User not logged in.");
            }
        }

        public static string UnitName
        {
            get
            {
                if (HttpContext.Current.Request.IsAuthenticated)
                {
                    return HttpContext.Current.User.Identity.Name.Split('|')[2];
                }
                else
                    throw new Exception("User not logged in.");
            }
        }
    }
}
