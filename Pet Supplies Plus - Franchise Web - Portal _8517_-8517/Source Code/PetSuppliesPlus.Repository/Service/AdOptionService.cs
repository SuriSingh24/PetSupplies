using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdOption;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IAdOptionService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<AdOptionModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        AdOptionModel Save(AdOptionModel model);

        /// <summary>
        /// to get page full detail
        /// </summary>
        /// <param name="encryptedId"></param>
        /// <returns></returns>
        AdOptionModel GetDetail(string encryptedId);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);

        AdOptionModel GetSpecialDetail(string specialAdoption);
    }
    public class AdOptionService : IAdOptionService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public AdOptionService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<AdOptionModel> GetList(ref DataPagingModel dataPaging)
        {
            List<AdOptionModel> model = new List<AdOptionModel>();
            var list = UnitofWork.RepoAdOption.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "name")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.ADOptionName.ToLower().Contains(value));
                    }
                    if (item.Key == "vendor")
                    {
                        string value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.Vendorname.ToLower().Contains(value));
                    }
                    if (item.Key == "isactive" && item.Value.ToLower() != "all")
                    {
                        var value = item.Value == "1";
                        list = list.Where(x => x.Inactive != value);
                    }
                    if (item.Key == "special")
                    {
                        var value = item.Value.ToString();
                        list = list.Where(x => x.SpecialChoice == value);
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
                        list = list.OrderBy(U => U.ADOptionName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOptionName);
                    }
                    break;
                case "adoptionid":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ADOptionID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ADOptionID);
                    }
                    break;
                case "vendor":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Vendorname);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Vendorname);

                    }
                    break;
                case "special":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.SpecialChoice);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.SpecialChoice);

                    }
                    break;
                case "isactive":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.Inactive);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.Inactive);

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

            model = list.ToList().Select(x => new AdOptionModel
            {
                ADOptionID = x.ADOptionID,
                ADOptionName = x.ADOptionName,
                MinimumCirculation = x.MinimumCirculation,
                Vendorname = x.Vendorname,
                IsActive = x.Inactive ?? true,
                SpecialChoice = x.SpecialChoice,
            }).ToList();

            model.ForEach(x => x.EncryptedID = x.ADOptionID.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public AdOptionModel Save(AdOptionModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {

                #region check duplicate 
                if (UnitofWork.RepoAdOption.Where(x => x.ADOptionName == model.ADOptionName && x.ADOptionID != model.ADOptionID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdOption", "Duplicate");
                    return model;
                }
                #endregion

                #region check duplicate Specail Ad option
                if (model.SpecialChoice != null)
                {
                    if (UnitofWork.RepoAdOption.Where(x => x.Inactive == true && x.SpecialChoice == model.SpecialChoice && x.ADOptionID != model.ADOptionID).Count() > 0)
                    {
                        model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdOption", "SpecialDuplicate");
                        return model;
                    }
                }
                #endregion

                ADOption dbAdOption = UnitofWork.RepoAdOption.Where(x => x.ADOptionID == model.ADOptionID).FirstOrDefault();

                bool isSave = false;

                if (dbAdOption == null)
                {
                    #region Save
                    dbAdOption = new ADOption();
                    UnitofWork.RepoAdOption.Add(dbAdOption);
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdOption", "Save");
                    isSave = true;
                    #endregion
                }
                else
                {
                    #region Update
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("AdOption", "Update");
                    #endregion
                }
                #region Set Value
                dbAdOption.ADOptionName = model.ADOptionName;
                dbAdOption.MinimumCirculation = model.MinimumCirculation;
                dbAdOption.Vendorname = model.Vendorname;
                dbAdOption.Inactive = model.IsActive;
                dbAdOption.SpecialChoice = model.SpecialChoice;
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
        public AdOptionModel GetDetail(string encryptedId)
        {
            AdOptionModel model = new AdOptionModel();
            try
            {
                int AdOptionId = Convert.ToInt32(encryptedId);
                #region get detail
                model = UnitofWork.RepoAdOption.Where(x => x.ADOptionID == AdOptionId).Select(x => new AdOptionModel
                {
                    ADOptionName = x.ADOptionName,
                    MinimumCirculation = x.MinimumCirculation,
                    Vendorname = x.Vendorname,
                    ADOptionID = x.ADOptionID,
                    IsActive = x.Inactive ?? true,
                    SpecialChoice = x.SpecialChoice,
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.ADOptionID.ToString().ToEnctypt();
                }
                else
                {
                    model = new AdOptionModel();
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
            model.Message = utilityHelper.ReadGlobalMessage("AdOption", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoAdOption.Where(x => x.ADOptionID == ID).FirstOrDefault();

                    ////reference count
                    //int refCount = 0;
                    //refCount = UnitofWork.RepoStoreAdChoice.Where(x => x.AdOptionID == ID).Count();
                    //refCount += UnitofWork.RepoAllowedStoreOption.Where(x => x.AdOptionID == ID).Count();

                    //if (refCount > 0)
                    //{
                    //    model.Message = utilityHelper.ReadGlobalMessage("AdOption", "NotDelete");
                    //    return model;
                    //}

                    if (item != null)
                    {
                        //UnitofWork.RepoAdOption.Delete(item);
                        item.Inactive = false;
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("AdOption", "Delete");
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

        public AdOptionModel GetSpecialDetail(string specialAdoption)
        {
            AdOptionModel model = new AdOptionModel();
            try
            {
                #region get detail
                model = UnitofWork.RepoAdOption.Where(x => x.SpecialChoice == specialAdoption).Select(x => new AdOptionModel
                {
                    ADOptionName = x.ADOptionName,
                    MinimumCirculation = x.MinimumCirculation,
                    Vendorname = x.Vendorname,
                    ADOptionID = x.ADOptionID,
                    IsActive = x.Inactive ?? true,
                    SpecialChoice = x.SpecialChoice,
                }).FirstOrDefault();
                #endregion

                if (model != null)
                {
                    model.EncryptedID = model.ADOptionID.ToString().ToEnctypt();
                }
                else
                {
                    model = new AdOptionModel();
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
