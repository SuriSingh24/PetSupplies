using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.Store
{
    public class StoreModel
    {
        public string EncryptedID { get; set; }
        [Required]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Store Id must be a Number.")]
        public int StoreID { get; set; }
        [Required]
        [Display(Name = "Owner Group")]
        [MaxLength(50, ErrorMessage = "Owner Group must be up to 50 characters long")]
        public string Ownergroup { get; set; }

        //[Required]
        //[MaxLength(100)]
        //public string Circulation { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "City must be up to 50 characters long")]
        public string City { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "State must be up to 50 characters long")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Store Name")]
        [MaxLength(50, ErrorMessage = "Store Name must be up to 50 characters long")]
        public string Storename { get; set; }

        [Display(Name = "Market Name")]
        [MaxLength(50, ErrorMessage = "Market Name must be up to 50 characters long")]
        public string MarketName { get; set; }

        [Required]
        [Display(Name = "Market")]
        public int MarketID { get; set; }

        [Display(Name = "Store Code")]
        [MaxLength(100, ErrorMessage = "Store Code must be up to 100 characters long")]
        public string StoreCode { get; set; }

        [Display(Name = "Artwork Code")]
        [MaxLength(100, ErrorMessage = "Artwork Code must be up to 100 characters long")]
        public string ArtworkCode { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }
}
