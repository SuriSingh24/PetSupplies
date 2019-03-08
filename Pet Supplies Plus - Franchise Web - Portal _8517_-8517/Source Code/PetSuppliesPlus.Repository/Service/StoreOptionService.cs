using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdOption;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetSuppliesPlus.Model.Store;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IStoreOptionService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<StoreOptionModel> GetList(ref DataPagingModel dataPaging);

        List<StoreOptionModel> GetList();

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        StoreOptionModel Save(StoreOptionModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        StoreOptionModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);

        /// <summary>
        /// to save list of stores in repect of an ad option.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        AllowedStoreOptionModel Save(AllowedStoreOptionModel model);

        /// <summary>
        /// to get all allowstore ad option list
        /// </summary>
        /// <param name="adOptionId"></param>
        /// <returns></returns>
        List<AllowedStoreOptionDetailModel> GetList(int adOptionId);
    }
    public class StoreOptionService : IStoreOptionService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public StoreOptionService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<StoreOptionModel> GetList(ref DataPagingModel dataPaging)
        {
            List<StoreOptionModel> model = new List<StoreOptionModel>();
            var list = UnitofWork.RepoAllowedStoreOption.GetAll();
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
                    if (item.Key == "storeid")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.StoreID == value);
                    }
                    if (item.Key == "optionname")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOption.ADOptionName.ToLower().Contains(value));
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
                case "storeid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.StoreID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.StoreID);
                    }
                    break;
                case "allowedstoreoptionid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.AllowedStoreOptionID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.AllowedStoreOptionID);
                    }
                    break;
                case "optionname":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ADOption.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOption.ADOptionName);

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

            model = list.ToList().Select(x => new StoreOptionModel
            {
                AllowedStoreOptionId = x.AllowedStoreOptionID,
                OptionId = x.AdOptionID ?? 0,
                OptionName = x.ADOption.ADOptionName ?? "",
                StoreId = x.StoreID,
                StoreName = x.Store.Storename,
                SpecialAdoption = x.ADOption.SpecialChoice,
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.AllowedStoreOptionId.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to get list 
        /// </summary>
        /// <returns>list of RoleModel</returns>
        public List<StoreOptionModel> GetList()
        {
            List<StoreOptionModel> model = new List<StoreOptionModel>();
            model = UnitofWork.RepoAllowedStoreOption.GetAll().Select(x => new StoreOptionModel
            {
                AllowedStoreOptionId = x.AllowedStoreOptionID,
                OptionId = x.AdOptionID ?? 0,
                OptionName = x.ADOption.ADOptionName ?? "",
                StoreId = x.StoreID,
                StoreName = x.Store.Storename,
                InActive = x.ADOption.Inactive ?? false,
                SpecialAdoption = x.ADOption.SpecialChoice,
                EncryptedID = x.AllowedStoreOptionID.ToString().ToEnctypt()

            }).ToList();

            //model.ForEach(x => x.EncryptedID = x.AllowedStoreOptionId.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public StoreOptionModel Save(StoreOptionModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {

                #region check duplicate 
                if (UnitofWork.RepoAllowedStoreOption.Where(x => x.StoreID == model.StoreId && x.AdOptionID == model.OptionId && x.AllowedStoreOptionID != model.AllowedStoreOptionId).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Duplicate");
                    return model;
                }
                #endregion

                AllowedStoreOption dbAllowedOption = UnitofWork.RepoAllowedStoreOption.Where(x => x.AllowedStoreOptionID == model.AllowedStoreOptionId).FirstOrDefault();

                bool isSave = false;

                if (dbAllowedOption == null)
                {
                    #region Save
                    dbAllowedOption = new AllowedStoreOption();
                    UnitofWork.RepoAllowedStoreOption.Add(dbAllowedOption);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Update");
                    #endregion
                }
                #region Set Value
                dbAllowedOption.AdOptionID = model.OptionId;
                dbAllowedOption.StoreID = model.StoreId;
                #endregion

                UnitofWork.Commit();
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
        public StoreOptionModel GetDetail(string encryptedId)
        {
            StoreOptionModel model = new StoreOptionModel();
            try
            {
                int AllowedOptionId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoAllowedStoreOption.Where(x => x.AllowedStoreOptionID == AllowedOptionId).Select(x => new StoreOptionModel
                {
                    AllowedStoreOptionId = x.AllowedStoreOptionID,
                    OptionId = x.AdOptionID ?? 0,
                    StoreId = x.StoreID,
                    OptionName = x.ADOption.ADOptionName,
                    StoreName = x.Store.Storename
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.AllowedStoreOptionId.ToString().ToEnctypt();
                }
                else
                {
                    model = new StoreOptionModel();
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
            model.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoAllowedStoreOption.Where(x => x.AllowedStoreOptionID == ID).FirstOrDefault();

                    if (item != null)
                    {
                        UnitofWork.RepoAllowedStoreOption.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Delete");
                        model.Status = MessageStatus.Success;
                    }
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        // Save all store with a single AdOption
        public AllowedStoreOptionModel Save(AllowedStoreOptionModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;

            try
            {
                if (model.AdOptionID > 0 && model.Detail != null && model.Detail.Count() > 0)
                {
                    #region Delete Existing Entry
                    var dbList = UnitofWork.RepoAllowedStoreOption.Where(x => x.AdOptionID == model.AdOptionID).ToList();
                    foreach (var item in dbList)
                    {
                        UnitofWork.RepoAllowedStoreOption.Delete(item);
                    }
                    UnitofWork.Commit();
                    #endregion

                    foreach (var item in model.Detail)
                    {
                        if (item.IsSelect && item.StoreID > 0)
                        {
                            UnitofWork.RepoAllowedStoreOption.Add(new AllowedStoreOption()
                            {
                                AdOptionID = model.AdOptionID,
                                StoreID = item.StoreID,
                            });
                        }
                    }
                    UnitofWork.Commit();
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("ManageStoreOption", "Save");
                    model.TransMessage.Status = MessageStatus.Success;
                }
            }
            catch (Exception ex)
            {
                EventLogHandler.WriteLog(ex);
            }
            return model;
        }

        /// <summary>
        /// to get list 
        /// </summary>
        /// <returns>list of RoleModel</returns>
        public List<AllowedStoreOptionDetailModel> GetList(int adOptionId)
        {
            List<AllowedStoreOptionDetailModel> model = new List<AllowedStoreOptionDetailModel>();
            var AllowAdOptions = UnitofWork.RepoAllowedStoreOption.Where(x => x.AdOptionID == adOptionId).Select(x => new
            {
                x.AdOptionID,
                x.ADOption.ADOptionName,
                x.StoreID,
                x.ADOption.Inactive,
                x.AllowedStoreOptionID,
            }).ToList();

            string adOptionName = AllowAdOptions.Count > 0 ? AllowAdOptions.FirstOrDefault().ADOptionName : "";

            model = UnitofWork.RepoStore.GetAll().Select(x => new AllowedStoreOptionDetailModel
            {
                AllowedStoreOptionID = AllowAdOptions.Where(a => a.StoreID == x.StoreID).Count() > 0 ? AllowAdOptions.Where(a => a.StoreID == x.StoreID).FirstOrDefault().AllowedStoreOptionID : 0,
                AdOptionID = adOptionId,
                AdOptionName = adOptionName,
                StoreID = x.StoreID,
                StoreName = x.Storename,
                IsSelect = AllowAdOptions.Where(a => a.StoreID == x.StoreID).Count() > 0,
            }).OrderBy(x => x.IsSelect).ToList();

            //model = UnitofWork.RepoAllowedStoreOption.Where(x => x.AdOptionID == adOptionId).Select(x => new AllowedStoreOptionDetailModel
            //{
            //    AllowedStoreOptionID = x.AllowedStoreOptionID,
            //    AdOptionID = x.AdOptionID ?? 0,
            //    AdOptionName = x.ADOption.ADOptionName ?? "",
            //    StoreID = x.StoreID ?? 0,
            //    StoreName = x.Store.Storename,
            //    IsSelect = true,
            //}).ToList();



            model.ForEach(x => x.EncryptedID = x.AllowedStoreOptionID.ToString().ToEnctypt());
            return model;
        }

    }
}
