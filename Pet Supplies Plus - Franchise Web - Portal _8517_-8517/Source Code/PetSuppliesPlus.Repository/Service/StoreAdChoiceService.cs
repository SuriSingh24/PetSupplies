using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Store;
using PetSuppliesPlus.Models;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IStoreAdChoiceService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<StoreAdChoiceModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">StoreAdChoicehistoryModel</param>
        /// <returns>StoreAdChoicehistoryModel</returns>
        StoreAdChoiceModel Save(StoreAdChoiceModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        StoreAdChoiceModel GetDetail(string encryptedId);

        List<StoreAdChoiceModel> GetDetail(int userId, int monthId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="adMonthId"></param>
        /// <returns></returns>
        TransactionMessage Save(List<StoreAdChoiceModel> model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        List<StoreAdChoiceModel> GetHistoryList(ref DataPagingModel dataPaging);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        List<StoreAdChoiceModel> GetStoreAdChoiceHistoryList(ref DataPagingModel dataPaging);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        List<StoreAdChoiceModel> GetMissingEntryList(ref DataPagingModel dataPaging);
    }
    public class StoreAdChoiceService : IStoreAdChoiceService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public StoreAdChoiceService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        public TransactionMessage Delete(string encryptedId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int StoreAdChoiceId = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoStoreAdChoice.Where(x => x.ChoiceID == StoreAdChoiceId).FirstOrDefault();
                    if (item != null)
                    {
                        UnitofWork.RepoStoreAdChoice.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "Delete");
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
        public StoreAdChoiceModel GetDetail(string encryptedId)
        {
            StoreAdChoiceModel model = new StoreAdChoiceModel();
            try
            {
                int choiceId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoStoreAdChoice.Where(x => x.ChoiceID == choiceId).Select(x => new StoreAdChoiceModel
                {
                    AdMonthID = x.AdMonthID ?? 0,
                    ADOptionName = x.ADOption.ADOptionName,
                    Storename = x.Store.Storename,
                    StoreID = x.StoreID ?? 0,
                    AdOptionID = x.AdOptionID ?? 0,
                    TimeStamp = x.TimeStamp ?? new TimeSpan(),
                    UserID = x.UserID ?? 0,
                    Ownername = x.User.Ownername,
                    IPAddress = x.IPAddress,
                    Device = x.Device,
                    Browser = x.Browser,
                    InHomeDate = x.InHomeDate ?? new DateTime(),
                    ChoiceInitials = x.ChoiceInitials,
                    NotPrinting = x.NotPrinting ?? false,
                    OwnDistribution = x.OwnDistribution ?? false,
                    FollowedCorporate = x.FollowedCorporate ?? false,
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.ChoiceID.ToString().ToEnctypt();
                }
                else
                {
                    model = new StoreAdChoiceModel();
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }
        public StoreAdChoiceModel Save(StoreAdChoiceModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                #region check duplicate 
                if (UnitofWork.RepoStoreAdChoice.Where(x => x.UserID == model.UserID && x.StoreID == model.StoreID && x.AdMonthID == model.AdMonthID && x.AdOptionID == model.AdOptionID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "Duplicate");
                    return model;
                }
                #endregion
                bool isSave = false;

                //Data.StoreAdChoice dbStoreAdChice = UnitofWork.RepoStoreAdChoice.Where(x => x.UserID==model.UserID && x.StoreID==model.StoreID && x.AdMonthID==model.AdMonthID && x.AdOptionID==model.AdOptionID).FirstOrDefault();
                ////user table 
                //if (dbStoreAdChice == null)
                //{
                #region Save
                Data.StoreAdChoice dbStoreAdChice = new Data.StoreAdChoice();
                dbStoreAdChice.AdMonthID = model.AdMonthID;
                dbStoreAdChice.StoreID = model.StoreID;
                dbStoreAdChice.AdOptionID = model.AdOptionID;
                dbStoreAdChice.UserID = model.UserID;
                dbStoreAdChice.TimeStamp = new TimeSpan();
                dbStoreAdChice.IPAddress = utilityHelper.IpAddress();
                dbStoreAdChice.Device = utilityHelper.UserDevice();
                dbStoreAdChice.Browser = utilityHelper.UserBrowser();
                dbStoreAdChice.InHomeDate = model.InHomeDate;
                dbStoreAdChice.ChoiceInitials = model.ChoiceInitials;
                dbStoreAdChice.NotPrinting = model.NotPrinting;
                dbStoreAdChice.OwnDistribution = model.OwnDistribution;
                dbStoreAdChice.FollowedCorporate = model.FollowedCorporate;
                UnitofWork.RepoStoreAdChoice.Add(dbStoreAdChice);
                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "Save");
                isSave = true;
                #endregion
                //}
                //else
                //{
                //    #region Update
                //    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "Update");
                //    #endregion
                //}

                //  #region Set Value
                //  dbStoreAdChice = new Data.StoreAdChoice();
                //  dbStoreAdChice.ChoiceID = model.ChoiceID;
                //  dbStoreAdChice.AdMonthID = model.AdMonthID;
                //  dbStoreAdChice.StoreID = model.StoreID;
                //  dbStoreAdChice.AdOptionID = model.AdOptionID;
                //  dbStoreAdChice.UserID = Convert.ToInt32( SessionHelper.UserId) ;
                //  dbStoreAdChice.TimeStamp = new TimeSpan();
                //  dbStoreAdChice.IPAddress = utilityHelper.IpAddress();
                //  dbStoreAdChice.Device = utilityHelper.UserDevice();
                //  dbStoreAdChice.Browser = utilityHelper.UserBrowser();
                //  dbStoreAdChice.InHomeDate = model.InHomeDate;
                //  dbStoreAdChice.FollowedCorporate = model.FollowedCorporate;
                //  dbStoreAdChice.ChoiceInitials = model.ChoiceInitials;
                ////  dbStoreAdChice.CouponID = model.CouponID;
                //  #endregion

                UnitofWork.Commit();
                model.TransMessage.Status = MessageStatus.Success;

            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        public List<StoreAdChoiceModel> GetList(ref DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            var list = UnitofWork.RepoStoreAdChoice.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "inhomedate")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.InHomeDate.ToString().Contains(value));
                    }
                    if (item.Key == "admonthid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.AdMonthID.ToString() == value);
                    }
                    if (item.Key == "userid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.UserID.ToString() == value);
                    }
                    if (item.Key == "storeid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreID.ToString() == value);
                    }
                    if (item.Key == "monthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID.HasValue && x.AdMonth.Month == value);
                    }

                    if (item.Key == "user")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.Contains(value));
                    }
                    if (item.Key == "adoptionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOption.ADOptionName.Contains(value));
                    }
                    if (item.Key == "username")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "ipaddress")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.IPAddress.ToLower().Contains(value));
                    }
                    if (item.Key == "device")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Device.ToLower().Contains(value));
                    }
                    if (item.Key == "browser")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Browser.ToLower().Contains(value));
                    }
                    if (item.Key == "choiceinitials")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ChoiceInitials.ToLower().Contains(value));
                    }
                    if (item.Key == "notprinting")
                    {
                        var value = Convert.ToBoolean(item.Value);
                        list = list.Where(x => x.NotPrinting == value);
                    }
                    if (item.Key == "followedcorporate")
                    {
                        var value = Convert.ToBoolean(item.Value);
                        list = list.Where(x => x.FollowedCorporate == value);
                    }
                    if (item.Key == "owndistribution")
                    {
                        var value = Convert.ToBoolean(item.Value);
                        list = list.Where(x => x.NotPrinting == value);
                    }

                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.Storename);
                    }
                    break;
                case "choiceid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ChoiceID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ChoiceID);
                    }
                    break;
                case "followedcorporate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.FollowedCorporate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.FollowedCorporate);
                    }
                    break;
                case "adoptionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOption.ADOptionName);
                    }
                    break;
                case "username":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.User.Ownername);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.User.Ownername);
                    }
                    break;
                case "ipaddress":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.IPAddress);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.IPAddress);
                    }
                    break;

                case "device":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Device);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Device);
                    }

                    break;
                case "browser":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Browser);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Browser);
                    }
                    break;
                case "inhomedate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.InHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.InHomeDate);
                    }
                    break;
                case "admonthid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;
                case "notprinting":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;

                case "owndistribution":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
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

            model = list.ToList().Select(x => new StoreAdChoiceModel
            {
                AdMonthID = x.AdMonthID ?? 0,
                Storename = x.Store.Storename,
                ADOptionName = x.ADOption != null ? x.ADOption.ADOptionName : "",
                TimeStamp = x.TimeStamp ?? new TimeSpan(),
                Ownername = x.User.Ownername,
                IPAddress = x.IPAddress,
                Device = x.Device,
                Browser = x.Browser,
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceInitials = x.ChoiceInitials,
                NotPrinting = x.NotPrinting ?? false,
                OwnDistribution = x.OwnDistribution ?? false,
                FollowedCorporate = x.FollowedCorporate ?? false,
                AdMonthDetail = x.AdMonthID.HasValue ? new Model.AdMonth.AdMonthModel()
                {
                    Month = x.AdMonth.Month ?? 0
                } : new Model.AdMonth.AdMonthModel(),
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }
        public List<StoreAdChoiceModel> GetDetail(int userId, int monthId)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            try
            {
                #region get detail
                model = UnitofWork.RepoStoreAdChoice.Where(x => x.AdMonthID == monthId && x.UserID == userId).Select(x => new StoreAdChoiceModel
                {
                    AdMonthID = x.AdMonthID ?? 0,
                    ADOptionName = x.ADOption.ADOptionName,
                    Storename = x.Store.Storename,
                    StoreID = x.StoreID ?? 0,
                    AdOptionID = x.AdOptionID ?? 0,
                    TimeStamp = x.TimeStamp ?? new TimeSpan(),//DateTime.Now.Hour,DateTime.Now.Minute,DateTime.Now.Second),
                    UserID = x.UserID ?? 0,
                    Ownername = x.User.Ownername,
                    IPAddress = x.IPAddress,
                    Device = x.Device,
                    Browser = x.Browser,
                    InHomeDate = x.InHomeDate ?? new DateTime(),
                    ChoiceInitials = x.ChoiceInitials,
                    NotPrinting = x.NotPrinting ?? false,
                    OwnDistribution = x.OwnDistribution ?? false,
                    FollowedCorporate = x.FollowedCorporate ?? false,
                    CouponID = x.CouponID ?? 0,
                    CouponName = x.CouponID.HasValue ? x.Coupon.CouponText : "",
                }).ToList();
                #endregion

                model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }
        public TransactionMessage Save(List<StoreAdChoiceModel> model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            try
            {
                int adMonthId = 0;
                DateTime LockOutEndDate = new DateTime();

                if (model != null && model.Count() > 0)
                {
                    adMonthId = model[0].AdMonthID;
                }

                var dbAdChoice = UnitofWork.RepoStoreAdChoice.Where(x => x.UserID == (int)SessionHelper.UserId && x.AdMonthID == adMonthId).ToList();
                bool isSave = true;
                if (dbAdChoice.Count() > 0) // for save
                {
                    isSave = false;
                }

                foreach (var item in model)
                {
                    // check duplicate 
                    StoreAdChoice dbItem = dbAdChoice.Where(x => x.StoreID == item.StoreID).FirstOrDefault(); //&& x.ChoiceID == item.ChoiceID
                    if (dbItem == null)
                    {
                        dbItem = new StoreAdChoice();
                        dbItem.AdMonthID = item.AdMonthID;
                        dbItem.UserID = (int)SessionHelper.UserId;
                        dbItem.StoreID = item.StoreID;

                        UnitofWork.RepoStoreAdChoice.Add(dbItem);
                    }
                    dbItem.AdOptionID = item.AdOptionID;
                    dbItem.TimeStamp = utilityHelper.CurrentDateTime.ToTimeSpan();
                    dbItem.IPAddress = utilityHelper.IpAddress();
                    dbItem.Device = utilityHelper.UserDevice();
                    dbItem.Browser = utilityHelper.UserBrowser();
                    dbItem.InHomeDate = item.InHomeDate.HasValue ? item.InHomeDate.Value.Year > 2000 ? (DateTime?)item.InHomeDate : null : null;
                    dbItem.ChoiceInitials = item.ChoiceInitials;
                    dbItem.FollowedCorporate = item.UserAdChoice == UserAdChoice.FollowCorporate;
                    dbItem.NotPrinting = item.UserAdChoice == UserAdChoice.NotPrinting;
                    dbItem.OwnDistribution = item.UserAdChoice == UserAdChoice.DoingOwnDistribution;
                    if (item.CouponID > 0)
                    {
                        dbItem.CouponID = item.CouponID;
                    }
                    else
                    {
                        dbItem.CouponID = null;
                    }
                    // UnitofWork.Commit();
                    //// ad new record to history table
                    StoreAdChoiceHistory dbHistory = new StoreAdChoiceHistory();
                    //dbHistory.ChoiceID = dbItem.ChoiceID;
                    dbHistory.AdMonthID = adMonthId;
                    dbHistory.StoreID = item.StoreID;
                    dbHistory.UserID = (int)SessionHelper.UserId;
                    dbHistory.AdOptionID = item.AdOptionID;
                    dbHistory.TimeStamp = utilityHelper.CurrentDateTime.ToTimeSpan();
                    dbHistory.IPAddress = utilityHelper.IpAddress();
                    dbHistory.Device = utilityHelper.UserDevice();
                    dbHistory.Browser = utilityHelper.UserBrowser();
                    dbHistory.InHomeDate = item.InHomeDate.HasValue ? item.InHomeDate.Value.Year > 2000 ? (DateTime?)item.InHomeDate : null : null;
                    dbHistory.ChoiceInitials = item.ChoiceInitials;
                    if (item.CouponID > 0)
                    {
                        dbHistory.CouponID = item.CouponID;
                    }

                    dbItem.StoreAdChoiceHistories.Add(dbHistory);
                    //UnitofWork.RepoStoreAdChoiceHistory.Add(dbHistory);
                }
                UnitofWork.Commit();


                if (dbAdChoice.Count() > 0 && dbAdChoice[0].AdMonth != null)
                {
                    LockOutEndDate = dbAdChoice[0].AdMonth.LockOutEndDate.HasValue ? dbAdChoice[0].AdMonth.LockOutEndDate.Value : new DateTime();
                }
                else
                {
                    LockOutEndDate = Convert.ToDateTime(SessionHelper.LockOutDate);
                }

                bool OwnChoice = false;
                foreach (var item in model)
                {
                    if (item.UserAdChoice == UserAdChoice.DoingOwnDistribution)
                    {
                        OwnChoice = true;
                        break;
                    }
                }
                if (OwnChoice == true)
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "SpecialChoice").Replace("[User]", SessionHelper.UserName).Replace("[adrequest@petsuppliesplus.com]", "<a href='mailto:adrequest@petsuppliesplus.com'>adrequest@petsuppliesplus.com</a>");
                    TransMessage.Status = MessageStatus.Success;
                }
                else
                {
                    TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "Save").Replace("[EndDate]", LockOutEndDate.ToString(ApplicationConstant.DateFormatDisplay));
                    TransMessage.Status = MessageStatus.Success;
                }

            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
                TransMessage.Message = ex.Message;
            }
            return TransMessage;
        }
        public List<StoreAdChoiceModel> GetHistoryList(ref DataPagingModel dataPaging)
        {
            var userstorelist = UnitofWork.RepoUserStore.Where(s => s.UserID == SessionHelper.UserId);
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();

            var list = UnitofWork.RepoStoreAdChoice.GetAll().Where(x => userstorelist.Any(s => s.StoreID == x.StoreID));
            if (SessionHelper.IsAdmin)
            {
                list = UnitofWork.RepoStoreAdChoice.GetAll();
            }
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "inhomedate")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.InHomeDate.ToString().Contains(value));
                    }
                    if (item.Key == "admonthid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.AdMonthID.ToString() == value);
                    }
                    if (item.Key == "userid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.UserID.ToString() == value);
                    }
                    if (item.Key == "storeid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreID.ToString() == value);
                    }
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.ToLower().Contains(value));
                    }
                    if (item.Key == "couponname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Coupon != null && x.Coupon.CouponText.ToLower().Contains(value));
                    }
                    if (item.Key == "adoptionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOption != null && x.ADOption.ADOptionName.ToLower().Contains(value));
                    }
                    if (item.Key == "username")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "ipaddress")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.IPAddress.ToLower().Contains(value));
                    }
                    if (item.Key == "device")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Device.ToLower().Contains(value));
                    }
                    if (item.Key == "browser")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Browser.ToLower().Contains(value));
                    }
                    if (item.Key == "choiceinitials")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ChoiceInitials.ToLower().Contains(value));
                    }
                    if (item.Key == "Month")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => new DateTime(x.AdMonth.Year ?? 2000, x.AdMonth.Month ?? 0, 1).ToString("MMMM").ToLower().Contains(value));
                    }
                    if (item.Key == "Year")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => new DateTime(x.AdMonth.Year ?? 2000, x.AdMonth.Month ?? 0, 1).ToString("yyyy").Contains(value));
                    }
                    if (item.Key == "dropnumber")
                    {
                        int value = Convert.ToInt32(item.Value.ToString().ToLower().Trim());
                        list = list.Where(x => x.AdMonth.DropNumber == value);
                    }


                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.Storename);
                    }
                    break;
                case "couponname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.Where(U => U.Coupon != null).OrderBy(U => U.Coupon.CouponText);
                    }
                    else
                    {
                        list = list.Where(U => U.Coupon != null).OrderByDescending(U => U.Coupon.CouponText);
                    }
                    break;
                case "adoptionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOption.ADOptionName);
                    }
                    break;
                case "username":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.User.Ownername);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.User.Ownername);
                    }
                    break;
                case "ipaddress":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.IPAddress);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.IPAddress);
                    }
                    break;

                case "device":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Device);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Device);
                    }

                    break;
                case "browser":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Browser);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Browser);
                    }
                    break;
                case "inhomedate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.InHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.InHomeDate);
                    }
                    break;
                case "admonthid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;
                case "dropnumber":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.DropNumber);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.DropNumber);
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

            model = list.ToList().Select(x => new StoreAdChoiceModel
            {
                AdOptionID = x.AdOptionID,
                CouponID = x.CouponID,
                StoreID = x.StoreID ?? 0,
                AdMonthID = x.AdMonthID ?? 0,
                AdCouponName = x.Coupon != null ? x.Coupon.CouponText : "",
                Storename = x.Store != null ? x.Store.Storename : "",
                ADOptionName = x.ADOption != null ? x.ADOption.ADOptionName : "",
                Ownername = x.User != null ? x.User.Ownername : "",
                // Storename = x.StoreAdChoice != null ? x.StoreAdChoice.Store != null ? x.StoreAdChoice.Store.Storename : "" : "",
                //ADOptionName = x.StoreAdChoice != null ? x.StoreAdChoice.ADOption != null ? x.StoreAdChoice.ADOption.ADOptionName : "" : "",
                TimeStamp = x.TimeStamp ?? new TimeSpan(),
                //Ownername = x.StoreAdChoice != null ? x.StoreAdChoice.User.Ownername : "",
                IPAddress = x.IPAddress,
                Device = x.Device,
                Browser = x.Browser,
                DropNumber = x.AdMonth == null ? 0 : (x.AdMonth.DropNumber ?? 0),
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceInitials = x.ChoiceInitials ?? "",
                AdMonthDetail = x.AdMonthID.HasValue && x.AdMonth != null ? new Model.AdMonth.AdMonthModel()
                {
                    Year = x.AdMonth.Year ?? 2000,
                    Month = x.AdMonth.Month ?? 0
                } : new Model.AdMonth.AdMonthModel(),
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }

        public List<StoreAdChoiceModel> GetMissingEntryList(ref DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            var list = UnitofWork.RepoStoreAdChoice.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "inhomedate")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.InHomeDate.ToString().Contains(value));
                    }
                    if (item.Key == "admonthid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.AdMonthID.ToString() == value);
                    }
                    if (item.Key == "userid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.UserID.ToString() == value);
                    }
                    if (item.Key == "storeid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreID.ToString() == value);
                    }
                    if (item.Key == "monthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID.HasValue && x.AdMonth.Month == value);
                    }

                    if (item.Key == "user")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.Contains(value));
                    }
                    if (item.Key == "adoptionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOption.ADOptionName.Contains(value));
                    }
                    if (item.Key == "username")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "ipaddress")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.IPAddress.ToLower().Contains(value));
                    }
                    if (item.Key == "device")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Device.ToLower().Contains(value));
                    }
                    if (item.Key == "browser")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Browser.ToLower().Contains(value));
                    }
                    if (item.Key == "choiceinitials")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ChoiceInitials.ToLower().Contains(value));
                    }
                    if (item.Key == "notprinting")
                    {
                        var value = Convert.ToBoolean(item.Value);
                        list = list.Where(x => x.NotPrinting == value);
                    }
                    if (item.Key == "owndistribution")
                    {
                        var value = Convert.ToBoolean(item.Value);
                        list = list.Where(x => x.NotPrinting == value);
                    }

                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.Storename);
                    }
                    break;
                case "choiceid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ChoiceID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ChoiceID);
                    }
                    break;
                case "adoptionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOption.ADOptionName);
                    }
                    break;
                case "username":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.User.Ownername);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.User.Ownername);
                    }
                    break;
                case "ipaddress":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.IPAddress);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.IPAddress);
                    }
                    break;

                case "device":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Device);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Device);
                    }

                    break;
                case "browser":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Browser);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Browser);
                    }
                    break;
                case "inhomedate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.InHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.InHomeDate);
                    }
                    break;
                case "admonthid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;
                case "notprinting":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;

                case "owndistribution":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
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

            model = list.ToList().Select(x => new StoreAdChoiceModel
            {
                AdMonthID = x.AdMonthID ?? 0,
                Storename = x.Store.Storename,
                ADOptionName = x.ADOption != null ? x.ADOption.ADOptionName : "",
                TimeStamp = x.TimeStamp ?? new TimeSpan(),
                Ownername = x.User.Ownername,
                IPAddress = x.IPAddress,
                Device = x.Device,
                Browser = x.Browser,
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceInitials = x.ChoiceInitials,
                NotPrinting = x.NotPrinting ?? false,
                OwnDistribution = x.OwnDistribution ?? false,
                AdMonthDetail = x.AdMonthID.HasValue ? new Model.AdMonth.AdMonthModel()
                {
                    Month = x.AdMonth.Month ?? 0
                } : new Model.AdMonth.AdMonthModel(),
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }


        public List<StoreAdChoiceModel> GetStoreAdChoiceHistoryList(ref DataPagingModel dataPaging)
        {
            // var userstorelist = UnitofWork.RepoUserStore.Where(s => s.UserID == SessionHelper.UserId);
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();

            var list = UnitofWork.RepoStoreAdChoiceHistory.GetAll();

            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "inhomedate")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.InHomeDate.ToString().Contains(value));
                    }
                    if (item.Key == "admonthid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.AdMonthID.ToString() == value);
                    }
                    if (item.Key == "userid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.UserID.ToString() == value);
                    }
                    if (item.Key == "storeid")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreID.ToString() == value);
                    }
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreAdChoice.Store.Storename.ToLower().Contains(value));
                    }
                    if (item.Key == "adoptionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreAdChoice.ADOption != null && x.StoreAdChoice.ADOption.ADOptionName.ToLower().Contains(value));
                    }
                    if (item.Key == "username")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.StoreAdChoice.User.Ownername.Contains(value));
                    }
                    if (item.Key == "ipaddress")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.IPAddress.ToLower().Contains(value));
                    }
                    if (item.Key == "device")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Device.ToLower().Contains(value));
                    }
                    if (item.Key == "browser")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Browser.ToLower().Contains(value));
                    }
                    if (item.Key == "choiceinitials")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ChoiceInitials.ToLower().Contains(value));
                    }
                    if (item.Key == "Month")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => new DateTime(x.AdMonth.Year ?? 2000, x.AdMonth.Month ?? 0, 1).ToString("MMMM").ToLower().Contains(value));
                    }
                    if (item.Key == "Year")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => new DateTime(x.AdMonth.Year ?? 2000, x.AdMonth.Month ?? 0, 1).ToString("yyyy").Contains(value));
                    }
                    if (item.Key == "dropnumber")
                    {
                        int value = Convert.ToInt32(item.Value.ToString().ToLower().Trim());
                        list = list.Where(x => x.AdMonth.DropNumber == value);
                    }


                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreAdChoice.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreAdChoice.Store.Storename);
                    }
                    break;
                case "adoptionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreAdChoice.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreAdChoice.ADOption.ADOptionName);
                    }
                    break;
                case "username":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreAdChoice.User.Ownername);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreAdChoice.User.Ownername);
                    }
                    break;
                case "ipaddress":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.IPAddress);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.IPAddress);
                    }
                    break;

                case "device":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Device);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Device);
                    }

                    break;
                case "browser":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Browser);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Browser);
                    }
                    break;
                case "inhomedate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.InHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.InHomeDate);
                    }
                    break;
                case "admonthid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;
                case "dropnumber":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.DropNumber);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.DropNumber);
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

            model = list.ToList().Select(x => new StoreAdChoiceModel
            {
                AdOptionID = x.AdOptionID,
                CouponID = x.CouponID,
                StoreID = x.StoreID ?? 0,
                AdMonthID = x.AdMonthID ?? 0,
                //Storename = x.Store != null ? x.Store.Storename : "",
                //ADOptionName = x.ADOption != null ? x.ADOption.ADOptionName : "",
                //Ownername = x.User != null ? x.User.Ownername : "",
                Storename = x.StoreAdChoice != null ? x.StoreAdChoice.Store != null ? x.StoreAdChoice.Store.Storename : "" : "",
                ADOptionName = x.StoreAdChoice != null ? x.StoreAdChoice.ADOption != null ? x.StoreAdChoice.ADOption.ADOptionName : "" : "",
                TimeStamp = x.TimeStamp ?? new TimeSpan(),
                Ownername = x.StoreAdChoice != null ? x.StoreAdChoice.User.Ownername : "",
                IPAddress = x.IPAddress,
                Device = x.Device,
                Browser = x.Browser,
                DropNumber = x.AdMonth == null ? 0 : (x.AdMonth.DropNumber ?? 0),
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceInitials = x.ChoiceInitials ?? "",
                AdMonthDetail = x.AdMonthID.HasValue && x.AdMonth != null ? new Model.AdMonth.AdMonthModel()
                {
                    Year = x.AdMonth.Year ?? 2000,
                    Month = x.AdMonth.Month ?? 0,
                    //AdCouponID = x.AdMonth.AdCoupnID ?? 0,
                    //AdCouponName = x.AdMonth.Coupon != null ? x.AdMonth.Coupon.CouponText : "",
                } : new Model.AdMonth.AdMonthModel(),
                AdCouponID = x.CouponID ?? 0,
                AdCouponName = x.Coupon != null ? x.Coupon.CouponText : "",
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }
    }
}
