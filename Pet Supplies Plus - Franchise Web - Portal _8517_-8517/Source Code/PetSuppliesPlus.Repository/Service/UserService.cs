using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Users;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Data;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IUserService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<UsersModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        UsersModel Save(UsersModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        UsersModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);
    }

    public class UserService : IUserService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public UserService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<UsersModel> GetList(ref DataPagingModel dataPaging)
        {
            List<UsersModel> model = new List<UsersModel>();
            var list = UnitofWork.RepoUser.Where(x => 1 == 1); //x.IsActive == true
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "name")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Ownername.ToLower().Contains(value));
                    }
                    if (item.Key == "email")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Email.ToLower().Contains(value));
                    }
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.UserStores.Where(y => y.UserID == x.UserID).FirstOrDefault().Store.Storename.ToLower().Contains(value));
                    }
                    if (item.Key == "status")
                    {
                        bool value = item.Value == "T";
                        list = list.Where(x => x.IsActive == value);
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "userid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.UserID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.UserID);
                    }
                    break;
                case "email":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Email);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Email);
                    }
                    break;
                case "name":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Ownername);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Ownername);
                    }
                    break;
                case "status":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.IsActive);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.IsActive);
                    }
                    break;
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.UserStores.Where(x => x.UserID == U.UserID).FirstOrDefault().Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.UserStores.Where(x => x.UserID == U.UserID).FirstOrDefault().Store.Storename);

                    }
                    break;
            }

            #endregion

            dataPaging.TotalRecords = list.Count();

            //********* Pagination of list *********//
            if (dataPaging.TotalRecords > dataPaging.PageSize && dataPaging.ShowAllRecord == false)
            {
                list = list.Skip(dataPaging.CurrentPageID.TableSkipRecord(dataPaging.PageSize)).Take(dataPaging.PageSize);
            }

            model = list.ToList().Select(x => new UsersModel
            {
                UserID = x.UserID,
                Email = x.Email,
                OwnerName = x.Ownername,
                IsActive = x.IsActive,
                IsAdmin = x.IsAdmin,
                StoreDetail = x.UserStores.Where(y => y.UserID == x.UserID).Count() > 0 ? x.UserStores.Where(y => y.UserID == x.UserID).FirstOrDefault().Store ?? new Store() : new Store(),
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.UserID.ToString().ToEnctypt());
            model.ForEach(x => x.StoreID = x.StoreDetail.StoreID);
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public UsersModel Save(UsersModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                //check duplicate 
                if (UnitofWork.RepoUser.Where(x => x.Email.ToLower() == model.Email.ToLower() && x.UserID != model.UserID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUser", "Duplicate");
                    return model;
                }

                Data.User dbUser = UnitofWork.RepoUser.Where(x => x.UserID == model.UserID).FirstOrDefault();

                bool isSave = false;
                string password = Guid.NewGuid().ToString();
                password = password.Replace("-", "").Substring(8);
                //user table 
                if (dbUser == null)
                {
                    #region Save
                    dbUser = new Data.User();
                    if (string.IsNullOrEmpty(model.Password)) { model.Password = password; }
                    dbUser.Password = model.Password.ToEnctyptedPassword();
                    //dbUser.UserStores.Add(new UserStore()
                    //{
                    //    StoreID = model.StoreID,
                    //});

                    UnitofWork.RepoUser.Add(dbUser);

                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUser", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    //foreach (var item in dbUser.UserStores)
                    //{
                    //    item.StoreID = model.StoreID;
                    //}
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUser", "Update");
                    #endregion
                }
                #region Set Value
                dbUser.Ownername = model.OwnerName;
                dbUser.Email = model.Email;
                dbUser.IsActive = model.IsActive;
                dbUser.IsAdmin = model.IsAdmin;
                #endregion

                UnitofWork.Commit();

                model.UserID = dbUser.UserID;
                model.EncryptedID = dbUser.UserID.ToString().ToEnctypt();
                model.TransMessage.Status = MessageStatus.Success;

                #region Send Mail
                if (isSave)
                {
                    model.StoreDetail = UnitofWork.RepoStore.Where(x => x.StoreID == model.StoreID).FirstOrDefault();

                    string subject = "Welcome to Pet Supplies Plus!";
                    string template = utilityHelper.ReadFromFile("Registration.html");
                    template = template.Replace("[Name]", model.OwnerName);
                    template = template.Replace("[Store]", model.StoreDetail != null ? model.StoreDetail.Storename : "");
                    template = template.Replace("[Email]", model.Email);
                    template = template.Replace("[Password]", model.Password);
                    template = template.Replace("[SiteUrl]", utilityHelper.SiteUrl());
                    utilityHelper.SendMail(model.Email, subject, template);
                }
                #endregion


            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        public UsersModel GetDetail(string encryptedId)
        {
            UsersModel model = new UsersModel();
            try
            {
                int UserId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoUserStore.Where(x => x.UserID == UserId).Select(x => new UsersModel
                {
                    UserID = x.UserID ?? 0,
                    Email = x.User.Email ?? "",
                    IsActive = x.User.IsActive,
                    IsAdmin = x.User.IsAdmin,
                    Password = x.User.Password ?? "",
                    StoreID = x.StoreID ?? 0,
                    OwnerName = x.User.Ownername,
                    StoreDetail = x.Store,
                }).FirstOrDefault();
                #endregion
                if (model == null)
                {
                    model = UnitofWork.RepoUser.Where(x => x.UserID == UserId).Select(x => new UsersModel
                    {
                        UserID = x.UserID,
                        Email = x.Email ?? "",
                        IsActive = x.IsActive,
                        IsAdmin = x.IsAdmin,
                        Password = x.Password ?? "",
                        StoreID = 0,
                        OwnerName = x.Ownername,
                    }).FirstOrDefault();
                }

                if (model != null)
                {
                    model.EncryptedID = model.UserID.ToString().ToEnctypt();
                }
                else
                {
                    model = new UsersModel();
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        public TransactionMessage Delete(string encryptedId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("ManageUser", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int UserId = Convert.ToInt32(encryptedId.ToDecrypt());
                    var _user = UnitofWork.RepoUser.Where(x => x.UserID == UserId).FirstOrDefault();
                    var refCount = UnitofWork.RepoUserStore.Where(x => x.UserID == UserId).Count();
                    if (_user != null) //&& refCount == 0
                    {
                        _user.IsActive = false;
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageUser", "Delete");
                        model.Status = MessageStatus.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

    }
}

