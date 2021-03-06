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
    
    public partial class Store
    {
        public Store()
        {
            this.AllowedStoreOptions = new HashSet<AllowedStoreOption>();
            this.ExceptionReports = new HashSet<ExceptionReport>();
            this.StoreAdChoices = new HashSet<StoreAdChoice>();
            this.StoreAdChoiceHistories = new HashSet<StoreAdChoiceHistory>();
            this.UserStores = new HashSet<UserStore>();
        }
    
        public int StoreID { get; set; }
        public string Ownergroup { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Storename { get; set; }
        public Nullable<int> MarketID { get; set; }
        public string StoreCode { get; set; }
        public string ArtworkCode { get; set; }
    
        public virtual ICollection<AllowedStoreOption> AllowedStoreOptions { get; set; }
        public virtual ICollection<ExceptionReport> ExceptionReports { get; set; }
        public virtual Market Market { get; set; }
        public virtual ICollection<StoreAdChoice> StoreAdChoices { get; set; }
        public virtual ICollection<StoreAdChoiceHistory> StoreAdChoiceHistories { get; set; }
        public virtual ICollection<UserStore> UserStores { get; set; }
    }
}
