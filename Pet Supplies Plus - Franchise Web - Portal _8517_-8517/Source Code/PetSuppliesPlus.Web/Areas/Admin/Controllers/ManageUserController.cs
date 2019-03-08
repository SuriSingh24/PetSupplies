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
using PetSuppliesPlus.Model.Users;
using PetSuppliesPlus.Repository.Service;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class ManageUserController : AdminBaseController
    {
        IUserService _user;
        ICommonService _common;
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public ManageUserController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _user = new UserService(_unitofwork);
            _common = new CommonService(_unitofwork);
        }

        public ActionResult Index(string id)
        {

            if (id != null)
            {
                return View(_user.GetDetail(id));
            }
            return View();
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
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.UserID = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                        isNew = false;
                    }

                    model = _user.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
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

        public ActionResult UserList()
        {
            UsersModel model = new UsersModel();
            return View(model);
        }
        /// <summary>
        /// Paging
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<UsersModel> model = new List<UsersModel>();
            model = _user.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteUser(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _user.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }
        [HttpGet]
        public ActionResult Add(string id)
        {
            UsersModel model = new UsersModel();
            ViewBag.StoreList = _common.GetStoreList();
            if (id != null)
            {
                return View(_user.GetDetail(id));
            }
            return View(model);
        }

    }
}
