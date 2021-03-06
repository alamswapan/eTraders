﻿using System;
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
    public class SupplierController : Controller
    {
        //
        // GET: /SD/Supplier/

        #region Fields
        public readonly BMSCommonSevice _SupplierInfoService;
        #endregion

        #region Constructor
        public SupplierController(BMSCommonSevice SupplierInfoService)
            {
                this._SupplierInfoService = SupplierInfoService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Supplier Read
            public JsonResult SupplierRead()
            {
                var mmodels = GetSupplierList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /Supplier/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SupplierInfoService.BMSUnit.SupplierRepository.GetByID(id);
                    if (model != null)
                    {
                        SupplierInfoModel viewModel = new SupplierInfoModel 
                        {
                            Id = model.Id,
                            SupplierId = model.SupplierId, 
                            SupplierName = model.SupplierName,
                            Address = model.Address,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax= model.Fax,
                            //Mobile = model.Mobile,
                            Email= model.Email,
                            WebAddress = model.WebAddress,
                            Industry=model.Industry,
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

            // GET: /Supplier/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {

                    //var SupplierTypeList = SelectListItemExtension.PopulateDropdownList(_SupplierInfoService.BMSUnit.SupplierTypeRepository.GetAll().ToList<INV_tblSupplierType>(), "Id", "Name").ToList();
                    var model = new SupplierInfoModel();
                    //{
                    //    ddlSupplierType = SupplierTypeList
                    //};
                    
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /Supplier/Create
            [HttpPost]
            public ActionResult Create(SupplierInfoModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (Session["lstContactDetails"] != null)
                    {
                        if (ModelState.IsValid)
                        {
                            #region Current User

                            var userName = HttpContext.User.Identity.Name;
                            viewModel.IUser = userName;
                            viewModel.IDate = DateTime.Now;
                            viewModel.EDate = DateTime.Now;
                            #endregion

                            List<ContactViewModel> list = new List<ContactViewModel>();
                            if (Session["lstContactDetails"] != null)
                            {
                                list = (List<ContactViewModel>)Session["lstContactDetails"];
                            }

                            var entity = viewModel.ToEntity();
                            _SupplierInfoService.BMSUnit.SupplierRepository.Add(entity);
                            _SupplierInfoService.BMSUnit.SupplierRepository.SaveChanges();

                            Session["SupplierId"] = entity.Id;

                            #region Save Contact Detail

                            if (list != null && list.Count > 0)
                            {
                                foreach (ContactViewModel detail in list)
                                {

                                    detail.SupplierId = Convert.ToInt64(entity.Id);
                                    var detailEntity = detail.ToEntity();
                                    _SupplierInfoService.BMSUnit.SupplierContactRepository.Add(detailEntity);
                                    _SupplierInfoService.BMSUnit.SupplierContactRepository.SaveChanges();
                                }
                            }

                            Session["lstContactDetails"] = null;

                            #endregion

                            return Content(Boolean.TrueString);
                            //return Content("Information has been saved successfully");
                        }

                    }                    

                    strMessage = Common.GetModelStateErrorMessage(ModelState);
                }
                catch (Exception ex)
                {
                    strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                }

                return Content(strMessage);
            }
            // GET: /Supplier/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SupplierInfoService.BMSUnit.SupplierRepository.GetByID(id);

                    Session["ContactId"] = model.Id;

                    if (model != null)
                    {
                        SupplierInfoModel viewModel = new SupplierInfoModel
                        {
                            Id = model.Id,
                            SupplierId = model.SupplierId,
                            SupplierName = model.SupplierName,
                            Address = model.Address,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax = model.Fax,
                            //Mobile=model.Mobile,
                            Email = model.Email,
                            WebAddress = model.WebAddress,
                            Industry=model.Industry,
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

            // POST: /Supplier/Edit/By ID
            [HttpPost]
            public ActionResult Edit(SupplierInfoModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _SupplierInfoService.BMSUnit.SupplierRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        List<ContactViewModel> list = new List<ContactViewModel>();
                        if (Session["lstContactDetails"] != null)
                        {
                            list = (List<ContactViewModel>)Session["lstContactDetails"];
                        }

                        var entity = viewModel.ToEntity();
                        // Get previous detail list
                        var lst = _SupplierInfoService.BMSUnit.SupplierContactRepository.GetAll().Where(
                                    q => q.SupplierId == entity.Id);

                        foreach (var dt in lst)
                        {
                            _SupplierInfoService.BMSUnit.SupplierContactRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }                        

                        _SupplierInfoService.BMSUnit.SupplierRepository.Update(entity);
                        _SupplierInfoService.BMSUnit.SupplierRepository.SaveChanges();

                        #region Save SampleRequest Detail

                        if (list != null && list.Count > 0)
                        {
                            foreach (ContactViewModel detail in list)
                            {
                                detail.SupplierId = Convert.ToInt64(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _SupplierInfoService.BMSUnit.SupplierContactRepository.Add(detailEntity);
                                _SupplierInfoService.BMSUnit.SupplierContactRepository.SaveChanges();
                            }
                        }
                        #endregion

                        Session["lstContactDetails"] = null;

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

            // GET: /Supplier/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SupplierInfoService.BMSUnit.SupplierRepository.GetByID(id);

                    if (model != null)
                    {
                        SupplierInfoModel viewModel = new SupplierInfoModel
                        {
                            Id = model.Id,
                            SupplierId = model.SupplierId,
                            SupplierName = model.SupplierName,
                            Address = model.Address,
                            Country = model.Country,
                            Phone = model.Phone,
                            //Fax = model.Fax,
                           // Mobile = model.Mobile,
                            Email = model.Email,
                            WebAddress = model.WebAddress,
                            Industry = model.Industry,
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
                    var model = _SupplierInfoService.BMSUnit.SupplierRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        //_invSupplierService.BMSUnit.SupplierRepository.Delete(model.SupplierId);
                        _SupplierInfoService.BMSUnit.SupplierRepository.SaveChanges();

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
            private List<SupplierInfoModel> GetSupplierList()
            {
                AppConstant permission = new AppConstant();

                var SupplierViewModels = _SupplierInfoService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                    md => new SupplierInfoModel
                    {
                        Id = md.Id,
                        SupplierId = md.SupplierId,
                        SupplierName= md.SupplierName,
                        Address = md.Address,
                        Country = md.Country,
                        Phone=md.Phone,
                        //Fax=md.Fax,
                        Email=md.Email,
                        WebAddress=md.WebAddress,
                        Industry=md.Industry,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Supplier", md.Id.ToString(), false, true, true)
                    }).OrderBy(o => o.SupplierName).ThenBy(ot => ot.Address);

                return SupplierViewModels.ToList();
            }
        #endregion

            //For Autocomplete
            public JsonResult PopulateCountryAutoComplete()
            {

                var GetCountryList = _SupplierInfoService.BMSUnit.CountryRepository.GetAll().ToList().Select(
                    MI => new CountryModel
                    {
                        CountryId = MI.Id,
                        CountryName = MI.CountryName
                    });

                return Json(GetCountryList, JsonRequestBehavior.AllowGet);
            }

        
            //Contact List save
            public JsonResult ContactDetailsListForSave(List<ContactViewModel> lstRequestContactDetails)
            {
                var strMessage = string.Empty;

                // Clear detail list
                Session["lstContactDetails"] = null;

                try
                {

                    List<ContactViewModel> list = new List<ContactViewModel>();
                    var _SampleContactModel = new SupplierInfoModel();

                    // Add new detail information
                    foreach (var item in lstRequestContactDetails)
                    {
                        ContactViewModel entityModel = new ContactViewModel();

                        entityModel.Id = item.Id;
                        entityModel.ContactName = item.ContactName;
                        entityModel.Designation = item.Designation;
                        entityModel.Phone = item.Phone;
                        list.Add(entityModel);
                    }

                    Session["lstContactDetails"] = list;
                    return Json(_SampleContactModel, JsonRequestBehavior.AllowGet);
                    //strMessage = Boolean.TrueString;
                }
                catch (Exception ex)
                {
                    strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                }

                return Json(new { strMessage = strMessage });
            }


            public JsonResult GetContactDetails()
            {
                long requestId = Convert.ToInt64(Session["ContactId"]);

                var contactDetailsModel = from sampleRequestDetail in _SupplierInfoService.BMSUnit.SupplierContactRepository.GetAll().Where(q => q.SupplierId == requestId)
                                                select new ContactViewModel()
                                                {
                                                    Id = Convert.ToInt64(sampleRequestDetail.Id),
                                                    ContactName = sampleRequestDetail.ContactName,                                                    
                                                    Designation = sampleRequestDetail.Designation,
                                                    Phone = sampleRequestDetail.Phone
                                                };

                var modelList = contactDetailsModel.OrderBy(x => x.Id).ToList();

                //List<ContactViewModel> list = new List<ContactViewModel>();

                //// Add new detail information
                //foreach (var item in modelList)
                //{
                //    ContactViewModel entityModel = new ContactViewModel();

                //    entityModel.Id = item.Id;
                //    entityModel.ContactName = item.ContactName;
                //    entityModel.Designation = item.Designation;
                //    entityModel.Phone = item.Phone;
                //    list.Add(entityModel);
                //}

                Session["lstContactDetails"] = modelList;

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }
    }
}
