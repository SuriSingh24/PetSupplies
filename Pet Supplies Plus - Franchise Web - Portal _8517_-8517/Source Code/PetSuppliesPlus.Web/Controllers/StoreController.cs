using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetSuppliesPlus.Framework;
using PetSuppliesPlus.Model.Store;
using PetSuppliesPlus.Model.AdMonth;
using PetSuppliesPlus.Models;
using PetSuppliesPlus.Repository;
using PetSuppliesPlus.Repository.Service;
using PetSuppliesPlus.Model.AdOption;

namespace PetSuppliesPlus.Web.Controllers
{
    [IsAuthorize]
    public class StoreController : BaseController
    {
        IStoreService _store;
        IAdMonthService _admonth;
        IStoreAdChoiceService _storeadchoice;
        ICommonService _common;
        IStoreOptionService _storeAdOption;
        IDocumentService _document;
        IAdOptionService _adoption;

        public StoreController(IUnitOfWork _unitofwork)
            : base(_unitofwork)
        {
            _store = new StoreService(_unitofwork);
            _admonth = new AdMonthService(_unitofwork);
            _storeadchoice = new StoreAdChoiceService(_unitofwork);
            _common = new CommonService(_unitofwork);
            _storeAdOption = new StoreOptionService(_unitofwork);
            _document = new DocumentService(_unitofwork);
            _adoption = new AdOptionService(_unitofwork);
        }

