using PetSuppliesPlus.Model.Users;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetSuppliesPlus.Web.Controllers
{
    public class UserController : BaseController
    {
      

        /// <summary>
        /// controller to list activities
        /// </summary>
        /// <param name="_unitofwork"></param>
        public UserController(IUnitOfWork unitofwork): base(unitofwork)
        {
            //_common = new CommonService(unitofwork);
            //_user = new UserService(unitofwork);
        }


        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Paging
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<UsersModel> model = new List<UsersModel>();

           // model = _user.SelectUserList(ref dataPaging);
            //if (SessionHelper.UserRole == UserRoleName.CompanyAdmin)
            //{
            //    model = model.Where(x => x.IsAdmin == false && x.RoleName!=UserRoleName.CompanyAdmin).ToList();
            //}
            //else if (SessionHelper.UserRole == UserRoleName.AdminUser)
            //{
            //    model = model.Where(x => x.IsAdmin == false && (x.RoleName != UserRoleName.CompanyAdmin && x.RoleName != UserRoleName.SuperAdmin && x.RoleName != UserRoleName.AdminUser)).ToList();
            //}
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
    }
}