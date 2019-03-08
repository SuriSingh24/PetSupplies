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
    public class ExceptionController : AdminBaseController
    {

        IExceptionReportService _exception;
        ICommonService _Common;
        public ExceptionController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _exception = new ExceptionReportService(_unitofwork);
            _Common = new CommonService(_unitofwork);
        }

        // GET: Admin/ManageMarket
        public ActionResult Index(string id,string month)
        {
            ViewBag.MonthList = _Common.GetMonthList();
            if (!string.IsNullOrEmpty(id) && id.IsNumber())
            {
                ViewBag.DropNumber = Convert.ToInt32(id);
            }
            if (!string.IsNullOrEmpty(month) && month.IsNumber())
            {
                ViewBag.MonthId = Convert.ToInt32(month);
            }
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<ExceptionReportModal> model = new List<ExceptionReportModal>();
            model = _exception.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

    }
}