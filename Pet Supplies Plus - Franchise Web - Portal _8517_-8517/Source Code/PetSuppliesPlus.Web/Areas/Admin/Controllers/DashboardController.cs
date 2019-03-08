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

namespace PetSuppliesPlus.Web.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    public class DashboardController : AdminBaseController
    {
        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public DashboardController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
        }

        [AuthorizeAdmin]
        public ActionResult Index(string id)
        {
            return RedirectToAction("Index", "ManageStore");
            return View();
        }

    }
}
