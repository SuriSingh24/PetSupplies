using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PetSuppliesPlus.Data;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Framework;

namespace PetSuppliesPlus.Model.Store
{
    public class StoreAdChoiceModel
    {
        public string EncryptedID { get; set; }

        //[Required]
        public int ChoiceID { get; set; }

        [Display(Name = "Ad Month")]
        public int AdMonthID { get; set; }

        public AdMonth.AdMonthModel AdMonthDetail { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public int StoreID { get; set; }


        public string Storename { get; set; }

        public string MarketName { get; set; }

        [Display(Name = "AdOption Name")]
        public int? AdOptionID { get; set; }


        [Display(Name = "AdCoupon Name")]
        public int? AdCouponID { get; set; }

        [Display(Name = "AdCoupon Name")]
        public string AdCouponName { get; set; }

        public string ADOptionName { get; set; }

        public TimeSpan TimeStamp { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public int UserID { get; set; }

        public string Ownername { get; set; }

        [MaxLength(100)]
        public string IPAddress { get; set; }

        [MaxLength(100)]
        public string Device { get; set; }

        [MaxLength(100)]
        public string Browser { get; set; }

        public DateTime? InHomeDate { get; set; }

        [MaxLength(100)]
        public string ChoiceInitials { get; set; }

        [Display(Name = "Coupon Name")]
        public int? CouponID { get; set; }

        public bool? IsCouponApply { get; set; }

        public bool NotPrinting { get; set; }

        public bool OwnDistribution { get; set; }

        public string CouponName { get; set; }

        public bool FollowedCorporate { get; set; }

        public int DropNumber { get; set; }

        public List<SelectListItem> AdOptionList { get; set; }

        public MultiSelectList StoreAdOptionList { get; set; }

        public List<int> SelectedAdOption { get; set; }

        public List<SelectListItem> CouponList { get; set; }

        public ADOption ADOption { get; set; }
        public Data.Store Store { get; set; }
        public User User { get; set; }

        public UserAdChoice UserAdChoice { get; set; }

        public TransactionMessage TransMessage { get; set; }

        public Data.ExceptionReport Exception { get; set; }
        public MailTamplate MailTamplate { get; set; }
       // public 
    }



}
