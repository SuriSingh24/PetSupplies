using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Users;
using PetSuppliesPlus.Models;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IUserStoreService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<UserStoreModel> GetList(string UserID);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        UserStoreModel Save(UserStoreModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        UserStoreModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);
    }

    public class UserStoreService : IUserStoreService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public UserStoreService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<UserStoreModel> GetList(string UserID)
        {
            int _userId = Convert.ToInt32(UserID.ToDecrypt());
            List<UserStoreModel> model = new List<UserStoreModel>();
            var list = UnitofWork.RepoUserStore.Where(x => x.UserID == _userId && x.User.IsActive == true);

            model = list.Select(x => new UserStoreModel
            {
                UserStoreID = x.UserStoreID,
                UserID = x.UserID ?? 0,
                StoreID = x.StoreID ?? 0,
                OwnerName = x.User.Ownername,
                StoreName = x.Store.Storename,
                City = x.Store.City,
                State = x.Store.State,
                MarketName = x.Store.Market != null ? x.Store.Market.Name : "",
            }).OrderByDescending(x => x.UserStoreID).ToList();

            model.ForEach(x => x.EncryptedID = x.UserStoreID.ToString().ToEnctypt());

            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public UserStoreModel Save(UserStoreModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {

                #region check duplicate 
                if (UnitofWork.RepoUserStore.Where(x => x.StoreID == model.StoreID && x.UserID == model.UserID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Duplicate");
                    return model;
                }
                #endregion

                UserStore dbuserStore = UnitofWork.RepoUserStore.Where(x => x.UserStoreID == model.UserStoreID).FirstOrDefault();

                bool isSave = false;

                if (dbuserStore == null)
                {
                    #region Save
                    dbuserStore = new UserStore();
                    UnitofWork.RepoUserStore.Add(dbuserStore);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Update");
                    #endregion
                }
                #region Set Value
                dbuserStore.UserID = model.UserID;
                dbuserStore.StoreID = model.StoreID;
                #endregion

                UnitofWork.Commit();
                model.TransMessage.Status = MessageStatus.Success;
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
        public UserStoreModel GetDetail(string encryptedId)
        {
            UserStoreModel model = new UserStoreModel();
            try
            {
                int UserId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoUserStore.Where(x => x.UserID == UserId && x.User.IsActive == true).Select(x => new UserStoreModel
                {
                    UserID = x.UserID ?? 0,
                    UserStoreID = x.UserStoreID,
                    StoreName = x.Store.Storename,
                    StoreID = x.StoreID ?? 0,
                    OwnerName = x.User.Ownername ?? "",
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.UserID.ToString().ToEnctypt();
                }
                else
                {
                    model = new UserStoreModel();
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
                    int UserStoreId = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoUserStore.Where(x => x.UserStoreID == UserStoreId).FirstOrDefault();
                    if (item != null)
                    {
                        UnitofWork.RepoUserStore.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Delete");
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

