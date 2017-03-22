using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Domain.PCV;
using LILI_CRM.Web.Areas.PCV.ViewModel;
using LILI_CRM.Web.Utility;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Web.ViewModels;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class PriceRequestController : Controller
    {
        #region Fields
        public readonly BMSCommonSevice _PriceRequestService;
        #endregion

        #region Constructor
        public PriceRequestController(BMSCommonSevice PriceRequestService)
            {
                this._PriceRequestService = PriceRequestService;            
            }
        #endregion


        #region Action
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
              
                var customerList = SelectListItemExtension.PopulateDropdownList(_PriceRequestService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                var supplierList = SelectListItemExtension.PopulateDropdownList(_PriceRequestService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(), "Id", "SupplierName").ToList();

                var SalesCallId = 34;

                var model = new PriceRequestModel()
                {
                    SalesCallId = Convert.ToInt64(SalesCallId),
                    PriceRequestDate = DateTime.Now,                   
                    ddlSupplier = supplierList,
                    ddlCustomer = customerList
                };

                return PartialView("_Create", model);

            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                return PartialView("_ErrorPopUp", errorViewModel);
            }

        }

        // GET: Sales Budget/Edit/By ID
        public ActionResult Edit(long id)
        {
            var errorViewModel = new ErrorViewModel();

            try
            {

                var model = _PriceRequestService.BMSUnit.PriceRequestRepository.GetByID(id);

                Session["PriceRequestMasterEditId"] = model.Id;


                var customerList = SelectListItemExtension.PopulateDropdownList(_PriceRequestService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                var supplierList = SelectListItemExtension.PopulateDropdownList(_PriceRequestService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(), "Id", "SupplierName").ToList();

                if (model != null)
                {
                    PriceRequestModel viewModel = new PriceRequestModel
                    {
                        Id = Convert.ToInt64(model.Id),
                        SalesCallId = Convert.ToInt64(model.SalesCallId),
                        PriceRequestDate = model.PriceRequestDate,
                        CustomerId = model.CustomerId,
                        SupplierId = model.SupplierId,
                        ddlSupplier = supplierList,
                        ddlCustomer = customerList
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

        public JsonResult GetPriceRequestDetails()
        {
            long SubmissionId = Convert.ToInt64(Session["PriceRequestMasterEditId"]);
            var detailListAll = _PriceRequestService.BMSUnit.PriceRequestDetailRepository.GetAll().Where(x => x.PriceRequestId == SubmissionId).ToList();
            var detailList = _PriceRequestService.BMSUnit.PriceRequestDetailRepository.GetAll().Where(x => x.PriceRequestId == SubmissionId).ToList().Select(
                 md => new PriceRequestDetailViewModel
                 {
                     Id = md.Id,
                     ArticleName = _PriceRequestService.BMSUnit.ProductRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == md.ProductId).FirstOrDefault().ProductName,
                     PriceRequestId = md.PriceRequestId,
                     PriceTermId = md.PriceTermId,
                     ExpectedPrice = md.ExpectedPrice,
                     ReceivedPrice = md.ReceivedPrice,
                     AsOnDate = md.AsOnDate,
                     ValidityDate = md.ValidityDate,
                     ShipmentCountryId = md.ShipmentCountryId,
                     ShipmentLeadTime = md.ShipmentLeadTime,
                     PriceRequestStatus = md.PriceRequestStatus

                 }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

            //var listDistinct
            //    = detailList.GroupBy(
            //        i => new { i.CustomerId, i.SupplierId },
            //        (key, group) => group.First()
            //    ).ToList();


            return Json(detailList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SavePriceRequestData(List<PriceRequestModel> lstSampleSubmissionMaster, List<PriceRequestDetailViewModel> lstSampleSubmissionDetails)
        {
            var strMessage = string.Empty;
            
            try
            {
                foreach (var item in lstSampleSubmissionMaster)
                {
                    PriceRequestModel masterEntityModel = new PriceRequestModel();

                    #region Current User
                    var userName = HttpContext.User.Identity.Name;
                    masterEntityModel.IUser = userName;
                    masterEntityModel.IDate = DateTime.Now;
                    masterEntityModel.EDate = DateTime.Now;
                    #endregion
                    masterEntityModel.SalesCallId = item.SalesCallId;
                    masterEntityModel.PriceRequestDate = item.PriceRequestDate;
                    masterEntityModel.SupplierId = item.SupplierId;
                    masterEntityModel.CustomerId = item.CustomerId;
                    //masterList.Add(masterEntityModel);

                    var entity = masterEntityModel.ToEntity();
                    _PriceRequestService.BMSUnit.PriceRequestRepository.Add(entity);
                    _PriceRequestService.BMSUnit.PriceRequestRepository.SaveChanges();

                    Session["PriceRequestMasterId"] = entity.Id;
                }


                // Add new detail information

                foreach (var item in lstSampleSubmissionDetails)
                {
                    PriceRequestDetailViewModel entityDetailModel = new PriceRequestDetailViewModel();

                    entityDetailModel.PriceRequestId = Convert.ToInt64(Convert.ToInt64(Session["PriceRequestMasterId"]));
                    entityDetailModel.ProductId = item.ArticleId;
                    entityDetailModel.PriceTermId = item.PriceTermId;
                    entityDetailModel.ExpectedPrice = item.ExpectedPrice;
                    entityDetailModel.ReceivedPrice = item.ReceivedPrice;
                    entityDetailModel.AsOnDate = item.AsOnDate;
                    entityDetailModel.ValidityDate = item.ValidityDate;
                    entityDetailModel.MOQ = item.MOQ;
                    entityDetailModel.ShipmentCountryId = item.ShipmentCountryId;
                    entityDetailModel.ShipmentLeadTime = item.ShipmentLeadTime;
                    entityDetailModel.PriceRequestStatus = item.PriceRequestStatus;

                    var detailEntity = entityDetailModel.ToEntity();
                    _PriceRequestService.BMSUnit.PriceRequestDetailRepository.Add(detailEntity);
                    _PriceRequestService.BMSUnit.PriceRequestDetailRepository.SaveChanges();                       

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

        public JsonResult PriceRequestRead()
        {
            var mmodels = GetPriceRequesttList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }



        private List<PriceRequestModel> GetPriceRequesttList()
        {
            //(_SalesBudgetService.BMSUnit.SBURepository.GetAll().Where(c=> Convert.ToInt64(c.Id) ==  Convert.ToInt64(md.SBUId)).FirstOrDefault().SBUName)
            AppConstant permission = new AppConstant();

            var _Models = _PriceRequestService.BMSUnit.PriceRequestRepository.GetAll().ToList().Select(
                md => new PriceRequestModel
                {
                    Id = Convert.ToInt64(md.Id),
                    PriceRequestDate = md.PriceRequestDate,
                    SupplierName = _PriceRequestService.BMSUnit.SupplierRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.SupplierId)).FirstOrDefault().SupplierName,
                    CustomerName = _PriceRequestService.BMSUnit.CustomerRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.CustomerId)).FirstOrDefault().CustomerName,                    
                    ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "PriceRequest", md.Id.ToString(), false, true, false)
                }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

            return _Models.ToList();
        }

        //For Autocomplete
        public JsonResult PopulateArticleAutoComplete()
        {

            var GetArticleList = _PriceRequestService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                MI => new ProductModel
                {
                    ProductCode = MI.ProductName
                });

            return Json(GetArticleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddArticleToGrid(string articleCode)
        {

            var GridRowItem = _PriceRequestService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == articleCode).ToList().Select(
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

        public JsonResult ReadPriceTermList()
        {
            var mmodels = GetPriceTermList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<PriceTermModel> GetPriceTermList()
        {
            AppConstant permission = new AppConstant();

            var termModels = _PriceRequestService.BMSUnit.PriceTermRepository.GetAll().ToList().Select(
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

            var countryListModels = _PriceRequestService.BMSUnit.CountryRepository.GetAll().ToList().Select(
                md => new CountryViewModel
                {
                    ShipmentCountryId = md.Id,
                    CountryName = md.CountryName
                }).OrderBy(o => o.ShipmentCountryId).ThenBy(ot => ot.ShipmentCountryId);

            return countryListModels.ToList();
        }

    }
}