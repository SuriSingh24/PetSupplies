using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetSuppliesPlus.Model.Coupon
{
    public class CouponModel
    {
        public string EncryptedID { get; set; }
        public int CouponID { get; set; }

        [Required]
        [Display(Name = "Coupon Text")]
        [MaxLength(100, ErrorMessage = "Coupon Text must be up to 100 characters long")]
        public string CouponText { get; set; }

        public bool IsActive { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }
}
