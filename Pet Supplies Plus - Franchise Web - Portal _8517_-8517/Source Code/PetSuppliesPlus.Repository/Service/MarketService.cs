using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Market;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository.Service
{

    public interface IMarketService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<MarketModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        MarketModel Save(MarketModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        MarketModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);
    }

    public class MarketService : IMarketService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public MarketService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<MarketModel> GetList(ref DataPagingModel dataPaging)
        {
            List<MarketModel> model = new List<MarketModel>();
            var list = UnitofWork.RepoMarket.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "name")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Name.ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "name":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Name);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Name);
                    }
                    break;
                case "marketid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.MarketID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.MarketID);
                    }
                    break;
            }

            #endregion

            dataPaging.TotalRecords = list.Count();

            //********* Pagination of list *********//
            if (dataPaging.TotalRecords > dataPaging.PageSize && dataPaging.ShowAllRecord == false)
            {
                list = list.Skip(dataPaging.CurrentPageID.TableSkipRecord(dataPaging.PageSize)).Take(dataPaging.PageSize);
            }

            model = list.ToList().Select(x => new MarketModel
            {
                MarketId = x.MarketID,
                Name = x.Name

            }).ToList();

            model.ForEach(x => x.EncryptedID = x.MarketId.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public MarketModel Save(MarketModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                //check duplicate 
                if (UnitofWork.RepoMarket.Where(x => x.Name.ToLower() == model.Name.ToLower() && x.MarketID != model.MarketId).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "Duplicate");
                    return model;
                }

                Data.Market dbMarket = UnitofWork.RepoMarket.Where(x => x.MarketID == model.MarketId).FirstOrDefault();
                bool isSave = false;
                //user table 
                if (dbMarket == null)
                {
                    #region Save
                    dbMarket = new Data.Market();
                    UnitofWork.RepoMarket.Add(dbMarket);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "Update");
                    #endregion
                }

                #region Set Value
                dbMarket.Name = model.Name;
                #endregion

                UnitofWork.Commit();

                model.MarketId = dbMarket.MarketID;
                model.EncryptedID = dbMarket.MarketID.ToString();
                model.TransMessage.Status = MessageStatus.Success;

            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        public MarketModel GetDetail(string encryptedId)
        {
            MarketModel model = new MarketModel();
            try
            {
                int marketId = Convert.ToInt32(encryptedId.ToDecrypt());
                #region get detail
                model = UnitofWork.RepoMarket.Where(x => x.MarketID == marketId).Select(x => new MarketModel
                {
                    MarketId = x.MarketID,
                    Name = x.Name
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.MarketId.ToString().ToEnctypt();
                }
                else
                {
                    model = new MarketModel();
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        public TransactionMessage Delete(string encryptedId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int Id = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoMarket.Where(x => x.MarketID == Id).FirstOrDefault();
                    var refCount = UnitofWork.RepoStore.Where(x => x.MarketID == Id).Count();
                    if (refCount > 0)
                    {
                        model.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "NotDelete");
                        return model;
                    }

                    if (item != null && refCount == 0)
                    {
                        UnitofWork.RepoMarket.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "Delete");
                        model.Status = MessageStatus.Success;
                    }
                    else
                    {
                       // model.Message = utilityHelper.ReadGlobalMessage("ManageMarket", "Delete");
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

    }


}

