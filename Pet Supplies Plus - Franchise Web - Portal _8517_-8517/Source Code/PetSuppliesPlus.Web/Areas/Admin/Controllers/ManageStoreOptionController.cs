
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

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class ManageStoreOptionController : AdminBaseController
    {
        IStoreOptionService _storeoption;
        ICommonService _common;
        public ManageStoreOptionController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _storeoption = new StoreOptionService(_unitofwork);
            _common = new CommonService(_unitofwork);
        }

        // GET: Admin/Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(string id)
        {
            StoreOptionModel model = new StoreOptionModel();
            if (id != null)
            {
                model = _storeoption.GetDetail(id);
            }
            ViewBag.StoreList = _common.GetStoreList();
            ViewBag.OptionList = _common.GetAdOptionList();
            return View(model);
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<StoreOptionModel> model = new List<StoreOptionModel>();
            model = _storeoption.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        #region Manage Single Store Ad Option
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult SaveStoreOption(StoreOptionModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.AllowedStoreOptionId = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    model = _storeoption.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "ErrorMessage");
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
        public JsonResult DeleteStoreOption(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _storeoption.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }
        #endregion

        #region Manage Allow Store ad option
        public ActionResult AllowAdOptions()
        {
            AllowedStoreOptionModel model = new AllowedStoreOptionModel();
            ViewBag.OptionList = _common.GetAdOptionList();
            return View(model);
        }

        public ActionResult StoreList(string id)
        {
            int adOptionId = 0;
            if (!string.IsNullOrEmpty(id) && id.IsNumber())
            {
                adOptionId = Convert.ToInt32(id);
            }
            AllowedStoreOptionModel model = new AllowedStoreOptionModel();
            model.Detail = _storeoption.GetList(adOptionId);
            return View(model);
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult AssignAdOptionsToStore(AllowedStoreOptionModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    model = _storeoption.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }
        #endregion
    }
}