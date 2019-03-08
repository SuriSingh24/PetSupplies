using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Store;
using PetSuppliesPlus.Models;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IStoreAdChoiceHistoryService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<StoreAdChoiceHistoryModel> GetList(ref DataPagingModel dataPaging);

        List<StoreReportModel> GetStoreAdOptionReport(ref DataPagingModel dataPaging);
        List<StoreReportModel> GetExprotReport();
    }
    public class StoreAdChoiceHistoryService : IStoreAdChoiceHistoryService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public StoreAdChoiceHistoryService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        public List<StoreAdChoiceHistoryModel> GetList(ref DataPagingModel dataPaging)
        {
            List<StoreAdChoiceHistoryModel> model = new List<StoreAdChoiceHistoryModel>();
            var list = UnitofWork.RepoStoreAdChoiceHistory.Where(x => 1 == 1);
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "monthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID.HasValue && x.AdMonth.Month == value && x.AdMonth.Year == DateTime.Now.Year);
                    }
                    if (item.Key == "storeid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreID == value);
                    }
                    if (item.Key == "store")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.Contains(value));
                    }
                    if (item.Key == "userid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.UserID == value);
                    }
                    if (item.Key == "user")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "ipaddress")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.IPAddress.Contains(value));
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
                    if (item.Key == "coupontext")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Coupon.CouponText.ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {

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
                case "coupontext":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Coupon.CouponText);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Coupon.CouponText);
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

            model = list.ToList().Select(x => new StoreAdChoiceHistoryModel
            {
                ChoiceID = x.ChoiceID ?? 0,
                AdOptionID = x.AdOptionID ?? 0,
                UserID = x.UserID ?? 0,
                AdMonthID = x.AdMonthID ?? 0,
                StoreID = x.StoreID ?? 0,
                TimeStamp = x.TimeStamp ?? new TimeSpan(),
                IPAddress = x.IPAddress,
                Device = x.Device,
                Browser = x.Browser,
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceInitials = x.ChoiceInitials,
                CouponName = x.Coupon.CouponText,
                Month = x.AdMonthID.HasValue ? x.AdMonth.Month ?? 0 : 0,
            }).ToList();

            var adOptions = UnitofWork.RepoAdOption.GetAll();
            var store = UnitofWork.RepoStore.GetAll();
            var user = UnitofWork.RepoUser.GetAll();

            model.Where(x => x.AdOptionID > 0).ToList().ForEach(x => x.ADOptionName = adOptions.Where(a => a.ADOptionID == x.AdOptionID).FirstOrDefault().ADOptionName);
            model.Where(x => x.UserID > 0).ToList().ForEach(x => x.Ownername = user.Where(a => a.UserID == x.UserID).FirstOrDefault().Ownername);
            model.Where(x => x.StoreID > 0).ToList().ForEach(x => x.Storename = store.Where(a => a.StoreID == x.StoreID).FirstOrDefault().Storename);
            model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }

        public List<StoreReportModel> GetStoreAdOptionReport(ref DataPagingModel dataPaging)
        {
            List<StoreReportModel> model = new List<StoreReportModel>();
            var list = UnitofWork.RepoStoreAdChoice.GetAll();
            if (list.Count() > 0)
            {
                SessionHelper.ReportCount = true;
            }
            //var storeList = UnitofWork.RepoStore.GetAll().Where(x => x.UserStores.Count() > 0 && x.UserStores.FirstOrDefault().UserID != null).GroupBy(s => s.UserStores.FirstOrDefault().UserID).ToList();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "storeid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreID == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID.HasValue && x.AdMonth.Month == value && x.AdMonth.Year == DateTime.Now.Year);
                    }
                    if (item.Key == "monthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonthID.HasValue && x.AdMonthID == value);
                    }
                    if (item.Key == "store")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.Contains(value));
                    }
                    if (item.Key == "userid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.UserID == value);
                    }
                    if (item.Key == "couponid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.CouponID == value);
                    }
                    if (item.Key == "user")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.User.Ownername.Contains(value));
                    }
                    if (item.Key == "storecode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.StoreCode.ToLower().Contains(value));
                    }
                    if (item.Key == "artworkcode")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.ArtworkCode.ToLower().Contains(value));
                    }
                    if (item.Key == "adoptionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOption != null && x.ADOption.ADOptionName.ToLower().Contains(value));
                    }
                    if (item.Key == "ihw")
                    {
                        DateTime minDate = Convert.ToDateTime(item.Value);
                        DateTime maxDate = new DateTime(minDate.Year, minDate.Month, minDate.Day, 23, 59, 59, 0);
                        list = list.Where(x => x.InHomeDate.HasValue && x.InHomeDate.Value >= minDate && x.InHomeDate <= maxDate);
                    }
                    if (item.Key == "coupontext")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Coupon != null && x.Coupon.CouponText.ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {

                case "month":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.Month.Value);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.Month.Value);
                    }
                    break;

                case "store":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.Storename);
                    }
                    break;
                case "storecode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.StoreCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.StoreCode);
                    }
                    break;
                case "artworkcode":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.ArtworkCode);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.ArtworkCode);
                    }
                    break;
                case "adoptionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.Where(x => x.ADOption != null).OrderBy(U => U.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.Where(x => x.ADOption != null).OrderByDescending(U => U.ADOption.ADOptionName);
                    }
                    break;
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
                case "ihw":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.InHomeDate);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.InHomeDate);
                    }
                    break;
                case "couponid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.CouponID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.CouponID);
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

                case "coupontext":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.Where(u => u.Coupon != null).OrderBy(U => U.Coupon.CouponText);
                    }
                    else
                    {
                        list = list.Where(u => u.Coupon != null).OrderByDescending(U => U.Store.Storename);
                    }
                    break;
            }

            #endregion

            dataPaging.TotalRecords = list.Count();

            //*********Pagination of list *********//
            if (dataPaging.TotalRecords > dataPaging.PageSize && dataPaging.ShowAllRecord == false)
            {
                list = list.Skip(dataPaging.CurrentPageID.TableSkipRecord(dataPaging.PageSize)).Take(dataPaging.PageSize);
            }
            var userStores = UnitofWork.RepoUserStore.GetAll().ToList();

            var na = userStores.Where(u => u.UserID == 1015 && u.StoreID == 8).ToList();




            model = list.ToList().Select(x => new StoreReportModel
            {
                InHomeDate = x.InHomeDate ?? new DateTime(),
                ChoiceID = x.ChoiceID,
                AdOptionID = x.AdOptionID ?? 0,
                AdMonthID = x.AdMonthID ?? 0,
                StoreID = x.StoreID ?? 0,
                UserID = x.Store.UserStores.Count > 0 ? x.Store.UserStores.FirstOrDefault().UserID ?? 0 : 0,
                StoreCode = x.Store.StoreCode ?? "",
                StoreName = x.Store.Storename ?? "",
                ArtWorkCode = x.Store.ArtworkCode ?? "",
                AdOptionName = x.ADOption != null ? x.ADOption.ADOptionName ?? "" : "",
                StoreGroup = string.Join(",", userStores.Where(u => u.UserID == x.UserID).Select(u => u.StoreID).ToList()),
                CouponID = x.CouponID.HasValue ? x.CouponID.ToString() ?? "" : "",
                CouponText = x.Coupon != null ? x.Coupon.CouponText : "",
            }).ToList();

            //var storeList = UnitofWork.RepoStore.GetAll().Where(x => x.UserStores.Count() > 0 && x.UserStores.FirstOrDefault().UserID != null).GroupBy(s => s.UserStores.FirstOrDefault().UserID).ToList();





            var adOptions = UnitofWork.RepoAdOption.GetAll();
            var store = UnitofWork.RepoStore.GetAll();
            var user = UnitofWork.RepoUser.GetAll();

            //model.Where(x => x.AdOptionID > 0).ToList().ForEach(x => x.ADOptionName = adOptions.Where(a => a.ADOptionID == x.AdOptionID).FirstOrDefault().ADOptionName);
            //model.Where(x => x.UserID > 0).ToList().ForEach(x => x.Ownername = user.Where(a => a.UserID == x.UserID).FirstOrDefault().Ownername);
            //model.Where(x => x.StoreID > 0).ToList().ForEach(x => x.Storename = store.Where(a => a.StoreID == x.StoreID).FirstOrDefault().Storename);
            //model.ForEach(x => x.EncryptedID = x.ChoiceID.ToString().ToEnctypt());
            return model;
        }

        public List<StoreReportModel> GetExprotReport()
        {
            List<StoreReportModel> model = new List<StoreReportModel>();
            var list = UnitofWork.RepoStoreAdChoice.GetAll();
            if (list.Count() > 0)
            {
                SessionHelper.ReportCount = true;
            }
            var userStores = UnitofWork.RepoUserStore.GetAll().ToList();
            var na = userStores.Where(u => u.UserID == 1015 && u.StoreID == 8).ToList();
            model = list.ToList().Select(x => new StoreReportModel
            {
                InHomeDate = x.InHomeDate.HasValue ? x.InHomeDate.Value : new DateTime(),
                ChoiceID = x.ChoiceID,
                AdOptionID = x.AdOptionID ?? 0,
                AdMonthID = x.AdMonthID ?? 0,
                StoreID = x.StoreID ?? 0,
                UserID = x.Store.UserStores.Count > 0 ? x.Store.UserStores.FirstOrDefault().UserID ?? 0 : 0,
                StoreCode = x.Store.StoreCode ?? "",
                StoreName = x.Store.Storename ?? "",
                ArtWorkCode = x.Store.ArtworkCode ?? "",
                AdOptionName = x.ADOption != null ? x.ADOption.ADOptionName ?? "" : "",
                StoreGroup = string.Join(",", userStores.Where(u => u.UserID == x.UserID).Select(u => u.StoreID).ToList()),
                CouponID = x.CouponID.HasValue ? x.CouponID.ToString() ?? "" : "",
                CouponText = x.Coupon != null ? x.Coupon.CouponText : "",
            }).ToList();
            var adOptions = UnitofWork.RepoAdOption.GetAll();
            var store = UnitofWork.RepoStore.GetAll();
            var user = UnitofWork.RepoUser.GetAll();
            return model;
        }
    }

}
