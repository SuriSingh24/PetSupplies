using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        #region Unit of Work
        IUnitOfWork unitOfWork;
        /// <summary>
        /// Property for UnitofWork
        /// </summary>
        public IUnitOfWork UnitofWork
        {
            get { return unitOfWork; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_unitofwork">Unit of work</param>

        public AdminBaseController(IUnitOfWork _unitofwork)
        {
            unitOfWork = _unitofwork;
        }
        #endregion

        #region Method For Notification
        /// <summary>
        /// Success notification.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(MessageStatus.Success, message, persistForTheNextRequest);
        }

        /// <summary>
        /// Info notification.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void InfoNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(MessageStatus.Info, message, persistForTheNextRequest);
        }

        /// <summary>
        /// Error notification
        /// </summary>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(MessageStatus.Error, message, persistForTheNextRequest);
        }


        /// <summary>
        /// Error notification
        /// </summary>
        /// <param name="modelstate">ModelState</param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void ErrorNotification(ModelStateDictionary modelstate, bool persistForTheNextRequest = true)
        {
            foreach (var state in ModelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    AddNotification(MessageStatus.Error, error.ErrorMessage, persistForTheNextRequest);
                }
            }
        }

        /// <summary>
        /// Action for add the notification.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <param name="persistForTheNextRequest"></param>
        protected virtual void AddNotification(MessageStatus type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                    TempData[dataKey] = new List<string>();
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                    ViewData[dataKey] = new List<string>();
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }

        public void Export(string ExcelName, object list)
        {

            GridView gv = new GridView();
            gv.DataSource = list;
            gv.DataBind();
            gv.CssClass = "table stacktable large-only tablularData";
            if (gv.HeaderRow != null)
            {
                for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
                {
                    gv.HeaderRow.Cells[i].Text = gv.HeaderRow.Cells[i].Text.Replace("_", " ");
                    gv.HeaderRow.Cells[i].Text = gv.HeaderRow.Cells[i].Text.Replace("9", "(");
                    gv.HeaderRow.Cells[i].Text = gv.HeaderRow.Cells[i].Text.Replace("0", ")");
                }
            }
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + ExcelName);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
        #endregion

    }
}
