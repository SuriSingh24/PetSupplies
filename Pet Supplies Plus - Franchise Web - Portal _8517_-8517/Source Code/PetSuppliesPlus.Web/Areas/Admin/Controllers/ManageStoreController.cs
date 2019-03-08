
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
using PetSuppliesPlus.Model.AdMonth;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class ManageStoreController : AdminBaseController
    {
        IStoreService _store;
        IStoreAdChoiceService _storeAdChoice;
        ICommonService _common;
        IStoreAdChoiceHistoryService _history;
        IAdMonthService _admonth;

        public ManageStoreController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _store = new StoreService(_unitofwork);
            _common = new CommonService(_unitofwork);
            _storeAdChoice = new StoreAdChoiceService(_unitofwork);
            _history = new StoreAdChoiceHistoryService(_unitofwork);
            _admonth = new AdMonthService(_unitofwork);
        }

        // GET: Admin/Store
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(string id)
        {
            StoreModel model = new StoreModel();
            if (id != null)
            {
                model = _store.GetDetail(id);
            }
            ViewBag.MarketList = _common.GetMarketList();

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
        public ActionResult SaveStore(StoreModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.StoreID = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    model = _store.Save(model);
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
        public JsonResult DeleteStore(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _store.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }

        #region Missng Entry
        public ActionResult MissingEntry()
        {
            // missingentry
            return View();
        }


        public ActionResult MissingEntryGridList(DataPagingModel dataPaging)
        {
            List<StoreModel> model = new List<StoreModel>();
            if (!dataPaging.SearchParameter.Keys.Contains("missingentry"))
            {
                dataPaging.SearchParameter.Add("missingentry", "1");
            }
            model = _store.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        #endregion

        #region StoreHistory
        [HttpGet]
        public ActionResult History(string id)
        {
            ViewBag.StoreId = 0;
            ViewBag.MonthList = _common.GetMonthList();
            if (!string.IsNullOrEmpty(id) && id.IsValidEncryptedID())
            {
                ViewBag.StoreId = id.ToDecrypt();
            }

            return View();
        }
        public ActionResult DataGridListForHistory(DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            model = _storeAdChoice.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        #endregion

        [HttpGet]
        public ActionResult ManageMissingEntry()
        {
            AdMonthModel adMonth = _admonth.GetDetailByMonthId();
            if (adMonth != null)
            {
                ViewBag.CurrentMonth = (new DateTime(adMonth.Year, adMonth.Month, 1)).ToString("(MMMM yyyy)");
            }
            return View(adMonth);
        }

        [HttpPost]
        public ActionResult PopulateMissingEntry(FormCollection data)
        {
            int dropNumber = Convert.ToInt32(data["DropNumber"].ToString());
            var StoreAdChoiceListModel = _store.UpdateMissingStoreEntry(dropNumber);
            SessionHelper.SessionForModel = StoreAdChoiceListModel;

            return Json(StoreAdChoiceListModel, JsonRequestBehavior.DenyGet);
            //return View(StoreAdChoiceListModel);
        }

        //[HttpPost]
        public ActionResult PopulateMissingEntryList()
        {
            if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(StoreAdChoiceListModel))
            {
                return View(SessionHelper.SessionForModel);
            }
            return View(new StoreAdChoiceListModel());
        }

        public ActionResult PopulateEntryConformed()
        {
            TransactionMessage model = new TransactionMessage();
            if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(StoreAdChoiceListModel))
            {
                StoreAdChoiceListModel storeAdChoiceListModel = (StoreAdChoiceListModel)SessionHelper.SessionForModel;
                model = _store.UpdateMissingEntry(storeAdChoiceListModel.storAdChoiceList);
                SessionHelper.SessionForModel = null;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report()
        {
            ViewBag.StoreList = _common.GetStoreList().OrderBy(x => Convert.ToInt32(x.Value));
            ViewBag.MonthList = _common.GetAdMonthList();
            return View();

        }
        public ActionResult ReportDataGridList(DataPagingModel dataPaging)
        {
            List<StoreReportModel> model = new List<StoreReportModel>();
            if (dataPaging.SearchParameter != null && dataPaging.SearchParameter.Keys.Where(x => x == "month").Count() == 0 && dataPaging.SearchParameter.Keys.Where(x => x == "monthid").Count() == 0)
            {
                dataPaging.SearchParameter.Add("month", DateTime.Now.Month.ToString());
            }
            model = _history.GetStoreAdOptionReport(ref dataPaging);
            ViewBag.DataPaging = dataPaging;

            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public void ExportOutputReportToExcel()
        {
            List<StoreReportModel> model = new List<StoreReportModel>();
            model = _history.GetExprotReport();

            Export("Output_Report.xls", model.Select(x => new
            {
                In_Home_Date = x.InHomeDate.Value.ToString(ApplicationConstant.DateFormatDisplay),
                Store_Group = x.StoreGroup,
                Individual_Store = x.StoreID,
                Copy_Version_A = "",
                Copy_Version_B = x.StoreCode,
                Version_Code = "",
                Product = x.AdOptionName,
                Art_Due = "",
                Coupon = x.CouponText,
                Art_Work_Code = x.ArtWorkCode,

            }));
        }


        #region AdChoiceHistory
        public ActionResult StoreAdChoiceHistory()
        {
            return View();
        }
        public ActionResult DataGridListForStoreAdChoiceHistory(DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            model = _storeAdChoice.GetStoreAdChoiceHistoryList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        #endregion AdChoiceHistory
    }
}