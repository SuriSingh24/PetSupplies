using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdOption;
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
    public class AdOptionController : AdminBaseController
    {
        IAdOptionService _AdOption;
        ICommonService _common;
        public AdOptionController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _AdOption = new AdOptionService(_unitofwork);
            _common = new CommonService(_unitofwork);
        }

        // GET: Admin/AdOption                                                       
        public ActionResult Index()
        {
            ViewBag.SpecialList = _common.GetAdoptionList();
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<AdOptionModel> model = new List<AdOptionModel>();
            model = _AdOption.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        public ActionResult Add(string id)
        {
            AdOptionModel model = new AdOptionModel();
            if (id != null)
            {
                model = _AdOption.GetDetail(id);
            }
            ViewBag.SpecialList = _common.GetAdoptionList();
            return View(model);
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult Save(AdOptionModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.ADOptionID = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    model = _AdOption.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("AdOption", "ErrorMessage");
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
        public JsonResult DeleteAdOption(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _AdOption.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }

    }
}