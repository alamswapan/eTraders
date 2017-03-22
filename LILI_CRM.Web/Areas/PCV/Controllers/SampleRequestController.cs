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
    public class SampleRequestController : Controller
    {
        // GET: /PCV/SampleRequest/

        #region Fields
        public readonly BMSCommonSevice _SampleRequestService;
        #endregion
        
        #region Constructor
        public SampleRequestController(BMSCommonSevice SampleRequestService)
            {
                this._SampleRequestService = SampleRequestService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //SampleRequest Read
            public JsonResult SampleRequestRead()
            {
                var mmodels = GetSampleRequestList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: SampleRequest/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SampleRequestService.BMSUnit.SampleRequestRepository.GetByID(id);
                    if (model != null)
                    {
                        SampleRequestModel viewModel = new SampleRequestModel
                        {
                            Id = model.Id,
                            RequestNo = model.RequestNo,
                            RequestDate = model.RequestDate,
                            Organization = _SampleRequestService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).SupplierName,
                            SupplierName = _SampleRequestService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).SupplierName,
                            //NET = model.NET,
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

            // GET: SampleRequest/SearchArticle
            /*
                        public ActionResult SearchArticle()
                        {
                            var errorViewModel = new ErrorViewModel();

                            try
                            {

                                var articleList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.ArticleRepository.GetAll().Where(x=> x.IsActive) .ToList<tblArticle>(), "Id", "ArticleName").ToList();

                                var model = new InvoiceDetailModel()
                                {
                                    ddlArticle = articleList
                                };

                                return PartialView("_SearchArticle", model);
                            }
                            catch (Exception ex)
                            {
                                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                                return PartialView("_ErrorPopUp", errorViewModel);
                            }
                        }

                        // POST: Invoice/SearchArticle
                        [HttpPost]
                        public ActionResult SearchArticle(InvoiceDetailModel viewModel)
                        {
                            var strMessage = string.Empty;
                            try
                            {
                                if (ModelState.IsValid)
                                {

                                    //var entity = viewModel.ToEntity();
                                    //_invSampleRequestService.BMSUnit.SampleRequestRepository.Add(entity);
                                    //_invSampleRequestService.BMSUnit.SampleRequestRepository.SaveChanges();

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
            */
            
            // GET: SampleRequest/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var supplierList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName", isEdit: true).ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    var transporterList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    var deliveryStateList = Common.PopulateDeliveryStateDDList();


                    var sampleDocumentModels = _SampleRequestService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                     md => new SampleDocumentModel
                     {
                         Id = md.Id,
                         DocName = md.DocName,
                         IsSelected = md.IsSelected
                     }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);

               
                    //List<SampleDocumentModel> fr = new List<SampleDocumentModel>();
                    //fr.Add(new SampleDocumentModel() { Id = 1, DocName = "asdad", IsSelected = true });
                    //fr.Add(new SampleDocumentModel() { Id = 2, DocName = "sfsfsfs", IsSelected = false });




                    //DocumentList docList = new DocumentList();
                    //docList.sampleDocumentModels = fr;

                    var userOrganization = 3; // Query and Set Defaul Organization Here
                    var defaultDeliveryState = Common.DeliveryStateEnum.Undelivered;

                    var model = new SampleRequestModel()
                    {
                        ddlSupplier = supplierList,
                        RequestDate=DateTime.Now,
                        ddlOrganization = organizationList,
                        OrganizationId = Convert.ToInt64(userOrganization),
                        ddlTransporter = transporterList,
                        ddlDeliveryState = deliveryStateList,
                        DeliveryStateId = Convert.ToInt32(defaultDeliveryState),
                        sampleDocumentModels = sampleDocumentModels.ToList()
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
            public ActionResult Create(SampleRequestModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {

                    if (Session["lstSampleRequestDetails"] != null)
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
                                viewModel.RequestNo = "Test";
                                viewModel.Remarks= "N/A";

                                if (viewModel.TransporterId == 0)
                                { viewModel.TransporterId = null; }
                            #endregion

                            List<SampleRequestDetailModel> list = new List<SampleRequestDetailModel>();
                            if (Session["lstSampleRequestDetails"] != null)
                            {
                                list = (List<SampleRequestDetailModel>)Session["lstSampleRequestDetails"];
                            }

                            var entity = viewModel.ToEntity();

                            _SampleRequestService.BMSUnit.SampleRequestRepository.Add(entity);
                            _SampleRequestService.BMSUnit.SampleRequestRepository.SaveChanges();

                            Session["SampleRequestMasterId"] = entity.Id;

                            #region Save SampleRequest Detail

                            if (list != null && list.Count > 0)
                            {
                                foreach (SampleRequestDetailModel detail in list)
                                {

                                    detail.RequestId = Convert.ToInt64(entity.Id);
                                    var detailEntity = detail.ToEntity();
                                    _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.Add(detailEntity);
                                    _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.SaveChanges();
                                }
                            }

                            Session["lstSampleRequestDetails"] = null;
              
                            #endregion

                            #region Save Sample Request Document


                            foreach (SampleDocumentModel doc in viewModel.sampleDocumentModels)
                            {
                                if (doc.IsSelected == true)
                                {
                                    SampleRequestDocumentModel aaa = new SampleRequestDocumentModel();
                                    aaa.RequestId = Convert.ToInt64(entity.Id);
                                    aaa.DocumentId = doc.Id;
                                    var detailDocumentEntity = aaa.ToEntity();
                                    _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.Add(detailDocumentEntity);
                                    _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.SaveChanges();
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
                    
                    var model = _SampleRequestService.BMSUnit.SampleRequestRepository.GetByID(id);

                    Session["SampleRequestId"] = model.Id;

                    var supplierList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    var transporterList = SelectListItemExtension.PopulateDropdownList(_SampleRequestService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    var deliveryStateList = Common.PopulateDeliveryStateDDList();

                    var defaultDeliveryState = Common.DeliveryStateEnum.Undelivered;

                    var sampleDocumentModels = _SampleRequestService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                     md => new SampleDocumentModel
                     {
                         Id = md.Id,
                         DocName = md.DocName,
                         IsSelected = _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.GetAll().ToList().Where(x=> x.RequestId == model.Id && x.DocumentId == md.Id).Count()>0 ? true:false

                         //md.IsSelected
                     }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);


                    if (model != null)
                    {
                        SampleRequestModel viewModel = new SampleRequestModel
                        {
                            Id = Convert.ToInt64(model.Id),
                            RequestNo = model.RequestNo,
                            RequestDate = model.RequestDate,
                            ddlOrganization = organizationList,
                            //Organization = _SampleRequestService.BMSUnit.OrganizationRepository.GetByID(model.OrganizationId).Organization,
                            OrganizationId = Convert.ToInt64(model.OrganizationId),
                            ddlSupplier = supplierList,
                            SupplierId = model.SupplierId,
                            SupplierAddress = _SampleRequestService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Address,
                            ddlTransporter = transporterList,
                            TransporterId=Convert.ToInt32(model.TransporterId),
                            DocTrackingNo=model.DocTrackingNo,
                            ddlDeliveryState = deliveryStateList,
                            //DeliveryStateId = (int)model.DeliveryStateId,
                            DeliveryStateId = model.DeliveryStateId != null ? (int)model.DeliveryStateId : Convert.ToInt32(defaultDeliveryState),
                            sampleDocumentModels = sampleDocumentModels.ToList()
                            //(int)Common.DeliveryStateEnum.Undelivered
                            //DeliveryState = Enum.GetName(typeof(Common.DeliveryStateEnum), (int)model.DeliveryStateId)
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
            public ActionResult Edit(SampleRequestModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {

                        var model = _SampleRequestService.BMSUnit.SampleRequestRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion
                        
                        if (viewModel.TransporterId == 0)
                        { viewModel.TransporterId = null; }

                        List<SampleRequestDetailModel> list = new List<SampleRequestDetailModel>();
                        if (Session["lstSampleRequestDetails"] != null)
                        {
                            list = (List<SampleRequestDetailModel>)Session["lstSampleRequestDetails"];
                        }

                        #region Releated Data
                        viewModel.RequestNo = "Test";
                        viewModel.Remarks = "N/A";
                        //viewModel.testVal1 = _SampleRequestService.Get_New_InvoiceId().FirstOrDefault().testVal1;
                        #endregion
                        
                        var entity = viewModel.ToEntity();

                        // Get previous detail list
                        var lst = _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.GetAll().Where(
                                    q => q.RequestId == entity.Id);

                        foreach (var dt in lst)
                        {
                            _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }

                        #region Save SampleRequest Detail

                        if (list != null && list.Count > 0)
                        {
                            foreach (SampleRequestDetailModel detail in list)
                            {

                                detail.RequestId = Convert.ToInt64(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.Add(detailEntity);
                                _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.SaveChanges();
                            }
                        }
                        #endregion

                        _SampleRequestService.BMSUnit.SampleRequestRepository.Update(entity);
                        _SampleRequestService.BMSUnit.SampleRequestRepository.SaveChanges();
                        
                        Session["lstSampleRequestDetails"] = null;



                        #region Save Sample Request Document

                        var sampleDocEntity = viewModel.ToEntity();
                        var docList = _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.GetAll().Where(
                                    q => q.RequestId == sampleDocEntity.Id);

                        foreach (var dt in docList)
                        {
                            _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }                        
                        
                        


                        foreach (SampleDocumentModel doc in viewModel.sampleDocumentModels)
                        {
                            if (doc.IsSelected == true)
                            {
                                SampleRequestDocumentModel aaa = new SampleRequestDocumentModel();
                                aaa.RequestId = Convert.ToInt64(entity.Id);
                                aaa.DocumentId = doc.Id;
                                var detailDocumentEntity = aaa.ToEntity();
                                _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.Add(detailDocumentEntity);
                                _SampleRequestService.BMSUnit.SampleRequestDocumentRepository.SaveChanges();
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
                    var model = _SampleRequestService.BMSUnit.SampleRequestRepository.GetByID(id);

                    if (model != null)
                    {
                        SampleRequestModel viewModel = new SampleRequestModel
                        {
                            Id = model.Id,
                            RequestNo = model.RequestNo,
                            RequestDate = model.RequestDate
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
                    var model = _SampleRequestService.BMSUnit.SampleRequestRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        //_SampleRequestService.BMSUnit.SampleRequestRepository.Delete_64Bit(model.Id);
                       // _SampleRequestService.BMSUnit.SampleRequestRepository.SaveChanges();

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

            private List<SampleRequestModel> GetSampleRequestList()
            {
                AppConstant permission = new AppConstant();
                
                //var userName = HttpContext.User.Identity.Name;
                //var companyCode = _SampleRequestService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().CompanyCode;
                //var voucherInfoViewModels = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetAll().Where(c=>c.UserId == userId && c.Posted == "N").ToList().Select(

                //  var _Models = _SampleRequestService.BMSUnit.SampleRequestRepository.GetAll().Where(x => x.DeliveryStateId == (int)Common.DeliveryStateEnum.Undelivered || x.DeliveryStateId == null).ToList().Select(
                var _Models = _SampleRequestService.BMSUnit.SampleRequestRepository.GetAll().ToList().Select(
                    md => new SampleRequestModel
                    {
                        Id = Convert.ToInt64(md.Id),
                        RequestNo = md.RequestNo,
                        RequestDate = md.RequestDate,
                        Organization = _SampleRequestService.BMSUnit.OrganizationRepository.GetByID(md.OrganizationId).Organization,
                        SupplierName = _SampleRequestService.BMSUnit.SupplierRepository.GetByID(md.SupplierId).SupplierName,
                        TransporterName = md.TransporterId != null ? _SampleRequestService.BMSUnit.TransporterRepository.GetByID(md.TransporterId).TransporterName:"",
                        DocTrackingNo = md.DocTrackingNo,
                        //DeliveryStateId=(int)md.DeliveryStateId,
                        //DeliveryState = Enum.GetName(typeof(Common.DeliveryStateEnum), (int)md.DeliveryStateId),
                        DeliveryState = md.DeliveryStateId != null ? Enum.GetName(typeof(Common.DeliveryStateEnum), (int)md.DeliveryStateId) : "",
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "SampleRequest", md.Id.ToString(),  false, true, false)
                    }).OrderBy(o => o.RequestNo).ThenBy(ot => ot.RequestDate);

                return _Models.ToList();
            }

            public JsonResult GetSampleRequestDetails()
            {
                long requestId = Convert.ToInt64(Session["SampleRequestId"]);

                var sampleRequestDetailsModel = from sampleRequestDetail in _SampleRequestService.BMSUnit.SampleRequestDetailsRepository.GetAll().Where(q => q.RequestId == requestId)
                    select new SampleRequestDetailModel()
                    {
                        Id = Convert.ToInt64(sampleRequestDetail.Id),
                        ProductId = sampleRequestDetail.ProductId,
                        ProductName = _SampleRequestService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == sampleRequestDetail.ProductId).FirstOrDefault().ProductName,
                        SamplingUnit = _SampleRequestService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == sampleRequestDetail.ProductId).FirstOrDefault().SamplingUnit,
                        RequestQuantity = sampleRequestDetail.RequestQuantity,
                        CustomerId = Convert.ToInt64(sampleRequestDetail.CustomerId),
                        CustomerName = _SampleRequestService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == sampleRequestDetail.CustomerId).FirstOrDefault().CustomerName,
                        Purpose = sampleRequestDetail.Purpose,
                        ReceivedQuantity = sampleRequestDetail.ReceivedQuantity,
                        ReceivedDate = sampleRequestDetail.ReceivedDate,//(DateTime)
                        DetailText= sampleRequestDetail.DetailText
                    };

                var modelList = sampleRequestDetailsModel.OrderBy(x => x.Id).ToList();

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }

            //For Autocomplete
            public JsonResult PopulateProductAutoComplete()
            {

                var productList = _SampleRequestService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductCode = MI.ProductName
                    });

                return Json(productList, JsonRequestBehavior.AllowGet);
            }

            public JsonResult AddProductToGrid(string product)
            {

                var GridRowItem = _SampleRequestService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == product).ToList().Select(
                    MI => new ProductModel
                    {
                        Id = MI.Id,
                        ProductCode = MI.ProductCode,
                        ProductName = MI.ProductName,
                        SamplingUnit = MI.SamplingUnit
                    });
                return Json(GridRowItem, JsonRequestBehavior.AllowGet);

            }

             public JsonResult GetSupplierDetails(long id)
             {
                 var jresult = _SampleRequestService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == id).ToList().Select(
                    MI => new SupplierInfoModel
                    {
                        Address = MI.Address
                    });

                 return Json(jresult, JsonRequestBehavior.AllowGet);
             }

             public JsonResult ReadCustomerList()
             {
                 var mmodels = GetCustList();
                 return Json(mmodels, JsonRequestBehavior.AllowGet);
             }

             private List<CustViewModel> GetCustList()
             {
                 AppConstant permission = new AppConstant();

                 var customerModels = _SampleRequestService.BMSUnit.CustomerRepository.GetAll().ToList().Select(
                     md => new CustViewModel
                     {
                         CustomerId = md.Id,
                         CustomerName = md.CustomerName
                     }).OrderBy(o => o.CustomerName).ThenBy(ot => ot.CustomerId);

                 return customerModels.ToList();
             }




             private List<CustomerInfoModel> GetCustomerList()
             {
                 AppConstant permission = new AppConstant();

                 var customerModels = _SampleRequestService.BMSUnit.CustomerRepository.GetAll().ToList().Select(
                     md => new CustomerInfoModel
                     {
                         Id = md.Id,
                         CustomerName = md.CustomerName
                     }).OrderBy(o => o.CustomerName);

                 return customerModels.ToList();
             }
         

             public JsonResult SetSampleRequestDetailsListForSave(List<SampleRequestDetailViewModel> lstSampleRequestDetails)
             {
                 var strMessage = string.Empty;

                 // Clear detail list
                 Session["lstSampleRequestDetails"] = null;

                 try
                 {

                     List<SampleRequestDetailModel> list = new List<SampleRequestDetailModel>();
                     var _SampleRequestModel = new SampleRequestModel();

                     // Add new detail information
                     foreach (var item in lstSampleRequestDetails)
                     {
                         SampleRequestDetailModel entityModel = new SampleRequestDetailModel();

                         entityModel.RequestId = item.RequestId;
                         entityModel.ProductId = item.ProductId;
                         entityModel.RequestQuantity = Convert.ToDecimal(item.RequestQuantity);
                         entityModel.CustomerId = item.CustomerId;
                         entityModel.Purpose = item.Purpose;
                         entityModel.ReceivedQuantity = Convert.ToDecimal(item.ReceivedQuantity);
                         entityModel.ReceivedDate = item.ReceivedDate;
                         entityModel.DetailText = item.DetailText;
                         list.Add(entityModel);

                     }

                     Session["lstSampleRequestDetails"] = list;
                     return Json(_SampleRequestModel, JsonRequestBehavior.AllowGet);
                     //strMessage = Boolean.TrueString;
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
   