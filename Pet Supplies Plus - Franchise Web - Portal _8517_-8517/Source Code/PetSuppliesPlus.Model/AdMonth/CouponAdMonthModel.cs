using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.AdMonth
{
    public class CouponAdMonthModel
    {
        public string EncryptedID { get; set; }
        public int MonthId { get; set; }

        [Required]
        [Display(Name ="Coupon")]
        public int CouponID { get; set; }

        public string Month { get; set; }

        public int Year { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }
}
