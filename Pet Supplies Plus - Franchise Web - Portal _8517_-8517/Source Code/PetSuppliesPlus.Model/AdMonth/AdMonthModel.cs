using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetSuppliesPlus.Model.AdMonth
{
    public class AdMonthModel
    {
        public string EncryptedID { get; set; }

        public int AdMonthID { get; set; }
       
        public int? AdCouponID { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int Month { get; set; }


        [Required]
        public int DropNumber { get; set; }

        [Required]
        [Display(Name = "Lockout End Date")]
        public DateTime LockOutEndDate { get; set; }

        [Required]
        [Display(Name = "Lockout Start Date")]
        public DateTime LockOutStartDate { get; set; }

        //[Required]
        //[Display(Name = "In Home Start")]
        //public DateTime InHomeStart { get; set; }

        //[Required]
        //[Display(Name = "In Home End")]
        //public DateTime InHomeEnd { get; set; }

        [Required]
        [Display(Name = "Pet Partner Info")]
        //[MaxLength(50, ErrorMessage = "Pet Partner Info must be up to 50 characters long")]
        public string PetpartnerInfo { get; set; }

        [Required]
        [Display(Name = "In Home Date")]
        public DateTime CorpInHomeDate { get; set; }

        [Required]
        [Display(Name = "Ad Option")]
        public int AdOptionID { get; set; }

        //[MaxLength(100, ErrorMessage = "Corporation Plan Text must be up to 100 characters long")]
        public string CorpPlanText { get; set; }

        public string AdOptionName { get; set; }

        public string AdCouponName { get; set; }

        public IEnumerable<SelectListItem> AdOptionList { get; set; }

        public IEnumerable<SelectListItem> YearList { get; set; }

        public IEnumerable<SelectListItem> MonthList { get; set; }

        public TransactionMessage TransMessage { get; set; }

        public String MonthName { get; set; }
    }
}
