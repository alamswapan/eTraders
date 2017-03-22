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
    public class CallReportController : Controller
    {
        

        IList<CurrentStageViewModel> currentStageList = new List<CurrentStageViewModel>() { 
                    new CurrentStageViewModel(){ StageId=1, StageName="Inquiry Received" },
                    new CurrentStageViewModel(){ StageId=2, StageName="Trial" },
                    new CurrentStageViewModel(){ StageId=3, StageName="Win" },
                    new CurrentStageViewModel(){ StageId=4, StageName="Lost" }
                };

        #region Fields
        public readonly BMSCommonSevice _CallReportInfoService;
        #endregion

        #region Constructor
        public CallReportController(BMSCommonSevice SupplierInfoService)
            {
                this._CallReportInfoService = SupplierInfoService;            
            }
        #endregion

        public ActionResult Index()
            {            
                return View();
            }

        #region Project
            //Project Read
            public JsonResult ProjectRead()
            {
                var mmodels = GetProjectList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /Project/Create
            public ActionResult CreateProject()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var userName = HttpContext.User.Identity.Name;
                    var currentStageList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CurrentStageRepository.GetAll().ToList<tblCurrentStage>(), "Id", "CurrentStage").ToList();
                    var supplierList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName", isEdit: true).ToList();
                    var customerList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();
                    var communicationChannelList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CommunicationChannelRepository.GetAll().ToList<tblCommunicationChannel>(), "Id", "CommunicationChannel").ToList();

                    var model = new CallReportProjectInfoModel()
                    {
                        CreateDate = DateTime.Now,
                        ddlCurrentStage = currentStageList,
                        ddlCustomer = customerList,
                        ddlSupplier = supplierList,
                        ddlCommunicationChannel = communicationChannelList,
                        SalesPerson = userName
                    };
                    
                    return PartialView("_CreateProject", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /Project/Create
            [HttpPost]
            public ActionResult CreateProject(CallReportProjectInfoModel viewModel)
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
                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.Add(entity);
                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.SaveChanges();

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

            // GET: /Project/Edit/By ID
            public ActionResult EditProject(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetByID(id);

                    var currentStageList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CurrentStageRepository.GetAll().ToList<tblCurrentStage>(), "Id", "CurrentStage").ToList();
                    var supplierList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName", isEdit: true).ToList();
                    var customerList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();
                    var communicationChannelList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CommunicationChannelRepository.GetAll().ToList<tblCommunicationChannel>(), "Id", "CommunicationChannel").ToList();

                    if (model != null)
                    {
                        CallReportProjectInfoModel viewModel = new CallReportProjectInfoModel
                        {
                            ddlCurrentStage = currentStageList,
                            ddlCustomer = customerList,
                            ddlSupplier = supplierList,
                            ddlCommunicationChannel = communicationChannelList,
                            Id = model.Id,
                            CreateDate = model.CreateDate,
                            ProjectName = model.ProjectName,
                            CurrentStageId = model.CurrentStageId,
                            CustomerId = model.CustomerId,
                            SupplierId = model.SupplierId,
                            Description = model.Description,
                            SellingOpportunity = model.SellingOpportunity,
                            PotVolPerYear = model.PotVolPerYear,
                            PotAmountPerYear = model.PotAmountPerYear,
                            RemarkNextAction = model.RemarkNextAction,
                            CommunicationChannelId = model.CommunicationChannelId,
                            SalesPerson = model.SalesPerson
                        };

                        //CallReportProjectInfoModel viewModel = new CallReportProjectInfoModel();
                        //viewModel = model.ToModel();

                        return PartialView("_EditProject", viewModel);
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

            // POST: /Project/Edit/By ID
            [HttpPost]
            public ActionResult EditProject(CallReportProjectInfoModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.Update(entity);
                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.SaveChanges();

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

            // GET: /Project/Delete/By ID
            public ActionResult DeleteProject(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetByID(id);

                    if (model != null)
                    {
                        CallReportProjectInfoModel viewModel = new CallReportProjectInfoModel
                        {
                            Id = model.Id,
                            SupplierId = model.SupplierId
                            
                        };

                        return PartialView("_DeleteProject", viewModel);
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

            // POST: /Project/Delete/By ID

            [HttpPost, ActionName("DeleteProject")]
            public ActionResult DeleteProjectConfirmed(long id)
            {
                var strMessage = string.Empty;
                try
                {
                    var model = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetByID(id);

                    if (model != null)
                    {
                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.Delete_64Bit(model.Id);
                        _CallReportInfoService.BMSUnit.CallReportProjectRepository.SaveChanges();

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

            private List<CallReportProjectInfoModel> GetProjectList()
            {
                AppConstant permission = new AppConstant();

                var ProjectViewModels = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetAll().ToList().Select(
                    md => new CallReportProjectInfoModel
                    {
                        Id = md.Id,
                        ProjectName = md.ProjectName,
                        CurrentStage = _CallReportInfoService.BMSUnit.CurrentStageRepository.GetAll().Where(c=>c.Id == md.CurrentStageId).FirstOrDefault().CurrentStage,
                        Description = md.Description,
                        SellingOpportunity = md.SellingOpportunity,
                        PotVolPerYear = md.PotVolPerYear,
                        PotAmountPerYear = md.PotAmountPerYear,
                        RemarkNextAction = md.RemarkNextAction,
                        ActionLink = Common.KendoUIGridActionLinkGenerateCallReport("PCV", "CallReport","Project", md.Id.ToString(), false, true, false)
                    }).OrderBy(o => o.ProjectName).ThenBy(ot => ot.CurrentStage);

                return ProjectViewModels.ToList();
            }

        #endregion


        
        #region SalesCallInquiry

            //SalesCallInquiry Read
            public JsonResult SalesCallInquiryRead()
            {
                var mmodels = GetSalesCallInquiryList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: /SalesCallInquiry/Create
            public ActionResult CreateSalesCallInquiry()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var userName = HttpContext.User.Identity.Name;
                    var supplierList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName", isEdit: true).ToList();
                    var customerList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();
                    ViewBag.currentStageList = currentStageList;
 
                    var model = new CallReportSalesCallInquiryInfoModel()
                    {
                        DateOfUpdate = DateTime.Now,
                        ddlCustomer = customerList,
                        ddlSupplier = supplierList,
                        SalesPerson = userName
                    };

                    return PartialView("_CreateSalesCallInquiry", model);
                }
                catch (Exception ex)
                {
                    errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                    return PartialView("_ErrorPopUp", errorViewModel);
                }
            }

            // POST: /SalesCallInquiry/Create
            [HttpPost]
            public ActionResult CreateSalesCallInquiry(CallReportSalesCallInquiryInfoModel viewModel)
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
                        _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.Add(entity);
                        _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.SaveChanges();

                        #region Detail Save

                        foreach (var item in viewModel.salesCallInquiryDetailViewModel)
                        {
                            item.SalesCallInquiryId = Convert.ToInt64(entity.Id);
                            item.IUser = userName;
                            item.IDate = DateTime.Now;
                            item.EDate = DateTime.Now;
                            //item.StageId = item.StageIdTemp;
                            item.StageId = item.StageId;
                            var detailEntity = item.ToEntity();
                            _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.Add(detailEntity);
                            _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.SaveChanges();
                        }

                        #endregion

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

            // GET: /SalesCallInquiry/Edit/By ID
            public ActionResult EditSalesCallInquiry(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.GetByID(id);

                    var userName = HttpContext.User.Identity.Name;
                    var supplierList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblSupplier>(), "Id", "SupplierName", isEdit: true).ToList();
                    var customerList = SelectListItemExtension.PopulateDropdownList(_CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.IUser.ToUpper() != "SYSTEM").ToList<tblCustomer>(), "Id", "CustomerName", isEdit: true).ToList();

                    var salesInquiryDetailsViewModel = from salesInqDetail in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.GetAll().Where(q => q.SalesCallInquiryId == id)
                        select new SalesCallInquiryDetailViewModel()
                        {
                            Id = salesInqDetail.Id,
                            ProductId = salesInqDetail.ProductId,
                            ProductName = salesInqDetail.ProductId !=0? _CallReportInfoService.BMSUnit.ProductRepository.GetAll().Where(c=>c.Id == salesInqDetail.ProductId).FirstOrDefault().ProductName : "",
                            Quantity = salesInqDetail.Quantity,
                            Unit = salesInqDetail.Unit,
                            OfferedAmountPerUnit = salesInqDetail.OfferedAmountPerUnit,
                            PriceValidity = salesInqDetail.PriceValidity,
                            BidPrice = salesInqDetail.BidPrice,
                            StageId = salesInqDetail.StageId,
                            StageIdTemp = salesInqDetail.StageId,
                            Remarks = salesInqDetail.Remarks
                           

                        };



                    if (model != null)
                    {
                        CallReportSalesCallInquiryInfoModel viewModel = new CallReportSalesCallInquiryInfoModel
                        {
                            ddlCustomer = customerList,
                            ddlSupplier = supplierList,
                            Id = model.Id,
                            DateOfUpdate = model.DateOfUpdate,
                            CustomerId = model.CustomerId,
                            SupplierId = model.SupplierId,
                            SalesPerson = model.SalesPerson,
                            salesCallInquiryDetailViewModel = salesInquiryDetailsViewModel
                        };

                        //CallReportProjectInfoModel viewModel = new CallReportProjectInfoModel();
                        //viewModel = model.ToModel();

                        return PartialView("_EditSalesCallInquiry", viewModel);
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

            public JsonResult GetSalesCallInquiryData()
            {
                
                var salesInquiryDetailsViewModel = from salesInqDetail in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.GetAll().Where(q => q.SalesCallInquiryId == 38)
                    select new SalesCallInquiryDetailViewModel()
                    {
                        //Id = salesInqDetail.Id,
                        ProductId = salesInqDetail.ProductId,
                        ProductName = salesInqDetail.ProductId != 0 ? _CallReportInfoService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == salesInqDetail.ProductId).FirstOrDefault().ProductName : "",
                        Quantity = salesInqDetail.Quantity,
                        Unit = salesInqDetail.Unit,
                        OfferedAmountPerUnit = salesInqDetail.OfferedAmountPerUnit,
                        PriceValidity = salesInqDetail.PriceValidity,
                        BidPrice = salesInqDetail.BidPrice,
                        StageId = salesInqDetail.StageId,
                        StageIdTemp = salesInqDetail.StageId,
                        Remarks = salesInqDetail.Remarks
                    };

                //var modelList = salesInquiryDetailsViewModel.OrderBy(x => x.Id).ToList();
                var modelList = salesInquiryDetailsViewModel.OrderBy(x => x.Id);
                modelList = null;
                return Json(modelList, JsonRequestBehavior.AllowGet);
            }

            private List<CallReportSalesCallInquiryInfoModel> GetSalesCallInquiryList()
            {
                AppConstant permission = new AppConstant();

                var SalesCallInquiryViewModels = _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.GetAll().ToList().Select(
                    md => new CallReportSalesCallInquiryInfoModel
                    {
                        Id = md.Id,
                        CustomerName = _CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == md.CustomerId).FirstOrDefault().CustomerName,
                        SupplierName = _CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == md.SupplierId).FirstOrDefault().SupplierName,
                        ActionLink = Common.KendoUIGridActionLinkGenerateCallReport("PCV", "CallReport", "SalesCallInquiry", md.Id.ToString(), false, true, false)
                    }).OrderBy(o => o.CustomerName).ThenBy(ot => ot.SupplierName);

                return SalesCallInquiryViewModels.ToList();
            }

            //For Autocomplete
            public JsonResult PopulateProductAutoComplete()
            {

                var GetArticleList = _CallReportInfoService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductName = MI.ProductName
                    });

                return Json(GetArticleList, JsonRequestBehavior.AllowGet);
            }

            public JsonResult ReadCurrentStageList()
            {
                var mmodels = GetCurrentStageList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            private List<CurrentStageViewModel> GetCurrentStageList()
            {
                AppConstant permission = new AppConstant();

                var customerModels = _CallReportInfoService.BMSUnit.CurrentStageRepository.GetAll().ToList().Select(
                    md => new CurrentStageViewModel
                    {
                        StageId = md.Id,
                        StageName = md.CurrentStage
                    }).OrderBy(o => o.StageId).ThenBy(ot => ot.StageId);

                return customerModels.ToList();
            }

            public JsonResult AddArticleToGrid(string productName)
            {

                var GridRowItem = _CallReportInfoService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == productName).ToList().Select(
                    MI => new ProductModel
                    {
                        Id = MI.Id,
                        ProductCode = MI.ProductCode,
                        ProductName = MI.ProductName,
                        SamplingUnit = MI.SamplingUnit,
                        OfferedAmountPerUnit = 0,
                        BidPrice = 0
                        
                    });
                return Json(GridRowItem, JsonRequestBehavior.AllowGet);
            }

        #endregion


        #region OrderReceived

            //OrderReceived Read
            public JsonResult SalesCallOrderReceivedRead()
            {
                var mmodels = GetOrderReceivedList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            private List<CallReportSalesCallInquiryInfoModel> GetOrderReceivedList()
            {
                AppConstant permission = new AppConstant();

                var SalesCallInquiryViewModels = from parent in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.GetAll()
                            join detail in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.GetAll()
                                on parent.Id equals detail.SalesCallInquiryId
                                where detail.StageId == 4 //Win
                               select new CallReportSalesCallInquiryInfoModel 
                               {
                                   Id = parent.Id,
                                   CustomerName = _CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == parent.CustomerId).FirstOrDefault().CustomerName,
                                   SupplierName = _CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == parent.SupplierId).FirstOrDefault().SupplierName,
                                   ActionLink = Common.KendoUIGridActionLinkGenerateCallReport("PCV", "CallReport", "SalesCallInquiry", parent.Id.ToString(), false, true, false)
                            };

                var modelListDistinct = SalesCallInquiryViewModels.GroupBy(test => test.Id)
                                       .Select(grp => grp.First());

                return modelListDistinct.ToList();
            }

        #endregion


        #region OrderLost

            //OrderLost Read
            public JsonResult SalesCallOrderLostRead()
            {
                var mmodels = GetOrderLostList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            private List<CallReportSalesCallInquiryInfoModel> GetOrderLostList()
            {
                AppConstant permission = new AppConstant();

                var SalesCallInquiryViewModels = from parent in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryRepository.GetAll()
                                                 join detail in _CallReportInfoService.BMSUnit.CallReportSalesCallInquiryDetailRepository.GetAll()
                                                     on parent.Id equals detail.SalesCallInquiryId
                                                 where detail.StageId == 5 //Lost
                                                 select new CallReportSalesCallInquiryInfoModel
                                                 {
                                                     Id = parent.Id,
                                                     CustomerName = _CallReportInfoService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == parent.CustomerId).FirstOrDefault().CustomerName,
                                                     SupplierName = _CallReportInfoService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == parent.SupplierId).FirstOrDefault().SupplierName,
                                                     ActionLink = Common.KendoUIGridActionLinkGenerateCallReport("PCV", "CallReport", "SalesCallInquiry", parent.Id.ToString(), false, true, false)
                                                 };

                var modelListDistinct = SalesCallInquiryViewModels.GroupBy(test => test.Id)
                                       .Select(grp => grp.First());

                return modelListDistinct.ToList();
            }

        #endregion



        #region Project
            //Project Read
            public JsonResult SampleRequestRead()
            {
                var mmodels = GetSampleRequestList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            private List<CallReportProjectInfoModel> GetSampleRequestList()
            {
                AppConstant permission = new AppConstant();

                var ProjectViewModels = _CallReportInfoService.BMSUnit.CallReportProjectRepository.GetAll().ToList().Select(
                    md => new CallReportProjectInfoModel
                    {
                        Id = md.Id,
                        ProjectName = md.ProjectName,
                        CurrentStage = _CallReportInfoService.BMSUnit.CurrentStageRepository.GetAll().Where(c => c.Id == md.CurrentStageId).FirstOrDefault().CurrentStage,
                        Description = md.Description,
                        SellingOpportunity = md.SellingOpportunity,
                        PotVolPerYear = md.PotVolPerYear,
                        PotAmountPerYear = md.PotAmountPerYear,
                        RemarkNextAction = md.RemarkNextAction,
                        ActionLink = Common.KendoUIGridActionLinkGenerateCallReport("PCV", "CallReport", "Project", md.Id.ToString(), false, true, false)
                    }).OrderBy(o => o.ProjectName).ThenBy(ot => ot.CurrentStage);

                return ProjectViewModels.ToList();
            }
        #endregion
    }
}
