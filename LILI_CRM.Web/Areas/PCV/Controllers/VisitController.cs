using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Domain.PCV;
using LILI_CRM.Web.ViewModels;
using LILI_CRM.Web.Areas.PCV.ViewModel;
using LILI_CRM.Web.Utility;
using LILI_CRM.DAL.CRM;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class VisitController : Controller
    {

        #region Fields
        public readonly BMSCommonSevice _VisitService;
        #endregion

        #region Constructor
        public VisitController(BMSCommonSevice VisitService)
            {
                this._VisitService = VisitService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Visit Read
            public JsonResult VisitRead()
            {
                var mmodels = GetVisitList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }


            // GET: Visit/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var userName = HttpContext.User.Identity.Name;
                    var model = new VisitInfoModel() { 
                        PreparationDate = DateTime.Now,
                        SalesPerson = userName
                    };
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: Visit/Create
            [HttpPost]
            public ActionResult Create(VisitInfoModel viewModel)
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

                        #endregion

                        var entity = viewModel.ToEntity();
                        _VisitService.BMSUnit.VisitInformationRepository.Add(entity);
                        _VisitService.BMSUnit.VisitInformationRepository.SaveChanges();

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
            // GET: Visit/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _VisitService.BMSUnit.VisitInformationRepository.GetByID(id);
                    
                    if (model != null)
                    {
                        VisitInfoModel viewModel = new VisitInfoModel();
                        viewModel = model.ToModel();

                        //VisitInfoModel viewModel = new VisitInfoModel
                        //{
                        //    Id = model.Id,
                        //    PreparationDate = model.PreparationDate,
                        //    SalesPerson = model.SalesPerson,
                        //    GenericName = model.GenericName,
                        //    ShortDesc = model.ShortDesc,
                        //    HSCode = model.HSCode,
                        //    SellingUnit = model.SellingUnit,
                        //    SamplingUnit = model.SamplingUnit,
                        //    UnitPrice = model.UnitPrice,
                        //    DutyStructure = model.DutyStructure,
                        //    Origin = model.Origin,
                        //    MOQ = model.MOQ,
                        //    LeadTime = model.LeadTime,
                        //    IsActive = Convert.ToBoolean(model.IsActive)
                        //};

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

            // POST: Visit/Edit/By ID
            [HttpPost]
            public ActionResult Edit(VisitInfoModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _VisitService.BMSUnit.VisitInformationRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _VisitService.BMSUnit.VisitInformationRepository.Update(entity);
                        _VisitService.BMSUnit.VisitInformationRepository.SaveChanges();

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

            // GET: ProductSubCategory/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _VisitService.BMSUnit.VisitInformationRepository.GetByID(id);

                    if (model != null)
                    {
                        VisitInfoModel viewModel = new VisitInfoModel();
                        viewModel = model.ToModel();

                        //ProductModel viewModel = new ProductModel
                        //{
                        //    Id = model.Id,
                        //    ProductCode = model.ProductCode,
                        //    ProductName = model.ProductName,
                        //    GenericName = model.GenericName,
                        //    ShortDesc = model.ShortDesc,
                        //    HSCode = model.HSCode,
                        //    SellingUnit = model.SellingUnit,
                        //    SamplingUnit = model.SamplingUnit,
                        //    UnitPrice = model.UnitPrice,
                        //    DutyStructure = model.DutyStructure,
                        //    Origin = model.Origin,
                        //    MOQ = model.MOQ,
                        //    LeadTime = model.LeadTime,
                        //    IsActive = Convert.ToBoolean(model.IsActive)
                        //};

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

            // POST: /Organization/Delete/By ID

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _VisitService.BMSUnit.VisitInformationRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _VisitService.BMSUnit.VisitInformationRepository.Delete_64Bit(model.Id);
                        _VisitService.BMSUnit.VisitInformationRepository.SaveChanges();

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

        #region Method
            private List<VisitInfoModel> GetVisitList()
            {
                AppConstant permission = new AppConstant();

                var VisitViewModels = _VisitService.BMSUnit.VisitInformationRepository.GetAll().ToList().Select(
                    md => new VisitInfoModel
                    {
                        Id = md.Id,
                        PreparationDate = md.PreparationDate,
                        PointOfDiscussion = md.PointOfDiscussion,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Visit", md.Id.ToString(), false, true, true)
                    }).OrderBy(o => o.Id);

                return VisitViewModels.ToList();
            }
        #endregion

    }
}
