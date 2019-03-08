using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using PetSuppliesPlus.Models;

namespace PetSuppliesPlus.Model.Store
{
    public   class StoreAdChoiceHistoryModel
    {
        public string EncryptedID { get; set; }

        [Required]
        public int ChoiceID { get; set; }

        [Display(Name = "Ad Month")]
        public int AdMonthID { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        public int StoreID { get; set; }

        public string Storename { get; set; }

        [Display(Name = "AdOption Name")]
        public int AdOptionID { get; set; }

        public string ADOptionName { get; set; }

        public TimeSpan TimeStamp { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public int UserID { get; set; }

        public string Ownername { get; set; }

        public string IPAddress { get; set; }

        public string Device { get; set; }

        public string Browser { get; set; }

        public DateTime InHomeDate { get; set; }

        public string ChoiceInitials { get; set; }
        [Display(Name = "Coupon Name")]
        public int CouponID { get; set; }

        public string CouponName { get; set; }

        public int Month{ get; set; }

        public List<SelectListItem> AdOptionList { get; set; }

        public List<SelectListItem> CouponList { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }
}
