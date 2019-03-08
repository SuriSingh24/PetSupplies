using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Repository.Service;
using PetSuppliesPlus.Model.Users;

namespace PetSuppliesPlus.Web.Controllers
{
    [IsAuthorize]
    public class MyAccountController : BaseController
    {
        //
        // GET: /Home/
        IUserService _user;

        public MyAccountController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _user = new UserService(UnitofWork);
        }


        public ActionResult Index()
        {
            return View(_user.GetDetail(SessionHelper.UserId.ToString().ToEnctypt()));
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult SaveUser(UsersModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    bool isNew = true;

                    model.UserID = SessionHelper.UserId;
                    isNew = false;

                    model = _user.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SessionHelper.UserName = model.OwnerName;
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUser", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }


        public ActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();

            return View(model);
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
