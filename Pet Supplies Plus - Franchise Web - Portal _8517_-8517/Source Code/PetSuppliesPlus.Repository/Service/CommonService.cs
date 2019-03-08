using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Framework;

namespace PetSuppliesPlus.Repository.Service
{
    public interface ICommonService
    {
        /// <summary>
        /// Returns the list of all the countries
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetMarketList();

        /// <summary>
        /// Returns the list of all the Store
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetStoreList();

        /// <summary>
        /// Returns the list of all the AdMonth
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAdOptionList();


        /// <summary>
        /// Returns the list of all the YearList
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetYearList();


        /// <summary>
        /// Returns the list of all the Month
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetMonthList();


        /// <summary>
        /// Returns the list of all the Coupon
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetCouponList(int? adMonthId);


        /// <summary>
        /// Returns the list of all the AllowedStore OptionList
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllowedStoreOptionList(int storeId);

        /// <summary>
        /// Returns the list of all the AllowedStore OptionList for all store
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAllowedStoreOptionList();

        /// <summary>
        /// Returns the list of all the Store
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetCouponList();

        IEnumerable<SelectListItem> GetAdMonthList();

        IEnumerable<SelectListItem> GetAdoptionList();
    }
    public class CommonService : ICommonService
    {
        #region Unit of Work

        public IUnitOfWork unitOfWork;

        public object ApplcationEnum { get; private set; }
        #endregion

        public CommonService(IUnitOfWork _unifOfWrok)
        {
            unitOfWork = _unifOfWrok;
        }

        public IEnumerable<SelectListItem> GetMarketList()
        {
            return unitOfWork.RepoMarket.GetAll().Select
            (x => new SelectListItem
            {
                Text = x.Name,
                Value = x.MarketID.ToString()
            }).OrderBy(x => x.Text);
        }

        public IEnumerable<SelectListItem> GetStoreList()
        {
            return unitOfWork.RepoStore.GetAll().Select
             (x => new SelectListItem
             {
                 Text = x.StoreID + " (" + x.Storename + ")",
                 Value = x.StoreID.ToString()
             }).OrderBy(x => Convert.ToInt32(x.Value));
        }

        public IEnumerable<SelectListItem> GetAdOptionList()
        {
            return unitOfWork.RepoAdOption.Where(x => x.Inactive == true && x.SpecialChoice == null).ToList().Select
             (x => new SelectListItem
             {
                 Text = x.ADOptionName,
                 Value = x.ADOptionID.ToString()
             }).OrderBy(x => x.Text);
        }

        public IEnumerable<SelectListItem> GetYearList()
        {
            List<SelectListItem> item = new List<SelectListItem>();
            for (int i = DateTime.Now.Year; i < (DateTime.Now.Year + 15); i++)
            {
                item.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            return item;
        }

        public IEnumerable<SelectListItem> GetMonthList()
        {
            List<SelectListItem> item = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                item.Add(new SelectListItem() { Text = new DateTime(2016, i, 1).ToString("MMMM"), Value = (i).ToString() });
            }
            return item;
        }
        public IEnumerable<SelectListItem> GetAdMonthList()
        {
            return unitOfWork.RepoAdMonth.GetAll().ToList().Select
             (x => new SelectListItem
             {
                 Text = (new DateTime(x.Year.Value, x.Month.Value, 1).ToString("MMMM, yyyy")) + " [" + (x.DropNumber ?? 0).ToString() + "]",
                 Value = x.AdMonthID.ToString()
             }).OrderBy(x => x.Text);
        }
        public IEnumerable<SelectListItem> GetCouponList(int? adMonthId)
        {
            var monthCoupon = unitOfWork.RepoAdMonth.Where(x => x.AdMonthID == adMonthId).FirstOrDefault();
            if (monthCoupon != null && monthCoupon.Coupons != null)
            {
                return monthCoupon.Coupons.Where(x => x.IsActive == true).ToList().Select
                (x => new SelectListItem
                {
                    Text = x.CouponText,
                    Value = x.CouponID.ToString()
                }).OrderBy(x => x.Text);
            }
            return new List<SelectListItem>();
        }

        public IEnumerable<SelectListItem> GetAllowedStoreOptionList(int storeId)
        {
            return unitOfWork.RepoAllowedStoreOption.Where(x => x.StoreID == storeId && x.ADOption.Inactive == true).ToList().Select(x => new SelectListItem
            {
                Value = x.AdOptionID.ToString(),
                Text = x.ADOption.ADOptionName
            }).ToList();

        }

        public IEnumerable<SelectListItem> GetAllowedStoreOptionList()
        {
            return unitOfWork.RepoAllowedStoreOption.Where(x => x.ADOption.Inactive == true).Select(x => new SelectListItem
            {
                Value = x.AdOptionID.ToString(),
                Text = x.ADOption.ADOptionName
            }).ToList();

        }

        public IEnumerable<SelectListItem> GetCouponList()
        {
            return unitOfWork.RepoCoupon.Where(x => x.IsActive == true).ToList().Select
             (x => new SelectListItem
             {
                 Text = x.CouponText,
                 Value = x.CouponID.ToString()
             }).OrderBy(x => x.Text);
        }

        public IEnumerable<SelectListItem> GetAdoptionList()
        {
            var values = Enum.GetValues(typeof(SpecialAddOptions)).Cast<SpecialAddOptions>();

            return values.Select(value => new SelectListItem
            {
                Text = value.ToString(),
                Value = ((byte)value).ToString(),
            }).OrderBy(x => x.Text);
        }
    }
}
