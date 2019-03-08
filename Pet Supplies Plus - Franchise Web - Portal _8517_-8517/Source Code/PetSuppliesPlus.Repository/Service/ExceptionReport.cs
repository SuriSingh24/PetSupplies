using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IExceptionReportService
    {
        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        ExceptionReportModal Save(ExceptionReportModal model);

        //ExceptionReportModal AddException(int MonthID, int StoreID, string ExceptionMsg, );

        List<ExceptionReportModal> GetList(ref DataPagingModel dataPaging);
    }

    public class ExceptionReportService : IExceptionReportService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;
        
        public ExceptionReportService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        public ExceptionReportModal Save(ExceptionReportModal model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                #region check duplicate 
                //if (UnitofWork.RepoExceptionReport.Where(x => x.ExceptionID == model.ExceptionID).Count() > 0)
                //{
                //    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ExceptionReport", "Duplicate");
                //    return model;
                //}
                #endregion

                ExceptionReport dbExceptionReport = UnitofWork.RepoExceptionReport.Where(x => x.ExceptionID == model.ExceptionID).FirstOrDefault();

                bool isSave = false;

                if (dbExceptionReport == null)
                {
                    #region Save
                    dbExceptionReport = new ExceptionReport();
                    UnitofWork.RepoExceptionReport.Add(dbExceptionReport);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ExceptionReport", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ExceptionReport", "Update");
                    #endregion
                }
                #region Set Value
                dbExceptionReport.StoreId = model.StoreId;
                dbExceptionReport.MonthId = model.MonthId;
                dbExceptionReport.Description = model.Description;
                dbExceptionReport.CreatedOn = model.CreatedOn;
                #endregion

                UnitofWork.Commit();
                model.ExceptionID = dbExceptionReport.ExceptionID;
                model.TransMessage.Status = MessageStatus.Success;
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }

            return model;
        }


        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<ExceptionReportModal> GetList(ref DataPagingModel dataPaging)
        {
            List<ExceptionReportModal> model = new List<ExceptionReportModal>();
            var list = UnitofWork.RepoExceptionReport.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "storename")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Store.Storename.ToLower().Contains(value));
                    }
                    if (item.Key == "description")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Description.ToLower().Contains(value));
                    }
                    if (item.Key == "year")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonth.Year == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonth.Month == value);
                    }
                    if (item.Key == "dropnumber")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.AdMonth.DropNumber == value);
                    }
                    if (item.Key == "admonthid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.MonthId == value);
                    }
                    if (item.Key == "adstoreid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreId == value);
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "storename":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Store.Storename);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Store.Storename);
                    }
                    break;
                case "description":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Description);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Description);
                    }
                    break;
                case "year":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.Year);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.Year);
                    }
                    break;

                case "month":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.Month);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.Month);
                    }
                    break;
                case "admonthid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.MonthId);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.MonthId);
                    }
                    break;
                case "dropnumber":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AdMonth.AdOptionID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AdMonth.AdOptionID);
                    }
                    break;
                case "adstoreid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreId);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreId);
                    }
                    break;
                default:
                    break;

            }
            #endregion

            dataPaging.TotalRecords = list.Count();

            //********* Pagination of list *********//
            if (dataPaging.TotalRecords > dataPaging.PageSize && dataPaging.ShowAllRecord == false)
            {
                list = list.Skip(dataPaging.CurrentPageID.TableSkipRecord(dataPaging.PageSize)).Take(dataPaging.PageSize);
            }

            model = list.ToList().Select(x => new ExceptionReportModal
            {
                MonthId = x.MonthId,
                StoreId = x.StoreId,
                StoreName = x.Store.Storename,
                Description = x.Description,
                Month = x.AdMonth.Month??0,
                Year = x.AdMonth.Year ?? 0,
                DropNumber = x.AdMonth.DropNumber ?? 0,
            }).ToList();

            return model;
        }
    }
}
