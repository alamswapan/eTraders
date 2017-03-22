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
    public class ProductController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
        public readonly BMSCommonSevice _ProductService;
        #endregion


        #region Constructor
        public ProductController(BMSCommonSevice ProductService)
            {
                this._ProductService = ProductService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Product Read
            public JsonResult ProductRead()
            {
                var mmodels = GetProductList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: Product/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _ProductService.BMSUnit.ProductRepository.GetByID(id);
                    if (model != null)
                    {
                        ProductModel viewModel = new ProductModel
                        {
                            Id = model.Id,
                            ProductCode = model.ProductCode,
                            ProductName = model.ProductName,
                            GenericName = model.GenericName,
                            ShortDesc = model.ShortDesc,
                            HSCode = model.HSCode,
                            SellingUnit = model.SellingUnit,
                            SamplingUnit = model.SamplingUnit,
                            UnitPrice = model.UnitPrice,
                            DutyStructure = model.DutyStructure,
                            Origin = model.Origin,
                            MOQ = model.MOQ,
                            LeadTime = model.LeadTime,
                            IsActive = Convert.ToBoolean(model.IsActive)
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

            // GET: ProductSubCategory/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {

                    var model = new ProductModel();
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: ProductSubCategory/Create
            [HttpPost]
            public ActionResult Create(ProductModel viewModel)
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
                        _ProductService.BMSUnit.ProductRepository.Add(entity);
                        _ProductService.BMSUnit.ProductRepository.SaveChanges();

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
            // GET: ProductSubCategory/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _ProductService.BMSUnit.ProductRepository.GetByID(id);
                    if (model != null)
                    {
                        ProductModel viewModel = new ProductModel
                        {
                            Id = model.Id,
                            ProductCode = model.ProductCode,
                            ProductName = model.ProductName,
                            GenericName = model.GenericName,
                            ShortDesc = model.ShortDesc,
                            HSCode = model.HSCode,
                            SellingUnit = model.SellingUnit,
                            SamplingUnit = model.SamplingUnit,
                            UnitPrice = model.UnitPrice,
                            DutyStructure = model.DutyStructure,
                            Origin = model.Origin,
                            MOQ = model.MOQ,
                            LeadTime = model.LeadTime,
                            IsActive = Convert.ToBoolean(model.IsActive)
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

            // POST: ProductSubCategory/Edit/By ID
            [HttpPost]
            public ActionResult Edit(ProductModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _ProductService.BMSUnit.ProductRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _ProductService.BMSUnit.ProductRepository.Update(entity);
                        _ProductService.BMSUnit.ProductRepository.SaveChanges();

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
                    var model = _ProductService.BMSUnit.ProductRepository.GetByID(id);

                    if (model != null)
                    {
                        ProductModel viewModel = new ProductModel
                        {
                            Id = model.Id,
                            ProductCode = model.ProductCode,
                            ProductName = model.ProductName,
                            GenericName = model.GenericName,
                            ShortDesc = model.ShortDesc,
                            HSCode = model.HSCode,
                            SellingUnit = model.SellingUnit,
                            SamplingUnit = model.SamplingUnit,
                            UnitPrice = model.UnitPrice,
                            DutyStructure = model.DutyStructure,
                            Origin = model.Origin,
                            MOQ = model.MOQ,
                            LeadTime = model.LeadTime,
                            IsActive = Convert.ToBoolean(model.IsActive)
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
                    var model = _ProductService.BMSUnit.ProductRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _ProductService.BMSUnit.ProductRepository.Delete_64Bit(model.Id);
                        _ProductService.BMSUnit.ProductRepository.SaveChanges();

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
            private List<ProductModel> GetProductList()
            {
                AppConstant permission = new AppConstant();

                var ProductViewModels = _ProductService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    md => new ProductModel
                    {
                        Id = md.Id,
                        ProductCode = md.ProductCode,
                        ProductName = md.ProductName,
                        GenericName = md.GenericName,
                        //ShortDesc = md.ShortDesc,
                        HSCode = md.HSCode,
                        SellingUnit = md.SellingUnit,
                        SamplingUnit = md.SamplingUnit,
                        UnitPrice = md.UnitPrice,
                        //DutyStructure = md.DutyStructure,
                        //Origin = md.Origin,
                        //MOQ = md.MOQ,
                        //LeadTime = md.LeadTime,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Product", md.Id.ToString(), false, true, true)
                    }).OrderBy(o => o.ProductName).ThenBy(ot => ot.GenericName);

                return ProductViewModels.ToList();
            }
        #endregion

    }
}
