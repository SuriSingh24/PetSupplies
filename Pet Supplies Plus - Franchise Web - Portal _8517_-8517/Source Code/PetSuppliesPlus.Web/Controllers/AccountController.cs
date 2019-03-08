using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Repository.Service;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Data;
using System.Data.Entity;

namespace PetSuppliesPlus.Web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Home/
        //AccountService _account;
        //CommonService _common;
        

        public AccountController(IUnitOfWork unitofwork)
            : base(unitofwork)
        {
            _account = new AccountService(unitofwork);
            _common = new CommonService(unitofwork);
            //_user = new ManageUserService(unitofwork);

        }
        
        public ActionResult Login(string id)
        {
            if (SessionHelper.IsUserLogin)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            LoginModel model = new LoginModel();
            model.ReturnUrl = id;
            return View(model);
            //return RedirectToAction("Index", "AdminLogin", new { area = "Admin" });
        }

        /// <summary>
        /// Login for end user
        /// </summary>
        /// <param name="model">LoginModel</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult DoLogin(LoginModel model)
        {
            //TransactionMessage TransMessage = new TransactionMessage();
            //TransMessage.Status = MessageStatus.Error;
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        TransMessage.Status = MessageStatus.Success;
            //        TransMessage.Message = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Dashboard") : model.ReturnUrl;
            //        string password = model.Password.ToEnctyptedPassword();
            //        // check user credential
            //        var user = UnitofWork.RepoAdminUser.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.Password == password && x.Status == 1).FirstOrDefault();
            //        if (user != null)
            //        {
            //            SessionHelper.AdminUserId = user.ID;
            //            SessionHelper.AdminUserName = user.Name ?? "";

            //            if (model.RememberMe)
            //            {
            //                SessionHelper.AdminUserCookie = user.ID.ToString().ToEnctypt();
            //            }

            //            TransMessage.Status = MessageStatus.Success;
            //            TransMessage.Message = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Dashboard") : model.ReturnUrl;
            //        }
            //        else
            //        {
            //            TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
            //        }
            //    }
            //    else
            //    {
            //        TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // write exception log
            //    EventLogHandler.WriteLog(ex);
            //}
            //return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }
        /// <summary>
        ///Logout for end user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //public ActionResult Logout()
        //{
        //    if (UserRoleName.SuperAdmin == SessionHelper.UserRole)
        //    {
        //        SessionHelper.Dispose();
        //        return Redirect("Login");
        //    }else
        //    {
        //        SessionHelper.Dispose();
        //        return RedirectToAction("Index","Home");
        //    }

        //}

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    _account.ChangePassword(model);
                }
                else
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       
        //public JsonResult ResetPassword(ForgotPasswordModel model)
        //{
        //    model.TransMessage = new TransactionMessage();
        //    model.TransMessage.Status = MessageStatus.Error;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _account.ForgotPassword(model);
        //        }
        //        else
        //        {
        //            model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "ErrorMessage");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        // write exception log
        //    }
        //    return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        //}


        /// <summary>
        /// Profile Page
        /// </summary>
        /// <returns></returns>
        

   
    }
}
