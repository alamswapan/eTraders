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
    public class InvoiceController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
        public readonly BMSCommonSevice _InvoiceService;
        #endregion
        
        #region Constructor
        public InvoiceController(BMSCommonSevice InvoiceService)
            {
                this._InvoiceService = InvoiceService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //Invoice Read
            public JsonResult InvoiceRead()
            {
                var mmodels = GetInvoiceList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: Invoice/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _InvoiceService.BMSUnit.InvoiceRepository.GetByID(id);
                    if (model != null)
                    {
                        InvoiceModel viewModel = new InvoiceModel
                        {
                            Id = model.Id,
                            InvoiceNo = model.InvoiceNo,
                            InvoiceDate = model.InvoiceDate,
                            //InvoiceType = _invInvoiceService.BMSUnit.InvoiceSubCategoryRepository.GetByID(model.SubCategoryId).SubCategoryName,
                            Organization = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).CustomerName,
                            //Organization = _InvoiceService.BMSUnit.INV_tblOrganizationInfoRepository.GetByID(model.OrganizationId).Name,
                            CustomerName = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).CustomerName,
                            NET = model.NET,
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
        
            // GET: Invoice/SearchArticle
            public ActionResult SearchArticle()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {

                    var productList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.ProductRepository.GetAll().Where(x=> x.IsActive) .ToList<tblProduct>(), "Id", "ArticleName").ToList();

                    var model = new InvoiceDetailModel()
                    {
                        ddlProduct = productList
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
                        //_invInvoiceService.BMSUnit.InvoiceRepository.Add(entity);
                        //_invInvoiceService.BMSUnit.InvoiceRepository.SaveChanges();

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

            // GET: Invoice/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var customerList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();

                    var currencyList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CurrencyRepository.GetAll().ToList<tblCurrency>(), "Id", "CurrencyName").ToList();
                    var priceTermList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.PriceTermRepository.GetAll().ToList<tblPriceTerm>(), "Id", "PriceTermName").ToList();
                    var commTypeList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CommCalculationTypeRepository.GetAll().ToList<tblCommCalculationType>(), "Id", "CommissionCalculationTerm").ToList();
                    var shipmentCountryList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CountryRepository.GetAll().ToList<tblCountry>(), "Id", "CountryName").ToList();
                    var portOfDischargeList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.PortRepository.GetAll().ToList<tblPort>(), "Id", "PortName").ToList();

                    var invoiceTypeList = Common.PopulateInvoiceTypeDDList();

                    var userOrganization = 3; // Query and Set Defaul Organization Here
                    var defaultInvoiceType = Common.InvoiceType.PI;
                    var model = new InvoiceModel()
                    {
                        ddlCustomer = customerList,
                        InvoiceDate=DateTime.Now,
                        InvoicePeriod=201605,
                        ddlOrganization = organizationList,
                        OrganizationId = Convert.ToInt64(userOrganization),
                        ddlInvoiceType = invoiceTypeList,
                        InvoiceTypeId= Convert.ToInt32(defaultInvoiceType),
                        ddlPriceTerm = priceTermList,
                        ddlCurrency=currencyList,
                        ddlShipmentCountry = shipmentCountryList,
                        ddlPortOfDischarge = portOfDischargeList,
                        ddlCommissionTerm=commTypeList
                        //OrganizationId = Convert.ToInt64(userOrganization)
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
            public ActionResult Create(InvoiceModel viewModel)
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

                        #region Releated Data
                        viewModel.InvoiceNo = "123";
                        //viewModel.testVal1 = _InvoiceService.Get_New_InvoiceId().FirstOrDefault().testVal1;
                        viewModel.InvoicePeriod = 201609;
                        viewModel.InvoiceType = "C";
                        viewModel.Remarks = "Remarks Field Has not found";
                        var PSavedGRlist = _InvoiceService.GenerateCustomInvoiceId().ToList();
                        viewModel.InvoiceNo = PSavedGRlist.FirstOrDefault().InvoiceId;
                        #endregion

                        List<InvoiceDetailModel> list = new List<InvoiceDetailModel>();
                        if (Session["lstInvoiceDetails"] != null)
                        {
                            list = (List<InvoiceDetailModel>)Session["lstInvoiceDetails"];
                        }

                        var entity = viewModel.ToEntity();

                        _InvoiceService.BMSUnit.InvoiceRepository.Add(entity);
                        _InvoiceService.BMSUnit.InvoiceRepository.SaveChanges();

                        Session["InvoiceMasterId"] = entity.Id;
                        
                        #region Save Invoice Detail
                        
                        if (list != null && list.Count > 0)
                        {
                            //foreach (InvoiceDetailViewModel detail in list)
                          foreach (InvoiceDetailModel detail in list)
                            {
                                detail.InvoiceId = Convert.ToInt32(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _InvoiceService.BMSUnit.InvoiceDetailsRepository.Add(detailEntity);
                                _InvoiceService.BMSUnit.InvoiceDetailsRepository.SaveChanges();
                            }
                        }

                        Session["lstInvoiceDetails"] = null;
                         
                        #endregion
                        return Content(Boolean.TrueString);
                        
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
                    var model = _InvoiceService.BMSUnit.InvoiceRepository.GetByID(id);

                    Session["InvoiceId"] = model.Id;

                    var customerList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CustomerRepository.GetAll().ToList<tblCustomer>(), "Id", "CustomerName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    var currencyList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CurrencyRepository.GetAll().ToList<tblCurrency>(), "Id", "CurrencyName").ToList();
                    var priceTermList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.PriceTermRepository.GetAll().ToList<tblPriceTerm>(), "Id", "PriceTermName").ToList();
                    var commTypeList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CommCalculationTypeRepository.GetAll().ToList<tblCommCalculationType>(), "Id", "CommissionCalculationTerm").ToList();
                    var shipmentCountryList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.CountryRepository.GetAll().ToList<tblCountry>(), "Id", "CountryName").ToList();
                    var portOfDischargeList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.PortRepository.GetAll().ToList<tblPort>(), "Id", "PortName").ToList();
                    var invoiceTypeList = Common.PopulateInvoiceTypeDDList();
                    
                    if (model != null)
                    {
                        InvoiceModel viewModel = new InvoiceModel
                        {
                            Id = model.Id,
                            InvoiceNo = model.InvoiceNo,
                            InvoiceDate = model.InvoiceDate,
                            ddlInvoiceType = invoiceTypeList,
                            InvoiceType = model.InvoiceType,
                            //InvoiceType = _InvoiceService.BMSUnit.Inv.GetByID(model.OrganizationId).Name,
                            InvoicePeriod=model.InvoicePeriod,

                            ddlOrganization = organizationList,
                            OrganizationId = Convert.ToInt64(model.OrganizationId),
                                //Organization = _InvoiceService.BMSUnit.OrganizationRepository.GetByID(model.OrganizationId).Organization,
                            ddlCustomer = customerList,
                            CustomerId=model.CustomerId,
                            CustomerName = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).CustomerName,
                            CustomerAddress = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).Address,
                            Phone = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).Phone,
                            Email= _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).Email,

                            TIN = _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).TIN,
                            BIN= _InvoiceService.BMSUnit.CustomerRepository.GetByID(model.CustomerId).BIN,

                            ddlPriceTerm = priceTermList,
                            PriceTermId= Convert.ToInt64(model.PriceTermId),
                            ddlCurrency=currencyList,
                            CurrencyId= Convert.ToInt64(model.CurrencyId),
                            ddlShipmentCountry = shipmentCountryList,
                            ShipmentFromCountryId= Convert.ToInt64(model.ShipmentFromCountryId),
                            ddlPortOfDischarge = portOfDischargeList,
                            PortOfDischargeId= Convert.ToInt64(model.PortOfDischargeId),

                            LCNo=model.LCNo,
                            LCDate=model.LCDate,
                           // LDS=model.LDS,
                            LDE=model.LDE,
                            ShipmentDate=model.ShipmentDate,
                            ArrivalDate=model.ArrivalDate,
                            CommercialInvoiceNo=model.CommercialInvoiceNo,
                            CommercialInvoiceDate=model.CommercialInvoiceDate,
                            BoLNo=model.BoLNo,
                            ddlCommissionTerm=commTypeList,
                            CommCalcTermId = Convert.ToInt64(model.CommCalcTermId),
                            Total=model.Total,
                            OtherCharges=model.OtherCharges,
                            CommissionPer=model.CommissionPercent,
                            Commission = model.Commission,
                            NET = model.NET
                            
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
            public ActionResult Edit(InvoiceModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _InvoiceService.BMSUnit.InvoiceRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _InvoiceService.BMSUnit.InvoiceRepository.Update(entity);
                        _InvoiceService.BMSUnit.InvoiceRepository.SaveChanges();

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
                    var model = _InvoiceService.BMSUnit.InvoiceRepository.GetByID(id);

                    if (model != null)
                    {
                        InvoiceModel viewModel = new InvoiceModel
                        {
                            Id = model.Id,
                            InvoiceNo = model.InvoiceNo,
                            InvoicePeriod = model.InvoicePeriod,
                            InvoiceDate = model.InvoiceDate,
                            //VAT = model.VAT,
                            Commission = model.Commission,
                            NET = model.NET
                            //ReceiveAmount = model.ReceiveAmount
                            //,IsActive = Convert.ToBoolean(model.IsActive)
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
                    var model = _InvoiceService.BMSUnit.InvoiceRepository.GetByID(id);

                    if (model != null)
                    {
                        //temporarily Blocked due
                        _InvoiceService.BMSUnit.InvoiceRepository.Delete_64Bit(model.Id);
                        _InvoiceService.BMSUnit.InvoiceRepository.SaveChanges();

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
            private String Get_New_InvoiceId()
            {
                //var maxId = _invInvoiceService.BMSUnit.InvoiceRepository.GetAll().Max(x => x.InvoiceNo);

                var query1 = @"SELECT RIGHT(YEAR(GETDATE()),2) + REPLICATE('0',6-LEN(ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1))
                                    +ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1
                              FROM [tblInvoice]
                              WHERE LEFT(InvoiceNo,2)=RIGHT(YEAR(GETDATE()),2)";

                var query = @"SELECT top 1 InvoiceNo FROM tblInvoice";

                var maxId1 = _InvoiceService.BMSUnit.InvoiceRepository.GetWithRawSql(query);

                return maxId1.ToString();
                //var user = _invInvoiceService.BMSUnit.InvoiceRepository.GetAll(
            }
            private List<InvoiceModel> GetInvoiceList()
            {
                AppConstant permission = new AppConstant();

                var InvoiceViewModels = _InvoiceService.BMSUnit.InvoiceRepository.GetAll().ToList().Select(
                    md => new InvoiceModel
                    {
                        Id = Convert.ToInt64(md.Id),
                        InvoiceNo = md.InvoiceNo,
                        InvoiceDate = md.InvoiceDate,
                        Organization = _InvoiceService.BMSUnit.OrganizationRepository.GetByID(md.OrganizationId).Organization,
                        CustomerName = _InvoiceService.BMSUnit.CustomerRepository.GetByID(md.CustomerId).CustomerName,
                        NET = md.NET,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "Invoice", md.Id.ToString(), true, true, true)
                    }).OrderBy(o => o.InvoicePeriod).ThenBy(ot => ot.InvoiceDate);

                return InvoiceViewModels.ToList();
            }

            public JsonResult GetInvoiceDetails()
            {
                long invoiceId = Convert.ToInt64(Session["InvoiceId"]);

                var invoiceDetailsModel = from invoiceDetail in _InvoiceService.BMSUnit.InvoiceDetailsRepository.GetAll().Where(q => q.InvoiceId == invoiceId)
                        select new InvoiceDetailModel()
                            {
                                Id = Convert.ToInt64(invoiceDetail.Id),
                                ProductId = invoiceDetail.ProductId,
                                ProductName = _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == invoiceDetail.ProductId).FirstOrDefault().ProductName,
                                GenericName = _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == invoiceDetail.ProductId).FirstOrDefault().GenericName,
                                HSCode = _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == invoiceDetail.ProductId).FirstOrDefault().HSCode,
                                Origin= _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == invoiceDetail.ProductId).FirstOrDefault().Origin,
                                SellingUnit = _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.Id == invoiceDetail.ProductId).FirstOrDefault().SellingUnit,
                                Quantity = invoiceDetail.Quantity,
                                UnitPrice = invoiceDetail.UnitPrice,
                                SubTotal= invoiceDetail.SubTotal
                            };

                var modelList = invoiceDetailsModel.OrderBy(x => x.Id).ToList();

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }
            //For Autocomplete
            public JsonResult PopulateProductAutoComplete()
            {

                var GetArticleList = _InvoiceService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductCode = MI.ProductName
                    });

                return Json(GetArticleList, JsonRequestBehavior.AllowGet);
            }

            public JsonResult AddProductToGrid(string product)
            {

                var GridRowItem = _InvoiceService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == product).ToList().Select(
                    MI => new ProductModel
                    {
                        Id = MI.Id,
                        ProductCode = MI.ProductCode,
                        ProductName = MI.ProductName,
                        GenericName=MI.GenericName,
                        HSCode=MI.HSCode,
                        Origin=MI.Origin,
                        SellingUnit = MI.SellingUnit,
                        UnitPrice = MI.UnitPrice
                    });


                
                //var modelListDistinct = GetSalesModelJan.GroupBy(test => test.ProductCode)
                //                       .Select(grp => grp.First());

                return Json(GridRowItem, JsonRequestBehavior.AllowGet);

            }

            public JsonResult GetCustomerDetails(long id)
            {
                var jresult = _InvoiceService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == id).ToList().Select(
                   CI => new CustomerInfoModel
                   {
                       CustomerName = CI.CustomerName,
                       Address=CI.Address,
                       Phone=CI.Phone,
                       Email=CI.Email,
                       TIN=CI.TIN,
                       BIN=CI.BIN
                   });

                return Json(jresult, JsonRequestBehavior.AllowGet);
            }

            // For saving detail data
            public JsonResult SetInvoiceDetailsListForSave_org(List<InvoiceDetailViewModel> lstInvoiceDetails)
            {
                var strMessage = string.Empty;

                // Clear detail list
                Session["lstInvoiceDetails"] = null;

                try
                {

                    List<InvoiceDetailViewModel> list = new List<InvoiceDetailViewModel>();
                    var invoiceModel = new InvoiceModel();

                    // Add new detail information
                    foreach (var item in lstInvoiceDetails)
                    {
                        InvoiceDetailViewModel entityModel = new InvoiceDetailViewModel();

                        entityModel.InvoiceId = item.InvoiceId;
                        entityModel.ProductId = item.ProductId;
                        entityModel.UnitPrice = item.UnitPrice;
                        //entityModel.SalesQTY = item.SalesQTY;
                        //entityModel.TP = entityModel.SalesTP * item.SalesQTY;
                        list.Add(entityModel);

                    }

                    Session["lstInvoiceDetails"] = list;
                    return Json(invoiceModel, JsonRequestBehavior.AllowGet); 
                    //strMessage = Boolean.TrueString;
                }
                catch (Exception ex)
                {
                    strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                }

                return Json(new { strMessage = strMessage });
            }

            public JsonResult SetInvoiceDetailsListForSave(List<InvoiceDetailViewModel> lstInvoiceDetails)
            {
                var strMessage = string.Empty;

                // Clear detail list
                Session["lstInvoiceDetails"] = null;

                try
                {

                    List<InvoiceDetailModel> list = new List<InvoiceDetailModel>();
                    var invoiceModel = new InvoiceModel();

                    // Add new detail information
                    foreach (var item in lstInvoiceDetails)
                    {
                        InvoiceDetailModel entityModel = new InvoiceDetailModel();

                        entityModel.InvoiceId = item.InvoiceId;
                        entityModel.ProductId = item.ProductId;
                        entityModel.UnitPrice = Convert.ToDecimal(item.UnitPrice);
                        entityModel.Quantity = Convert.ToDecimal(item.Quantity);
                        entityModel.SubTotal = entityModel.UnitPrice * Convert.ToDecimal(item.Quantity);
                        list.Add(entityModel);

                    }

                    Session["lstInvoiceDetails"] = list;
                    return Json(invoiceModel, JsonRequestBehavior.AllowGet);
                    //strMessage = Boolean.TrueString;
                }
                catch (Exception ex)
                {
                    strMessage = CommonExceptionMessage.GetExceptionMessage(ex, CommonAction.Save);
                }

                return Json(new { strMessage = strMessage });
            }

        #endregion


            #region RnD
            public ActionResult SearchArticle_Test(string query)
            {
                query = query.Replace(" ", "");
                if (query.Length > 1)
                {
                    int op = query.LastIndexOf(",");
                    query = query.Substring(op + 1);
                }
                var users = (from u in _InvoiceService.BMSUnit.ProductRepository.GetAll()
                             where u.ProductName.Contains(query)
                             orderby u.ProductName// optional
                             select u.ProductName).Distinct().ToArray();

                return Json(users, JsonRequestBehavior.AllowGet);
            }

            [HttpGet]
            public JsonResult GetVendors(string query)
            {
                if (query == null)
                { query = "x"; }
                var returnerX = (from a in _InvoiceService.BMSUnit.ProductRepository.GetAll()
                                 where a.ProductName.Contains(query)
                                 select new { id = a.ProductName, label = a.ProductName.Trim(), value = a.ProductName.Trim() })
                       .Take(20);

                var returner = (from u in _InvoiceService.BMSUnit.ProductRepository.GetAll()
                                where u.ProductName.Contains(query)
                                orderby u.ProductName // optional
                                select u.ProductName).Distinct().ToArray();


                return Json(returnerX, JsonRequestBehavior.AllowGet);
            }

            private List<ProductModel> GetProduct()
        {
            var productList = SelectListItemExtension.PopulateDropdownList(_InvoiceService.BMSUnit.ProductRepository.GetAll().Where(x => x.IsActive).ToList<tblProduct>(), "Id", "ArticleName").ToList().Select(
            md => new ProductModel
            {
                ProductName = md.Text
            });

            return productList.ToList();
        }
            #endregion


    }
}
   