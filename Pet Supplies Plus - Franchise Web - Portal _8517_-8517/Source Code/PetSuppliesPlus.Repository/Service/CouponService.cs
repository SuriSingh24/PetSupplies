using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Coupon;
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
    public interface ICouponService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<CouponModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        CouponModel Save(CouponModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        CouponModel GetDetail(string encryptedId);

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
        CouponAdMonthModel CouponAdMonthSave(CouponAdMonthModel model);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage DeleteCouponAdMonth(string encryptedId, string monthId);

    }
    public class CouponService : ICouponService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public CouponService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<CouponModel> GetList(ref DataPagingModel dataPaging)
        {
            List<CouponModel> model = new List<CouponModel>();
            var list = UnitofWork.RepoCoupon.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "name")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.CouponText.ToLower().Contains(value));
                    }
                    if (item.Key == "monthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonths.Where(s => s.AdMonthID == value).Count() > 0);
                    }
                    if (item.Key == "status")
                    {
                        bool value = item.Value == "1";
                        list = list.Where(x => x.IsActive == value);
                    }
                    if (item.Key == "monthid1")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonths1.Where(s => s.AdMonthID == value).Count() > 0);
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "name":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.CouponText);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.CouponText);
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

            model = list.ToList().Select(x => new CouponModel
            {
                CouponID = x.CouponID,
                CouponText = x.CouponText,
                IsActive = x.IsActive ?? true,
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.CouponID.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public CouponModel Save(CouponModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                #region check duplicate 
                if (UnitofWork.RepoCoupon.Where(x => x.CouponText == model.CouponText && x.CouponID != model.CouponID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Coupon", "Duplicate");
                    return model;
                }
                #endregion

                Coupon dbCoupon = UnitofWork.RepoCoupon.Where(x => x.CouponID == model.CouponID).FirstOrDefault();

                bool isSave = false;

                if (dbCoupon == null)
                {
                    #region Save
                    dbCoupon = new Coupon();
                    UnitofWork.RepoCoupon.Add(dbCoupon);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Coupon", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Coupon", "Update");
                    #endregion
                }
                #region Set Value
                dbCoupon.CouponText = model.CouponText;
                dbCoupon.IsActive = model.IsActive;
                #endregion

                UnitofWork.Commit();
                model.CouponID = dbCoupon.CouponID;

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
        public CouponModel GetDetail(string encryptedId)
        {
            CouponModel model = new CouponModel();
            try
            {
                int CouponId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoCoupon.Where(x => x.CouponID == CouponId).Select(x => new CouponModel
                {
                    CouponText = x.CouponText,
                    CouponID = x.CouponID,
                    IsActive = x.IsActive ?? true,
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.CouponID.ToString().ToEnctypt();
                }
                else
                {
                    model = new CouponModel();
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
            model.Message = utilityHelper.ReadGlobalMessage("Coupon", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoCoupon.Where(x => x.CouponID == ID).FirstOrDefault();
                    //UnitofWork.RepoCoupon.Delete(item);
                    if (item != null)
                    {
                        item.IsActive = false;
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("Coupon", "Delete");
                        model.Status = MessageStatus.Success;
                        #region reference count
                        //int refCount = UnitofWork.RepoStoreAdChoice.Where(x => x.CouponID == ID).Count();

                        //if (item != null)
                        //{
                        //    refCount += item.AdMonths.Count();

                        //    if (refCount > 0)
                        //    {
                        //        model.Message = utilityHelper.ReadGlobalMessage("Coupon", "NotDelete");
                        //    }
                        //    else
                        //    {
                        //        UnitofWork.RepoCoupon.Delete(item);
                        //        UnitofWork.Commit();
                        //        model.Message = utilityHelper.ReadGlobalMessage("Coupon", "Delete");
                        //        model.Status = MessageStatus.Success;
                        //    }
                        //}
                        #endregion
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
        public CouponAdMonthModel CouponAdMonthSave(CouponAdMonthModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                using (dbPetSupplies_8517 db = new dbPetSupplies_8517())
                {
                    #region check duplicate 
                    if (db.Coupons.Where(x => x.CouponID == model.CouponID && x.AdMonths1.Where(a => a.AdMonthID == model.MonthId).Count() > 0).Count() > 0)
                    {
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "Duplicate");
                        return model;
                    }
                    #endregion
                    Coupon dbCoupon = db.Coupons.Where(x => x.CouponID == model.CouponID).FirstOrDefault();
                    AdMonth dbAdMonth = db.AdMonths.Where(x => x.AdMonthID == model.MonthId).FirstOrDefault();
                    if (dbCoupon != null && dbAdMonth != null)
                    {
                        //int refCount = UnitofWork.RepoStoreAdChoice.Where(x => x.CouponID == model.CouponID).Count();
                        //if (refCount == 0)
                        //{
                            dbAdMonth.Coupons.Add(dbCoupon);
                            db.SaveChanges();
                            model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "Save");
                            model.TransMessage.Status = MessageStatus.Success;
                        //}
                        //else
                        //{
                        //    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "NotAssign");
                        //}
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
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        public TransactionMessage DeleteCouponAdMonth(string encryptedId, string monthId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int _couponId = Convert.ToInt32(encryptedId.ToDecrypt());
                    int _monthId = Convert.ToInt32(monthId);
                    using (dbPetSupplies_8517 db = new dbPetSupplies_8517())
                    {
                        
                        var item = db.Coupons.Where(x => x.CouponID == _couponId && x.AdMonths1.Where(a => a.AdMonthID == _monthId).Count() > 0).FirstOrDefault();
                        Coupon dbCoupon = db.Coupons.Where(x => x.CouponID == _couponId).FirstOrDefault();
                        AdMonth dbAdMonth = db.AdMonths.Where(x => x.AdMonthID == _monthId).FirstOrDefault();
                        var associated = db.StoreAdChoices.Where(x => x.AdMonthID == _monthId && x.CouponID == _couponId).Count() > 0 ? true : false;
                        if (associated)
                        {
                            model.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "AssociatedCoupon");
                            model.Status = MessageStatus.Error;
                        }
                        else if(dbAdMonth.AdCoupnID == _couponId)
                        {
                            model.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "DefaultCoupon");
                            model.Status = MessageStatus.Error;
                        }
                        else
                        {
                            if (item != null && dbCoupon != null && dbAdMonth != null)
                            {
                                dbAdMonth.Coupons.Remove(dbCoupon);
                                db.SaveChanges();
                                model.Message = utilityHelper.ReadGlobalMessage("ManageCouponAdMonth", "Delete");
                                model.Status = MessageStatus.Success;
                            }
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
    }
}
