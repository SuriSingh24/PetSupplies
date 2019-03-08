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
    [AuthorizeAdmin]
    public class AdminChangePasswordController : AdminBaseController
    {
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public AdminChangePasswordController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Index()
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
                    if (model.NewPassword == model.ConfirmPassword)
                    {
                        var user = UnitofWork.RepoUser.Where(x => x.UserID == SessionHelper.UserId).FirstOrDefault();
                        if (user != null && user.Password == model.CurrentPassword.ToEnctyptedPassword())
                        {

                            string newPassword = model.NewPassword.ToEnctyptedPassword();
                            if (user.Password != newPassword)
                            {
                                // Update Password 
                                user.Password = newPassword;
                                UnitofWork.Commit();

                                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "SuccessMessage");
                                model.TransMessage.Status = MessageStatus.Success;
                            }
                            else
                            {
                                // old n new password are same
                                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "OldAndNewSame");
                            }
                        }
                        else
                        {
                            // wrong current password
                            model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "WrongPassword");
                        }
                    }
                    else
                    {
                        // new n confirm not match
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ChangePassword", "NotMatch");
                    }
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

    }
}
