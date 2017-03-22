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
    public class CustomerController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
            public readonly BMSCommonSevice _CustomerService;
        #endregion


        #region Constructor
            public CustomerController(BMSCommonSevice CustomerService)
            {
                this._CustomerService = CustomerService;            
            }
        #endregion


        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Customer Read
            public JsonResult CustomerRead()
            {
                var mmodels = GetCustomerList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /Customer/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CustomerService.BMSUnit.CustomerRepository.GetByID(id);
                    if (model != null)
                    {
                        CustomerInfoModel viewModel = new CustomerInfoModel 
                        {
                            Id = model.Id,
                            CustomerId = model.CustomerId,
                            CustomerName = model.CustomerName,
                            Address= model.Address,
                            //Address2 = model.Address2,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax = model.Fax,
                            //Mobile = model.Mobile,
                            Email = model.Email,
                            WebAddress = model.WebAddress
                            
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

            // GET: /Customer/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = new CustomerInfoModel();
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /Customer/Create
            [HttpPost]
            public ActionResult Create(CustomerInfoModel viewModel)
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
                        _CustomerService.BMSUnit.CustomerRepository.Add(entity);
                        _CustomerService.BMSUnit.CustomerRepository.SaveChanges();

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
            // GET: /Customer/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CustomerService.BMSUnit.CustomerRepository.GetByID(id);

                    if (model != null)
                    {
                        CustomerInfoModel viewModel = new CustomerInfoModel
                        {
                            Id = model.Id,
                            CustomerId = model.CustomerId,
                            CustomerName = model.CustomerName,
                            Address = model.Address,
                            //Address2 = model.Address2,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax = model.Fax,
                           // Mobile = model.Mobile,
                            Email = model.Email,
                            WebAddress = model.WebAddress,
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

            // POST: /Customer/Edit/By ID
            [HttpPost]
            public ActionResult Edit(CustomerInfoModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _CustomerService.BMSUnit.CustomerRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _CustomerService.BMSUnit.CustomerRepository.Update(entity);
                        _CustomerService.BMSUnit.CustomerRepository.SaveChanges();

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

            // GET: /Customer/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CustomerService.BMSUnit.CustomerRepository.GetByID(id);

                    if (model != null)
                    {
                        CustomerInfoModel viewModel = new CustomerInfoModel
                        {
                            Id = model.Id,
                            CustomerId = model.CustomerId,
                            CustomerName = model.CustomerName,
                            Address = model.Address,
                            //Address2 = model.Address2,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax = model.Fax,
                            //Mobile = model.Mobile,
                            Email = model.Email,
                            WebAddress = model.WebAddress,
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
                    var model = _CustomerService.BMSUnit.CustomerRepository.GetByID(id);

                    if (model != null)
                    {
                        _CustomerService.BMSUnit.CustomerRepository.Delete_64Bit(model.Id);
                        _CustomerService.BMSUnit.CustomerRepository.SaveChanges();

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
            private List<CustomerInfoModel> GetCustomerList()
            {
                AppConstant permission = new AppConstant();

                var CustomerViewModels = _CustomerService.BMSUnit.CustomerRepository.GetAll().ToList().Select(
                    md => new CustomerInfoModel
                    {
                            Id = md.Id,
                            CustomerId = md.CustomerId, 
                            CustomerName = md.CustomerName,
                            Address = md.Address,
                            //Address2 = md.Address2,
                            Country = md.Country,
                            Phone = md.Phone,
                            //Fax = md.Fax,
                            //Mobile = md.Mobile,
                            Email = md.Email,
                            WebAddress = md.WebAddress,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Customer", md.Id.ToString(), false, true, true)
                    }).OrderBy(o => o.CustomerName).ThenBy(ot => ot.Address);

                return CustomerViewModels.ToList();
            }
        #endregion

    }
}
