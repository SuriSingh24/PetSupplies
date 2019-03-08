using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PetSuppliesPlus.Model.Store
{

    public class StoreOptionModel
    {
        public string EncryptedID { get; set; }

        public int AllowedStoreOptionId { get; set; }

        [Required]
        [Display(Name = "Store")]
        public int? StoreId { get; set; }

        public string StoreName { get; set; }

        public string SpecialAdoption { get; set; }


        [Required]
        [Display(Name = "Ad Option")]
        public int OptionId { get; set; }

        public string OptionName { get; set; }

        public bool InActive { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }

    public class AllowedStoreOptionModel
    {
        [Required]
        [Display(Name = "Ad Option")]
        public int AdOptionID { get; set; }

        public List<SelectListItem> AdOptionList { get; set; }
        public List<AllowedStoreOptionDetailModel> Detail { get; set; }

        public TransactionMessage TransMessage { get; set; }
    }


    public class AllowedStoreOptionDetailModel
    {
        public int AllowedStoreOptionID { get; set; }
        public string EncryptedID { get; set; }
        [Required]
        [Display(Name = "Store ID")]
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public int AdOptionID { get; set; }
        public string AdOptionName { get; set; }
        public bool IsSelect { get; set; }
    }
}
