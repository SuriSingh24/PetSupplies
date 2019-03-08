using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Models;

namespace PetSuppliesPlus.Web.Utility
{
    public class UserHelper
    {
        /// <summary>
        /// to unique identity
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public static bool IsExist(string email)
        {
            try
            {
                //using (dbPetSuppliesPlus DB = new dbPetSuppliesPlus())
                //{
                //    var user = DB.Users.Where(x => x.Email.ToLower() == email.ToLower() && x.Status == (byte)Status.Active).FirstOrDefault();
                //    return (user != null);
                //}
            }
            catch (Exception ex)
            {
                //EventLogHandler.WriteLog(ex);
            }
            return false;
        }
    }
}