        /// <summary>
        ///GET: Store List 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string id)
        {
            ViewBag.SearchText = id;
            ViewBag.StoreChoice = "";
            ViewBag.CouponList = new List<SelectListItem>();
            //SessionHelper.SessionForModel = null;
            var adMonth = _admonth.GetDetailByMonthId();
            if (adMonth != null)
            {
                ViewBag.currentAdOptionName = adMonth.AdOptionName;
                ViewBag.PetPartnerInfo = adMonth.PetpartnerInfo;
                ViewBag.CorpPlanText = adMonth.CorpPlanText;
                ViewBag.CurrentMonth = (new DateTime(adMonth.Year, adMonth.Month, 1)).ToString("(MMMM yyyy)");
                var storeChoice = _storeadchoice.GetDetail((int)SessionHelper.UserId, adMonth.AdMonthID);
                if (storeChoice != null && storeChoice.Count() > 0)
                {
                    ViewBag.StoreChoice = "You have already created Ad for the month " + utilityHelper.CurrentDateTime.ToString("MMMM yyyy");
                }

                ViewBag.CouponList = _common.GetCouponList(adMonth != null ? adMonth.AdMonthID : 0).ToList();
            }
            else
            {
                ViewBag.Message = "NoMonth";
            }
            ViewBag.AdOptionList = _common.GetAdOptionList();
            ViewBag.DoucumentList = _document.GetListByMonth();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        public ActionResult DataGridList(DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
            if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
            {
                list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
            }
            else
            {
                var adMonth = _admonth.GetDetailByMonthId();



                if (adMonth != null)
                {
                    SessionHelper.LockOutDate = adMonth.LockOutEndDate.Date.ToString();
                    List<StoreModel> model = new List<StoreModel>();
                    if (dataPaging.SearchParameter != null && dataPaging.SearchParameter.Where(x => x.Key == "userid").Count() == 0)
                    {
                        dataPaging.SearchParameter.Add("userid", SessionHelper.UserId.ToString()); // user will be see assigned stores only.
                    }
                    model = _store.GetList(ref dataPaging);
                    ViewBag.DataPaging = dataPaging;

                    var adOptionList = _storeAdOption.GetList().Where(x => x.InActive == true && x.SpecialAdoption == null).ToList();

                    var adMonthCoupon = _common.GetCouponList(adMonth != null ? adMonth.AdMonthID : 0).ToList();

                    // get existing ad detail from DB
                    var storeChoice = new List<StoreAdChoiceModel>();

                    storeChoice = _storeadchoice.GetDetail((int)SessionHelper.UserId, adMonth.AdMonthID);

                    foreach (var item in model)
                    {
                        StoreAdChoiceModel adChoice = new StoreAdChoiceModel();
                        adChoice.StoreID = item.StoreID;

                        var dbAdChoice = storeChoice.Where(x => x.StoreID == item.StoreID).FirstOrDefault();
                        if (dbAdChoice != null)
                        {
                            adChoice = dbAdChoice;

                            if (dbAdChoice.FollowedCorporate) { adChoice.UserAdChoice = UserAdChoice.FollowCorporate; }
                            else if (dbAdChoice.OwnDistribution) { adChoice.UserAdChoice = UserAdChoice.DoingOwnDistribution; }
                            else if (dbAdChoice.NotPrinting) { adChoice.UserAdChoice = UserAdChoice.NotPrinting; }
                            else
                            {
                                adChoice.UserAdChoice = UserAdChoice.IndividualChoice;
                                adChoice.IsCouponApply = true;
                                adChoice.SelectedAdOption = new List<int>() { dbAdChoice.AdOptionID ?? 0 };
                                adChoice.AdOptionID = dbAdChoice.AdOptionID;
                                //adChoice.AdCouponID = dbAdChoice.AdOptionID;
                            }

                        }

                        adChoice.Storename = item.Storename;
                        adChoice.Ownername = item.Ownergroup;

                        adChoice.AdOptionList = adOptionList.Where(x => x.StoreId == item.StoreID).Select(x => new SelectListItem
                        {
                            Value = x.OptionId.ToString(),
                            Text = x.OptionName
                        }).ToList();

                        adChoice.StoreAdOptionList = new MultiSelectList(adOptionList.Where(x => x.StoreId == item.StoreID).ToList(), "OptionId", "OptionName", adChoice.SelectedAdOption);
                        adChoice.AdMonthDetail = adMonth ?? new AdMonthModel();
                        adChoice.AdMonthID = adMonth != null ? adMonth.AdMonthID : 0;
                        adChoice.CouponList = adMonthCoupon;
                        adChoice.AdCouponID = adMonth != null ? adMonth.AdCouponID : 0;
                        adChoice.AdCouponName = adMonth != null ? adMonth.AdCouponName : "";
                        adChoice.InHomeDate = adMonth != null ? adMonth.CorpInHomeDate : new DateTime();
                        list.Add(adChoice);
                    }
                    SessionHelper.SessionForModel = list;
                }
            }

            return View(list);
        }

        public ActionResult Next(List<StoreAdChoiceModel> model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            TransMessage.Message = "Next step is in progress";
            string msg = "";
            int counter = 0;
            int i = 0;
            try
            {
                if (model != null)
                {
                    List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
                    if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
                    {
                        list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
                    }

                    #region Validation
                    foreach (var item in model)
                    {
                        //set default value
                        list[i].CouponID = 0;
                        list[i].CouponName = "";
                        list[i].FollowedCorporate = false;
                        list[i].NotPrinting = false;
                        list[i].OwnDistribution = false;
                        list[i].ADOptionName = "";
                        list[i].CouponName = "";

                        list[i].UserAdChoice = item.UserAdChoice;
                        // validate
                        switch (item.UserAdChoice)
                        {
                            #region FollowCorporate
                            case UserAdChoice.FollowCorporate:
                                item.FollowedCorporate = true;
                                list[i].FollowedCorporate = true;
                                list[i].AdOptionID = list[i].AdMonthDetail.AdOptionID;
                                list[i].ADOptionName = list[i].AdMonthDetail.AdOptionName;
                                list[i].AdCouponID = list[i].AdMonthDetail.AdCouponID;
                                list[i].AdCouponName = list[i].AdMonthDetail.AdCouponName;
                                list[i].CouponID = list[i].AdMonthDetail.AdCouponID;
                                break;
                            #endregion

                            #region IndividualChoice
                            case UserAdChoice.IndividualChoice:
                                //if (item.SelectedAdOption != null && item.SelectedAdOption.Count() > 0)
                                //{
                                //    list[i].SelectedAdOption = item.SelectedAdOption;
                                //    list[i].ADOptionName = list[i].AdOptionList.Where(x => item.SelectedAdOption.Where(s => s.ToString() == x.Value).Count() > 0).Select(x => x.Text).ToArray().ToString();
                                //    list[i].AdOptionList.ForEach(x => x.Selected = false);
                                //    foreach (var ad in item.SelectedAdOption)
                                //    {
                                //        list[i].AdOptionID = ad; // todo: right now only a single ad option is saving.
                                //        list[i].ADOptionName += list[i].AdOptionList.Where(x => x.Value == ad.ToString()).FirstOrDefault().Text + ", ";
                                //        list[i].AdOptionList.Where(x => x.Value == ad.ToString()).FirstOrDefault().Selected = true;
                                //        //list[i].StoreAdOptionList.Where(x => x.Value == ad.ToString()).FirstOrDefault().Selected = true;
                                //    }
                                //    //if (!string.IsNullOrEmpty(list[i].ADOptionName) && list[i].ADOptionName.Contains(", "))
                                //    //{
                                //    //    list[i].ADOptionName = list[i].ADOptionName.Substring(0, list[i].ADOptionName.Length - 2);
                                //    //}

                                //}
                                if (item.AdOptionID > 0)
                                {
                                    list[i].AdOptionID = item.AdOptionID;
                                    list[i].ADOptionName = list[i].AdOptionList.Where(x => x.Value == item.AdOptionID.ToString()).FirstOrDefault().Text;
                                    list[i].AdOptionList.Where(x => x.Value == item.AdOptionID.ToString()).FirstOrDefault().Selected = true;
                                    //list[i].InHomeDate = item.InHomeDate;
                                    #region check coupon is selected or not
                                    if (item.IsCouponApply.HasValue) // check is select yes/no
                                    {
                                        list[i].IsCouponApply = item.IsCouponApply.Value;
                                        if (item.IsCouponApply.Value)// if choose yes then 
                                        {
                                            if (item.CouponID > 0)
                                            {
                                                list[i].CouponID = item.CouponID;
                                                list[i].CouponName = list[i].CouponList.Where(x => x.Value == item.CouponID.ToString()).FirstOrDefault().Text;
                                                list[i].CouponList.ForEach(x => x.Selected = false);
                                                list[i].CouponList.Where(x => x.Value == item.CouponID.ToString()).FirstOrDefault().Selected = true;
                                            }
                                            else
                                            {
                                                counter++;
                                                msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "ChooseCoupon").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        counter++;
                                        msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "YesNo").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    counter++;
                                    msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "IndividualChoice").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                                }
                                break;
                            #endregion

                            #region NotPrinting
                            case UserAdChoice.NotPrinting:
                                AdOptionModel admodel = new AdOptionModel();
                                admodel = _adoption.GetSpecialDetail(((byte)SpecialAddOptions.NotPrinting).ToString());
                                list[i].AdOptionID = admodel.ADOptionID;
                                list[i].ADOptionName = admodel.ADOptionName;
                                list[i].NotPrinting = true;
                                item.NotPrinting = true;

                                break;
                            #endregion

                            #region Doing Own Distribution (Creating Own Artwork)
                            case UserAdChoice.DoingOwnDistribution:
                                AdOptionModel adselfmodel = new AdOptionModel();
                                adselfmodel = _adoption.GetSpecialDetail(((byte)SpecialAddOptions.SelfDistributing).ToString());
                                list[i].AdOptionID = adselfmodel.ADOptionID;
                                list[i].ADOptionName = adselfmodel.ADOptionName;
                                list[i].OwnDistribution = true;
                                item.OwnDistribution = true;
                                break;
                            #endregion

                            #region if Not selected
                            case UserAdChoice.None:
                                counter++;
                                msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "None").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                                break;
                                #endregion
                        }
                        i++;
                    }
                    #endregion

                    if (counter > 0)
                    {
                        TransMessage.Status = MessageStatus.Error;
                        TransMessage.Message = msg;
                    }
                    else
                    {
                        SessionHelper.SessionForModel = list;
                        TransMessage.Status = MessageStatus.Success;
                        TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "RedirectToConfirmation");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }

        #region Coupon Page
        public ActionResult CouponSelection()
        {
            return RedirectToAction("AdConfirmation");
            List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
            if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
            {
                list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
                return View(list);
            }
            else
            {
                SessionHelper.SessionForModel = null;
                return RedirectToAction("Index");
            }
        }

        public ActionResult NextToConfirmation(List<StoreAdChoiceModel> model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            TransMessage.Message = "Next step is in progress";
            string msg = "";
            int counter = 0;
            int i = 0;
            try
            {
                if (model != null)
                {
                    List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
                    if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
                    {
                        list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
                    }

                    #region Validation
                    foreach (var item in model)
                    {
                        list[i].CouponID = 0;
                        list[i].CouponName = "";

                        if (item.IsCouponApply.HasValue) // check is select yes/no
                        {
                            list[i].IsCouponApply = item.IsCouponApply.Value;
                            if (item.IsCouponApply.Value)// if choose yes then 
                            {
                                if (item.CouponID > 0)
                                {
                                    list[i].CouponID = item.CouponID;
                                    list[i].CouponName = list[i].CouponList.Where(x => x.Value == item.CouponID.ToString()).FirstOrDefault().Text;
                                    list[i].CouponList.Where(x => x.Value == item.CouponID.ToString()).FirstOrDefault().Selected = true;
                                }
                                else
                                {
                                    counter++;
                                    msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "ChooseCoupon").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                                }
                            }
                        }
                        else
                        {
                            if (item.UserAdChoice != UserAdChoice.NotPrinting)
                            {
                                counter++;
                                msg += utilityHelper.ReadGlobalMessage("StoreAdChoice", "YesNo").Replace("[Store]", item.StoreID.ToString()) + "<br>";
                            }
                        }
                        i++;
                    }
                    #endregion

                    if (counter > 0)
                    {
                        TransMessage.Status = MessageStatus.Error;
                        TransMessage.Message = msg;
                    }
                    else
                    {
                        SessionHelper.SessionForModel = list;
                        TransMessage.Status = MessageStatus.Success;
                        TransMessage.Message = utilityHelper.ReadGlobalMessage("StoreAdChoice", "RedirectToConfirmation");
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }
        #endregion

        public ActionResult AdConfirmation()
        {
            List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
            if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
            {
                list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
                if (list.Count > 0 && list[0].AdMonthDetail != null)
                    ViewBag.CurrentMonth = (new DateTime(list[0].AdMonthDetail.Year, list[0].AdMonthDetail.Month, 1)).ToString("(MMMM yyyy)");
                return View(list);
            }
            else
            {
                SessionHelper.SessionForModel = null;
                return RedirectToAction("Index");
            }
        }

        public ActionResult SaveUserAdChoice(List<StoreAdChoiceModel> model)
        {
            TransactionMessage TransMessage = new TransactionMessage();
            TransMessage.Status = MessageStatus.Error;
            TransMessage.Message = "Redirecting to next step";

            string msg = "";
            int counter = 0;
            int i = 0;
            try
            {
                if (model != null)
                {
                    List<StoreAdChoiceModel> list = new List<StoreAdChoiceModel>();
                    if (SessionHelper.SessionForModel != null && SessionHelper.SessionForModel.GetType() == typeof(List<StoreAdChoiceModel>))
                    {
                        list = (List<StoreAdChoiceModel>)SessionHelper.SessionForModel;
                    }

                    #region Validation
                    foreach (var item in model)
                    {
                        list[i++].ChoiceInitials = item.ChoiceInitials;
                    }
                    SessionHelper.SessionForModel = list;
                    #endregion

                    if (counter > 0)
                    {
                        TransMessage.Status = MessageStatus.Error;
                        TransMessage.Message = msg;
                    }
                    else // save to database
                    {
                        TransMessage = _storeadchoice.Save(list);
                        if (TransMessage.Status == MessageStatus.Success)
                        {
                            // TransMessage.Message = utilityHelper.ReadGlobalMessage("Store", "Save");
                            SessionHelper.SessionForModel = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TransMessage.Message = utilityHelper.ReadGlobalMessage("Store", "Error");
                EventLogHandler.WriteLog(ex);
            }

            return Json(TransMessage, JsonRequestBehavior.DenyGet);
        }

        #region History
        public ActionResult History()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPaging"></param>
        /// <returns></returns>
        public ActionResult DataGridListForHistory(DataPagingModel dataPaging)
        {
            List<StoreAdChoiceModel> model = new List<StoreAdChoiceModel>();
            model = _storeadchoice.GetHistoryList(ref dataPaging);
            ViewBag.DataPaging = dataPaging;
            return View(model);
        }
        #endregion
    }
}