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
    public class DivisionController : Controller
    {
        //
        // GET: /SD/Organization/



        #region Fields
        public readonly BMSCommonSevice _DivisionService;
        #endregion


        #region Constructor
        public DivisionController(BMSCommonSevice ArticleService)
            {
                this._DivisionService = ArticleService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Customer Read
            public JsonResult DivisionRead()
            {
                var mmodels = GetDivisionList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /Division/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = new DivisionModel();                    
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /Division/Create
            [HttpPost]
            public ActionResult Create(DivisionModel viewModel)
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
                        _DivisionService.BMSUnit.DivisionRepository.Add(entity);
                        _DivisionService.BMSUnit.DivisionRepository.SaveChanges();

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

            // GET: Division/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _DivisionService.BMSUnit.DivisionRepository.GetByID(id);
                    if (model != null)
                    {
                        DivisionModel viewModel = new DivisionModel
                        {
                            Id = model.Id,
                            DivisionName = model.DivisionName,
                            Descriptions = model.Descriptions
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

            // POST: Division/Edit/By ID
            [HttpPost]
            public ActionResult Edit(DivisionModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _DivisionService.BMSUnit.DivisionRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _DivisionService.BMSUnit.DivisionRepository.Update(entity);
                        _DivisionService.BMSUnit.DivisionRepository.SaveChanges();

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

            // GET: Division View
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _DivisionService.BMSUnit.DivisionRepository.GetByID(id);
                    if (model != null)
                    {
                        DivisionModel viewModel = new DivisionModel
                        {
                            Id = model.Id,
                            DivisionName = model.DivisionName,
                            Descriptions = model.Descriptions
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

            // GET: Division/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _DivisionService.BMSUnit.DivisionRepository.GetByID(id);

                    if (model != null)
                    {
                        DivisionModel viewModel = new DivisionModel
                        {
                            Id = model.Id,
                            DivisionName = model.DivisionName,
                            Descriptions = model.Descriptions
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

            // POST: Division Delete/Delete/By ID

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _DivisionService.BMSUnit.DivisionRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _DivisionService.BMSUnit.DivisionRepository.Delete_64Bit(model.Id);
                        _DivisionService.BMSUnit.DivisionRepository.SaveChanges();

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
            private List<DivisionModel> GetDivisionList()
            {
                var DivisionViewModels = _DivisionService.BMSUnit.DivisionRepository.GetAll().ToList().Select(
                    md => new DivisionModel
                    {
                        Id = md.Id,
                        DivisionName = md.DivisionName,
                        Descriptions = md.Descriptions,                        
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Division", md.Id.ToString(), true, true, true)
                    }).OrderBy(o => o.DivisionName).ThenBy(ot => ot.Descriptions);

                return DivisionViewModels.ToList();
            }
        #endregion

           

    }
}
