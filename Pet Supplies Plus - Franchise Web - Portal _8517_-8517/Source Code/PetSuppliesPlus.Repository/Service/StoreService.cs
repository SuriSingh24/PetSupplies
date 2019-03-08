using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Store;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Model.Users;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Model.AdOption;

namespace PetSuppliesPlus.Repository.Service
{

    public interface IStoreService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<StoreModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        StoreModel Save(StoreModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        StoreModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);
        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UserStoreModel</param>
        /// <returns>UserStoreModel</returns>
        UserStoreModel UserStoreSave(UserStoreModel model);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage DeleteUserStore(string encryptedId, string userId);

        ///
        ///
        StoreAdChoiceListModel UpdateMissingStoreEntry(int dropNumber);

        /// <summary>
        /// After Conformation Add Adchoice to store
        /// </summary>
        /// <param name="storeAdChoiceList"></param>
        /// <returns>TransactionModel</returns>
        TransactionMessage UpdateMissingEntry(List<StoreAdChoiceModel> storeAdChoiceList);

    }

    public class StoreService : IStoreService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;
        public IExceptionReportService _Exception;
        public IStoreOptionService _storeAdOption;
        public IAdOptionService _AdOption;
        public StoreService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
            _Exception = new ExceptionReportService(_unifOfWrok);
            _storeAdOption = new StoreOptionService(_unifOfWrok);
            _AdOption = new AdOptionService(_unifOfWrok);
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<StoreModel> GetList(ref DataPagingModel dataPaging)
        {
            List<StoreModel> model = new List<StoreModel>();
            var list = UnitofWork.RepoStore.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key.ToLower() == "ownergroup")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Ownergroup.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "missingentry")
                    {
                        var value = DateTime.Now.Month;
                        list = list.Where(x => x.StoreAdChoices.Where(c => c.StoreID == x.StoreID && c.AdMonth.Month == value).Count() == 0);
                    }
                    if (item.Key.ToLower() == "city")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.City.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "state")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.State.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Storename.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "marketname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Market.Name.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "userid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.UserStores.Where(s => s.UserID == value && s.StoreID == x.StoreID).Count() > 0);
                    }
                    if (item.Key.ToLower() == "storeid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreID == value);
                    }
                    if (item.Key.ToLower() == "address")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.City.ToLower().Contains(value) || x.State.ToLower().Contains(value) || x.Market.Name.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "storecode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => (x.StoreCode ?? "").ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "artworkcode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => (x.ArtworkCode ?? "").ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "ownergroup":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Ownergroup);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Ownergroup);
                    }
                    break;
                case "storeid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreID);
                    }
                    break;
                //case "circulation":
                //    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                //    {
                //        list = list.OrderBy(U => U.Circulation);
                //    }
                //    else
                //    {
                //        list = list.OrderByDescending(U => U.Circulation);
                //    }
                //    break;
                case "city":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.City);
                    }
                    break;
                case "state":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.State);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.State);
                    }
                    break;
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Storename);
                    }
                    break;
                case "marketname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Market.Name);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Market.Name);
                    }
                    break;
                case "storecode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreCode);
                    }
                    break;
                case "artworkcode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ArtworkCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ArtworkCode);
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

            model = list.Select(x => new StoreModel
            {
                StoreID = x.StoreID,
                Ownergroup = x.Ownergroup,
                // Circulation = x.Circulation,
                City = x.City,
                State = x.State,
                Storename = x.Storename,
                MarketID = x.MarketID ?? 0,
                MarketName = x.Market.Name,
                StoreCode = x.StoreCode ?? "",
                ArtworkCode = x.ArtworkCode ?? "",
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.StoreID.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public StoreModel Save(StoreModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                //check duplicate 
                if (UnitofWork.RepoStore.Where(x => x.Storename.ToLower() == model.Storename.ToLower() && x.StoreID != model.StoreID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Duplicate");
                    return model;
                }
                //check store id duplicate 
                if (string.IsNullOrEmpty(model.EncryptedID))
                {
                    if (UnitofWork.RepoStore.Where(x => x.StoreID == model.StoreID).Count() > 0)
                    {
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "StoreidDuplicate");
                        return model;
                    }
                }
                Data.Store dbStore = UnitofWork.RepoStore.Where(x => x.StoreID == model.StoreID).FirstOrDefault();
                bool isSave = false;
                //user table 
                int marketId = Convert.ToInt32(model.MarketID);
                if (dbStore == null)
                {
                    #region Save
                    dbStore = new Data.Store();
                    UnitofWork.RepoStore.Add(dbStore);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Update");
                    #endregion
                }

                #region Set Value
                dbStore.StoreID = model.StoreID;
                dbStore.Ownergroup = model.Ownergroup;
                // dbStore.Circulation = model.Circulation;
                dbStore.City = model.City;
                dbStore.State = model.State;
                dbStore.MarketID = marketId;
                dbStore.Storename = model.Storename;

                dbStore.StoreCode = model.StoreCode;
                dbStore.ArtworkCode = model.ArtworkCode;
                #endregion

                UnitofWork.Commit();

                model.StoreID = dbStore.StoreID;
                model.EncryptedID = dbStore.StoreID.ToString().ToEnctypt();
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
        public StoreModel GetDetail(string encryptedId)
        {
            StoreModel model = new StoreModel();
            try
            {
                int StoreID = Convert.ToInt32(encryptedId.ToDecrypt());
                #region get detail
                model = UnitofWork.RepoStore.Where(x => x.StoreID == StoreID).Select(x => new StoreModel
                {
                    StoreID = x.StoreID,
                    Ownergroup = x.Ownergroup,
                    //Circulation = x.Circulation,
                    City = x.City,
                    State = x.State,
                    Storename = x.Storename,
                    MarketName = x.Market.Name,
                    MarketID = x.MarketID ?? 0,
                    StoreCode = x.StoreCode ?? "",
                    ArtworkCode = x.ArtworkCode ?? "",
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.StoreID.ToString().ToEnctypt();
                }
                else
                {
                    model = new StoreModel();
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
            model.Message = utilityHelper.ReadGlobalMessage("ManageStore", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoStore.Where(x => x.StoreID == ID).FirstOrDefault();
                    var refcount = UnitofWork.RepoStoreAdChoice.Where(x => x.StoreID == ID).Count();
                    refcount += UnitofWork.RepoAllowedStoreOption.Where(x => x.StoreID == ID).Count();
                    refcount += UnitofWork.RepoUserStore.Where(x => x.StoreID == ID).Count();
                    if (refcount > 0)
                    {
                        model.Message = utilityHelper.ReadGlobalMessage("ManageStore", "NotDelete");
                        return model;
                    }

                    if (item != null)
                    {
                        //_user.IsActive = (byte)Status.Deleted;
                        UnitofWork.RepoStore.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Delete");
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

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public UserStoreModel UserStoreSave(UserStoreModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {

                #region check duplicate 

                if (UnitofWork.RepoUserStore.Where(x => x.StoreID == model.StoreID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUserStore", "StoreExist");
                    return model;
                }
                if (UnitofWork.RepoUserStore.Where(x => x.StoreID == model.StoreID && x.UserID == model.UserID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageUserStore", "Duplicate");
                    return model;
                }
                #endregion

                #region Save
                UserStore dbuserStore = new UserStore();
                dbuserStore.UserID = model.UserID;
                dbuserStore.StoreID = model.StoreID;
                UnitofWork.RepoUserStore.Add(dbuserStore);
                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Save");
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
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        public TransactionMessage DeleteUserStore(string encryptedId, string userId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("ManageUserStore", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int StoreId = Convert.ToInt32(encryptedId.ToDecrypt());
                    int _userId = Convert.ToInt32(userId);
                    var item = UnitofWork.RepoUserStore.Where(x => x.StoreID == StoreId && x.UserID == _userId).FirstOrDefault();

                    var refCount = UnitofWork.RepoStoreAdChoice.Where(x => x.StoreID == StoreId && x.UserID == _userId).Count();

                    if (refCount > 0)
                    {
                        model.Message = utilityHelper.ReadGlobalMessage("ManageUserStore", "NotDelete");
                        return model;
                    }

                    if (item != null)
                    {
                        UnitofWork.RepoUserStore.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageUserStore", "Delete");
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

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<StoreModel> GetMissingEntryList(ref DataPagingModel dataPaging)
        {
            List<StoreModel> model = new List<StoreModel>();
            var list = UnitofWork.RepoStore.Where(x => UnitofWork.RepoStoreAdChoice.Where(c => c.StoreID == x.StoreID).Count() == 0);
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key.ToLower() == "ownergroup")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Ownergroup.ToLower().Contains(value));
                    }
                    //if (item.Key.ToLower() == "circulation")
                    //{
                    //    string value = item.Value.ToString().ToLower().Trim();
                    //    list = list.Where(x => x.Circulation.ToLower().Contains(value));
                    //}
                    if (item.Key.ToLower() == "city")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.City.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "state")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.State.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Storename.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "marketname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Market.Name.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "userid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.UserStores.Where(s => s.UserID == value && s.StoreID == x.StoreID).Count() > 0);
                    }
                    if (item.Key.ToLower() == "storeid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreID == value);
                    }
                    if (item.Key.ToLower() == "address")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.City.ToLower().Contains(value) || x.State.ToLower().Contains(value) || x.Market.Name.ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "storecode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => (x.StoreCode ?? "").ToLower().Contains(value));
                    }
                    if (item.Key.ToLower() == "artworkcode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => (x.ArtworkCode ?? "").ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "ownergroup":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Ownergroup);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Ownergroup);
                    }
                    break;
                case "storeid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreID);
                    }
                    break;
                //case "circulation":
                //    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                //    {
                //        list = list.OrderBy(U => U.Circulation);
                //    }
                //    else
                //    {
                //        list = list.OrderByDescending(U => U.Circulation);
                //    }
                //    break;
                case "city":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.City);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.City);
                    }
                    break;
                case "state":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.State);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.State);
                    }
                    break;
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Storename);
                    }
                    break;
                case "marketname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Market.Name);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Market.Name);
                    }
                    break;
                case "storecode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreCode);
                    }
                    break;
                case "artworkcode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ArtworkCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ArtworkCode);
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

            model = list.Select(x => new StoreModel
            {
                StoreID = x.StoreID,
                Ownergroup = x.Ownergroup,
                // Circulation = x.Circulation,
                City = x.City,
                State = x.State,
                Storename = x.Storename,
                MarketID = x.MarketID ?? 0,
                MarketName = x.Market.Name,
                StoreCode = x.StoreCode ?? "",
                ArtworkCode = x.ArtworkCode ?? "",
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.StoreID.ToString().ToEnctypt());
            return model;
        }

        public StoreAdChoiceListModel UpdateMissingStoreEntry(int dropNumber)
        {
            List<StoreAdChoiceModel> newStoreAdChoiceModelList = new List<StoreAdChoiceModel>();
            TransactionMessage transMessage = new TransactionMessage();
            transMessage.Status = MessageStatus.Error;
            transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Failed");

            StoreAdChoiceListModel storeAdChoiceListModel = new StoreAdChoiceListModel()
            {
                storAdChoiceList = newStoreAdChoiceModelList,
                TransMessage = transMessage,
            };
            try
            {

                //AdMonth currentMonth = UnitofWork.RepoAdMonth.Where(m => m.Month == utilityHelper.CurrentDateTime.Month && m.Year == utilityHelper.CurrentDateTime.Year && m.DropNumber == dropNumber).FirstOrDefault();
                AdMonth currentMonth = UnitofWork.RepoAdMonth.Where(m => m.LockOutStartDate <= utilityHelper.CurrentDateTime && m.LockOutEndDate >= utilityHelper.CurrentDateTime && m.DropNumber == dropNumber).FirstOrDefault();

                List<StoreAdChoice> currentMonthStoreAdChoice = new List<StoreAdChoice>();
                if (currentMonth != null)
                {
                    currentMonthStoreAdChoice = UnitofWork.RepoStoreAdChoice.Where(c => c.AdMonthID == currentMonth.AdMonthID).ToList();
                }
                else
                {
                    transMessage.Status = MessageStatus.Error;
                    transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "NoCurrentMonthPlan");
                    return storeAdChoiceListModel;
                }

                DateTime CurrentMonthDate = new DateTime(currentMonth.Year ?? 2016, currentMonth.Month ?? 01, 1);
                DateTime previousMonthDate = CurrentMonthDate.AddMonths(-1);



                AdMonth prevoiousMonth = UnitofWork.RepoAdMonth.Where(m => m.Month == previousMonthDate.Month && m.DropNumber == dropNumber).FirstOrDefault();

                List<StoreAdChoice> previousMonthStoreAdChoice = new List<StoreAdChoice>();
                if (prevoiousMonth != null)
                {
                    previousMonthStoreAdChoice = UnitofWork.RepoStoreAdChoice.Where(c => c.AdMonthID == prevoiousMonth.AdMonthID).ToList();
                }
                else
                {
                    transMessage.Status = MessageStatus.Error;
                    transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "NoPreviousDropNo");
                   // return storeAdChoiceListModel;
                }

                List<Store> storeList = UnitofWork.RepoStore.GetAll().Where(s => currentMonthStoreAdChoice.Where(y => y.StoreID == s.StoreID).Count() <= 0).ToList();
                List<UserStore> userList = UnitofWork.RepoUserStore.Where(u => u.User.IsActive == true).ToList();
                var adOptionList = _storeAdOption.GetList().Where(x => x.InActive == true).ToList();
                var con = storeList.Where(s => userList.Any(u => u.StoreID == s.StoreID)).Count();
                var ExceptionCount = UnitofWork.RepoExceptionReport.Where(e => e.MonthId == currentMonth.AdMonthID).Count();
                int UpdateStoreCount = 0;
                #region ValidUserStoreList
                if (con > 0 && storeList.Count > 0 && ExceptionCount <= 0)
                {
                    #region AssignAdchoice
                    foreach (Store store in storeList)
                    {
                        #region NewBaseObject
                        StoreAdChoiceModel newstoreAdChoice = new StoreAdChoiceModel()
                        {
                            AdMonthID = currentMonth.AdMonthID,
                            StoreID = store.StoreID,
                            Storename = store.Storename,
                            MarketName = store.Market.Name,
                            IPAddress = utilityHelper.IpAddress(),
                            Device = utilityHelper.UserDevice(),
                            Browser = utilityHelper.UserBrowser(),
                            TimeStamp = utilityHelper.CurrentDateTime.ToTimeSpan(),
                            InHomeDate = currentMonth.CorpInHomeDate,
                            CouponID = currentMonth.AdCoupnID,
                            CouponName = currentMonth.Coupon != null ? currentMonth.Coupon.CouponText : "",
                        };
                        // newstoreAdChoice.TransMessage = model;
                        #endregion NewBaseObject

                        var user = userList.Where(u => u.StoreID == store.StoreID).FirstOrDefault();
                        if (user != null)
                        {
                            #region if(AssignAccordingtoPreviousMonth)
                            StoreAdChoice previousStoreAdChoice = previousMonthStoreAdChoice.Where(c => c.StoreID == store.StoreID).FirstOrDefault();
                            if (previousStoreAdChoice != null)
                            {
                                #region if(PriviosmonthplanisFollwCorp)
                                if (previousStoreAdChoice.FollowedCorporate.HasValue && previousStoreAdChoice.FollowedCorporate.Value)
                                {
                                    var ADOPTION = adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).ToList();
                                    if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).Count() > 0)
                                    {
                                        newstoreAdChoice.AdOptionID = currentMonth.AdOptionID;
                                        newstoreAdChoice.ADOptionName = currentMonth.ADOption.ADOptionName;
                                        newstoreAdChoice.FollowedCorporate = true;
                                        newstoreAdChoice.ChoiceInitials = previousStoreAdChoice.ChoiceInitials;
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousMonthFollwCorporate"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                            IsAnyAdChoiseAssigned = true,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                        UpdateStoreCount++;
                                    }
                                    else
                                    {
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "CurrentAdOptionNotAllowed"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                    }
                                }
                                #endregion if(PriviosmonthplanisFollwCorp)

                                #region if(PriviosmonthplanisNotPrinting)
                                if (previousStoreAdChoice.NotPrinting.HasValue && previousStoreAdChoice.NotPrinting.Value)
                                {
                                    AdOptionModel admodel = new AdOptionModel();
                                    admodel = _AdOption.GetSpecialDetail(((byte)SpecialAddOptions.NotPrinting).ToString());
                                    var ADOPTION = adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).ToList();
                                    if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).Count() > 0)
                                    {

                                        newstoreAdChoice.AdOptionID = admodel != null ? admodel.ADOptionID : 0;
                                        newstoreAdChoice.ADOptionName = admodel != null ? currentMonth.ADOption.ADOptionName : "";
                                        newstoreAdChoice.NotPrinting = true;
                                        newstoreAdChoice.ChoiceInitials = previousStoreAdChoice.ChoiceInitials;
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousMonthNotPrinting"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                            IsAnyAdChoiseAssigned = true,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                        UpdateStoreCount++;
                                    }
                                    else
                                    {
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "CurrentAdOptionNotPrintingAllowed"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                    }
                                }
                                #endregion if(PriviosmonthplanisFollwCorp)

                                #region if(PriviosmonthplanisSelfDistributing)
                                if (previousStoreAdChoice.OwnDistribution.HasValue && previousStoreAdChoice.OwnDistribution.Value)
                                {
                                    AdOptionModel admodel = new AdOptionModel();
                                    admodel = _AdOption.GetSpecialDetail(((byte)SpecialAddOptions.SelfDistributing).ToString());
                                    var ADOPTION = adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).ToList();
                                    if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).Count() > 0)
                                    {

                                        newstoreAdChoice.AdOptionID = admodel != null ? admodel.ADOptionID : 0;
                                        newstoreAdChoice.ADOptionName = admodel != null ? currentMonth.ADOption.ADOptionName : "";
                                        newstoreAdChoice.NotPrinting = true;
                                        newstoreAdChoice.ChoiceInitials = previousStoreAdChoice.ChoiceInitials;
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousMonthSelfdistributing"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                            IsAnyAdChoiseAssigned = true,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                        UpdateStoreCount++;
                                    }
                                    else
                                    {
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "CurrentAdOptionNotSelfAllowed"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                    }
                                }
                                #endregion if(PriviosmonthplanisFollwCorp)

                                #region if(!PriviosmonthplanisFollwCorp)
                                else
                                {
                                    if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == previousStoreAdChoice.AdOptionID).Count() > 0)
                                    {
                                        #region if(PreviousMonthCouponExistinCurentMonth)                        
                                        if (currentMonth.Coupons.Where(c => c.CouponID == previousStoreAdChoice.CouponID).Count() > 0)
                                        {
                                            newstoreAdChoice.AdOptionID = previousStoreAdChoice.AdOptionID;
                                            newstoreAdChoice.ADOptionName = previousStoreAdChoice.ADOption.ADOptionName;
                                            newstoreAdChoice.CouponID = previousStoreAdChoice.CouponID;
                                            newstoreAdChoice.CouponName = previousStoreAdChoice.Coupon != null ? previousStoreAdChoice.Coupon.CouponText : "";
                                            newstoreAdChoice.ChoiceInitials = previousStoreAdChoice.ChoiceInitials;
                                            ExceptionReport exceptionReport = new ExceptionReport()
                                            {
                                                StoreId = store.StoreID,
                                                MonthId = currentMonth.AdMonthID,
                                                Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousMonthAdOption"),
                                                CreatedOn = utilityHelper.CurrentDateTime,
                                                IsAnyAdChoiseAssigned = true,
                                            };
                                            newstoreAdChoice.Exception = exceptionReport;
                                        }
                                        #endregion if(PreviousMonthCouponExistinCurentMonth)
                                        #region if(!PreviousMonthCouponExistinCurentMonth)
                                        else
                                        {
                                            if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).Count() > 0)
                                            {
                                                newstoreAdChoice.AdOptionID = currentMonth.AdOptionID;
                                                newstoreAdChoice.FollowedCorporate = true;
                                                newstoreAdChoice.ADOptionName = currentMonth.ADOption.ADOptionName;
                                                newstoreAdChoice.ChoiceInitials = previousStoreAdChoice.ChoiceInitials;
                                                ExceptionReport exceptionReport = new ExceptionReport()
                                                {
                                                    StoreId = store.StoreID,
                                                    MonthId = currentMonth.AdMonthID,
                                                    Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousMonthCouponNotMatch"),
                                                    CreatedOn = utilityHelper.CurrentDateTime,
                                                    IsAnyAdChoiseAssigned = true,
                                                };
                                                newstoreAdChoice.Exception = exceptionReport;
                                            }
                                            else
                                            {
                                                ExceptionReport exceptionReport = new ExceptionReport()
                                                {
                                                    StoreId = store.StoreID,
                                                    MonthId = currentMonth.AdMonthID,
                                                    Description = utilityHelper.ReadGlobalMessage("ManageStore", "CurrentAdOptionNotAllowed"),
                                                    CreatedOn = utilityHelper.CurrentDateTime,

                                                };
                                                newstoreAdChoice.Exception = exceptionReport;
                                            }
                                        }
                                        #endregion if(!PreviousMonthCouponExistinCurentMonth)
                                        UpdateStoreCount++;
                                    }
                                    else
                                    {
                                        ExceptionReport exceptionReport = new ExceptionReport()
                                        {
                                            StoreId = store.StoreID,
                                            MonthId = currentMonth.AdMonthID,
                                            Description = utilityHelper.ReadGlobalMessage("ManageStore", "PreviousAdOptionNotAllowed"),
                                            CreatedOn = utilityHelper.CurrentDateTime,
                                        };
                                        newstoreAdChoice.Exception = exceptionReport;
                                    }
                                }
                                #endregion if(!PriviosmonthplanisFollwCorp)
                            }
                            #endregion if(AssignAccordingtoPreviousMonth)
                            #region if(NoPlanOnPreiousMonth)
                            else
                            {
                                if (adOptionList.Where(ao => ao.StoreId == store.StoreID && ao.OptionId == currentMonth.AdOptionID).Count() > 0)
                                {
                                    newstoreAdChoice.AdOptionID = currentMonth.AdOptionID;
                                    newstoreAdChoice.FollowedCorporate = true;
                                    newstoreAdChoice.ADOptionName = currentMonth.ADOption.ADOptionName;
                                    ExceptionReport exceptionReport = new ExceptionReport()
                                    {
                                        StoreId = store.StoreID,
                                        MonthId = currentMonth.AdMonthID,
                                        Description = utilityHelper.ReadGlobalMessage("ManageStore", "NoPreviousPlan"),
                                        CreatedOn = utilityHelper.CurrentDateTime,
                                        IsAnyAdChoiseAssigned = true,
                                    };
                                    newstoreAdChoice.Exception = exceptionReport;
                                    UpdateStoreCount++;
                                }
                                else
                                {

                                    ExceptionReport exceptionReport = new ExceptionReport()
                                    {
                                        StoreId = store.StoreID,
                                        MonthId = currentMonth.AdMonthID,
                                        Description = utilityHelper.ReadGlobalMessage("ManageStore", "CurrentAdOptionNotAllowed"),
                                        CreatedOn = utilityHelper.CurrentDateTime,
                                    };
                                    newstoreAdChoice.Exception = exceptionReport;

                                }
                            }
                            #endregion if(NoPlanOnPreiousMonth)

                            newstoreAdChoice.UserID = user.User.UserID;
                            newStoreAdChoiceModelList.Add(newstoreAdChoice);

                        }
                        else
                        {
                            #region NoUser
                            ExceptionReport exceptionReport = new ExceptionReport()
                            {
                                StoreId = store.StoreID,
                                MonthId = currentMonth.AdMonthID,
                                Description = utilityHelper.ReadGlobalMessage("ManageStore", "UnDefinedUser"),
                                CreatedOn = utilityHelper.CurrentDateTime,
                            };
                            newstoreAdChoice.Exception = exceptionReport;
                            #endregion NoUser
                            newStoreAdChoiceModelList.Add(newstoreAdChoice);
                        }
                    }
                    #endregion AssignAdchoice
                    if (UpdateStoreCount > 0)
                    {
                        transMessage.Status = MessageStatus.Success;
                        transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Update");
                    }
                    else
                    {
                        transMessage.Status = MessageStatus.Error;
                        transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "NoEntry");
                    }
                }
                #endregion ValidUserStoreList
                #region NoUserFound
                else
                {
                    transMessage.Status = MessageStatus.Error;
                    transMessage.Message = utilityHelper.ReadGlobalMessage("ManageStore", "NoEntry");
                    return storeAdChoiceListModel;
                }
                #endregion NoUserFound
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            storeAdChoiceListModel.storAdChoiceList = storeAdChoiceListModel.storAdChoiceList.OrderBy(i => (i.Exception == null ? i.StoreID.ToString() : i.Exception.Description)).ThenBy(i => i.StoreID).ToList();
            return storeAdChoiceListModel;
        }

        public TransactionMessage UpdateMissingEntry(List<StoreAdChoiceModel> storeAdChoiceList)
        {
            List<MailTamplate> mailTamplateList = new List<MailTamplate>();
            //List<UserStore> userList = UnitofWork.RepoUserStore.Where(u => u.User.IsActive == true).ToList();

            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Failed");
            int CurrentAdMonthID = 0;
            try
            {
                foreach (var storeAdChoiceModel in storeAdChoiceList)
                {
                    if (storeAdChoiceModel.Exception != null && (storeAdChoiceModel.Exception.IsAnyAdChoiseAssigned ?? false))
                    {
                        StoreAdChoice storeAdChoice = new StoreAdChoice()
                        {
                            AdMonthID = storeAdChoiceModel.AdMonthID,
                            AdOptionID = storeAdChoiceModel.AdOptionID,
                            CouponID = storeAdChoiceModel.CouponID,
                            ChoiceInitials = storeAdChoiceModel.ChoiceInitials,
                            Device = storeAdChoiceModel.Device,
                            FollowedCorporate = storeAdChoiceModel.FollowedCorporate,
                            InHomeDate = storeAdChoiceModel.InHomeDate,
                            IPAddress = storeAdChoiceModel.IPAddress,
                            NotPrinting = storeAdChoiceModel.NotPrinting,
                            OwnDistribution = storeAdChoiceModel.OwnDistribution,
                            StoreID = storeAdChoiceModel.StoreID,
                            TimeStamp = storeAdChoiceModel.TimeStamp,
                            UserID = storeAdChoiceModel.UserID,
                        };
                        UnitofWork.RepoStoreAdChoice.Add(storeAdChoice);
                        CurrentAdMonthID = storeAdChoiceModel.AdMonthID;

                        StoreAdChoiceHistory dbHistory = new StoreAdChoiceHistory();
                        //dbHistory.ChoiceID = dbItem.ChoiceID;
                        dbHistory.AdMonthID = storeAdChoiceModel.AdMonthID;
                        dbHistory.StoreID = storeAdChoiceModel.StoreID;
                        dbHistory.UserID = (int)SessionHelper.UserId;
                        dbHistory.AdOptionID = storeAdChoiceModel.AdOptionID;
                        dbHistory.TimeStamp = new TimeSpan();
                        dbHistory.IPAddress = utilityHelper.IpAddress();
                        dbHistory.Device = utilityHelper.UserDevice();
                        dbHistory.Browser = utilityHelper.UserBrowser();
                        dbHistory.InHomeDate = utilityHelper.CurrentDateTime;
                        dbHistory.ChoiceInitials = storeAdChoiceModel.ChoiceInitials;
                        if (storeAdChoiceModel.CouponID > 0)
                        {
                            dbHistory.CouponID = storeAdChoiceModel.CouponID;
                        }

                        storeAdChoice.StoreAdChoiceHistories.Add(dbHistory);
                    }
                    else
                    {
                        if (storeAdChoiceModel.Exception != null && storeAdChoiceModel.Exception.Description != "-")
                        {
                            ExceptionReport exceptionReport = new ExceptionReport()
                            {
                                StoreId = storeAdChoiceModel.StoreID,
                                MonthId = storeAdChoiceModel.AdMonthID,
                                Description = storeAdChoiceModel.Exception.Description,
                                CreatedOn = utilityHelper.CurrentDateTime,
                            };
                            UnitofWork.RepoExceptionReport.Add(exceptionReport);
                        }
                    }
                }

                UnitofWork.Commit();
                #region test
                var mailstoreAdChoiceList = storeAdChoiceList.Where(sm => sm.Exception != null && (sm.Exception.IsAnyAdChoiseAssigned ?? false));
                // List<Store> storeList = UnitofWork.RepoStore.Where(s=> mailstoreAdChoiceList.Any(sm=>sm.StoreID == s.StoreID)).ToList();
                List<User> userList = UnitofWork.RepoUser.Where(u => u.IsActive == true).ToList();
                AdMonth adMonth = UnitofWork.RepoAdMonth.Where(m => m.AdMonthID == CurrentAdMonthID).FirstOrDefault();

                foreach (var user in userList)
                {
                    #region MailTamplate          
                    string subject = utilityHelper.ReadGlobalMessage("MailTamplateSubject", "AutoStoreAdChoiceSelection");
                    subject = subject.Replace("[CurrentMonth]", (new DateTime(adMonth.Year ?? 2000, adMonth.Month ?? utilityHelper.CurrentDateTime.Month, 1)).ToString("MMMM, yyyy"));
                    subject = subject.Replace("[LockoutEndDate]", adMonth.LockOutEndDate.HasValue ? adMonth.LockOutEndDate.Value.ToString(ApplicationConstant.DateFormatDisplay) : "");
                    subject = subject.Replace("[DropNumber]", adMonth.DropNumber.ToString());

                    string template = utilityHelper.ReadFromFile("AutoManageCoupon.html");
                    template = template.Replace("[UserName]", user.Ownername);
                    template = template.Replace("[CurrentMonth]", (new DateTime(adMonth.Year ?? 2000, adMonth.Month ?? utilityHelper.CurrentDateTime.Month, 1)).ToString("MMMM, yyyy"));
                    template = template.Replace("[DropNumber]", adMonth.DropNumber.ToString());
                    template = template.Replace("[CorpPlanText]", adMonth.CorpPlanText);
                    template = template.Replace("[PetPartnerInfo]", adMonth.PetpartnerInfo);
                    template = template.Replace("[LockoutEndDate]", adMonth.LockOutEndDate.HasValue ? adMonth.LockOutEndDate.Value.ToString(ApplicationConstant.DateFormatDisplay) : "");
                    template = template.Replace("[SiteUrl]", utilityHelper.SiteUrl());
                    string storeDetail = "";

                    var userstoreAdChoiceList = mailstoreAdChoiceList.Where(s => user.UserStores.Any(us => us.StoreID == s.StoreID)).ToList();
                    userstoreAdChoiceList.ForEach(s =>
                    {
                        storeDetail = storeDetail + "<br/> Store " + s.StoreID + " " + (s.FollowedCorporate ? "Following Corp" : s.ADOptionName.ToString());
                    });
                    template = template.Replace("[StoreDetail]", storeDetail);
                    //template = template.Replace("[StoreName]", storeAdChoiceModel.Storename ?? "");
                    MailTamplate MailTamplate = new MailTamplate()
                    {
                        Subject = subject,
                        EmailID = user.Email,
                        Tamplate = template,
                    };
                    if (userstoreAdChoiceList.Count() > 0)
                        mailTamplateList.Add(MailTamplate);
                    #endregion MailTamplate
                }
                #endregion test

                mailTamplateList.ForEach(t => { Framework.utilityHelper.SendMail(t.EmailID, t.Subject, t.Tamplate); });
                //mailTamplateList.ForEach(t => { Framework.utilityHelper.SendMail("hgupta@synapseindia.email", t.Subject, t.Tamplate); });
                model.Status = MessageStatus.Success;
                model.Message = utilityHelper.ReadGlobalMessage("ManageStore", "Update");
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }

            return model;
        }
    }


}

//if (store.StoreID == 1 || store.StoreID == 2 || store.StoreID == 3)
//{

//}