using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Coupon;
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
    public class ManageCouponAdMonthController : AdminBaseController
    {
        ICouponService _coupon;
        IAdMonthService _admonth;
        ICommonService _common;
        public ManageCouponAdMonthController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _coupon = new CouponService(_unitofwork);
            _common = new CommonService(_unitofwork);
            _admonth = new AdMonthService(_unitofwork);
        }

        /// <summary>
        /// Show the Index Page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Index(string id)
        {
            AdMonthModel objadmonth = _admonth.GetDetail(id.ToEnctypt());
            ViewBag.MonthId = Convert.ToInt32(id.ToDecrypt());
            ViewBag.Month = _common.GetMonthList().Where(a => a.Value == (objadmonth.Month).ToString()).Select(a => a.Text).FirstOrDefault();
            ViewBag.Year = objadmonth.Year;
            return View();
        }

        /// <summary>
        /// Contain all data in grid list
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns>View</returns>
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<CouponModel> model = new List<CouponModel>();
            model = _coupon.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        /// <summary>
        /// open page where we can Add coupon in month
        /// </summary>
        /// <param name="id">monthid</param>
        /// <returns>View with model</returns>
        public ActionResult Add(string id)
        {
            CouponAdMonthModel model = new CouponAdMonthModel();
            AdMonthModel objadmonth = _admonth.GetDetail(id.ToEnctypt());
            model.MonthId = Convert.ToInt32(id);
            model.Month = _common.GetMonthList().Where(a => a.Value == (objadmonth.Month).ToString()).Select(a => a.Text).FirstOrDefault();
            model.Year = objadmonth.Year;
            ViewBag.CouponList = _common.GetCouponList();
            return View(model);
        }

        /// <summary>
        /// save the coupon correspong to month in the database
        /// </summary>
        /// <param name="model">contain data like couponid ,monthid</param>
        /// <returns>json</returns>
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult SaveCouponAdMonth(CouponAdMonthModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    model = _coupon.CouponAdMonthSave(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "ErrorMessage");
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return Json(model.TransMessage, JsonRequestBehavior.DenyGet);
        }

        /// <summary>
        /// delete the assiged coupon of month
        /// </summary>
        /// <param name="id">Couponid</param>
        /// <param name="MonthId">MonthId</param>
        /// <returns>json</returns>
        [HttpPost]
        public JsonResult DeleteCouponAdMonth(string id, string MonthId)
        {
            TransactionMessage model = new TransactionMessage();
            model = _coupon.DeleteCouponAdMonth(id, MonthId);
            return Json(model, JsonRequestBehavior.DenyGet);
        }
    }
}