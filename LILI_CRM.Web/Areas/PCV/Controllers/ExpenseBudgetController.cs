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
    public class ExpenseBudgetController : Controller
    {
        //
        // GET: /PCV/SalesBudget/
        #region Fields
        public readonly BMSCommonSevice _ExpenseBudgetService;
        #endregion
        
        #region Constructor
        public ExpenseBudgetController(BMSCommonSevice ExpenseBudgetService)
            {
                this._ExpenseBudgetService = ExpenseBudgetService;            
            }
        #endregion

        #region Action
        public ActionResult Index()
        {
            return View();
        }
        //ExpenseBudget Read
        public JsonResult ExpenseBudgetRead()
        {
            var mmodels = GetExpenseBudgetReadList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        //ExpenseBudget Create
        public ActionResult Create()
        {
            var errorViewModel = new ErrorViewModel();

            try
            {
                var SalesPersonName = HttpContext.User.Identity.Name;
                var SalesPersonId = _ExpenseBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.UserName == SalesPersonName).FirstOrDefault().Id;
                var divisionList = SelectListItemExtension.PopulateDropdownList(_ExpenseBudgetService.BMSUnit.DivisionRepository.GetAll().ToList<tblDivision>(),"Id","DivisionName").ToList();
                var sbuList = SelectListItemExtension.PopulateDropdownList(_ExpenseBudgetService.BMSUnit.SBURepository.GetAll().ToList<tblSBU>(), "Id", "SBUName").ToList();

                var model = new ExpenseBudgetModel()
                {
                    BudgetYear = DateTime.Now.Year.ToString(),
                    SalesPersonId = Convert.ToInt64(SalesPersonId),
                    SalesPersonName = SalesPersonName,
                    ddlDivision =  divisionList,
                    ddlSBU = sbuList
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

                        _ExpenseBudgetService.BMSUnit.SalesBudgetRepository.Add(entity);
                        _ExpenseBudgetService.BMSUnit.SalesBudgetRepository.SaveChanges();

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

                var model = _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.GetByID(id);
                 

                Session["ExpenseBudgetMasterId"] = model.Id;
                var SalesPersonName = HttpContext.User.Identity.Name; 
                var SalesPersonId = _ExpenseBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.UserName == SalesPersonName).FirstOrDefault().Id;
                var divisionList = SelectListItemExtension.PopulateDropdownList(_ExpenseBudgetService.BMSUnit.DivisionRepository.GetAll().ToList<tblDivision>(), "Id", "DivisionName").ToList();
                var sbuList = SelectListItemExtension.PopulateDropdownList(_ExpenseBudgetService.BMSUnit.SBURepository.GetAll().ToList<tblSBU>(), "Id", "SBUName").ToList();


                if (model != null)
                {
                    ExpenseBudgetModel viewModel = new ExpenseBudgetModel
                    {
                        Id = Convert.ToInt64(model.Id),
                        SalesPersonId = Convert.ToInt64(model.SalesPersonId),
                        SalesPersonName = SalesPersonName,
                        BudgetYear = model.BudgetYear,
                        SBUId = Convert.ToInt64(model.SBUId),
                        DivisionId = Convert.ToInt64(model.DivisionId),
                        ddlSBU = sbuList,
                        ddlDivision = divisionList
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


        private List<SalesBudgetModel> GetExpenseBudgetReadList()
        {
            //(_SalesBudgetService.BMSUnit.SBURepository.GetAll().Where(c=> Convert.ToInt64(c.Id) ==  Convert.ToInt64(md.SBUId)).FirstOrDefault().SBUName)
            AppConstant permission = new AppConstant();
  
            var _Models = _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.GetAll().ToList().Select(
                md => new SalesBudgetModel
                {
                    Id = Convert.ToInt64(md.Id),
                    BudgetYear = md.BudgetYear,
                    SalesPersonName = _ExpenseBudgetService.BMSUnit.UserInfoNameRepository.GetAll().Where(c => c.Id == md.SalesPersonId).FirstOrDefault().UserName,
                    SBUName = (Convert.ToInt64(md.SBUId) > 0) ? (_ExpenseBudgetService.BMSUnit.SBURepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.SBUId)).FirstOrDefault().SBUName) : "",
                    DivisionName = (Convert.ToInt64(md.DivisionId) > 0) ? (_ExpenseBudgetService.BMSUnit.DivisionRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == Convert.ToInt64(md.DivisionId)).FirstOrDefault().DivisionName) : "",
                    
                    ActionLink = Common.KendoUIGridActionLinkGenerate("PCV", "ExpenseBudget", md.Id.ToString(), false, true, false)
                }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

            return _Models.ToList();
        }



        public JsonResult ReadParticularList()
        {
            var mmodels = GetParticularList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<ParticularViewModel> GetParticularList()
        {
            AppConstant permission = new AppConstant();

            var particularModels = _ExpenseBudgetService.BMSUnit.ParticularRepository.GetAll().ToList().Select(
                md => new ParticularViewModel
                {
                    ParticularId = md.Id,
                    ParticularName = md.ParticularName
                }).OrderBy(o => o.ParticularId).ThenBy(ot => ot.ParticularId);

            return particularModels.ToList();
        }

        public JsonResult ReadSupplierList()
        {
            var mmodels = GetSupplierList();
            return Json(mmodels, JsonRequestBehavior.AllowGet);
        }

        private List<SupplierViewModel> GetSupplierList()
        {
            AppConstant permission = new AppConstant();

            var supplierModels = _ExpenseBudgetService.BMSUnit.SupplierRepository.GetAll().ToList().Select(
                md => new SupplierViewModel
                {
                    SupplierId = md.Id,
                    SupplierName = md.SupplierName
                }).OrderBy(o => o.SupplierName).ThenBy(ot => ot.SupplierId);

            return supplierModels.ToList();
        }

        public JsonResult SetExpenseBudgetDetailsListForSave(List<ExpenseBudgetModel> lstExpenseBudgetMaster, List<ExpenseBudgetDetailViewModel> lstExpenseBudgetDetails)
        {
            var strMessage = string.Empty;

            // Clear detail list
            //Session["lstSampleRequestDetails"] = null;

            try
            {
                if (ModelState.IsValid)
                {

                    //List<SalesBudgetModel> masterList = new List<SalesBudgetModel>();

                    foreach (var item in lstExpenseBudgetMaster)
                    {
                        ExpenseBudgetModel masterEntityModel = new ExpenseBudgetModel();

                        #region Current User
                        var userName = HttpContext.User.Identity.Name;
                        masterEntityModel.IUser = userName;
                        masterEntityModel.IDate = DateTime.Now;
                        masterEntityModel.EDate = DateTime.Now;
                        #endregion
                        masterEntityModel.SalesPersonId = item.SalesPersonId;
                        masterEntityModel.DivisionId = item.DivisionId;
                        masterEntityModel.SBUId = item.SBUId;
                        masterEntityModel.BudgetYear = item.BudgetYear;                        
                        
                        
                        //masterList.Add(masterEntityModel);

                        var entity = masterEntityModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.Add(entity);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.SaveChanges();

                        Session["ExpenseBudgetId"] = entity.Id;
                    }


                    // Add new detail information

                    foreach (var item in lstExpenseBudgetDetails)
                    {
                        ExpenseBudgetDetail entityDetailModel = new ExpenseBudgetDetail();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 1;
                        entityDetailModel.Value = item.JanVal;

                        var detailEntityjan = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityjan);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;                       
                        entityDetailModel.BudgetMonth = 2;                        
                        entityDetailModel.Value = item.FebVal;

                        var detailEntityFeb = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityFeb);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 3;
                        entityDetailModel.Value = item.MarVal;

                        var detailEntityMarch = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityMarch);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 4;
                        entityDetailModel.Value = item.AprVal;

                        var detailEntityApril = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityApril);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 5;
                        entityDetailModel.Value = item.MayVal;

                        var detailEntityMay = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityMay);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 6;
                        entityDetailModel.Value = item.JunVal;

                        var detailEntityJun = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityJun);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 7;
                        entityDetailModel.Value = item.JulyVal;

                        var detailEntityJuly = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityJuly);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 8;
                        entityDetailModel.Value = item.AugVal;

                        var detailEntityAugest = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityAugest);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 9;
                        entityDetailModel.Value = item.SepVal;

                        var detailEntitySeptember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntitySeptember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 10;
                        entityDetailModel.Value = item.OctVal;

                        var detailEntityOctober = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityOctober);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 11;
                        entityDetailModel.Value = item.NovVal;

                        var detailEntityNovember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityNovember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 12;
                        entityDetailModel.Value = item.DecVal;

                        var detailEntityDecember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityDecember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();


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

        public JsonResult GetExpenseBudgetDetails()
        {
            long SubmissionId = Convert.ToInt64(Session["ExpenseBudgetMasterId"]);
            var detailListAll = _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.GetAll().Where(x => x.ExpenseBudgetId == SubmissionId).ToList();
            var detailList = _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.GetAll().Where(x => x.ExpenseBudgetId == SubmissionId).ToList().Select(
                 md => new ExpenseBudgetDetailViewModel
                 {
                     Id = md.Id,
                     ParticularId = md.ParticularId,
                     ParticularName = _ExpenseBudgetService.BMSUnit.ParticularRepository.GetAll().Where(c => Convert.ToInt64(c.Id) == md.ParticularId).FirstOrDefault().ParticularName,
                     JanVal = detailListAll.Where(x => x.BudgetMonth == 1 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     FebVal = detailListAll.Where(x => x.BudgetMonth == 2 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     MarVal = detailListAll.Where(x => x.BudgetMonth == 3 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     AprVal = detailListAll.Where(x => x.BudgetMonth == 4 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     MayVal = detailListAll.Where(x => x.BudgetMonth == 5 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     JunVal = detailListAll.Where(x => x.BudgetMonth == 6 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     JulyVal = detailListAll.Where(x => x.BudgetMonth == 7 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     AugVal = detailListAll.Where(x => x.BudgetMonth == 8 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     SepVal = detailListAll.Where(x => x.BudgetMonth == 9 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     OctVal = detailListAll.Where(x => x.BudgetMonth == 10 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     NovVal = detailListAll.Where(x => x.BudgetMonth == 11 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value,
                     DecVal = detailListAll.Where(x => x.BudgetMonth == 12 && x.ParticularId == md.ParticularId && x.ExpenseBudgetId == SubmissionId).FirstOrDefault().Value
                 }).OrderBy(o => o.Id).ThenBy(ot => ot.Id);

                var listDistinct
                    = detailList.GroupBy(
                        i => i.ParticularId,
                        (key, group) => group.First()
                    ).ToList();



                return Json(listDistinct, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SetExpenseBudgetForUpdate(List<ExpenseBudgetModel> lstExpenseBudgetMaster, List<ExpenseBudgetDetailViewModel> lstExpenseBudgetDetails, string UpdateId)
        {
            var strMessage = string.Empty;

            // Clear detail list
            //Session["lstSampleRequestDetails"] = null;

            try
            {
                if (ModelState.IsValid)
                {
                    Int64 Id = Convert.ToInt64(UpdateId);
                    var model = _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.GetByID(Id);

                    

                    List<SalesBudgetModel> masterList = new List<SalesBudgetModel>();

                    foreach (var item in lstExpenseBudgetMaster)
                    {
                        ExpenseBudgetModel masterEntityModel = new ExpenseBudgetModel();

                        #region Current User
                        var userName = HttpContext.User.Identity.Name;
                        masterEntityModel.IUser = model.IUser;
                        masterEntityModel.IDate = model.IDate;
                        masterEntityModel.EUser = userName;
                        masterEntityModel.EDate = DateTime.Now;
                        #endregion
                        masterEntityModel.SalesPersonId = item.SalesPersonId;
                        masterEntityModel.DivisionId = item.DivisionId;
                        masterEntityModel.SBUId = item.SBUId;
                        masterEntityModel.BudgetYear = item.BudgetYear;
                        masterEntityModel.Id = Id;
                        //masterList.Add(masterEntityModel);

                        var entity = masterEntityModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.Update(entity);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetRepository.SaveChanges();

                        Session["ExpenseBudgetUpdateId"] = entity.Id;
                    }

                    var lst = _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.GetAll().Where(
                                    q => q.ExpenseBudgetId == Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));

                    foreach (var dt in lst)
                    {
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Delete_64Bit(Convert.ToInt64(dt.Id));
                    }


                    // Add new detail information

                    foreach (var item in lstExpenseBudgetDetails)
                    {
                        ExpenseBudgetDetail entityDetailModel = new ExpenseBudgetDetail();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 1;
                        entityDetailModel.Value = item.JanVal;

                        var detailEntityjan = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityjan);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 2;
                        entityDetailModel.Value = item.FebVal;

                        var detailEntityFeb = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityFeb);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 3;
                        entityDetailModel.Value = item.MarVal;

                        var detailEntityMarch = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityMarch);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 4;
                        entityDetailModel.Value = item.AprVal;

                        var detailEntityApril = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityApril);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 5;
                        entityDetailModel.Value = item.MayVal;

                        var detailEntityMay = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityMay);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 6;
                        entityDetailModel.Value = item.JunVal;

                        var detailEntityJun = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityJun);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 7;
                        entityDetailModel.Value = item.JulyVal;

                        var detailEntityJuly = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityJuly);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 8;
                        entityDetailModel.Value = item.AugVal;

                        var detailEntityAugest = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityAugest);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 9;
                        entityDetailModel.Value = item.SepVal;

                        var detailEntitySeptember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntitySeptember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 10;
                        entityDetailModel.Value = item.OctVal;

                        var detailEntityOctober = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityOctober);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 11;
                        entityDetailModel.Value = item.NovVal;

                        var detailEntityNovember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityNovember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();

                        entityDetailModel.ExpenseBudgetId = Convert.ToInt64(Convert.ToInt64(Session["ExpenseBudgetUpdateId"]));
                        entityDetailModel.ParticularId = item.ParticularId;
                        entityDetailModel.BudgetMonth = 12;
                        entityDetailModel.Value = item.DecVal;

                        var detailEntityDecember = entityDetailModel.ToEntity();
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.Add(detailEntityDecember);
                        _ExpenseBudgetService.BMSUnit.ExpenseBudgetDetailRepository.SaveChanges();
           

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
