using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetSuppliesPlus.Models
{
    public class ForgotPasswordModel
    {
        [DisplayName("Email ID")]
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Current Password must be between 6 to 20 characters length")]
        [MinLength(6,ErrorMessage = "Current Password must be between 6 to 20 characters length")]
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "New Password must be between 6 to 20 characters length")]
        [MinLength(6,ErrorMessage = "New Password must be between 6 to 20 characters length")]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Confirm Password must be between 6 to 20 characters length")]
        [MinLength(6, ErrorMessage = "Confirm Password must be between 6 to 20 characters length")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

       public TransactionMessage TransMessage { get; set; }

    }
}