using PetSuppliesPlus.Data;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.AdMonth;
using PetSuppliesPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetSuppliesPlus.Repository.Service
{
    public interface IDocumentService
    {
        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of CompanyModel</returns>
        List<DocumentModel> GetList(ref DataPagingModel dataPaging);

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">CompanyModel</param>
        /// <returns>CompanyModel</returns>
        DocumentModel Save(DocumentModel model);

        /// <summary>
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        TransactionMessage Delete(string encryptedId);

        List<DocumentModel> GetListByMonth();

    }
    public class DocumentService : IDocumentService
    {
        #region Unit of Work

        public IUnitOfWork UnitofWork;

        public DocumentService(IUnitOfWork _unifOfWrok)
        {
            UnitofWork = _unifOfWrok;
        }

        #endregion

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<DocumentModel> GetList(ref DataPagingModel dataPaging)
        {
            List<DocumentModel> model = new List<DocumentModel>();
            var list = UnitofWork.RepoDocument.GetAll();
            #region Searching
            if (dataPaging.SearchParameter != null)
            {
                foreach (var item in dataPaging.SearchParameter)
                {
                    if (item.Key == "id")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.ID == value);
                    }
                    if (item.Key == "month")
                    {
                        var value = Convert.ToInt32(item.Value);
                        list = list.Where(x => x.MonthID == value);
                    }

                    if (item.Key == "name")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.FileName.ToLower().Contains(value));
                    }
                    if (item.Key == "path")
                    {
                        var value = item.Value.ToString().ToLower().Trim();
                        list = list.Where(x => x.FilePath.ToLower().Contains(value));
                    }
                }
            }
            #endregion

            #region Sorting of list
            switch (dataPaging.SortingColumn.Trim().ToLower())
            {
                case "id":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.ID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.ID);
                    }
                    break;
                case "month":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.MonthID);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.MonthID);
                    }
                    break;
                case "name":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.FileName);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.FileName);
                    }
                    break;
                case "path":
                    if (dataPaging.SortingOrder == SortingOrder.Ascending)
                    {
                        list = list.OrderBy(U => U.FilePath);
                    }
                    else
                    {
                        list = list.OrderByDescending(U => U.FilePath);
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

            model = list.ToList().Select(x => new DocumentModel
            {
                ID = x.ID,
                MonthID = x.MonthID ?? 0,
                FileName = x.FileName ?? "",
                FilePath = x.FilePath ?? "",
            }).ToList();

            model.ForEach(x => x.EncyptedID = x.ID.ToString().ToEnctypt());
            return model;
        }

        /// <summary>
        /// to save or update Page
        /// </summary>
        /// <param name="model">UsersModel</param>
        /// <returns>UsersModel</returns>
        public DocumentModel Save(DocumentModel model)
        {
            model.TransMessage = new TransactionMessage();
            model.TransMessage.Status = MessageStatus.Error;
            try
            {
                #region check duplicate 
                if (UnitofWork.RepoDocument.Where(x => x.FileName == model.FileName && x.MonthID == model.MonthID).Count() > 0)
                {
                    model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Document", "Duplicate");
                    return model;
                }
                #endregion

                bool isSave = false;

                #region Save
                Document Document = new Document();
                Document.FileName = model.FileName;
                Document.FilePath = model.FilePath;
                Document.MonthID = model.MonthID;
                UnitofWork.RepoDocument.Add(Document);
                model.TransMessage.Message = utilityHelper.ReadGlobalMessage("Document", "Save");
                isSave = true;
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
        /// to delete page 
        /// </summary>
        /// <param name="encryptedId">encrypted page id</param>
        /// <returns></returns>
        public TransactionMessage Delete(string encryptedId)
        {
            TransactionMessage model = new TransactionMessage();
            model.Status = MessageStatus.Error;
            model.Message = utilityHelper.ReadGlobalMessage("Document", "InvalidRequest");
            try
            {
                if (encryptedId.IsValidEncryptedID())
                {
                    int ID = Convert.ToInt32(encryptedId.ToDecrypt());
                    var item = UnitofWork.RepoDocument.Where(x => x.ID == ID).FirstOrDefault();

                    if (item != null)
                    {
                        UnitofWork.RepoDocument.Delete(item);
                        UnitofWork.Commit();
                        model.Message = utilityHelper.ReadGlobalMessage("Document", "Delete");
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

        /// <summary>
        /// to get filter list with sorting and paging
        /// </summary>
        /// <param name="dataPaging">DataPagingModel</param>
        /// <returns>list of RoleModel</returns>
        public List<DocumentModel> GetListByMonth()
        {
            List<DocumentModel> model = new List<DocumentModel>();
            var month = utilityHelper.CurrentDateTime.Month;
            var list = UnitofWork.RepoDocument.GetAll().Where(x => x.AdMonth != null && x.AdMonth.Month == month);
            model = list.ToList().Select(x => new DocumentModel
            {
                ID = x.ID,
                MonthID = x.MonthID ?? 0,
                FileName = x.FileName ?? "",
                FilePath = x.FilePath ?? "",
            }).ToList();

            model.ForEach(x => x.EncyptedID = x.ID.ToString().ToEnctypt());
            return model;
        }

    }
}
