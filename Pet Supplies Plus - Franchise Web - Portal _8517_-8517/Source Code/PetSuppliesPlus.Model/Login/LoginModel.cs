using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PetSuppliesPlus.Models
{
    public class LoginModel  
    {
        [DisplayName("Email")]
        [MaxLength(50)]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DisplayName("Password")]
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }
}