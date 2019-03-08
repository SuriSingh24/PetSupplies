using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.AdOption
{
    public class AdOptionModel
    {
        public string EncryptedID { get; set; }
        
        public int ADOptionID { get; set; }

        [Required]
        [Display(Name = "Ad Option Name")]
        [MaxLength(50, ErrorMessage = " Ad Option Name must be up to 50 characters long")]
        public string ADOptionName { get; set; }

        [MaxLength(50, ErrorMessage = "Minimum Circulation must be up to 50 characters long")]
        public string  MinimumCirculation{ get; set; }

        [Required]
        [Display(Name = "Vendor Name")]
        [MaxLength(50, ErrorMessage = "Vendor Name must be up to 50 characters long")]
        public string Vendorname { get; set; }

        public string SpecialChoice { get; set; }

        public bool IsActive { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }
}
