using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Store;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Model.Users;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class ManageUserStoreController : AdminBaseController
    {
        IStoreService _store;
        ICommonService _common;
        IUserStoreService _UserStore;
        IUserService _user;
        public ManageUserStoreController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _store = new StoreService(_unitofwork);
            _common = new CommonService(_unitofwork);
            _UserStore = new UserStoreService(_unitofwork);
            _user = new UserService(_unitofwork);
        }

        // GET: Admin/Store
        public ActionResult Index(string id)
        {
            ViewBag.UserID = Convert.ToInt32(id.ToDecrypt());
            ViewBag.OwnerName = _user.GetDetail(id.ToEnctypt()).OwnerName;
            return View();
        }
        public ActionResult Add(string id)
        {
            UserStoreModel model = new UserStoreModel();
            model.UserID = Convert.ToInt32(id);
            model.OwnerName = _user.GetDetail(id.ToEnctypt()).OwnerName;
            ViewBag.StoreList = _common.GetStoreList();
            return View(model);
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<StoreModel> model = new List<StoreModel>();
            model = _store.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult SaveUserStore(UserStoreModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    model = _store.UserStoreSave(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }
        [HttpPost]
        public JsonResult DeleteUserStore(string id, string UserId)
        {
            TransactionMessage model = new TransactionMessage();
            model = _store.DeleteUserStore(id, UserId);
            return Json(model, JsonRequestBehavior.DenyGet);
        }

    }
}