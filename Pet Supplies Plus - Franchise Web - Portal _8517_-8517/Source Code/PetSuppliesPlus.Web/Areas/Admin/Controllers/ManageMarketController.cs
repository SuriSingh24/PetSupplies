using PetSuppliesPlus.Model.Market;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Repository.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Framework;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class ManageMarketController : AdminBaseController
    {

        IMarketService _market;
        public ManageMarketController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _market = new MarketService(_unitofwork);
        }

        // GET: Admin/ManageMarket
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<MarketModel> model = new List<MarketModel>();
            model = _market.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        public ActionResult Add(string id)
        {
            MarketModel model = new MarketModel();
            if (id != null)
            {
                model = _market.GetDetail(id);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult Save(MarketModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.MarketId = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    model = _market.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "ErrorMessage");
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
        public JsonResult DeleteMarket(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _market.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }
    }
}