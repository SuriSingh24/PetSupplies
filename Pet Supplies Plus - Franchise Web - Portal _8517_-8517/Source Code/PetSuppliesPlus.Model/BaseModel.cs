using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model
{
    public class BaseModel
    {
        public string EncryptedID { get; set; }
        public long CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }
}
