using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Web.Utility;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    public class AdminForgotPasswordController : AdminBaseController
    {
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public AdminForgotPasswordController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ResetPassword(ForgotPasswordModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    // check user Email
                    var user = UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower()).FirstOrDefault();
                    if (user != null)
                    {
                        Guid gid = Guid.NewGuid();
                        string password = gid.ToString().Substring(0, 8);
                        // Update Password 
                        user.Password = password.ToEnctyptedPassword();
                        UnitofWork.Commit();

                        // Send Mail
                        string subject = "Reset Password";
                        string template = utilityHelper.ReadFromFile("ForgotPassword.html");
                        template = template.Replace("[Name]", user.Ownername);
                        template = template.Replace("[Email]", user.Email);
                        template = template.Replace("[Password]", password);
                        template = template.Replace("[SiteUrl]", utilityHelper.SiteUrl());
                        Framework.utilityHelper.SendMail(user.Email, subject, template);

                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "SuccessMessage");
                        model.TransMessage.Status = MessageStatus.Success;
                    }
                    else
                    {
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "ErrorMessage");
                    }
                }
                else
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ForgotPassword", "ErrorMessage");
                }

            }
            catch (Exception ex)
            {
                // write exception log
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }



    }
}
