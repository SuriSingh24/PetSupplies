using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Web.Utility;
using PetSuppliesPlus.Repository.Service;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {

        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public AccountController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {

        }

        public ActionResult Index(string id)
        {
            if (SessionHelper.IsUserLogin && SessionHelper.IsAdmin)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            LoginModel model = new LoginModel();
            model.ReturnUrl = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult DoLogin(LoginModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    string password = model.Password.ToEnctyptedPassword();
                    // check user credential
                    var user = UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.Password == password && x.IsAdmin == true && x.IsActive == true).FirstOrDefault();
                    if (user != null)
                    {
                        SessionHelper.UserId = user.UserID;
                        SessionHelper.UserName = user.Ownername ?? "";
                        SessionHelper.IsAdmin = user.IsAdmin;

                        if (model.RememberMe)
                        {
                            SessionHelper.UserCookie = user.UserID.ToString().ToEnctypt();
                        }

                        #region Add to History
                        var browser = Request.Browser;

                        UnitofWork.RepoLoginHistory.Add(new LoginHistory()
                        {
                            Browser = browser.Browser,
                            TimeStamp = utilityHelper.CurrentDateTime,
                            UserID = user.UserID,
                            IPaddress = utilityHelper.IpAddress(),
                            Device = browser.IsMobileDevice ? "Mobile" : "Web",
                        });
                        UnitofWork.Commit();
                        #endregion

                        TransMessage.Status = MessageStatus.Success;
                        TransMessage.Message = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Dashboard") : model.ReturnUrl;
                    }
                    else
                    {
                        TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("Login", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Logout()
        {
            SessionHelper.Dispose();
            return Redirect("Index");
        }
    }
}
