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

namespace PetSuppliesPlus.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        IDocumentService _document;
        public HomeController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _document = new DocumentService(_unitofwork);
        }

        public ActionResult Index(string id)
        {
            if (SessionHelper.IsUserLogin)
            {
                return RedirectToAction("Index", "Store");
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
                    var user = UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.Password == password && x.IsActive == true).FirstOrDefault();
                    if (user != null)
                    {
                        SessionHelper.UserId = user.UserID;
                        SessionHelper.UserName = user.Ownername ?? "";
                        SessionHelper.IsAdmin = false;

                        if (model.RememberMe)
                        {
                            SessionHelper.UserCookie = user.UserID.ToString().ToEnctypt();
                        }

                        #region Add to History

                        UnitofWork.RepoLoginHistory.Add(new LoginHistory()
                        {
                            Browser = utilityHelper.UserBrowser(),
                            TimeStamp = utilityHelper.CurrentDateTime,
                            UserID = user.UserID,
                            IPaddress = utilityHelper.IpAddress(),
                            Device = utilityHelper.UserDevice(),
                        });
                        UnitofWork.Commit();
                        #endregion

                        TransMessage.Status = MessageStatus.Success;
                        TransMessage.Message = string.IsNullOrEmpty(model.ReturnUrl) ? Url.Action("Index", "Store") : model.ReturnUrl;
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

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Document()
        {
            List<DocumentModel> model = new List<DocumentModel>();
            model = _document.GetListByMonth();
        
            return View(model);
        }
    }
}
