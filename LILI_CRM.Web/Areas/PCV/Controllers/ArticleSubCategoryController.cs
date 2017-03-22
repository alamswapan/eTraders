using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SouthWind.Domain.INV;
using SouthWind.Web.ViewModels;
using SouthWind.Web.Areas.SD.Models;
using SouthWind.Web.Utility;
using LILI_BMS.Web.Utility;
using SouthWind.DAL.INV;

namespace SouthWind.Web.Areas.SD.Controllers
{
    public class ArticleSubCategoryController : Controller
    {
        //
        // GET: /SD/ArticleSubCategory/

        #region Fields
        public readonly SWCommonService _invArtSubCategoryService;
        #endregion


        #region Constructor
        public ArticleSubCategoryController(SWCommonService invArtSubCategoryService)
            {
                this._invArtSubCategoryService = invArtSubCategoryService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //ArticleSubCategory Read
            public JsonResult ArticleSubCategoryRead()
            {
                var mmodels = GetSubCategoryList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /ArticleSubCategory/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetByID(id);
                    if (model != null)
                    {
                        ArticleSubCategoryModel viewModel = new ArticleSubCategoryModel 
                        {
                            Id = model.Id,
                            ArticleCategory = _invArtSubCategoryService.INVUnit.ArticleCategoryRepository.GetByID(model.CategoryId).CategoryName,
                            SubCategoryName = model.SubCategoryName,
                            Description = model.Description,
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

            // GET: /ArticleSubCategory/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {

                    //var subCategoryList = SelectListItemExtension.PopulateDropdownList(_invArticleService.INVUnit.ArticleSubCategoryRepository.GetAll().ToList<tblArticleSubCategory>(), "Id", "SubCategoryName").ToList();
                    var categoryList = SelectListItemExtension.PopulateDropdownList(_invArtSubCategoryService.INVUnit.ArticleCategoryRepository.GetAll().ToList<tblArticleCategory>(), "Id", "CategoryName").ToList();
                    var model = new ArticleSubCategoryModel()
                    {
                        ddlArticleCategory = categoryList 
                        

                    };
                    
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /ArticleSubCategory/Create
            [HttpPost]
            public ActionResult Create(ArticleSubCategoryModel viewModel)
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
                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.Add(entity);
                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.SaveChanges();

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
            // GET: /ArticleSubCategory/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetByID(id);
                    var categoryList = SelectListItemExtension.PopulateDropdownList(_invArtSubCategoryService.INVUnit.ArticleCategoryRepository.GetAll().ToList<tblArticleCategory>(), "Id", "CategoryName").ToList();

                    if (model != null)
                    {
                        ArticleSubCategoryModel viewModel = new ArticleSubCategoryModel
                        {
                            Id = model.Id,
                            ddlArticleCategory = categoryList,
                            CategoryId = model.CategoryId,

                            SubCategoryName = model.SubCategoryName,
                            Description = model.Description,
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

            // POST: /ArticleSubCategory/Edit/By ID
            [HttpPost]
            public ActionResult Edit(ArticleSubCategoryModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.Update(entity);
                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.SaveChanges();

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

            // GET: /ArticleSubCategory/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetByID(id);

                    if (model != null)
                    {
                        ArticleSubCategoryModel viewModel = new ArticleSubCategoryModel
                        {
                            Id = model.Id,
                            CategoryId = model.CategoryId,
                            SubCategoryName = model.SubCategoryName,
                            Description = model.Description,
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
                    var model = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.Delete_64Bit(model.Id);
                        _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.SaveChanges();

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
            private List<ArticleSubCategoryModel> GetSubCategoryList()
            {
                AppConstant permission = new AppConstant();

                var articleSubCategoryViewModels = _invArtSubCategoryService.INVUnit.ArticleSubCategoryRepository.GetAll().ToList().Select(
                    md => new ArticleSubCategoryModel
                    {
                        Id = md.Id,
                        //CategoryId = md.CategoryId,
                        ArticleCategory = _invArtSubCategoryService.INVUnit.ArticleCategoryRepository.GetByID(md.CategoryId).CategoryName,
                        SubCategoryName = md.SubCategoryName,
                        Description = md.Description,
                        //IsActive = md.IsActive,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("SD", "ArticleSubCategory", md.Id.ToString(), true, true, true)
                    }).OrderBy(o => o.SubCategoryName).ThenBy(ot => ot.Description);

                return articleSubCategoryViewModels.ToList();
            }
        #endregion

    }
}
