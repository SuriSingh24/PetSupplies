using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdMonth;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IAdMonthService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<AdMonthModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        AdMonthModel Save(AdMonthModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        AdMonthModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        AdMonthModel GetDetailByMonthId();


    }
    public class AdMonthService : IAdMonthService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public AdMonthService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<AdMonthModel> GetList(ref DataPagingModel dataPaging)
        {
            List<AdMonthModel> model = new List<AdMonthModel>();
            var list = UnitofWork.RepoAdMonth.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "year")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.Year == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.Month == value);
                    }

                    if (item.Key == "lockoutenddate" && item.Value.isDate())
                    {
                        DateTime minDate = Convert.ToDateTime(item.Value);
                        DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                        list = list.Where(x => x.LockOutEndDate.HasValue && x.LockOutEndDate.Value >= minDate && x.LockOutEndDate <= maxDate);
                    }

                    if (item.Key == "lockoutstartdate" && item.Value.isDate())
                    {
                        DateTime minDate = Convert.ToDateTime(item.Value);
                        DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                        list = list.Where(x => x.LockOutStartDate.HasValue && x.LockOutStartDate.Value >= minDate && x.LockOutStartDate <= maxDate);
                    }

                    //if (item.Key == "inhomestart" && item.Value.isDate())
                    //{
                    //    DateTime minDate = Convert.ToDateTime(item.Value);
                    //    DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                    //    list = list.Where(x => x.InHomeStart.HasValue && x.InHomeStart.Value >= minDate && x.InHomeStart <= maxDate);
                    //}
                    //if (item.Key == "inhomeend" && item.Value.isDate())
                    //{
                    //    DateTime minDate = Convert.ToDateTime(item.Value);
                    //    DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                    //    list = list.Where(x => x.InHomeEnd.HasValue && x.InHomeEnd.Value >= minDate && x.InHomeEnd <= maxDate);
                    //}
                    if (item.Key == "inhomedate" && item.Value.isDate())
                    {
                        DateTime minDate = Convert.ToDateTime(item.Value);
                        DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                        list = list.Where(x => x.CorpInHomeDate.HasValue && x.CorpInHomeDate.Value >= minDate && x.CorpInHomeDate <= maxDate);
                    }
                    if (item.Key == "dropnumber")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.DropNumber == value);
                    }
                    if (item.Key == "adoptionid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdOptionID == value);
                    }
                    if (item.Key == "adcouponid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdCoupnID == value);
                    }
                    if (item.Key == "petpartnerinfo")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => !string.IsNullOrEmpty(x.PetpartnerInfo) && x.PetpartnerInfo.ToLower().Contains(value));
                    }
                    if (item.Key == "corporateplantext")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => !string.IsNullOrEmpty(x.CorpPlanText) && x.CorpPlanText.ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {

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
                case "adcouponid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonthID);
                    }
                    break;
                case "year":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Year);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Year);
                    }
                    break;

                case "month":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Month);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Month);
                    }
                    break;

                case "lockoutenddate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.LockOutEndDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.LockOutEndDate);
                    }
                    break;

                case "lockoutstartdate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.LockOutStartDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.LockOutStartDate);
                    }
                    break;
                //case "inhomestart":
                //    if (dataPaging.SortingOrder == SortingOrder.Descending)
                //    {
                //        list = list.OrderBy(U => U.InHomeStart);
                //    }
                //    else
                //    {
                //        list = list.OrderByDescending(U => U.InHomeStart);
                //    }
                //    break;
                //case "inhomeend":
                //    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                //    {
                //        list = list.OrderBy(U => U.InHomeEnd);
                //    }
                //    else
                //    {
                //        list = list.OrderByDescending(U => U.InHomeEnd);
                //    }
                //    break;
                case "petpartnerinfo":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.PetpartnerInfo);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.PetpartnerInfo);
                    }
                    break;
                case "inhomedate":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.CorpInHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.CorpInHomeDate);
                    }
                    break;
                case "adoptionid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdOptionID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdOptionID);
                    }
                    break;
                case "dropnumber":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdOptionID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdOptionID);
                    }
                    break;
                default:
                    break;

            }

            #endregion

            dataPaging.TotalRecords = list.Count();

            //********* Pagination of list *********//
            if (dataPaging.TotalRecords > dataPaging.PageSize && dataPaging.ShowAllRecord == false)
            {
                list = list.Skip(dataPaging.CurrentPageID.TableSkipRecord(dataPaging.PageSize)).Take(dataPaging.PageSize);
            }

            model = list.ToList().Select(x => new AdMonthModel
            {
                AdMonthID = x.AdMonthID,
                AdOptionID = x.AdOptionID ?? 0,
                AdCouponID = x.AdCoupnID ?? 0,
                AdCouponName = x.Coupon != null ? x.Coupon.CouponText ?? "" : "",
                AdOptionName = x.ADOption != null ? x.ADOption.ADOptionName ?? "" : "",
                CorpInHomeDate = x.CorpInHomeDate ?? new DateTime(),
                //InHomeEnd = x.InHomeEnd ?? new DateTime(),
                //InHomeStart = x.InHomeStart ?? new DateTime(),
                DropNumber = x.DropNumber ?? 0,
                LockOutEndDate = x.LockOutEndDate ?? new DateTime(),
                LockOutStartDate = x.LockOutStartDate ?? new DateTime(),
                Month = x.Month ?? 0,
                PetpartnerInfo = x.PetpartnerInfo ?? "",
                Year = x.Year ?? 0,
                CorpPlanText = x.CorpPlanText,
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.AdMonthID.ToString().ToEnctypt());
            return model;
        }


        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public AdMonthModel Save(AdMonthModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                #region check duplicate 
                if (UnitofWork.RepoAdMonth.Where(x => x.Year == model.Year && x.Month == model.Month && x.DropNumber == model.DropNumber && x.AdMonthID != model.AdMonthID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "Duplicate");
                    return model;
                }
                #endregion

                #region Valid Date and Month
                //commented on 5th augest 16
                //if (model.LockOutStartDate.Month != model.Month || model.LockOutEndDate.Month != model.Month)
                //{
                //    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "InvalidDateMonth");
                //    return model;
                //}
                #endregion

                #region Lock out Start/End Date 
                if (UnitofWork.RepoAdMonth.Where(x => ((x.LockOutStartDate < model.LockOutStartDate && x.LockOutEndDate > model.LockOutEndDate) || (x.LockOutStartDate >= model.LockOutStartDate && x.LockOutStartDate <= model.LockOutEndDate) || (x.LockOutEndDate >= model.LockOutStartDate && x.LockOutEndDate <= model.LockOutEndDate)) && x.AdMonthID != model.AdMonthID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "Date");
                    return model;
                }
                #endregion

                #region Check is Auto pouplated 
                if (UnitofWork.RepoExceptionReport.Where(x => x.MonthId == model.AdMonthID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "AutoPopulated");
                    return model;
                }
                #endregion

                AdMonth dbAdMonth = UnitofWork.RepoAdMonth.Where(x => x.AdMonthID == model.AdMonthID).FirstOrDefault();

                bool isSave = false;

                if (dbAdMonth == null)
                {
                    #region Save
                    dbAdMonth = new AdMonth();
                    UnitofWork.RepoAdMonth.Add(dbAdMonth);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdMonth", "Update");
                    #endregion
                }
                #region Set Value
                dbAdMonth.AdOptionID = model.AdOptionID;
                dbAdMonth.CorpInHomeDate = model.CorpInHomeDate;
                //dbAdMonth.InHomeEnd = model.InHomeEnd;
                //dbAdMonth.InHomeStart = model.InHomeStart;
                dbAdMonth.DropNumber = model.DropNumber;
                dbAdMonth.LockOutEndDate = model.LockOutEndDate;
                dbAdMonth.LockOutStartDate = model.LockOutStartDate;
                dbAdMonth.Month = model.Month;
                dbAdMonth.PetpartnerInfo = model.PetpartnerInfo;
                dbAdMonth.Year = model.Year;
                dbAdMonth.CorpPlanText = model.CorpPlanText;
                dbAdMonth.AdCoupnID = model.AdCouponID;
                Coupon defaultcoupon = UnitofWork.RepoCoupon.Where(c => c.CouponID == model.AdCouponID).FirstOrDefault();
                dbAdMonth.Coupons.Add(defaultcoupon);
                #endregion

                UnitofWork.Commit();
                model.AdMonthID = dbAdMonth.AdMonthID;
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
        public AdMonthModel GetDetail(string encryptedId)
        {
            AdMonthModel model = new AdMonthModel();
            try
            {
                int AdMonthId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoAdMonth.Where(x => x.AdMonthID == AdMonthId).Select(x => new AdMonthModel
                {
                    AdMonthID = x.AdMonthID,
                    AdOptionID = x.AdOptionID ?? 0,
                    AdCouponID = x.AdCoupnID ?? 0,
                    AdCouponName = x.Coupon != null ? x.Coupon.CouponText ?? "" : "",
                    AdOptionName = x.ADOption != null ? x.ADOption.ADOptionName ?? "" : "",
                    CorpInHomeDate = x.CorpInHomeDate ?? new DateTime(),
                    DropNumber = x.DropNumber ?? 0,
                    //InHomeEnd = x.InHomeEnd ?? new DateTime(),
                    //InHomeStart = x.InHomeStart ?? new DateTime(),
                    LockOutEndDate = x.LockOutEndDate ?? new DateTime(),
                    LockOutStartDate = x.LockOutStartDate ?? new DateTime(),
                    Month = x.Month ?? 0,
                    PetpartnerInfo = x.PetpartnerInfo ?? "",
                    Year = x.Year ?? 0,
                    CorpPlanText = x.CorpPlanText,
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.AdMonthID.ToString().ToEnctypt();
                }
                else
                {
                    model = new AdMonthModel();
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
            model.Message = utilityHelper.ReadGlobalMessage("AdMonth", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoAdMonth.Where(x => x.AdMonthID == ID).FirstOrDefault();
                    //reference count
                    int refCount = 0;
                    refCount = UnitofWork.RepoStoreAdChoice.Where(x => x.AdMonthID == ID).Count();
                    refCount += UnitofWork.RepoStoreAdChoiceHistory.Where(x => x.AdMonthID == ID).Count();

                    if (item != null)
                    {
                        if (refCount > 0 || item.Coupons.Count() > 0 || item.Documents.Count() > 0)
                        {
                            model.Message = utilityHelper.ReadGlobalMessage("AdMonth", "NotDelete");
                        }
                        else
                        {
                            UnitofWork.RepoAdMonth.Delete(item);
                            UnitofWork.Commit();
                            model.Message = utilityHelper.ReadGlobalMessage("AdMonth", "Delete");
                            model.Status = MessageStatus.Success;
                        }
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
        /// to get page full detail
        /// //changed according to client discussion on 5th aug 2016
        /// User choice will be done according to lock out start & end date
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        public AdMonthModel GetDetailByMonthId()
        {
            AdMonthModel model = new AdMonthModel();
            try
            {
                #region get detail
                DateTime currentDate = utilityHelper.CurrentDateTime.Date;
                model = UnitofWork.RepoAdMonth.Where(x => x.LockOutStartDate.HasValue && x.LockOutStartDate <= currentDate && x.LockOutEndDate.HasValue && x.LockOutEndDate >= currentDate).Select(x => new AdMonthModel
                {
                    AdMonthID = x.AdMonthID,
                    AdOptionID = x.AdOptionID ?? 0,
                    AdCouponID = x.AdCoupnID ?? 0,
                    AdCouponName = x.Coupon != null ? x.Coupon.CouponText ?? "" : "",
                    AdOptionName = x.ADOption != null ? x.ADOption.ADOptionName ?? "" : "",
                    CorpInHomeDate = x.CorpInHomeDate ?? new DateTime(),
                    //InHomeEnd = x.InHomeEnd ?? new DateTime(),
                    //InHomeStart = x.InHomeStart ?? new DateTime(),
                    DropNumber = x.DropNumber ?? 0,
                    LockOutEndDate = x.LockOutEndDate ?? new DateTime(),
                    LockOutStartDate = x.LockOutStartDate ?? new DateTime(),
                    Month = x.Month ?? 0,
                    PetpartnerInfo = x.PetpartnerInfo ?? "",
                    Year = x.Year ?? 0,
                    CorpPlanText = x.CorpPlanText,
                }).FirstOrDefault();
                #endregion
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

    }
}
