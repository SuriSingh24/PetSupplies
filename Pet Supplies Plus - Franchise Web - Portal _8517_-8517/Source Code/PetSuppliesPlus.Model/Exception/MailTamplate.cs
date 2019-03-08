using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Models
{
    public class MailTamplate
    {
        public int TamplateID { get; set; }
        public string EmailID { get; set; }
        public string Subject { get; set; }
        public string Tamplate { get; set; }
    }
}
