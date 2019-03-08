using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Models
{
    public class ExceptionReportModal 
    {
        public int ExceptionID { get; set; }
        public int? MonthId { get; set; }
        public int? StoreId { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreadetBy { get; set; }
        public TransactionMessage TransMessage { get; set; }

        public string StoreName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int DropNumber { get; set; }
    }
}
