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
    public class DebitNoteController : Controller
    {
        //
        // GET: /SD/Organization/

        #region Fields
        public readonly BMSCommonSevice _DebitNoteService;
        #endregion
        
        #region Constructor
        public DebitNoteController(BMSCommonSevice DebitNoteService)
            {
                this._DebitNoteService = DebitNoteService;            
            }
        #endregion

        #region Action

            public ActionResult Index()
            {            
                return View();
            }

            //DebitNote Read
            public JsonResult DebitNoteRead()
            {
                var mmodels = GetDebitNoteList();
                return Json(mmodels, JsonRequestBehavior.AllowGet);
            }

            // GET: DebitNote/Details/By ID
            public ActionResult Details(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _DebitNoteService.BMSUnit.DebitNoteRepository.GetByID(id);

                    var supplieerList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(), "Id", "SupplierName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    var bankList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.BankRepository.GetAll().ToList<tblBank>(), "Id", "BankName").ToList();


                    if (model != null)
                    {
                        DebitNoteModel viewModel = new DebitNoteModel
                        {
                            Id = model.Id,
                            DebitNoteNo = model.DebitNoteNo,
                            DebitNoteDate = model.DebitNoteDate,

                            ddlSupplier = supplieerList,
                            SupplierId = model.SupplierId,

                            ddlOrganization = organizationList,
                            OrganizationId = Convert.ToInt64(model.OrganizationId),

                            ddlBank = bankList,
                            BankId = model.BankId,

                            SupplierName = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).SupplierName,
                            SupplierAddress = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Address,
                            AttentionPerson = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Phone
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
        
            // GET: DebitNote/Create
            public ActionResult Create()
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var supplieerList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(), "Id", "SupplierName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();

                    var bankList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.BankRepository.GetAll().ToList<tblBank>(), "Id", "BankName").ToList();

                    var userOrganization = 3; // Query and Set Defaul Organization Here
                    var model = new DebitNoteModel()
                    {
                        ddlSupplier = supplieerList,
                        DebitNoteDate=DateTime.Now,
                        ddlOrganization = organizationList,
                        OrganizationId = Convert.ToInt64(userOrganization),
                        ddlBank = bankList,
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

            // POST: DebitNote/Create
            [HttpPost]
            public ActionResult Create(DebitNoteModel viewModel)
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
                        //viewModel.DebitNoteNo = "123";
                        //var PSavedGRlist = _DebitNoteService.GenerateCustomDebitNoteId().ToList();
                        //viewModel.DebitNoteNo = PSavedGRlist.FirstOrDefault().DebitNoteId;
                        #endregion

                        List<DebitNoteDetailModel> list = new List<DebitNoteDetailModel>();
                        if (Session["lstDebitNoteDetails"] != null)
                        {
                            list = (List<DebitNoteDetailModel>)Session["lstDebitNoteDetails"];
                        }

                        var entity = viewModel.ToEntity();

                        _DebitNoteService.BMSUnit.DebitNoteRepository.Add(entity);
                        _DebitNoteService.BMSUnit.DebitNoteRepository.SaveChanges();

                        Session["DebitNoteMasterId"] = entity.Id;
                        
                        #region Save DebitNote Detail
                        
                        if (list != null && list.Count > 0)
                        {
                            //foreach (DebitNoteDetailViewModel detail in list)
                          foreach (DebitNoteDetailModel detail in list)
                            {
                                detail.DebitNoteId = Convert.ToInt32(entity.Id);
                                var detailEntity = detail.ToEntity();
                                _DebitNoteService.BMSUnit.DebitNoteDetailRepository.Add(detailEntity);
                                _DebitNoteService.BMSUnit.DebitNoteDetailRepository.SaveChanges();
                            }
                        }

                        Session["lstDebitNoteDetails"] = null;
                         
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
            
            // GET: DebitNote/Edit/By ID
            public ActionResult Edit(long id)
            {
                var errorViewModel = new ErrorViewModel();

                try
                {
                    var model = _DebitNoteService.BMSUnit.DebitNoteRepository.GetByID(id);

                    Session["DebitNoteId"] = model.Id;

                    var supplieerList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.SupplierRepository.GetAll().ToList<tblSupplier>(), "Id", "SupplierName").ToList();
                    var organizationList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.OrganizationRepository.GetAll().ToList<tblOrganization>(), "Id", "Organization").ToList();
                    var bankList = SelectListItemExtension.PopulateDropdownList(_DebitNoteService.BMSUnit.BankRepository.GetAll().ToList<tblBank>(), "Id", "BankName").ToList();
                    
                    if (model != null)
                    {
                        DebitNoteModel viewModel = new DebitNoteModel
                        {
                            Id = model.Id,
                            DebitNoteNo = model.DebitNoteNo,
                            DebitNoteDate = model.DebitNoteDate,

                            ddlSupplier = supplieerList,
                            SupplierId=model.SupplierId,

                            ddlOrganization = organizationList,
                            OrganizationId = Convert.ToInt64(model.OrganizationId),
                            
                            ddlBank = bankList,
                            BankId=model.BankId,

                            SupplierName = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).SupplierName,
                            SupplierAddress = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Address,
                            AttentionPerson = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(model.SupplierId).Phone
                            //LCNo=model.LCNo,
                            //Total= model.tot`al
                            
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

            // POST: DebitNote/Edit/By ID
            [HttpPost]
            public ActionResult Edit(DebitNoteModel viewModel)
            {
                var strMessage = string.Empty;
                try
                {
                    if (ModelState.IsValid)
                    {
                        var model = _DebitNoteService.BMSUnit.DebitNoteRepository.GetByID(viewModel.Id);

                        #region Current User

                        viewModel.IUser = model.IUser;
                        viewModel.IDate = model.IDate;

                        var userName = HttpContext.User.Identity.Name;
                        viewModel.EUser = userName;
                        viewModel.EDate = DateTime.Now;

                        #endregion

                        var entity = viewModel.ToEntity();

                        _DebitNoteService.BMSUnit.DebitNoteRepository.Update(entity);
                        _DebitNoteService.BMSUnit.DebitNoteRepository.SaveChanges();

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
        #endregion

        #region Method
            private String Get_New_DebitNoteId()
            {
                //var maxId = _invDebitNoteService.BMSUnit.DebitNoteRepository.GetAll().Max(x => x.DebitNoteNo);

                var query1 = @"SELECT RIGHT(YEAR(GETDATE()),2) + REPLICATE('0',6-LEN(ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1))
                                    +ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1
                              FROM [tblInvoice]
                              WHERE LEFT(InvoiceNo,2)=RIGHT(YEAR(GETDATE()),2)";

                var query = @"SELECT top 1 InvoiceNo FROM tblInvoice";

                var maxId1 = _DebitNoteService.BMSUnit.DebitNoteRepository.GetWithRawSql(query);

                return maxId1.ToString();
                //var user = _invDebitNoteService.BMSUnit.DebitNoteRepository.GetAll(
            }


            private List<DebitNoteModel> GetDebitNoteList()
            {
                AppConstant permission = new AppConstant();

                var DebitNoteViewModels = _DebitNoteService.BMSUnit.DebitNoteRepository.GetAll().ToList().Select(
                    md => new DebitNoteModel
                    {
                        Id = Convert.ToInt64(md.Id),
                        DebitNoteNo = md.DebitNoteNo,
                        DebitNoteDate = md.DebitNoteDate,
                        Organization = _DebitNoteService.BMSUnit.OrganizationRepository.GetByID(md.OrganizationId).Organization,
                        SupplierName = _DebitNoteService.BMSUnit.SupplierRepository.GetByID(md.SupplierId).SupplierName,
                        ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "DebitNote", md.Id.ToString(), true, true, true)
                    }).OrderBy(o => o.DebitNoteNo).ThenBy(ot => ot.DebitNoteDate);

                return DebitNoteViewModels.ToList();
            }

            public JsonResult GetDebitNoteDetails()
            {
                long invoiceId = Convert.ToInt64(Session["DebitNoteId"]);
                //.Where(q => q.DebitNoteId == invoiceId)
                var invoiceDetailsModel = from debitNoteDetail in _DebitNoteService.BMSUnit.InvoiceRepository.GetAll()
                        select new DebitNoteDetailModel()
                            {
                                //Id = Convert.ToInt64(debitNoteDetail.Id),
                                InvoiceId = debitNoteDetail.Id,
                                InvoiceDate=debitNoteDetail.InvoiceDate,
                                CommercialInvoiceNo=debitNoteDetail.CommercialInvoiceNo,
                                CommercialInvoiceDate=debitNoteDetail.CommercialInvoiceDate,
                                CommissionCalculationTerm = _DebitNoteService.BMSUnit.CommCalculationTypeRepository.GetAll().Where(c => c.Id == debitNoteDetail.CommCalcTermId).FirstOrDefault().CommissionCalculationTerm,
                                CommissionPer=debitNoteDetail.CommissionPercent,
                                Commission=debitNoteDetail.Commission,
                                CustomerName = _DebitNoteService.BMSUnit.CustomerRepository.GetAll().Where(c => c.Id == debitNoteDetail.CustomerId).FirstOrDefault().CustomerName,
                                Quantity = _DebitNoteService.BMSUnit.InvoiceDetailsRepository.GetAll().Where(c => c.InvoiceId == debitNoteDetail.Id).FirstOrDefault().Quantity
                                //SubTotal = invoiceDetail.SubTotal
                            };

                var modelList = invoiceDetailsModel.OrderBy(x => x.Id).ToList();

                return Json(modelList, JsonRequestBehavior.AllowGet);

            }
            //For Autocomplete
            public JsonResult PopulateProductAutoComplete()
            {

                var GetArticleList = _DebitNoteService.BMSUnit.ProductRepository.GetAll().ToList().Select(
                    MI => new ProductModel
                    {
                        ProductCode = MI.ProductName
                    });

                return Json(GetArticleList, JsonRequestBehavior.AllowGet);
            }

            public JsonResult AddProductToGrid(string product)
            {

                var GridRowItem = _DebitNoteService.BMSUnit.ProductRepository.GetAll().Where(c => c.ProductName == product).ToList().Select(
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

            public JsonResult GetSupplierDetails(long id)
            {
                var jresult = _DebitNoteService.BMSUnit.SupplierRepository.GetAll().Where(c => c.Id == id).ToList().Select(
                   CI => new SupplierInfoModel
                   {
                       SupplierName = CI.SupplierName,
                       Address=CI.Address,
                   });

                return Json(jresult, JsonRequestBehavior.AllowGet);
            }


            public JsonResult SetDebitNoteDetailsListForSave(List<DebitNoteDetailViewModel> lstDebitNoteDetails)
            {
                var strMessage = string.Empty;

                // Clear detail list
                Session["lstDebitNoteDetails"] = null;

                try
                {

                    List<DebitNoteDetailModel> list = new List<DebitNoteDetailModel>();
                    var invoiceModel = new DebitNoteModel();

                    // Add new detail information
                    foreach (var item in lstDebitNoteDetails)
                    {
                        DebitNoteDetailModel entityModel = new DebitNoteDetailModel();

                        //entityModel.DebitNoteId = item.DebitNoteId;
                        //entityModel.ProductId = item.ProductId;
                        //entityModel.UnitPrice = Convert.ToDecimal(item.UnitPrice);
                        //entityModel.Quantity = Convert.ToDecimal(item.Quantity);
                        //entityModel.SubTotal = entityModel.UnitPrice * Convert.ToDecimal(item.Quantity);
                        list.Add(entityModel);

                    }

                    Session["lstDebitNoteDetails"] = list;
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
        #endregion


    }
}
   