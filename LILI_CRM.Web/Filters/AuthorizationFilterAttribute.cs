using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using LILI_CRM.Web.AIMSSecurityService;
using LILI_BMS.DAL.PCV;

namespace LILI_CRM.Web.Filters
{
    public class AuthorizationFilterAttribute : AuthorizeAttribute
    {
        #region Fields
        private string controller;
        private string action;
        private string requestType;
        #endregion

        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    var request = httpContext.Request;
        //    requestType = request.HttpMethod.ToString();
        //    controller = Convert.ToString(request.RequestContext.RouteData.Values["controller"] ?? request["controller"]);
        //    action = Convert.ToString(request.RequestContext.RouteData.Values["action"] ?? request["action"]);
            
        //    if (controller.Equals("Account")
        //        || (controller.Equals("Home") && action.Equals("Public"))
        //        || (controller.Equals("Home") && action.Equals("BackToLogOn"))
        //        || (controller.Equals("Dashboard"))
        //        || (controller.Equals("Charts") && action.StartsWith("_"))
        //        || (controller.Equals("ProjectInfo") && action.Equals("UpdateADPProjectInfo") && Utility.AppConstant.IsADPInfoAssigned)
        //        || (controller.Equals("ProjectInfo") && action.Equals("LockProjectFundingInfo") && Utility.AppConstant.IsLockAssigned)
        //        || (controller.Equals("ProjectInfo") && action.Equals("VerifyProjectFundingCommitment") && Utility.AppConstant.IsApproveAssigned)
        //        || (controller.Equals("ProjectInfo") && action.Equals("CreateOrEditProjectNote") && Utility.AppConstant.IsAttachmentsAssigned)
        //        || (controller.Equals("ProjectInfo") && action.Equals("CreateProjectAttachment") && Utility.AppConstant.IsAttachmentsAssigned)
        //        )
        //    {
        //        return true;
        //    }
        //    var isAuthorized = base.AuthorizeCore(httpContext);

        //    if (isAuthorized)
        //    {
        //        var currentUser = httpContext.User.Identity.Name;

        //        return ValidatePageUrl(controller, action);
        //    }
        //    return isAuthorized;
        //}

        private bool ValidatePageUrl(string controller, string action)
        {
            if (controller.Contains("Home") && action.Contains("Index")) return true;

            bool flag = true;
            var currentMenus = (List<LILI_CRM.DAL.CRM.CustomEntities.MenuList>)HttpContext.Current.Session["CurrentMenus"];

            //session out then user compel to signout
            if (currentMenus == null) { System.Web.Security.FormsAuthentication.SignOut(); return false; }

            var visitingMenu = currentMenus.Find(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower()));

            if (visitingMenu == null && !action.Contains("Index"))
                visitingMenu = (LILI_CRM.DAL.CRM.CustomEntities.MenuList)HttpContext.Current.Session["CurrentMenu"];

            //if (currentMenus != null && action.Contains("Index")) //check view permission
            //{
            //    flag = visitingMenu != null ? visitingMenu.IsAssignedMenu : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAssignedMenu);
            //}
            ////else if (currentMenus != null && action.Contains("Create") && requestType.Contains("GET")) // check view permission
            ////{
            ////    flag = visitingMenu != null ? (visitingMenu.IsAssignedMenu | visitingMenu.IsAddAssigned) : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAddAssigned);
            ////}
            //else if (currentMenus != null && action.Contains("Create") && requestType.Contains("POST")) // check add permission
            //{
            //    flag = visitingMenu != null ? visitingMenu.IsAddAssigned : false;//currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsAddAssigned);
            //}
            //else if (currentMenus != null && action.Contains("Edit") && requestType.Contains("POST")) // check update permission
            //{
            //    flag = visitingMenu != null ? visitingMenu.IsEditAssigned : currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsEditAssigned);
            //}
            //else if (currentMenus != null && action.Contains("Delete")) // check delete permission
            //{
            //    flag = visitingMenu != null ? visitingMenu.IsDeleteAssigned : currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.IsDeleteAssigned);
            //}

            if (currentMenus != null && currentMenus.Exists(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower())))
                HttpContext.Current.Session["CurrentMenu"] = visitingMenu; //currentMenus.Find(x => x.PageUrl.ToLower().Contains(controller.ToLower()) && x.PageUrl.ToLower().Contains(action.ToLower()));

            return flag;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //filterContext.Result = new RedirectResult("~/Account/LogOn");
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("BackToLogOn", "Home"));
            }
            else
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.Result = new RedirectResult(urlHelper.Action("Unauthorized", "Home"));
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var request = httpContext.Request;
            var controller = request.RequestContext.RouteData.Values["controller"] ?? request["controller"];
            if (controller.Equals("Account"))
            {
                return true;
            }
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (isAuthorized)
            {
                var action = request.RequestContext.RouteData.Values["action"] ?? request["action"];

                var currentUser = httpContext.User.Identity.Name;
                return HasPermissionsForBudget(currentUser, action);
            }
            return isAuthorized;
        }

        private bool HasPermissionsForBudget(string currentUser, object action)
        {
            return true;
        }
    }
}