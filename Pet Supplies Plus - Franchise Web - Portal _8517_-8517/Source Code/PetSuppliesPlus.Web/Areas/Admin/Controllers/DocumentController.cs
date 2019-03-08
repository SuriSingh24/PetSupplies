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
using PetSuppliesPlus.Repository.Service;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class DocumentController : AdminBaseController
    {
        IDocumentService _document;
        ICommonService _common;
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public DocumentController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _document = new DocumentService(_unitofwork);
            _common = new CommonService(_unitofwork);
        }

        public ActionResult Index(string id)
        {
            Session["MonthId"] = Convert.ToInt32(id.ToDecrypt());
            ViewBag.MonthList = _common.GetMonthList();
            return View();
        }
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<DocumentModel> model = new List<DocumentModel>();
            var MonthId = Session["MonthId"];
            if (MonthId != null)
            {
                if (dataPaging != null)
                {
                    if (dataPaging.SearchParameter == null) { dataPaging.SearchParameter = new Dictionary<string, string>(); }
                    dataPaging.SearchParameter.Add("month", MonthId.ToString());
                }
            }
            model = _document.GetList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }

        public ActionResult Add(string id)
        {
            DocumentModel model = new DocumentModel();
            model.MonthID = Convert.ToInt32(id);
            ViewBag.Month = _common.GetMonthList();
            return View(model);
        }
        [HttpPost]
        [ValidateOnlyIncomingValues]
        public ActionResult Save(DocumentModel model, HttpPostedFileBase files)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                if (ModelState.IsValid && files != null)
                {
                    model.FileName = files.FileName;
                    model.FilePath = utilityHelper.UploadFile(files);
                    model = _document.Save(model);
                    if (model.TransMessage.Status == MessageStatus.Success)
                    {
                        SuccessNotification(model.TransMessage.Message);
                    }
                }
                else
                {
                    ErrorNotification(utilityHelper.ReadGlobalMessage("Document", "ErrorMessage"));
                }
            }
            catch (Exception ex)
            {
                // write exception log
                EventLogHandler.WriteLog(ex);
            }
            return RedirectToAction("Index", new { id = model.MonthID });
        }

        [HttpPost]
        public JsonResult DeleteDocument(string id)
        {
            TransactionMessage model = new TransactionMessage();
            model = _document.Delete(id);
            return Json(model, JsonRequestBehavior.DenyGet);
        }
    }
}
