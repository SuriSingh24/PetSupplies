using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.Store
{
    public class StoreReportModel
    {
        public int ChoiceID { get; set; }
        public int StoreID { get; set; }
        public DateTime? InHomeDate { get; set; }
        public int UserID { get; set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public string StoreGroup { get; set; }
        public int AdMonthID { get; set; }
        public int AdOptionID { get; set; }
        public string AdOptionName { get; set; }
        public string ArtWorkCode { get; set; }
        public string CouponID { get; set; }
        public string CouponText { get; set; }
    }
}
