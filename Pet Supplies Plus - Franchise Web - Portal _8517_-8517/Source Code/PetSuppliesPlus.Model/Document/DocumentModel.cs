using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Models
{
    public class DocumentModel
    {
        public int ID { get; set; }
        [Required]
        public int MonthID { get; set; }
        [Required]
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string EncyptedID { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }
}
