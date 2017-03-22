using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SouthWind.Domain.INV;
using SouthWind.Web.ViewModels;
//using SouthWind.Web.Areas.SD.Models;
using SouthWind.Web.Utility;
using LILI_BMS.Web.Utility;
using SouthWind.DAL.INV;
using SouthWind.Web.Areas.SD.Models;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class ArticleCategoryController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
        public readonly SWCommonService _sdArtCategoryService;
        #endregion


        #region Constructor
            public ArticleCategoryController(SWCommonService sdArtCategoryService)
            {
                this._sdArtCategoryService = sdArtCategoryService;            
            }
        #endregion


        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //ArticleCategory Read
            public JsonResult ArticleCategoryRead()
            {
                var mmodels = GetArticleCategoryList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /ArticleCategory/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetByID(id);
                    if (model != null)
                    {
                        ArticleCategoryModel viewModel = new ArticleCategoryModel
                        {
                            Id = model.Id,
                            CategoryName = model.CategoryName, 
                            Description = model.Description
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

            // GET: /ArticleCategory/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = new ArticleCategoryModel();
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /ArticleCategory/Create
            [HttpPost]
            public ActionResult Create(ArticleCategoryModel viewModel)
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
                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.Add(entity);
                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.SaveChanges();

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
            // GET: /ArticleCategory/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetByID(id);

                    if (model != null)
                    {
                        ArticleCategoryModel viewModel = new ArticleCategoryModel
                        {
                            Id = model.Id,
                            CategoryName = model.CategoryName,
                            Description = model.Description
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

            // POST: /ArticleCategory/Edit/By ID
            [HttpPost]
            public ActionResult Edit(ArticleCategoryModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.Update(entity);
                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.SaveChanges();

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

            // GET: /ArticleCategory/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetByID(id);

                    if (model != null)
                    {
                        ArticleCategoryModel viewModel = new ArticleCategoryModel
                        {
                            Id = model.Id,
                            CategoryName = model.CategoryName,
                            Description = model.Description
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

            // POST: /ArticleCategory/Delete/By ID

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.Delete_64Bit(model.Id);
                        _sdArtCategoryService.INVUnit.ArticleCategoryRepository.SaveChanges();

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
            private List<ArticleCategoryModel> GetArticleCategoryList()
            {
                AppConstant permission = new AppConstant();

                var articleCategoryViewModels = _sdArtCategoryService.INVUnit.ArticleCategoryRepository.GetAll().ToList().Select(
                    md => new ArticleCategoryModel
                    {
                        Id = md.Id,
                        CategoryName = md.CategoryName,
                        Description = md.Description,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("SD", "ArticleCategory", md.Id.ToString(), true, true, true)
                    }).OrderBy(o => o.CategoryName).ThenBy(ot => ot.Description);

                return articleCategoryViewModels.ToList();
            }
        #endregion

    }
}
