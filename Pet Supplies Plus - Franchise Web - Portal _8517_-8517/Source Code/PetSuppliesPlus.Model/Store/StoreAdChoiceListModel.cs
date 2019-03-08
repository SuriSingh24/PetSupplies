using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Model.Store
{
    public class StoreAdChoiceListModel
    {
        public List<StoreAdChoiceModel> storAdChoiceList { get; set; }
        public TransactionMessage TransMessage { get; set; }
    }
}
