using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Domain.PCV;
using LILI_CRM.Web.ViewModels;
using LILI_CRM.Web.Areas.PCV.ViewModel;
using LILI_CRM.Web.Utility;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class PettyCashVoucherController : Controller
    {
        #region Fields
        public readonly BMSCommonSevice _pettyCashVoucherInfoService;
        #endregion

        #region Constructor
        public PettyCashVoucherController(BMSCommonSevice pettyCashVoucherInfoService)
        {
            this._pettyCashVoucherInfoService = pettyCashVoucherInfoService;
        }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }

        //Voucher  Read
        public JsonResult PettyCashVoucherRead()
        {
            var mmodels = GetPettyCashVoucherList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        // GET: /PettyCashVoucher/Create
        public ActionResult Create()
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
                var userName = HttpContext.User.Identity.Name;


                var payTo = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().UserName;
                var staffId = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().StaffID;
                var companyCode = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().CompanyCode;
                var busiNo = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().BusiNo;
                var dept = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().Dept;
                var userId = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().UserId;

                var model = new VoucherViewModel()
                {
                    PayTo = payTo,
                    StaffID = staffId,
                    CompanyCode = companyCode,
                    BusiNo = busiNo,
                    Dept = dept,
                    UserId = userId,
                    VoucherDate = DateTime.Now
                };

                return PartialView("_Create", model);
            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                return PartialView("_ErrorPopUp", errorViewModel);
            }

        }

        // POST: /PettyCashVoucher/Create
        [HttpPost]
        public ActionResult Create(VoucherViewModel viewModel)
        {
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    #region Current User

                    var userName = HttpContext.User.Identity.Name;
                    viewModel.IUser = userName;
                    viewModel.IDate = DateTime.Now;
                    viewModel.EDate = DateTime.Now;
                    viewModel.TransDate = DateTime.Now;
                    viewModel.Posted = "N";
                    viewModel.Period = 0;
                    viewModel.JVNo = "";

                    #endregion

                    List<VoucherDetailEntityModel> list = new List<VoucherDetailEntityModel>();

                    if (Session["lstPettyCashVoucherDetails"] != null)
                    {
                        list = (List<VoucherDetailEntityModel>)Session["lstPettyCashVoucherDetails"];                        
                    }
                   

                    var entity = viewModel.ToEntity();
                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.Add(entity);
                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.SaveChanges();

                    Session["DailyBreakdownMasterId"] = entity.Id;

                    #region Save Detail

                    if (list != null && list.Count > 0)
                    {
                        foreach (VoucherDetailEntityModel detail in list)
                        {
                            detail.VoucherNo = Convert.ToInt32(entity.Id);
                            //detail.VoucherNo = Convert.ToInt32(entity.VoucherNo);
                            detail.ASCode = "";
                            var detailEntity = detail.ToEntity();
                            _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.Add(detailEntity);
                            _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.SaveChanges();
                        }
                    }

                    #endregion

                    return Content(Boolean.TrueString);
                    //return Content("Information has been saved successfully");
                }

                strMessage = Common.GetModelStateErrorMessage(ModelState);
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
            }

            return Content(strMessage);
        }

        // GET: /PettyCashVoucher/Edit/By ID
        public ActionResult Edit(long id)
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
                var model = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetByID(id);

                Session["VoucherNo"] = model.Id;

                if (model != null)
                {
                    VoucherViewModel viewModel = new VoucherViewModel { 
                        Id = Convert.ToInt64(model.Id), 
                        //VoucherNo = model.VoucherNo, 
                        CompanyCode = model.CompanyCode, 
                        PayTo = model.PayTo, 
                        StaffID = model.StaffID, 
                        VoucherDate = model.VoucherDate,
                        UserId = model.UserId,
                        Posted = model.Posted,
                        Period = model.Period,
                        JVNo = model.JVNo
                        };
                    return PartialView("_Edit", viewModel);
                }
                else
                {
                    errorViewModel.ErrorMessage = CommonMessage.ErrorOccurred;
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                return PartialView("_ErrorPopUp", errorViewModel);
            }

        }

        // POST: /PettyCashVoucher/Edit/By ID
        [HttpPost]
        public ActionResult Edit(VoucherViewModel viewModel)
        {
            var strMessage = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {

                    List<VoucherDetailEntityModel> list = new List<VoucherDetailEntityModel>();

                    if (Session["lstPettyCashVoucherDetails"] != null)
                    {
                        list = (List<VoucherDetailEntityModel>)Session["lstPettyCashVoucherDetails"];
                    }


                    //var model = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetByID(viewModel.Id);
                    viewModel.Posted = "N";
                    viewModel.TransDate = DateTime.Now;
                    viewModel.JVNo = "";
                    viewModel.Period = 0;
                    var entity = viewModel.ToEntity();


                    // Get previous detail list
                    var lst = _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.GetAll().Where(
                                q => q.VoucherNo == entity.Id);


                    foreach (var dt in lst)
                    {
                        _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                    }

                    foreach (VoucherDetailEntityModel detail in list)
                    {
                        detail.VoucherNo = Convert.ToInt64(entity.Id);
                        detail.ASCode = "";
                        var detailEntity = detail.ToEntity();

                        _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.Add(detailEntity);
                        _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.SaveChanges();
                    }


                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.Update(entity);
                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                strMessage = Common.GetModelStateErrorMessage(ModelState);
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Update);
            }

            return Content(strMessage);
        }

        public ActionResult Delete(long id)
        {
            var errorViewModel = new ErrorViewModel();

            if (Session["MasterId"] != null)
            {
                Session["MasterId"] = null;
            }
            // Clear detail list
            if (Session["lstPettyCashVoucherDetails"] != null)
            {
                Session["lstPettyCashVoucherDetails"] = null;
            }

            try
            {
                var model = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetByID(id);

                if (model != null)
                {
                    VoucherViewModel viewModel = new VoucherViewModel
                    {
                        Id = Convert.ToInt32(model.Id)

                    };

                    return PartialView("_Delete", viewModel);
                }
                else
                {
                    errorViewModel.ErrorMessage = CommonMessage.ErrorOccurred;
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                return PartialView("_ErrorPopUp", errorViewModel);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            var strMessage = string.Empty;
            try
            {
                var model = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetByID(id);

                if (model != null)
                {
                    var lst = _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.GetAll().Where(
                                q => q.VoucherNo == model.Id);

                    // Delete details
                    foreach (var dt in lst)
                    {
                        _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.Delete_64Bit(dt.Id);
                    }

                    // Delete master
                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.Delete_64Bit(model.Id);
                    _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.SaveChanges();

                    return Content(Boolean.TrueString);
                }

                strMessage = Common.GetModelStateErrorMessage(ModelState);
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Delete);
            }

            return Content(strMessage);
        }

        #endregion

        #region Methods
        private List<VoucherViewModel> GetPettyCashVoucherList()
        {
            AppConstant permission = new AppConstant();

            var userName = HttpContext.User.Identity.Name;
            var userId = _pettyCashVoucherInfoService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().UserId;

            var voucherInfoViewModels = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetAll().Where(c=>c.UserId == userId && c.Posted == "N").ToList().Select(
                md => new VoucherViewModel
                {
                    Id = Convert.ToInt64(md.Id),
                    VoucherNo =  md.Id,
                    CompanyCode = md.CompanyCode,
                    PayTo = md.PayTo,
                    StaffID = md.StaffID,
                    VoucherDate = md.VoucherDate,
                    UserId = md.UserId,
                    //IsActive = md.IsActive,
                    ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "PettyCashVoucher", md.Id.ToString(), false, true, true)
                }).OrderBy(o => o.VoucherNo).ThenBy(ot => ot.VoucherDate);

            return voucherInfoViewModels.ToList();
        }

        public JsonResult ReadDebitACList()
        {
            var mmodels = GetDebitACList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<VoucherDebitACModel> GetDebitACList()
        {
            AppConstant permission = new AppConstant();

            var voucherDebitACModels = _pettyCashVoucherInfoService.BMSUnit.viewWebACMasterRepository.GetAll().ToList().Select(
                md => new VoucherDebitACModel
                {
                    AMCode = md.AMCode,
                    AMDetails = md.AMDetails
                    //IsActive = md.IsActive,
                   // ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "PettyCashVoucher", md.Id.ToString(), false, true, true)
                }).OrderBy(o => o.AMCode).ThenBy(ot => ot.AMCode);

            return voucherDebitACModels.ToList();
        }


        // For saving detail data

        public JsonResult SetVoucherDetailsListForSave(List<VoucherDetailViewModel> lstPettyCashVoucherDetails)
        {
            var strMessage = string.Empty;

            // Clear detail list
            Session["lstPettyCashVoucherDetails"] = null;


            try
            {

                List<VoucherDetailEntityModel> list = new List<VoucherDetailEntityModel>();
                // Add new detail information
                foreach (var item in lstPettyCashVoucherDetails)
                {
                    VoucherDetailEntityModel entityModel = new VoucherDetailEntityModel();

                    entityModel.IUser = User.Identity.Name;
                    entityModel.IDate = DateTime.Now;
                    entityModel.EUser = User.Identity.Name;
                    entityModel.EDate = DateTime.Now;

                    entityModel.Id = item.Id;
                    entityModel.AMCode = item.AMCode;
                    entityModel.ASCode = item.ASCode;
                    entityModel.Narration = item.Narration;
                    entityModel.Debit = Convert.ToDecimal(item.Debit);
                    entityModel.Credit = Convert.ToDecimal(item.Credit);

                    list.Add(entityModel);
                }

                Session["lstPettyCashVoucherDetails"] = list;
                strMessage = Boolean.TrueString;
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
            }

            return Json(new { strMessage = strMessage });
        }

        public JsonResult ShowVoucherDetailListToGridRead()
        {
            int voucherNo = Convert.ToInt32(Session["VoucherNo"]);

            var voucherDetailsViewModel = from voucherDetail in _pettyCashVoucherInfoService.BMSUnit.WebVoucherDetailsRepository.GetAll().Where(q => q.VoucherNo == voucherNo)
                    select new VoucherDetailViewModel()
                    {
                        Id = voucherDetail.Id,
                        VoucherNo = voucherDetail.VoucherNo,
                        AMCode = voucherDetail.AMCode,
                        AMDetails = _pettyCashVoucherInfoService.BMSUnit.viewWebACMasterRepository.GetAll().Where(c=>c.AMCode == voucherDetail.AMCode).FirstOrDefault().AMDetails,
                        ASCode = voucherDetail.ASCode,
                        Narration = voucherDetail.Narration,
                        Debit = (float)voucherDetail.Debit,
                        Credit = (float)voucherDetail.Credit
                    };

            var modelList = voucherDetailsViewModel.OrderBy(x => x.Id).ToList();

            return Json(modelList, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}
