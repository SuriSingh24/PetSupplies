using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using System.Web.Security;
//using PetSuppliesPlus.Data;
using System.Threading;
using System.Globalization;
using PetSuppliesPlus.Data;

namespace PetSuppliesPlus.Framework
{
    public static class SessionHelper
    {


        /// <summary>
        /// to get User id from session
        /// </summary>
        public static long UserId
        {
            get
            {
                return HttpContext.Current.Session["UserId"] != null ? Convert.ToInt64(HttpContext.Current.Session["UserId"]) : 0;
            }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        /// <summary>
        /// to check is user login or not
        /// </summary>
        public static bool IsUserLogin
        {
            get
            {
                Initialize();
                return UserId > 0;
            }
        }

        /// <summary>
        /// to get User name from session
        /// </summary>
        public static string UserName
        {
            get
            {
                return HttpContext.Current.Session["UserName"] != null ? Convert.ToString(HttpContext.Current.Session["UserName"]) : "";
            }
            set { HttpContext.Current.Session["UserName"] = value; }
        }

        public static bool IsAdmin
        {
            get
            {
                return HttpContext.Current.Session["IsAdmin"] != null ? Convert.ToBoolean(HttpContext.Current.Session["IsAdmin"]) : false;
            }
            set { HttpContext.Current.Session["IsAdmin"] = value; }
        }

        public static string LockOutDate
        {
            get
            {
                return HttpContext.Current.Session["LockOutDate"] != null ? Convert.ToString(HttpContext.Current.Session["LockOutDate"]) : utilityHelper.CurrentDateTime.ToString();
            }
            set { HttpContext.Current.Session["LockOutDate"] = value; }
        }

        public static bool ReportCount
        {
            get
            {
                return HttpContext.Current.Session["ReportCount"] != null ? Convert.ToBoolean(HttpContext.Current.Session["ReportCount"]) : false;
            }
            set { HttpContext.Current.Session["ReportCount"] = value; }
        }

        /// <summary>
        /// to Get or Set user login detail from/into cookie
        /// </summary>
        public static string UserCookie
        {
            get
            {
                return HttpContext.Current.Request.Cookies["fuid"] != null ? HttpContext.Current.Request.Cookies["fuid"].Value.ToString() : "0".ToEnctypt();
            }
            set
            {
                HttpCookie uc = new HttpCookie("fuid", value);
                uc.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(uc);
            }
        }

        public static void Dispose()
        {
            HttpContext.Current.Session.Abandon();
            var fuid = HttpContext.Current.Request.Cookies["fuid"];
            if (fuid != null)
            {
                fuid.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(fuid);
            }
            var fauid = HttpContext.Current.Request.Cookies["fauid"];
            if (fauid != null)
            {
                fauid.Expires = DateTime.Now.AddDays(-2);
                HttpContext.Current.Response.Cookies.Add(fauid);
            }
        }

        /// <summary>
        /// Get/Set the culture based on culture code and get/set the 
        /// value in/from cookie
        /// </summary>
        /// <param name="cultureCode"></param>
        public static string Culture
        {
            get
            {
                string culture = HttpContext.Current.Request.Cookies["UserLanguage"] != null ? HttpContext.Current.Request.Cookies["UserLanguage"].Value.ToString() : "en";

                //Ui Culture for Localized text in the UI
                Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(culture.ToLower());// + "-" + culture.ToUpper());

                //Sets  Culture for Current thread
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

                //Thread.CurrentThread.CurrentCulture =
                //System.Globalization.CultureInfo.CreateSpecificCulture("en");

                return culture;
            }
            set
            {
                HttpCookie uc = new HttpCookie("UserLanguage", value);
                uc.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(uc);

                //Ui Culture for Localized text in the UI
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(value.ToLower());// + "-" + value.ToUpper());

                //Sets  Culture for Current thread
                //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            }
        }

        private static void Initialize()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    // check user in cookie
                    if (UserId == 0 || string.IsNullOrEmpty(UserName))
                    {
                        UserId = UserCookie.IsValidEncryptedID() ? Convert.ToInt64(UserCookie.ToDecrypt()) : 0;
                        // intialize user value
                        dbPetSupplies_8517 DB = new Data.dbPetSupplies_8517();
                        User user = DB.Users.Where(x => x.UserID == UserId).FirstOrDefault();
                        if (user != null)
                        {
                            UserId = user.UserID;
                            UserName = user.Ownername ?? "";
                            IsAdmin = user.IsAdmin;
                        }
                    }

                }
            }
            catch { }
        }

        public static object SessionForModel
        {
            get
            {
                return HttpContext.Current.Session["SessionForModel"];
            }
            set { HttpContext.Current.Session["SessionForModel"] = value; }
        }
    }
}
