//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetSuppliesPlus.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class StoreAdChoice
    {
        public StoreAdChoice()
        {
            this.StoreAdChoiceHistories = new HashSet<StoreAdChoiceHistory>();
        }
    
        public int ChoiceID { get; set; }
        public Nullable<int> AdMonthID { get; set; }
        public Nullable<int> StoreID { get; set; }
        public Nullable<int> AdOptionID { get; set; }
        public Nullable<System.TimeSpan> TimeStamp { get; set; }
        public Nullable<int> UserID { get; set; }
        public string IPAddress { get; set; }
        public string Device { get; set; }
        public string Browser { get; set; }
        public Nullable<System.DateTime> InHomeDate { get; set; }
        public string ChoiceInitials { get; set; }
        public Nullable<bool> FollowedCorporate { get; set; }
        public Nullable<bool> NotPrinting { get; set; }
        public Nullable<bool> OwnDistribution { get; set; }
        public Nullable<int> CouponID { get; set; }
    
        public virtual AdMonth AdMonth { get; set; }
        public virtual ADOption ADOption { get; set; }
        public virtual Coupon Coupon { get; set; }
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<StoreAdChoiceHistory> StoreAdChoiceHistories { get; set; }
    }
}
