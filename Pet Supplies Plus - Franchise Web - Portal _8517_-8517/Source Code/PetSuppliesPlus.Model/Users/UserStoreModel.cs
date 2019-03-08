using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PetSuppliesPlus.Framework;
using System.Web.Mvc;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Data;


namespace PetSuppliesPlus.Model.Users
{
    public class UserStoreModel
    {
        
        public string EncryptedID { get; set; }
        public int UserStoreID { get; set; }
        [Required]
        [Display(Name = "Store")]
        public int StoreID { get; set; }

        [Required]
        [Display(Name = "User")]
        public int UserID { get; set; }

        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }

        [MaxLength(50, ErrorMessage = "Store Name must be up to 50 characters long")]
        public string StoreName { get; set; }

        [MaxLength(100, ErrorMessage = "City must be up to 100 characters long")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "State must be up to 50 characters long")]
        public string State { get; set; }

        [MaxLength(50, ErrorMessage = "Market Name must be up to 50 characters long")]
        public string MarketName { get; set; }

        public TransactionMessage TransMessage { get; set; }

    }
}