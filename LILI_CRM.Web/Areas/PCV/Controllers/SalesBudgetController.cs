using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Domain.PCV;
using LILI_CRM.Web.Areas.PCV.ViewModel;
using LILI_CRM.Web.Utility;
using LILI_CRM.Web.Utility;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Web.ViewModels;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class SalesBudgetController : Controller
    {
        //
        // GET: /PCV/SalesBudget/
        #region Fields
        public readonly BMSCommonSevice _SalesBudgetService;
        #endregion
        
        #region Constructor
        public SalesBudgetController(BMSCommonSevice SalesBudgetService)
            {
                this._SalesBudgetService = SalesBudgetService;            
            }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }
        //SalesBudget Read
        public JsonResult SalesBudgetRead()
        {
            var mmodels = GetSalesBudgetList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
               
                var sbuList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.SBURepository.GetAll().ToList<tblSBU>(), "Id", "SBUName").ToList();
                var productList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.ProductRepository.GetAll().ToList<tblProduct>(), "Id", "ProductName").ToList();

                var customerList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(),"Id","CustomerName").ToList();

                

                var supplierList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(),"Id","SupplierName").ToList();

                var SalePersonName = HttpContext.User.Identity.Name;
                var SalePersonId = _SalesBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.UserName == SalePersonName).ToList().FirstOrDefault().Id;
                //var divisionName = 
                var divCount = _SalesBudgetService.BMSUnit.SalesPersonRepository.GetAll().Where(c => c.SalesPersonId == SalePersonName).ToList().Count();
                string divisionName = null;
                
                if (divCount > 0)
                {
                    var divisionId = _SalesBudgetService.BMSUnit.SalesPersonRepository.GetAll().Where(c => c.SalesPersonId == SalePersonName).ToList().FirstOrDefault().DivisionId;
                    divisionName = _SalesBudgetService.BMSUnit.DivisionRepository.GetAll().Where(c => c.Id == divisionId).ToList().FirstOrDefault().DivisionName;
                }               

                
                
                var model = new SalesBudgetModel()
                {
                    BudgetYear = DateTime.Now.Year.ToString(),
                    SalesPersonId = Convert.ToInt64(SalePersonId),
                    SalesPersonName = SalePersonName,
                    DivisionName =  divisionName,
                    ddlSBU = sbuList,
                    ddlProduct = productList
                };

                return PartialView("_Create",model);                

            }
            catch (Exception ex)
            {
                errorViewModel.ErrorMessage = CommonExceptionMessage.GetExceptionMessage(ex);
                return PartialView("_ErrorPopUp", errorViewModel);
            }        
        
        }

        // POST: Invoice/Create
        [HttpPost]
        public ActionResult Create(SalesBudgetModel viewModel)
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
                        



                        //List<SalesBudgetDetailViewModel> list = new List<SalesBudgetDetailViewModel>();                        
                        //if (Session["lstSampleRequestDetails"] != null)
                        //{
                        //    list = (List<SalesBudgetDetailViewModel>)Session["lstSampleRequestDetails"];
                        //}

                        var entity = viewModel.ToEntity();

                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.Add(entity);
                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.SaveChanges();

                        Session["SampleRequestMasterId"] = entity.Id;

                        #region Save SampleRequest Detail

                        //if (list != null && list.Count > 0)
                        //{
                        //    foreach (SampleRequestDetailModel detail in list)
                        //    {

                        //        detail.RequestId = Convert.ToInt64(entity.Id);
                        //        var detailEntity = detail.ToEntity();
                        //        _SalesBudgetService.BMSUnit.SampleRequestDetailsRepository.Add(detailEntity);
                        //        _SalesBudgetService.BMSUnit.SampleRequestDetailsRepository.SaveChanges();
                        //    }
                        //}

                        //Session["lstSampleRequestDetails"] = null;

                        //#endregion

                        //#region Save Sample Request Document

                        //foreach (SampleDocumentModel doc in viewModel.sampleDocumentModels)
                        //{
                        //    if (doc.IsSelected == true)
                        //    {
                        //        SampleRequestDocumentModel aaa = new SampleRequestDocumentModel();
                        //        aaa.RequestId = Convert.ToInt64(entity.Id);
                        //        aaa.DocumentId = doc.Id;
                        //        var detailDocumentEntity = aaa.ToEntity();
                        //        _SalesBudgetService.BMSUnit.SampleRequestDocumentRepository.Add(detailDocumentEntity);
                        //        _SalesBudgetService.BMSUnit.SampleRequestDocumentRepository.SaveChanges();
                        //    }
                        //}

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

        // GET: Sales Budget/Edit/By ID
        public ActionResult Edit(long id)
        {
            var errorViewModel = new ErrorViewModel();

            try
            {

                var model = _SalesBudgetService.BMSUnit.SalesBudgetRepository.GetByID(id);

                Session["SalesBudgetMasterId"] = model.Id;

                var sbuList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.SBURepository.GetAll().ToList<tblSBU>(), "Id", "SBUName").ToList();
                var productList = SelectListItemExtension.PopulateDropdownList(_SalesBudgetService.BMSUnit.ProductRepository.GetAll().ToList<tblProduct>(), "Id", "ProductName").ToList();

                var SalesPersonName = HttpContext.User.Identity.Name;
                var SalesPersonId = _SalesBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.UserName == SalesPersonName).ToList().FirstOrDefault().Id;
                //var divisionName = 
                var divCount = _SalesBudgetService.BMSUnit.SalesPersonRepository.GetAll().Where(c => c.SalesPersonId == SalesPersonName).ToList().Count();
                string divisionName = null;

                if (divCount > 0)
                {
                    var divisionId = _SalesBudgetService.BMSUnit.SalesPersonRepository.GetAll().Where(c => c.SalesPersonId == SalesPersonName).ToList().FirstOrDefault().DivisionId;
                    divisionName = _SalesBudgetService.BMSUnit.DivisionRepository.GetAll().Where(c => c.Id == divisionId).ToList().FirstOrDefault().DivisionName;
                }
                


                if (model != null)
                {
                    SalesBudgetModel viewModel = new SalesBudgetModel
                    {
                        Id = Convert.ToInt64(model.Id),
                        SalesPersonId = Convert.ToInt64(model.SalesPersonId),
                        SalesPersonName = SalesPersonName,
                        DivisionName = divisionName,
                        BudgetYear = model.BudgetYear,
                        SBUId = Convert.ToInt64(model.SBUId),
                        ProductId = Convert.ToInt64(model.ProductId),
                        ddlSBU = sbuList,                        
                        ddlProduct = productList
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

            




        #endregion


        private List<SalesBudgetModel> GetSalesBudgetList()
        {
            //(_SalesBudgetService.BMSUnit.SBURepository.GetAll().Where(c=> Convert.ToInt64(c.Id) ==  Convert.ToInt64(md.SBUId)).FirstOrDefault().SBUName)
            AppConstant permission = new AppConstant();
  
            var _Models = _SalesBudgetService.BMSUnit.SalesBudgetRepository.GetAll().ToList().Select(
                md => new SalesBudgetModel
                {
                    Id = Convert.ToInt64(md.Id),
                    BudgetYear = md.BudgetYear,
                    SalesPersonName = _SalesBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.Id == md.SalesPersonId).FirstOrDefault().UserName,
                    SBUName = (Convert.ToInt64(md.SBUId) > 0) ? (_SalesBudgetService.BMSUnit.SBURepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.SBUId)).FirstOrDefault().SBUName) : "",
                    ProductName = (Convert.ToInt64(md.ProductId) > 0) ? (_SalesBudgetService.BMSUnit.ProductRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.ProductId)).FirstOrDefault().ProductName) : "",
                    
                    ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "SalesBudget", md.Id.ToString(), false, true, false)
                }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

            return _Models.ToList();
        }

       

        public JsonResult ReadCustomerList()
        {
            var mmodels = GetCustList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<CustViewModel> GetCustList()
        {
            AppConstant permission = new AppConstant();

            var customerModels = _SalesBudgetService.BMSUnit.CustomerRepository.GetAll().ToList().Select(
                md => new CustViewModel
                {
                    CustomerId = md.Id,
                    CustomerName = md.CustomerName
                }).OrderBy(o => o.CustomerName).ThenBy(ot => ot.CustomerId);

            return customerModels.ToList();
        }

        public JsonResult ReadSupplierList()
        {
            var mmodels = GetSupplierList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<SupplierViewModel> GetSupplierList()
        {
            AppConstant permission = new AppConstant();

            var supplierModels = _SalesBudgetService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                md => new SupplierViewModel
                {
                    SupplierId = md.Id,
                    SupplierName = md.SupplierName
                }).OrderBy(o => o.SupplierName).ThenBy(ot => ot.SupplierId);

            return supplierModels.ToList();
        }

        public JsonResult SetSampleRequestDetailsListForSave(List<SalesBudgetModel> lstSampleSubmissionMaster, List<SalesBudgetDetailViewModel> lstSampleSubmissionDetails)
        {
            var strMessage = string.Empty;

            // Clear detail list
            //Session["lstSampleRequestDetails"] = null;

            try
            {
                if (ModelState.IsValid)
                {

                    List<SalesBudgetModel> masterList = new List<SalesBudgetModel>();

                    foreach (var item in lstSampleSubmissionMaster)
                    {
                        SalesBudgetModel masterEntityModel = new SalesBudgetModel();

                        #region Current User
                        var userName = HttpContext.User.Identity.Name;
                        masterEntityModel.IUser = userName;
                        masterEntityModel.IDate = DateTime.Now;
                        masterEntityModel.EDate = DateTime.Now;
                        #endregion
                        masterEntityModel.SalesPersonId = item.SalesPersonId;
                        masterEntityModel.BudgetYear = item.BudgetYear;
                        masterEntityModel.ProductId = item.ProductId;
                        masterEntityModel.SBUId = item.SBUId;
                        //masterList.Add(masterEntityModel);

                        var entity = masterEntityModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.Add(entity);
                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.SaveChanges();

                        Session["SalesBudgetId"] = entity.Id;
                    }


                    // Add new detail information

                    foreach (var item in lstSampleSubmissionDetails)
                    {
                        SalesBudgetDetail entityDetailModel = new SalesBudgetDetail();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 1;
                        entityDetailModel.Quantity = item.JanQty;
                        entityDetailModel.Value = item.JanVal;
                        entityDetailModel.Commission = item.JanCom;

                        var detailEntityjan = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityjan);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 2;
                        entityDetailModel.Quantity = item.FebQty;
                        entityDetailModel.Value = item.FebVal;
                        entityDetailModel.Commission = item.FebCom;

                        var detailEntityFeb = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityFeb);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 3;
                        entityDetailModel.Quantity = item.MarQty;
                        entityDetailModel.Value = item.MarVal;
                        entityDetailModel.Commission = item.MarCom;

                        var detailEntityMarch = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityMarch);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 4;
                        entityDetailModel.Quantity = item.AprQty;
                        entityDetailModel.Value = item.AprVal;
                        entityDetailModel.Commission = item.AprCom;

                        var detailEntityApril = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityApril);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 5;
                        entityDetailModel.Quantity = item.MayQty;
                        entityDetailModel.Value = item.MayVal;
                        entityDetailModel.Commission = item.MayCom;

                        var detailEntityMay = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityMay);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 6;
                        entityDetailModel.Quantity = item.JunQty;
                        entityDetailModel.Value = item.JunVal;
                        entityDetailModel.Commission = item.JunCom;

                        var detailEntityJun = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityJun);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 7;
                        entityDetailModel.Quantity = item.JulyQty;
                        entityDetailModel.Value = item.JulyVal;
                        entityDetailModel.Commission = item.JulyCom;

                        var detailEntityJuly = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityJuly);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 8;
                        entityDetailModel.Quantity = item.AugQty;
                        entityDetailModel.Value = item.AugVal;
                        entityDetailModel.Commission = item.AugCom;

                        var detailEntityAugest = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityAugest);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 9;
                        entityDetailModel.Quantity = item.SepQty;
                        entityDetailModel.Value = item.SepVal;
                        entityDetailModel.Commission = item.SepVal;

                        var detailEntitySeptember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntitySeptember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 10;
                        entityDetailModel.Quantity = item.OctQty;
                        entityDetailModel.Value = item.OctVal;
                        entityDetailModel.Commission = item.OctCom;

                        var detailEntityOctober = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityOctober);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 11;
                        entityDetailModel.Quantity = item.NovQty;
                        entityDetailModel.Value = item.NovVal;
                        entityDetailModel.Commission = item.NovCom;

                        var detailEntityNovember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityNovember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 12;
                        entityDetailModel.Quantity = item.DecQty;
                        entityDetailModel.Value = item.DecVal;
                        entityDetailModel.Commission = item.DecCom;

                        var detailEntityDecember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityDecember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();


                        //list.Add(entityDetailModel);
                    }
                    strMessage = "Information has been saved successfully";
                }


                
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
            }

            return Json(new { strMessage = strMessage });
        }

        public JsonResult GetSalesBudgetDetails()
        {
            long SubmissionId = Convert.ToInt64(Session["SalesBudgetMasterId"]);
            var detailListAll = _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.GetAll().Where(x => x.SalesBudgetId == SubmissionId).ToList();
            var detailList = _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.GetAll().Where(x => x.SalesBudgetId == SubmissionId).ToList().Select(
                 md => new SalesBudgetDetailViewModel
                 {
                     Id = md.Id,
                     CustomerId = md.CustomerId,                     
                     SupplierId = md.SupplierId,
                     CustomerName = _SalesBudgetService.BMSUnit.CustomerRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == md.CustomerId).FirstOrDefault().CustomerName,
                     SupplierName = _SalesBudgetService.BMSUnit.SupplierRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == md.SupplierId).FirstOrDefault().SupplierName,
                     JanQty = detailListAll.Where(x => x.BudgetMonth == 1 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     JanVal = detailListAll.Where(x => x.BudgetMonth == 1 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     JanCom = detailListAll.Where(x => x.BudgetMonth == 1 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     FebQty = detailListAll.Where(x => x.BudgetMonth == 2 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     FebVal = detailListAll.Where(x => x.BudgetMonth == 2 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     FebCom = detailListAll.Where(x => x.BudgetMonth == 2 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     MarQty = detailListAll.Where(x => x.BudgetMonth == 3 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     MarCom = detailListAll.Where(x => x.BudgetMonth == 3 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     MarVal = detailListAll.Where(x => x.BudgetMonth == 3 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     AprQty = detailListAll.Where(x => x.BudgetMonth == 4 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     AprVal = detailListAll.Where(x => x.BudgetMonth == 4 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     AprCom = detailListAll.Where(x => x.BudgetMonth == 4 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     MayQty = detailListAll.Where(x => x.BudgetMonth == 5 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     MayVal = detailListAll.Where(x => x.BudgetMonth == 5 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     MayCom = detailListAll.Where(x => x.BudgetMonth == 5 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     JunQty = detailListAll.Where(x => x.BudgetMonth == 6 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     JunVal = detailListAll.Where(x => x.BudgetMonth == 6 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     JunCom = detailListAll.Where(x => x.BudgetMonth == 6 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     JulyQty = detailListAll.Where(x => x.BudgetMonth == 7 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     JulyVal = detailListAll.Where(x => x.BudgetMonth == 7 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     JulyCom = detailListAll.Where(x => x.BudgetMonth == 7 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     AugQty = detailListAll.Where(x => x.BudgetMonth == 8 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     AugVal = detailListAll.Where(x => x.BudgetMonth == 8 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     AugCom = detailListAll.Where(x => x.BudgetMonth == 8 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     SepQty = detailListAll.Where(x => x.BudgetMonth == 9 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     SepVal = detailListAll.Where(x => x.BudgetMonth == 9 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     SepCom = detailListAll.Where(x => x.BudgetMonth == 9 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     OctQty = detailListAll.Where(x => x.BudgetMonth == 10 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     OctVal = detailListAll.Where(x => x.BudgetMonth == 10 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     OctCom = detailListAll.Where(x => x.BudgetMonth == 10 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     NovQty = detailListAll.Where(x => x.BudgetMonth == 11 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     NovVal = detailListAll.Where(x => x.BudgetMonth == 11 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     NovCom = detailListAll.Where(x => x.BudgetMonth == 11 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission,
                     DecQty = detailListAll.Where(x => x.BudgetMonth == 12 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Quantity,
                     DecVal = detailListAll.Where(x => x.BudgetMonth == 12 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Value,
                     DecCom = detailListAll.Where(x => x.BudgetMonth == 12 && x.SupplierId == md.SupplierId && x.CustomerId == md.CustomerId).FirstOrDefault().Commission



                 }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

                var listDistinct
                    = detailList.GroupBy(
                        i => new {i.CustomerId,i.SupplierId},
                        (key, group) => group.First()
                    ).ToList();



            //var SalesBudgetDetailsModel = from SampleSubmissionDetail in _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.GetAll().Where(q => q.Id == SubmissionId)
            //                              select new SalesBudgetDetailViewModel()
            //                              {
            //                                  Id = Convert.ToInt64(SampleSubmissionDetail.Id),
            //                                  CustomerId = SampleSubmissionDetail.CustomerId,
            //                                  CustomerName = _SalesBudgetService.BMSUnit.CustomerRepository.GetAll().Where(c => c.CustomerId == SampleSubmissionDetail.CustomerId).FirstOrDefault().CustomerName,
            //                                  ProductName = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.ArticleId).FirstOrDefault().ProductName,
            //                                  SamplingUnit = _SampleSubmissionService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.ArticleId).FirstOrDefault().SamplingUnit,
            //                                  SubmissionQuantity = SampleSubmissionDetail.SubmissionQuantity,
            //                                  ExpieryDate = SampleSubmissionDetail.ExpieryDate,
            //                                  Purpose = SampleSubmissionDetail.Purpose,
            //                                  SupplierId = Convert.ToInt64(SampleSubmissionDetail.SupplierId),
            //                                  SupplierName = _SampleSubmissionService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == SampleSubmissionDetail.SupplierId).FirstOrDefault().SupplierName,
            //                                  Origin = SampleSubmissionDetail.Origin,
            //                                  TrialReport = SampleSubmissionDetail.TrialReport,
            //                                  ApproveState = Convert.ToInt32(SampleSubmissionDetail.ApproveState),
            //                                  DetailText = SampleSubmissionDetail.DetailText
            //                              };

            //var modelList = detailList.OrderBy(x => x.Id).ToList();

                return Json(listDistinct, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SetSalesBudgetForUpdate(List<SalesBudgetModel> lstSampleSubmissionMaster, List<SalesBudgetDetailViewModel> lstSampleSubmissionDetails, string UpdateId)
        {
            var strMessage = string.Empty;

            // Clear detail list
            //Session["lstSampleRequestDetails"] = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Int64 Id = Convert.ToInt64(UpdateId);
                    var model = _SalesBudgetService.BMSUnit.SalesBudgetRepository.GetByID(Id);

                    //var lst = _SalesBudgetService.BMSUnit.SalesBudgetRepository.GetAll().Where(
                    //                q => q.Id == model.Id);

                    //foreach (var dt in lst)
                    //{
                    //    _SalesBudgetService.BMSUnit.SalesBudgetRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                    //}

                    List<SalesBudgetModel> masterList = new List<SalesBudgetModel>();

                    foreach (var item in lstSampleSubmissionMaster)
                    {
                        SalesBudgetModel masterEntityModel = new SalesBudgetModel();

                        #region Current User
                        var userName = HttpContext.User.Identity.Name;
                        masterEntityModel.IUser = model.IUser;
                        masterEntityModel.IDate = model.IDate;
                        masterEntityModel.EUser = userName;
                        masterEntityModel.EDate = DateTime.Now;
                        #endregion
                        masterEntityModel.SalesPersonId = item.SalesPersonId;
                        masterEntityModel.BudgetYear = item.BudgetYear;
                        masterEntityModel.ProductId = item.ProductId;
                        masterEntityModel.SBUId = item.SBUId;
                        masterEntityModel.Id = Id;
                        //masterList.Add(masterEntityModel);

                        var entity = masterEntityModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.Update(entity);
                        _SalesBudgetService.BMSUnit.SalesBudgetRepository.SaveChanges();

                        Session["SalesBudgetId"] = entity.Id;
                    }

                    var lst = _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.GetAll().Where(
                                    q => q.SalesBudgetId == Convert.ToInt64(Session["SalesBudgetId"]));

                    foreach (var dt in lst)
                    {
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                    }


                    // Add new detail information

                    foreach (var item in lstSampleSubmissionDetails)
                    {
                        SalesBudgetDetail entityDetailModel = new SalesBudgetDetail();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 1;
                        entityDetailModel.Quantity = item.JanQty;
                        entityDetailModel.Value =item.JanVal;
                        entityDetailModel.Commission = item.JanCom;

                        var detailEntityjan = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityjan);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 2;
                        entityDetailModel.Quantity = item.FebQty;
                        entityDetailModel.Value = item.FebVal;
                        entityDetailModel.Commission = item.FebCom;

                        var detailEntityFeb = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityFeb);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 3;
                        entityDetailModel.Quantity = item.MarQty;
                        entityDetailModel.Value = item.MarVal;
                        entityDetailModel.Commission = item.MarCom;

                        var detailEntityMarch = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityMarch);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 4;
                        entityDetailModel.Quantity = item.AprQty;
                        entityDetailModel.Value = item.AprVal;
                        entityDetailModel.Commission = item.AprCom;

                        var detailEntityApril = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityApril);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 5;
                        entityDetailModel.Quantity = item.MayQty;
                        entityDetailModel.Value = item.MayVal;
                        entityDetailModel.Commission = item.MayCom;

                        var detailEntityMay = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityMay);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 6;
                        entityDetailModel.Quantity = item.JunQty;
                        entityDetailModel.Value = item.JunVal;
                        entityDetailModel.Commission = item.JunCom;

                        var detailEntityJun = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityJun);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 7;
                        entityDetailModel.Quantity = item.JulyQty;
                        entityDetailModel.Value = item.JulyVal;
                        entityDetailModel.Commission = item.JulyCom;

                        var detailEntityJuly = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityJuly);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 8;
                        entityDetailModel.Quantity = item.AugQty;
                        entityDetailModel.Value = item.AugVal;
                        entityDetailModel.Commission = item.AugCom;

                        var detailEntityAugest = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityAugest);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 9;
                        entityDetailModel.Quantity = item.SepQty;
                        entityDetailModel.Value = item.SepVal;
                        entityDetailModel.Commission = item.SepVal;

                        var detailEntitySeptember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntitySeptember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 10;
                        entityDetailModel.Quantity = item.OctQty;
                        entityDetailModel.Value = item.OctVal;
                        entityDetailModel.Commission = item.OctCom;

                        var detailEntityOctober = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityOctober);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 11;
                        entityDetailModel.Quantity = item.NovQty;
                        entityDetailModel.Value = item.NovVal;
                        entityDetailModel.Commission = item.NovCom;

                        var detailEntityNovember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityNovember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();

                        entityDetailModel.SalesBudgetId = Convert.ToInt64(Convert.ToInt64(Session["SalesBudgetId"]));
                        entityDetailModel.CustomerId = item.CustomerId;
                        entityDetailModel.SupplierId = item.SupplierId;
                        entityDetailModel.BudgetMonth = 12;
                        entityDetailModel.Quantity = item.DecQty;
                        entityDetailModel.Value = item.DecVal;
                        entityDetailModel.Commission = item.DecCom;

                        var detailEntityDecember = entityDetailModel.ToEntity();
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.Add(detailEntityDecember);
                        _SalesBudgetService.BMSUnit.SalesBudgetDetailRepository.SaveChanges();


                        //list.Add(entityDetailModel);
                    }
                }

                //Session["lstSampleRequestDetails"] = list;
                //return Json(_SampleRequestModel, JsonRequestBehavior.AllowGet);
                //strMessage = Boolean.TrueString;

                strMessage = "Information has been Updated successfully";
            }
            catch (Exception ex)
            {
                strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
            }

            return Json(new { strMessage = strMessage });
        }

    }
}
