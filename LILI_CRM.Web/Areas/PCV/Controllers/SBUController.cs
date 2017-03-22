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
    public class SBUController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
        public readonly BMSCommonSevice _SBUService;
        #endregion


        #region Constructor
        public SBUController(BMSCommonSevice SBUService)
            {
                this._SBUService = SBUService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //SBU Read
            public JsonResult SBURead()
            {
                var mmodels = GetSBUList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: SBU/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SBUService.BMSUnit.SBURepository.GetByID(id);
                    if (model != null)
                    {
                        SBUModel viewModel = new SBUModel
                        {
                            Id = model.Id,
                            SBUName = model.SBUName,
                            Descriptions = model.Descriptions
                            //,
                            //IsActive = Convert.ToBoolean(model.IsActive)
                        };

                        return PartialView("_Details", viewModel);
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

            // GET: SBUSubCategory/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {

                    var model = new SBUModel();
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: SBUSubCategory/Create
            [HttpPost]
            public ActionResult Create(SBUModel viewModel)
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
                        _SBUService.BMSUnit.SBURepository.Add(entity);
                        _SBUService.BMSUnit.SBURepository.SaveChanges();

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
            // GET: SBUSubCategory/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SBUService.BMSUnit.SBURepository.GetByID(id);
                    if (model != null)
                    {
                        SBUModel viewModel = new SBUModel
                        {
                            Id = model.Id,
                            SBUName = model.SBUName,
                            Descriptions = model.Descriptions
                            //,
                            //IsActive = Convert.ToBoolean(model.IsActive)
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

            // POST: SBUSubCategory/Edit/By ID
            [HttpPost]
            public ActionResult Edit(SBUModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _SBUService.BMSUnit.SBURepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _SBUService.BMSUnit.SBURepository.Update(entity);
                        _SBUService.BMSUnit.SBURepository.SaveChanges();

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

            // GET: SBUSubCategory/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SBUService.BMSUnit.SBURepository.GetByID(id);

                    if (model != null)
                    {
                        SBUModel viewModel = new SBUModel
                        {
                            Id = model.Id,
                            SBUName = model.SBUName,
                            Descriptions = model.Descriptions
                            //,
                            //IsActive = Convert.ToBoolean(model.IsActive)
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

            // POST: /Organization/Delete/By ID

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _SBUService.BMSUnit.SBURepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _SBUService.BMSUnit.SBURepository.Delete_64Bit(model.Id);
                        _SBUService.BMSUnit.SBURepository.SaveChanges();

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
            private List<SBUModel> GetSBUList()
            {
                AppConstant permission = new AppConstant();

                var SBUViewModels = _SBUService.BMSUnit.SBURepository.GetAll().ToList().Select(
                    md => new SBUModel
                    {
                        Id = md.Id,
                        SBUName = md.SBUName,
                        Descriptions = md.Descriptions,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "SBU", md.Id.ToString(), false, true, true)
                    }).OrderBy(o => o.SBUName).ThenBy(ot => ot.Descriptions);

                return SBUViewModels.ToList();
            }
        #endregion

    }
}
