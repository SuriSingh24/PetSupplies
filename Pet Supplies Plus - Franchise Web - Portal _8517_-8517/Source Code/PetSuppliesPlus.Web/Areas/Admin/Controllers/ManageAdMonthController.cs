using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdMonth;
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
    public class ManageAdMonthController : AdminBaseController
    {
        IAdMonthService _AdMonth;
        ICommonService _Common;
        public ManageAdMonthController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _AdMonth = new AdMonthService(_unitofwork);
            _Common = new CommonService(_unitofwork);
        }

        // GET: Admin/AdMonth                                                       
        public ActionResult Index()
        {
            ViewBag.CouponList = _Common.GetCouponList();
            ViewBag.AdOptionList = _Common.GetAdOptionList();
            ViewBag.MonthList = _Common.GetMonthList();
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<AdMonthModel> model = new List<AdMonthModel>();
            model = _AdMonth.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        public ActionResult Add(string id)
        {
            AdMonthModel model = new AdMonthModel();
            if (id != null)
            {
                model = _AdMonth.GetDetail(id);
            }
            ViewBag.CouponList = _Common.GetCouponList();
            model.MonthList = _Common.GetMonthList();
            model.YearList = _Common.GetYearList();
            model.AdOptionList = _Common.GetAdOptionList();
            return View(model);
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult Save(AdMonthModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.AdMonthID = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    
                    model = _AdMonth.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "ErrorMessage");
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
        public JsonResult DeleteAdMonth(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _AdMonth.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }

    }
}