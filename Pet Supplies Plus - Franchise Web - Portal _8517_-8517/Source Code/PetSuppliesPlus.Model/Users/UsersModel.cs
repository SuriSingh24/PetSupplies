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
    public class UsersModel
    {
        public UsersModel()
        {
            StoreDetail = new Data.Store();
        }

        public string EncryptedID { get; set; }
        [Required]
        [Display(Name = "Store")]
        public int StoreID { get; set; }

        public long UserID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Owner Name must be up to 50 characters long")]
        [DisplayName("Owner Name")]
        public string OwnerName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Email must be up to 50 characters long")]
        public string Email { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Password must be up to 20 characters long")]
        [MinLength(6, ErrorMessage = "Password must be least 6 characters long")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsActive { get; set; }
        public Data.Store StoreDetail { get; set; }

        public TransactionMessage TransMessage { get; set; }

    }
}