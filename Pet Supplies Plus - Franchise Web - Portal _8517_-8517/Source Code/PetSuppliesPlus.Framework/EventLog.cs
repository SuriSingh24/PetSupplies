using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetSuppliesPlus.Data;

namespace PetSuppliesPlus.Framework
{
    public static class EventLogHandler
    {
        public static void WriteLog(Exception ex)
        {
            try
            {
                if (ex.InnerException != null) { WriteLog(ex.InnerException); }
                using (dbPetSupplies_8517 DB = new dbPetSupplies_8517())
                {
                    Data.EventLog log = new Data.EventLog();
                    log.Message = ex.Message.ToString();
                    log.Source = ex.Source.ToString();
                    log.StackTrace = ex.StackTrace.ToString();
                    log.IpAddress = IpAddress();
                    log.Datetime = DateTime.UtcNow;
                    log.Url = HttpContext.Current.Request.Url.ToString();
                    DB.EventLogs.Add(log);
                    DB.SaveChanges();
                }
            }
            catch
            {
            }
        }

        public static string IpAddress()
        {
            return (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ? HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
    }
}