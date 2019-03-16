using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Web
{
    public class Helper
    {
        public static string UserData
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
        public static int RoleId
        {
            get
            {
                return Convert.ToInt32(UserData.Split('^')[1]);
            }
        }

        public static string RoleName
        {
            get
            {
                return UserData.Split('^')[2];
            }
        }
        public static int UserId
        {
            get
            {
                return Convert.ToInt32(UserData.Split('^')[0]);
            }

        }
        public static string UserNameOld
        {
            get
            {
                if (UserData != null && UserData != "")
                {
                    return UserData.Split('^')[3];
                }
                else
                {
                    return null;
                }
            }
        }

        public static string UserName
        {
            get
            {
                if (UserData != null && UserData != "")
                {
                    return UserData;
                }
                else
                {
                    return null;
                }
            }
        }
        public static string Firstname
        {
            get
            {
                return UserData.Split('^')[4];
            }
        }
        public static string LastName
        {
            get
            {
                return UserData.Split('^')[5];
            }
        }
        public static string EmailId
        {
            get
            {
                return UserData.Split('^')[6];
            }
        }
        public static string Password
        {
            get
            {
                return UserData.Split('^')[7];
            }
        }
        public static string UserAddress
        {
            get
            {

                return UserData.Split('^')[8];
            }
        }
        public static DateTime CreatedDate
        {
            get
            {
                return Convert.ToDateTime(UserData.Split('^')[9]);
            }
        }
        public static DateTime UpdatedDate
        {
            get
            {
                return Convert.ToDateTime(UserData.Split('^')[10]);
            }
        }
    }

    public enum Status
    {
        Processing = 1,
        Pending,
        Delivered,
        Cancelled
    }

}