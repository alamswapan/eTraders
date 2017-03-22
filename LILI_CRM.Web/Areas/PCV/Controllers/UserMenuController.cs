using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Web.Areas.PCV.ViewModel;
using Lib.Web.Mvc.JQuery.JqGrid;
using LILI_CRM.Web.Resources;
using LILI_CRM.Web.Utility;
using System.Collections;
using LILI_CRM.Domain.PCV;


namespace LILI_CRM.Web.Areas.PCV.Controllers
{ 
    public class UserMenuController : Controller
    {
        #region Fields
        private readonly BMSCommonSevice _invUserMenuService;
        #endregion

        #region Ctor
        public UserMenuController(BMSCommonSevice invUserMenuService)
        {
            _invUserMenuService = invUserMenuService;

        }
        #endregion

        #region Properties
        public string Message { get; set; }
        #endregion

        #region Actions
        public ViewResult Index(UserMenuSearch model)
        {
            if (model == null)
                model = new UserMenuSearch();

            return View(model);
        }

        public ActionResult BackToList()
        {
            var model = new UserMenuSearch();
            return View("_Index", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetList(JqGridRequest request, UserMenuSearch model)
        {
            string filterExpression = String.Empty;
            int totalRecords = 0;
            dynamic list = null;

            list = _invUserMenuService.GetUserMenuSearchList(model.UserName);

            totalRecords = list == null ? 0 : list.Count;

            JqGridResponse response = new JqGridResponse()
            {
                TotalPagesCount = (int)Math.Ceiling((float)totalRecords / (float)request.RecordsCount),
                PageIndex = request.PageIndex,
                TotalRecordsCount = totalRecords
            };

            foreach (var d in list)
            {
                response.Records.Add(new JqGridRecord(Convert.ToString(d.Id), new List<object>()
                {
                    d.Id,
                    d.UserName,
                    //d.EmpFullName,
                    //d.Designation,
                    //d.Department,
                    "Delete"
                }));
            }
            return new JqGridJsonResult() { Data = response };
        }

        public ActionResult Create()
        {
            var model = new UserMenuModel();
            PrepareModel(model);
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Create(UserMenuModel model)
        //{
        //    var strMessage = CheckBusinessRule(model);
        //    if (ModelState.IsValid && string.IsNullOrEmpty(strMessage))
        //    {
        //        try
        //        {
        //            var entity = model.ToEntity();

        //            foreach (var item in model.UserMenuDetail.Select(x => x.ToEntity()))
        //            {
        //                entity.BMS_tblUserWiseMenuAssign.Add(item);
        //            }

        //            //_invUserMenuService.BMSUnit.UserInfoRepository.Add(entity);
        //            _invUserMenuService.BMSUnit.UserInfoRepository.SaveChanges();

        //            model.ErrorClass = "success"; model.IsError = 0;
        //            model.Message = strMessage = Resources.ErrorMessages.InsertSuccessful;
        //            return RedirectToAction("Index", model);
        //        }
        //        catch (Exception ex)
        //        {
        //            if (ex.InnerException.Message.Contains("duplicate"))
        //                strMessage = ErrorMessages.UniqueIndex;
        //            else
        //                strMessage = Common.GetExceptionMessage(ex, Common.CommonAction.Save);

        //            model.ButtonText = "Save"; model.SelectedClass = "selected"; model.ErrorClass = "failed"; model.IsError = 1;
        //            model.Message = strMessage;
        //            return PartialView("Create", model);
        //        }
        //    }
        //    model.ButtonText = "Save"; model.SelectedClass = "selected"; model.ErrorClass = "failed"; model.IsError = 1;
        //    model.Message = strMessage = Common.GetModelStateErrorMessage(ModelState, false);
        //    return View(model);
        //}

        //public ActionResult Edit(int id)
        //{
        //    var entity = _invUserMenuService.BMSUnit.UserInfoRepository.GetByID(id);
        //    var childEntities = entity.BMS_tblUserWiseMenuAssign;
        //    var model = entity.ToModel();
        //    childEntities.ToList().ForEach(x => model.UserMenuDetail.Add(x.ToModel()));
        //    PrepareModelEdit(model);
        //    model.Mode = "Edit";
        //    return View(model);
        //}




        //[HttpPost]
        //public ActionResult Edit(UserMenuModel model)
        //{
        //    string strMessage = CheckBusinessRule(model);
        //    if (ModelState.IsValid && string.IsNullOrEmpty(Message))
        //    {
        //        try
        //        {
        //            var entity = model.ToEntity();
        //            var navigationList = new Dictionary<Type, ArrayList>();
        //            var childEntities = new ArrayList();
        //            model.UserMenuDetail.ToList().ForEach(x => x.UserId = model.Id);
        //            //model.ApprovalPathDetails.ToList().ForEach(x => childEntities.Add(x.ToEntity()));
        //            model.UserMenuDetail.Select(x => x.ToEntity()).ToList().ForEach(x => childEntities.Add(x));

        //            navigationList.Add(typeof(BMS_tblUserWiseMenuAssign), childEntities);
        //            //_invUserMenuService.BMSUnit.UserInfoRepository.Update(entity, "Id", navigationList, "Id");
        //            _invUserMenuService.BMSUnit.UserInfoRepository.SaveChanges();

        //            model.ErrorClass = "success"; model.IsError = 0;
        //            model.Message = strMessage = Resources.ErrorMessages.UpdateSuccessful;
        //            return RedirectToAction("Index", model);
        //        }
        //        catch (Exception ex)
        //        {
        //            model.ButtonText = "Update"; model.SelectedClass = "selected"; model.ErrorClass = "failed"; model.IsError = 1;
        //            model.Message = strMessage = Common.GetExceptionMessage(ex, Common.CommonAction.Update);
        //            return PartialView("Edit", model);
        //        }

        //    }

        //    model.ButtonText = "Update"; model.SelectedClass = "selected"; model.ErrorClass = "failed"; model.IsError = 1;
        //    model.Message = strMessage = Common.GetModelStateErrorMessage(ModelState, false);
        //    return View(model);
        //}

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var entity = _invUserMenuService.BMSUnit.UserInfoRepository.GetByID(id, "Id");

            try
            {
                List<Type> allTypes = new List<Type> { typeof(BMS_tblUserWiseMenuAssign) };
                //_invUserMenuService.BMSUnit.UserInfoRepository.Delete(entity.Id, "Id", allTypes, "UserId");
                _invUserMenuService.BMSUnit.UserInfoRepository.SaveChanges();

                return Json(new
                {
                    Success = 1,
                    Message = ErrorMessages.DeleteSuccessful
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json(new
                {
                    Success = 0,
                    Message = ErrorMessages.DeleteFailed
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        
        #region Utils
        private void PrepareModel(UserMenuModel model)
        {
            List<SelectListItem> MenuList = new List<SelectListItem>();
            var list = _invUserMenuService.BMSUnit.AllMenuRepository.GetAll().ToList();
            //int totalRows = 0;
            model.MenuList = Common.PopulateMenuList(_invUserMenuService.BMSUnit.AllMenuRepository.GetAll().ToList());
        }

        private void PrepareModelEdit(UserMenuModel model)
        {
            List<SelectListItem> MenuList = new List<SelectListItem>();
            var list = _invUserMenuService.BMSUnit.AllMenuRepository.GetAll().ToList();
            //int totalRows = 0;
            model.MenuList = Common.PopulateMenuList(_invUserMenuService.BMSUnit.AllMenuRepository.GetAll().ToList());

            var mnuList = (from mnu in _invUserMenuService.BMSUnit.AllMenuRepository.GetAll()
                           //where emp.DesignationId == model.DesignationId
                           select mnu
                               ).Distinct().ToList();
            foreach (var item in model.UserMenuDetail)
            {
                item.MenuName = mnuList.Where(x => x.Id == item.MenuId).FirstOrDefault().MenuName;
            }
        }

        //private void PrepareModelEdit(IndentRequestModel model)
        //{
        //    int totalRows = 0;
        //    model.PurchaseTypeList = Common.PopulatePurchaseTypeList();
        //    model.DepartmentList = Common.PopulateDllList(_invIndentRequestService.INVUnit.FunctionRepository.GetAllCommonConfig("Department", 0, "", "", "", 0, 100, out totalRows));

        //    var empList = (from itm in _invItemService.INVUnit.INV_tblItemInformationRepository.GetAll()
        //                   select itm
        //                           ).Distinct().ToList();
        //    foreach (var item in model.IndentRequestModelDetails)
        //    {
        //        item.ItemName = empList.Where(x => x.Id == item.ItemId).FirstOrDefault().ItemName;
        //        item.PartNo = empList.Where(x => x.Id == item.ItemId).FirstOrDefault().PartNo;
        //        item.MachineName = empList.Where(x => x.Id == item.ItemId).FirstOrDefault().MachineName;
        //        item.Description = empList.Where(x => x.Id == item.ItemId).FirstOrDefault().Description;
        //        item.UnitId = empList.Where(x => x.Id == item.ItemId).FirstOrDefault().UnitId;

        //        var unitName = (from c in _invIndentRequestService.INVUnit.FunctionRepository.GetAllCommonConfig("Unit", item.UnitId, "", "", "", 0, 100, out totalRows)
        //                        select c).Distinct().ToList();


        //        item.Unit = unitName.FirstOrDefault().Name.ToString();
        //    }
        //}



        private string CheckBusinessRule(UserMenuModel model)
        {
            if (model.UserMenuDetail.Count <= 0)
            {
                return "Please Select Menu.";
            }
            return string.Empty;
        }

        
        #endregion
    }
}