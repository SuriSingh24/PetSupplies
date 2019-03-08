using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.Market
{
    public class MarketModel
    {
        public string EncryptedID { get; set; }
        public int MarketId { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Market Name must be up to 50 characters long")]
        public string Name { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }
}
