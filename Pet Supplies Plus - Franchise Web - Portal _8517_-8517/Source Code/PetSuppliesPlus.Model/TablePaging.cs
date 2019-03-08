using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetSuppliesPlus.Models
{
    public enum SortingOrder
    {
        Ascending = 1,
        Descending = 2
    }

    public class DataPagingModel
    {
        public DataPagingModel()
        {
            CurrentPageID = 0;
            PageSize = 10;
            TotalRecords = 0;
            TotalPages = 0;
            PageUrl = "";
            SearchText = "";
            SortingColumn = "";
            SortingOrder = SortingOrder.Ascending;
            ShowInactive = false;
        }

        public int CurrentPageID { get; set; }
        public int TotalPages { get; set; }
        public int StartPageNumber { get; set; }
        public int EndPageNumber { get; set; }

        public int TotalRecords { get; set; }
        public int StartRecord { get; set; }
        public int EndRecord { get; set; }

        public int PageSize { get; set; }
        public string PageUrl { get; set; }
        public string AjaxUrl { get; set; }

        public string SortingColumn { get; set; }
        public SortingOrder SortingOrder { get; set; }

        public string SearchText { get; set; }
        
        public bool ShowAllRecord { get; set; }

        public bool ShowInactive { get; set; }

        private Dictionary<string, string> _SearchParameter = new Dictionary<string, string>();
        public Dictionary<string, string> SearchParameter { get { return _SearchParameter; } set { _SearchParameter = value; } }
    }

}