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
    public class SampleSubmissionController : Controller
    {
        // GET: /PCV/SampleSubmission/

        #region Fields
        public readonly BMSCommonSevice _SampleSubmissionService;
        #endregion
        
        #region Constructor
        public SampleSubmissionController(BMSCommonSevice SampleSubmissionService)
            {
                this._SampleSubmissionService = SampleSubmissionService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //SampleSubmission Read
            public JsonResult SampleSubmissionRead()
            {
                var mmodels = GetSampleSubmissionList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: SampleSubmission/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetByID(id);
                    if (model != null)
                    {
                        SampleSubmissionModel viewModel = new SampleSubmissionModel
                        {
                            Id = model.Id,
                            SubmissionNo = model.SubmissionNo,
                            SubmissionDate = model.SubmissionDate,
                            //Organization = _SampleSubmissionService.BMSUnit.SupplierRepository.GetByID(model.OrganizationId).org,
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

            // GET: SampleSubmission/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    //var customerList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.CustomerRepository.GetAll().Where(c=> c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    var customerList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    //var cusContactsList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    //var deliveryStateList = Common.PopulateDeliveryStateDDList();

                    var userOrganization = 3; // Query and Set Defaul Organization Here


                    var sampleDocumentModels = _SampleSubmissionService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                     md => new SampleDocumentModel
                     {
                         Id = md.Id,
                         DocName = md.DocName,
                         IsSelected = md.IsSelected
                     }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);

                    var model = new SampleSubmissionModel()
                    {
                        ddlCustomer = customerList,
                        SubmissionDate=DateTime.Now,
                        ddlOrganization = organizationList,
                        OrganizationId = Convert.ToInt64(userOrganization),
                        sampleDocumentModels = sampleDocumentModels.ToList()
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
            public ActionResult Create(SampleSubmissionModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {

                    if (Session["lstSampleSubmissionDetails"] != null)
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
                                viewModel.SubmissionNo = "Test";
                                viewModel.SubmissionText= "N/A";
                            #endregion

                            List<SampleSubmissionDetailModel> list = new List<SampleSubmissionDetailModel>();
                            if (Session["lstSampleSubmissionDetails"] != null)
                            {
                                list = (List<SampleSubmissionDetailModel>)Session["lstSampleSubmissionDetails"];
                            }

                            var entity = viewModel.ToEntity();

                            _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.Add(entity);
                            _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.SaveChanges();

                            Session["SampleSubmissionMasterId"] = entity.Id;

                            #region Save SampleSubmission Detail

                            if (list != null && list.Count > 0)
                            {
                              foreach (SampleSubmissionDetailModel detail in list)
                                {

                                    detail.SubmissionId = Convert.ToInt64(entity.Id);
                                    var detailEntity = detail.ToEntity();
                                    _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.Add(detailEntity);
                                    _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.SaveChanges();
                                }
                        }

                        Session["lstSampleSubmissionDetails"] = null;
              
                        #endregion

                        #region Save Submission Attached Document 

                        foreach (SampleDocumentModel doc in viewModel.sampleDocumentModels)
                        {
                            if (doc.IsSelected == true)
                            {
                                SampleSubmissionDocumentModel attachDocs = new SampleSubmissionDocumentModel();
                                attachDocs.SubmissionId = Convert.ToInt64(entity.Id);
                                attachDocs.DocumentId = doc.Id;
                                var detailDocumentEntity = attachDocs.ToEntity();
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.Add(detailDocumentEntity);
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.SaveChanges();
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
                    
                    var model = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetByID(id);

                    Session["SampleSubmissionId"] = model.Id;

                    var customerList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    //var cusContactsList = SelectListItemExtension.PopulateDropdownList(_SampleSubmissionService.BMSUnit.TransporterRepository.GetAll().ToList<tblTransporter>(), "Id", "TransporterName").ToList();
                    //var deliveryStateList = Common.PopulateDeliveryStateDDList();


                    var sampleDocumentModels = _SampleSubmissionService.BMSUnit.SampleDocumentRequiredRepository.GetAll().ToList().Select(
                          md => new SampleDocumentModel
                          {
                              Id = md.Id,
                              DocName = md.DocName,
                              IsSelected = _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.GetAll().ToList().Where(x => x.SubmissionId == model.Id && x.DocumentId == md.Id).Count() > 0 ? true : false

                              //md.IsSelected
                          }).OrderBy(o => o.DocName).ThenBy(ot => ot.Id);


                    if (model != null)
                    {
                        SampleSubmissionModel viewModel = new SampleSubmissionModel
                        {
                            Id = Convert.ToInt64(model.Id),
                            SubmissionNo = model.SubmissionNo,
                            SubmissionDate = model.SubmissionDate,
                            ddlOrganization = organizationList,
                            OrganizationId = Convert.ToInt64(model.OrganizationId),
                            ddlCustomer = customerList,
                            CustomerId = model.CustomerId,
                            sampleDocumentModels = sampleDocumentModels.ToList()

                            //Designation = _SampleSubmissionService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Address,
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
            public ActionResult Edit(SampleSubmissionModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {

                        var model = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetByID(viewModel.Id);

                        #region Current User
                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion


                        List<SampleSubmissionDetailModel> list = new List<SampleSubmissionDetailModel>();
                        if (Session["lstSampleSubmissionDetails"] != null)
                        {
                            list = (List<SampleSubmissionDetailModel>)Session["lstSampleSubmissionDetails"];
                        }

                        #region Releated Data
                        viewModel.SubmissionNo = "Test";
                        viewModel.SubmissionText = "N/A";
                        //viewModel.testVal1 = _SampleSubmissionService.Get_New_InvoiceId().FirstOrDefault().testVal1;
                        #endregion
                        
                        var entity = viewModel.ToEntity();

                        // Get previous detail list
                        var lst = _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.GetAll().Where(
                                    q => q.SubmissionId == entity.Id);

                        foreach (var dt in lst)
                        {
                            _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }

                        #region Save SampleSubmission Detail

                        if (list != null && list.Count > 0)
                        {
                            foreach (SampleSubmissionDetailModel detail in list)
                            {

                                detail.SubmissionId = Convert.ToInt64(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.Add(detailEntity);
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.SaveChanges();
                            }
                        }
                        #endregion

                        #region Save Sample Submission Document

                        // Get and delete previous detail list 
                        var prevAttachDocList = _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.GetAll().Where(
                                    q => q.SubmissionId == entity.Id);

                        foreach (var dt in prevAttachDocList)
                        {
                            _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                        }

                        foreach (SampleDocumentModel doc in viewModel.sampleDocumentModels)
                        {
                            if (doc.IsSelected == true)
                            {
                                SampleSubmissionDocumentModel attachDocs = new SampleSubmissionDocumentModel();
                                attachDocs.SubmissionId = Convert.ToInt64(entity.Id);
                                attachDocs.DocumentId = doc.Id;
                                var detailDocumentEntity = attachDocs.ToEntity();
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.Add(detailDocumentEntity);
                                _SampleSubmissionService.BMSUnit.SampleSubmissionDocumentRepository.SaveChanges();
                            }
                        }
                        #endregion


                        _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.Update(entity);
                        _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.SaveChanges();
                        
                        Session["lstSampleSubmissionDetails"] = null;


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
                    var model = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetByID(id);

                    if (model != null)
                    {
                        SampleSubmissionModel viewModel = new SampleSubmissionModel
                        {
                            Id = model.Id,
                            SubmissionNo = model.SubmissionNo,
                            SubmissionDate = model.SubmissionDate
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
                    var model = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        //_SampleSubmissionService.BMSUnit.SampleSubmissionRepository.Delete_64Bit(model.Id);
                       // _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.SaveChanges();

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

            private List<SampleSubmissionModel> GetSampleSubmissionList()
            {
                AppConstant permission = new AppConstant();
                
                //var userName = HttpContext.User.Identity.Name;
                //var companyCode = _SampleSubmissionService.BMSUnit.UserInfoRepository.GetAll().Where(c => c.UserId == userName).ToList().FirstOrDefault().CompanyCode;
                //var voucherInfoViewModels = _pettyCashVoucherInfoService.BMSUnit.WebVoucherMasterRepository.GetAll().Where(c=>c.UserId == userId && c.Posted == "N").ToList().Select(

                var _Models = _SampleSubmissionService.BMSUnit.SampleSubmissionRepository.GetAll().ToList().Select(
                    md => new SampleSubmissionModel
                    {
                        Id = Convert.ToInt64(md.Id),
                        SubmissionNo = md.SubmissionNo,
                        SubmissionDate = md.SubmissionDate,
                        Organization = _SampleSubmissionService.BMSUnit.OrganizationRepository.GetByID(md.OrganizationId).Organization,
                        CustomerName = _SampleSubmissionService.BMSUnit.CustomerRepository.GetByID(md.CustomerId).CustomerName,
                        //TransporterName=md.TransporterId.ToString(),                        
                        //TransporterName = md.TransporterId != null ? _SampleSubmissionService.BMSUnit.TransporterRepository.GetByID(md.TransporterId).TransporterName:"",
                        //TransporterName = _SampleSubmissionService.BMSUnit.TransporterRepository.GetAll().Where(c => c.Id == md.TransporterId).ToList().FirstOrDefault().TransporterName,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "SampleSubmission", md.Id.ToString(),  false, true, false)
                    }).OrderBy(o => o.SubmissionNo).ThenBy(ot => ot.SubmissionDate);

                return _Models.ToList();
            }

            public JsonResult GetSampleSubmissionDetails()
            {
                long SubmissionId = Convert.ToInt64(Session["SampleSubmissionId"]);

                var SampleSubmissionDetailsModel = from SampleSubmissionDetail in _SampleSubmissionService.BMSUnit.SampleSubmissionDetRepository.GetAll().Where(q => q.SubmissionId == SubmissionId)
                    select new SampleSubmissionDetailModel()
                    {
                        Id = Convert.ToInt64(SampleSubmissionDetail.Id),
                        ProductId = SampleSubmissionDetail.ProductId,
                        ProductName = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.ProductId).FirstOrDefault().ProductName,
                        SamplingUnit = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.ProductId).FirstOrDefault().SamplingUnit,
                        SubmissionQuantity = SampleSubmissionDetail.SubmissionQuantity,
                        ExpieryDate=SampleSubmissionDetail.ExpieryDate,
                        Purpose = SampleSubmissionDetail.Purpose,
                        SupplierId = Convert.ToInt64(SampleSubmissionDetail.SupplierId),
                        SupplierName = _SampleSubmissionService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.SupplierId).FirstOrDefault().SupplierName,
                        Origin= SampleSubmissionDetail.Origin,
                        TrialReport= SampleSubmissionDetail.TrialReport,
                        ApproveState=Convert.ToInt32(SampleSubmissionDetail.ApproveState),
                        DetailText= SampleSubmissionDetail.DetailText
                    };

                var modelList = SampleSubmissionDetailsModel.OrderBy(x => x.Id).ToList();

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }

            //For Autocomplete
            public JsonResult PopulateProductAutoComplete()
            {

                var GetProductList = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductCode = MI.ProductName
                    });

                return Json(GetProductList, JsonRequestBehavior.AllowGet);
            }

            public JsonResult AddProductToGrid(string product)
            {

                var GridRowItem = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == product).ToList().Select(
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
                 var jresult = _SampleSubmissionService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == id).ToList().Select(
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

                 var supplierModels = _SampleSubmissionService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                     md => new SupplierViewModel
                     {
                         SupplierId = md.Id,
                         SupplierName = md.SupplierName
                     }).OrderBy(o => o.SupplierName).ThenBy(ot => ot.SupplierId);

                 return supplierModels.ToList();
             }
       
       
             public JsonResult SetSampleSubmissionDetailsListForSave(List<SampleSubmissionDetailViewModel> lstSampleSubmissionDetails)
             {
                 var strMessage = string.Empty;

                 // Clear detail list
                 Session["lstSampleSubmissionDetails"] = null;

                 try
                 {

                     List<SampleSubmissionDetailModel> list = new List<SampleSubmissionDetailModel>();
                     var _SampleSubmissionModel = new SampleSubmissionModel();

                     // Add new detail information
                     foreach (var item in lstSampleSubmissionDetails)
                     {
                         SampleSubmissionDetailModel entityModel = new SampleSubmissionDetailModel();

                         entityModel.SubmissionId = item.SubmissionId;
                         entityModel.ProductId = item.ProductId;
                         entityModel.SubmissionQuantity = Convert.ToDecimal(item.SubmissionQuantity);
                         entityModel.ExpieryDate = item.ExpieryDate; 
                         entityModel.Purpose = item.Purpose;
                         entityModel.SupplierId = item.SupplierId;
                         entityModel.Origin = item.Origin;
                         entityModel.TrialReport = item.TrialReport;
                         entityModel.ApproveState = item.ApproveState;
                         entityModel.DetailText = item.DetailText;
                         list.Add(entityModel);

                     }

                     Session["lstSampleSubmissionDetails"] = list;
                     return Json(_SampleSubmissionModel, JsonRequestBehavior.AllowGet);
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
   