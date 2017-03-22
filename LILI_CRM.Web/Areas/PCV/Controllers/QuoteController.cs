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
    public class QuoteController : Controller
    {
        // GET: /PCV/Quote/

        #region Fields
        public readonly BMSCommonSevice _QuoteService;
        #endregion
        
        #region Constructor
        public QuoteController(BMSCommonSevice QuoteService)
            {
                this._QuoteService = QuoteService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Quote Read
            public JsonResult QuoteRead()
            {
                var mmodels = GetQuoteList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: Quote/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _QuoteService.BMSUnit.QuoteRepository.GetByID(id);
                    if (model != null)
                    {
                        QuoteModel viewModel = new QuoteModel
                        {
                            Id = model.Id,
                            QuoteNo = model.QuoteNo,
                            QuoteDate = model.QuoteDate,
                            //Organization = _QuoteService.BMSUnit.SupplierRepository.GetByID(model.OrganizationId).org,
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

            // GET: Quote/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    //SupplierName = _QuoteService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == QuoteDetail.SupplierId).FirstOrDefault().SupplierName,
                    var customerList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.CustomerRepository.GetAll().Where(c=> c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    //var customerList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    //var cusContactsList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    //var deliveryStateList = Common.PopulateDeliveryStateDDList();


                    var documentModels = _QuoteService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                     md => new DocumentModel
                     {
                         Id = md.Id,
                         DocName = md.DocName,
                         IsSelected = md.IsSelected
                     }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);


                    var userOrganization = 3; // Query and Set Defaul Organization Here
                    

                    var model = new QuoteModel()
                    {
                        ddlCustomer = customerList,
                        SalesCallId = 34,
                        QuoteDate=DateTime.Now,
                        ddlOrganization = organizationList,
                        OrganizationId = Convert.ToInt64(userOrganization),
                        documentModels = documentModels.ToList()
                        //ddlContactPerson = cusContactsList,
                    };

                    
                    return PartialView("_Create", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: Invoice/Create
            [HttpPost]
            public ActionResult Create(QuoteModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {

                    if (Session["lstQuoteDetails"] != null)
                    {
                        if (ModelState.IsValid)
                        {
                            #region Current User

                            var userName = HttpContext.User.Identity.Name;
                                viewModel.IUser = userName;
                                viewModel.IDate = DateTime.Now;
                                viewModel.EDate = DateTime.Now;
                            #endregion

                            #region Releated Data
                                viewModel.QuoteNo = "Test";
                                viewModel.QuoteText= "N/A";
                            #endregion

                            List<QuoteDetailModel> list = new List<QuoteDetailModel>();
                            if (Session["lstQuoteDetails"] != null)
                            {
                                list = (List<QuoteDetailModel>)Session["lstQuoteDetails"];
                            }

                            var entity = viewModel.ToEntity();

                            _QuoteService.BMSUnit.QuoteRepository.Add(entity);
                            _QuoteService.BMSUnit.QuoteRepository.SaveChanges();

                            Session["QuoteMasterId"] = entity.Id;

                            #region Save Quote Detail

                            if (list != null && list.Count > 0)
                            {
                              foreach (QuoteDetailModel detail in list)
                                {

                                    detail.QuoteId = Convert.ToInt64(entity.Id);
                                    var detailEntity = detail.ToEntity();
                                    _QuoteService.BMSUnit.QuoteDetailsRepository.Add(detailEntity);
                                    _QuoteService.BMSUnit.QuoteDetailsRepository.SaveChanges();
                                }
                        }

                        Session["lstQuoteDetails"] = null;
              
                        #endregion


                        #region Save Sample Request Document

                        foreach (DocumentModel doc in viewModel.documentModels)
                        {
                            if (doc.IsSelected == true)
                            {
                                QuoteDocumentModel qdm = new QuoteDocumentModel();
                                qdm.QuoteId = Convert.ToInt64(entity.Id);
                                qdm.DocumentId = doc.Id;
                                var detailDocumentEntity = qdm.ToEntity();
                                _QuoteService.BMSUnit.QuoteDocumentRepository.Add(detailDocumentEntity);
                                _QuoteService.BMSUnit.QuoteDocumentRepository.SaveChanges();
                            }
                        }

                        #endregion

                        
                        return Content(Boolean.TrueString);
                        
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
            
            // GET: Invoice/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    
                    var model = _QuoteService.BMSUnit.QuoteRepository.GetByID(id);

                    Session["QuoteId"] = model.Id;

                    var customerList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    //var cusContactsList = SelectListItemExtension.PopulateDropdownList(_QuoteService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    //var deliveryStateList = Common.PopulateDeliveryStateDDList();


                    var documentModels = _QuoteService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                     md => new DocumentModel
                     {
                         Id = md.Id,
                         DocName = md.DocName,
                         IsSelected = _QuoteService.BMSUnit.QuoteDocumentRepository.GetAll().ToList().Where(x => x.QuoteId == model.Id && x.DocumentId == md.Id).Count() > 0 ? true : false

                         //md.IsSelected
                     }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);




                    if (model != null)
                    {
                        QuoteModel viewModel = new QuoteModel
                        {
                            Id = Convert.ToInt64(model.Id),                            
                            QuoteDate = model.QuoteDate,                            
                            ddlCustomer = customerList,
                            CustomerId = model.CustomerId,
                            documentModels = documentModels.ToList()
                            //Designation = _QuoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Address,
                            //ddlContactPerson = cusContactsList,
                            //CusContactId=Convert.ToInt32(model.CusContactId),
                            //,IsActive = Convert.ToBoolean(model.IsActive)
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

            // POST: Invoice/Edit/By ID
            [HttpPost]
            public ActionResult Edit(QuoteModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {

                        var model = _QuoteService.BMSUnit.QuoteRepository.GetByID(viewModel.Id);

                        #region Current User
                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion


                        List<QuoteDetailModel> list = new List<QuoteDetailModel>();
                        if (Session["lstQuoteDetails"] != null)
                        {
                            list = (List<QuoteDetailModel>)Session["lstQuoteDetails"];
                        }

                        #region Releated Data
                        viewModel.QuoteNo = "Test";
                        viewModel.QuoteText = "N/A";
                        //viewModel.testVal1 = _QuoteService.Get_New_InvoiceId().FirstOrDefault().testVal1;
                        #endregion
                        
                        var entity = viewModel.ToEntity();

                        // Get previous detail list
                        var lst = _QuoteService.BMSUnit.QuoteDetailsRepository.GetAll().Where(
                                    q => q.QuoteId == entity.Id);

                        foreach (var dt in lst)
                        {
                            _QuoteService.BMSUnit.QuoteDetailsRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }


                        

                        #region Save Quote Detail

                        if (list != null && list.Count > 0)
                        {
                            foreach (QuoteDetailModel detail in list)
                            {

                                detail.QuoteId = Convert.ToInt64(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _QuoteService.BMSUnit.QuoteDetailsRepository.Add(detailEntity);
                                _QuoteService.BMSUnit.QuoteDetailsRepository.SaveChanges();
                            }
                        }
                        #endregion

                        _QuoteService.BMSUnit.QuoteRepository.Update(entity);
                        _QuoteService.BMSUnit.QuoteRepository.SaveChanges();
                        
                        Session["lstQuoteDetails"] = null;


                        #region Save Quote Document

                        // Get and delete previous detail list 
                        var prevAttachDocList = _QuoteService.BMSUnit.QuoteDocumentRepository.GetAll().Where(
                                    q => q.QuoteId == entity.Id);

                        foreach (var dt in prevAttachDocList)
                        {
                            _QuoteService.BMSUnit.QuoteDocumentRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }

                        foreach (DocumentModel doc in viewModel.documentModels)
                        {
                            if (doc.IsSelected == true)
                            {
                                QuoteDocumentModel qdm = new QuoteDocumentModel();
                                qdm.QuoteId = Convert.ToInt64(entity.Id);
                                qdm.DocumentId = doc.Id;
                                var detailDocumentEntity = qdm.ToEntity();
                                _QuoteService.BMSUnit.QuoteDocumentRepository.Add(detailDocumentEntity);
                                _QuoteService.BMSUnit.QuoteDocumentRepository.SaveChanges();
                            }
                        }
                        #endregion


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

            // GET: Invoice/Delete/By ID
            public ActionResult Delete(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _QuoteService.BMSUnit.QuoteRepository.GetByID(id);

                    if (model != null)
                    {
                        QuoteModel viewModel = new QuoteModel
                        {
                            Id = model.Id,
                            QuoteNo = model.QuoteNo,
                            QuoteDate = model.QuoteDate
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

            // POST: /Invoice/Delete/By ID
            [HttpPost, ActionName("Delete")]

            public ActionResult DeleteConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _QuoteService.BMSUnit.QuoteRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        //_QuoteService.BMSUnit.QuoteRepository.Delete_64Bit(model.Id);
                       // _QuoteService.BMSUnit.QuoteRepository.SaveChanges();

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

            private List<QuoteModel> GetQuoteList()
            {
                AppConstant permission = new AppConstant();
                
                //var userName = HttpContext.User.Identity.Name;
                //var companyCode = _QuoteService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().CompanyCode;
                //var voucherInfoViewModels = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetAll().Where(c=>c.UserId == userId && c.Posted == "N").ToList().Select(

                var _Models = _QuoteService.BMSUnit.QuoteRepository.GetAll().ToList().Select(
                    md => new QuoteModel
                    {
                        Id = Convert.ToInt64(md.Id),
                        QuoteNo = md.QuoteNo,
                        QuoteDate = md.QuoteDate,
                        //Organization = _QuoteService.BMSUnit.OrganizationRepository.GetByID(md.OrganizationId).Organization,
                        CustomerName = _QuoteService.BMSUnit.CustomerRepository.GetByID(md.CustomerId).CustomerName,
                        //TransporterName=md.TransporterId.ToString(),                        
                        //TransporterName = md.TransporterId != null ? _QuoteService.BMSUnit.TransporterRepository.GetByID(md.TransporterId).TransporterName:"",
                        //TransporterName = _QuoteService.BMSUnit.TransporterRepository.GetAll().Where(c => c.Id == md.TransporterId).ToList().FirstOrDefault().TransporterName,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Quote", md.Id.ToString(),  false, true, false)
                    }).OrderBy(o => o.QuoteNo).ThenBy(ot => ot.QuoteDate);

                return _Models.ToList();
            }

            public JsonResult GetQuoteDetails()
            {
                long QuoteId = Convert.ToInt64(Session["QuoteId"]);

                var QuoteDetailsModel = from QuoteDetail in _QuoteService.BMSUnit.QuoteDetailsRepository.GetAll().Where(q => q.QuoteId == QuoteId)
                    select new QuoteDetailModel()
                    {
                        Id = Convert.ToInt64(QuoteDetail.Id),
                        ArticleId = QuoteDetail.ArticleId,
                        ArticleName = _QuoteService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == QuoteDetail.ArticleId).FirstOrDefault().ProductName,
                        BaseUnit = _QuoteService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == QuoteDetail.ArticleId).FirstOrDefault().SellingUnit,
                        QuoteQuantity = QuoteDetail.QuoteQuantity,
                        MOQ = Convert.ToInt64(QuoteDetail.MOQ),
                        ValidityDate=QuoteDetail.ValidityDate,
                        UnitPrice = QuoteDetail.UnitPrice,
                        SupplierId = Convert.ToInt64(QuoteDetail.SupplierId),
                        SupplierName = _QuoteService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == QuoteDetail.SupplierId).FirstOrDefault().SupplierName,
                        ApproveState=QuoteDetail.ApproveState,
                        DetailText= QuoteDetail.DetailText
                    };

                var modelList = QuoteDetailsModel.OrderBy(x => x.Id).ToList();

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }

            //For Autocomplete
            public JsonResult PopulateArticleAutoComplete()
            {

                var GetArticleList = _QuoteService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductCode = MI.ProductName
                    });

                return Json(GetArticleList, JsonRequestBehavior.AllowGet);
            }

            //public JsonResult AddArticleToGrid(string productCode)
            //{

            //    var GridRowItem = _QuoteService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == productCode).ToList().Select(
            //        MI => new ProductModel
            //        {
            //            Id = MI.Id,
            //            ProductCode = MI.ProductCode,
            //            ProductName = MI.ProductName,
            //            SellingUnit = MI.SellingUnit
            //        });
            //    return Json(GridRowItem, JsonRequestBehavior.AllowGet);

            //}

            public JsonResult AddArticleToGrid(string articleCode)
            {

                var GridRowItem = _QuoteService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == articleCode).ToList().Select(
                    MI => new ProductModel
                    {
                        Id = MI.Id,
                        ProductCode = MI.ProductCode,
                        ProductName = MI.ProductName,
                        SamplingUnit = MI.SamplingUnit,
                        Origin = MI.Origin,
                        MOQ = MI.MOQ
                    });



                //var modelListDistinct = GetSalesModelJan.GroupBy(test => test.ProductCode)
                //                       .Select(grp => grp.First());

                return Json(GridRowItem, JsonRequestBehavior.AllowGet);

            }

             public JsonResult GetSupplierDetails(long id)
             {
                 var jresult = _QuoteService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == id).ToList().Select(
                    MI => new SupplierInfoModel
                    {
                        Address = MI.Address
                    });

                 return Json(jresult, JsonRequestBehavior.AllowGet);
             }

             public JsonResult ReadSupplierList()
             {
                 var mmodels = GetSupplierList();
                 return Json(mmodels, JsonRequestBehavior.AllowGet);
             }

             private List<SupplierViewModel> GetSupplierList()
             {
                 AppConstant permission = new AppConstant();

                 var supplierModels = _QuoteService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                     md => new SupplierViewModel
                     {
                         SupplierId = md.Id,
                         SupplierName = md.SupplierName
                     }).OrderBy(o => o.SupplierName).ThenBy(ot => ot.SupplierId);

                 return supplierModels.ToList();
             }


             public JsonResult ReadApproveStatusList()
             {
                 var mmodels = GetApproveStatusList();
                 return Json(mmodels, JsonRequestBehavior.AllowGet);
             }

             private List<ApproveStateViewModel> GetApproveStatusList()
             {
                 AppConstant permission = new AppConstant();
                
                 List<ApproveStateViewModel> approveStateModelList = new List<ApproveStateViewModel> ();

                 ApproveStateViewModel approveStateModel = new ApproveStateViewModel();
                 approveStateModel.ApproveStateId = "Approved";
                 approveStateModel.ApproveStateName = "Approved";
                 approveStateModelList.Add(approveStateModel);

                 approveStateModel = new ApproveStateViewModel();
                 approveStateModel.ApproveStateId = "Rejected";
                 approveStateModel.ApproveStateName = "Rejected";
                 approveStateModelList.Add(approveStateModel);


                 return approveStateModelList;
             }

             public JsonResult ReadSupplierList1()
             {
                 //var mmodels = GetCustomerList();
                 var supplierModels = _QuoteService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                     md => new SupplierInfoModel
                     {
                         //Id = md.Id,
                         Id = md.Id,
                         SupplierName = md.SupplierName
                     }).OrderBy(o => o.SupplierName);


                 return Json(supplierModels, JsonRequestBehavior.AllowGet);
             }
        
             public JsonResult SetQuoteDetailsListForSave(List<QuoteDetailViewModel> lstQuoteDetails)
             {
                 var strMessage = string.Empty;

                 // Clear detail list
                 Session["lstQuoteDetails"] = null;

                 try
                 {

                     List<QuoteDetailModel> list = new List<QuoteDetailModel>();
                     var _QuoteModel = new QuoteModel();

                     // Add new detail information
                     foreach (var item in lstQuoteDetails)
                     {
                         QuoteDetailModel entityModel = new QuoteDetailModel();

                         entityModel.QuoteId = item.QuoteId;
                         entityModel.ArticleId = item.ArticleId;
                         entityModel.QuoteQuantity = Convert.ToDecimal(item.QuoteQuantity);
                         entityModel.MOQ = Convert.ToInt64(item.MOQ);
                         entityModel.ValidityDate = item.ValidityDate;
                         entityModel.UnitPrice = Convert.ToDecimal(item.UnitPrice); 
                         entityModel.SupplierId = item.SupplierId;
                         entityModel.Origin = item.Origin;
                         entityModel.ApproveState = item.ApproveState;
                         entityModel.DetailText = item.DetailText;
                         list.Add(entityModel);

                     }

                     Session["lstQuoteDetails"] = list;
                     return Json(_QuoteModel, JsonRequestBehavior.AllowGet);
                     //strMessage = Boolean.TrueString;
                 }
                 catch (Exception ex)
                 {
                     strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                 }

                 return Json(new { strMessage = strMessage });
             }

             public JsonResult ReadPriceTermList()
             {
                 var mmodels = GetPriceTermList();
                 return Json(mmodels, JsonRequestBehavior.AllowGet);
             }

             private List<PriceTermModel> GetPriceTermList()
             {
                 AppConstant permission = new AppConstant();

                 var termModels = _QuoteService.BMSUnit.PriceTermRepository.GetAll().ToList().Select(
                     md => new PriceTermModel
                     {
                         PriceTermId = md.Id,
                         PriceTermName = md.PriceTermName
                     }).OrderBy(o => o.PriceTermId).ThenBy(ot => ot.PriceTermId);

                 return termModels.ToList();
             }

             public JsonResult GetCountryList()
             {
                 var mmodels = CountryList();
                 return Json(mmodels, JsonRequestBehavior.AllowGet);
             }


             private List<CountryViewModel> CountryList()
             {
                 AppConstant permission = new AppConstant();

                 var countryListModels = _QuoteService.BMSUnit.CountryRepository.GetAll().ToList().Select(
                     md => new CountryViewModel
                     {
                         ShipmentCountryId = md.Id,
                         CountryName = md.CountryName
                     }).OrderBy(o => o.ShipmentCountryId).ThenBy(ot => ot.ShipmentCountryId);

                 return countryListModels.ToList();
             }

             public JsonResult SaveQouteData(List<QuoteModel> lstSampleSubmissionMaster, List<QuoteDetailModel> lstSampleSubmissionDetails)
             {
                 var strMessage = string.Empty;

                 try
                 {
                     foreach (var item in lstSampleSubmissionMaster)
                     {
                         QuoteModel masterEntityModel = new QuoteModel();

                         #region Current User
                         var userName = HttpContext.User.Identity.Name;
                         masterEntityModel.IUser = userName;
                         masterEntityModel.IDate = DateTime.Now;
                         masterEntityModel.EDate = DateTime.Now;
                         #endregion
                         masterEntityModel.QuoteNo = "Test";
                         masterEntityModel.SalesCallId = Convert.ToInt64(item.SalesCallId);
                         masterEntityModel.QuoteDate = item.QuoteDate;
                         masterEntityModel.CustomerId = Convert.ToInt64(item.CustomerId);
                         //masterList.Add(masterEntityModel);

                         var entity = masterEntityModel.ToEntity();
                         _QuoteService.BMSUnit.QuoteRepository.Add(entity);
                         _QuoteService.BMSUnit.QuoteRepository.SaveChanges();

                         Session["QuoteMasterId"] = entity.Id;
                     }


                     // Add new detail information

                     foreach (var item in lstSampleSubmissionDetails)
                     {
                         QuoteDetailModel entityDetailModel = new QuoteDetailModel();

                         entityDetailModel.QuoteId = Convert.ToInt64(Convert.ToInt64(Session["QuoteMasterId"]));
                         entityDetailModel.ArticleId = item.ArticleId;
                         entityDetailModel.MOQ = item.MOQ;
                         entityDetailModel.ValidityDate = item.ValidityDate;
                         entityDetailModel.SupplierId = item.SupplierId;
                         entityDetailModel.PriceTermId = item.PriceTermId;
                         entityDetailModel.ShipmentCountryId = item.ShipmentCountryId;
                         entityDetailModel.ShipmentLeadTime = item.ShipmentLeadTime;
                         entityDetailModel.DetailText = item.DetailText;

                         var detailEntity = entityDetailModel.ToEntity();
                         _QuoteService.BMSUnit.QuoteDetailsRepository.Add(detailEntity);
                         _QuoteService.BMSUnit.QuoteDetailsRepository.SaveChanges();

                     }
                     strMessage = "Information has been saved successfully";


                 }
                 catch (Exception ex)
                 {
                     strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                 }

                 return Json(new { strMessage = strMessage });
             }
        #endregion
    }
}
   