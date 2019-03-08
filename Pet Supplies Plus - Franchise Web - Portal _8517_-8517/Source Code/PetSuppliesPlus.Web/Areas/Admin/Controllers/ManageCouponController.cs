using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Coupon;
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
    public class ManageCouponController : AdminBaseController
    {
        ICouponService _Coupon;
        public ManageCouponController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _Coupon = new CouponService(_unitofwork);
        }

        // GET: Admin/Coupon                                                       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<CouponModel> model = new List<CouponModel>();
            model = _Coupon.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        public ActionResult Add(string id)
        {
            CouponModel model = new CouponModel();
            if (id != null)
            {
                model = _Coupon.GetDetail(id);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult Save(CouponModel model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.EncryptedID.IsValidEncryptedID() && model.EncryptedID.ToDecrypt().IsNumber())
                    {
                        model.CouponID = Convert.ToInt32(model.EncryptedID.ToDecrypt());
                    }

                    model = _Coupon.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("Coupon", "ErrorMessage");
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
        public JsonResult DeleteCoupon(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _Coupon.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }

    }
}