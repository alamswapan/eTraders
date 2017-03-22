using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Web.Models;
using System.Web.Security;
using LILI_CRM.Domain.PCV;



namespace LILI_CRM.Web.Controllers
{
    public class MenuController : Controller
    {
        #region Fields
        private readonly BMSCommonSevice _userAgent;
        #endregion

        #region Properties
        private List<LILI_CRM.Web.Models.Menu> _menus
        {
            get
            {
                if (Session["CurrentMenus"] != null)
                    return (List<LILI_CRM.Web.Models.Menu>)Session["CurrentMenus"];
                else
                    return new List<LILI_CRM.Web.Models.Menu>();
            }
            set
            {
                Session["CurrentMenus"] = value;
            }
        }
        #endregion

        #region Ctor
        public MenuController(BMSCommonSevice userAgent)
        {
            this._userAgent = userAgent;
        }
        #endregion

        public ActionResult Index(string ModuleName)
        {
            return View();
        }

        //public PartialViewResult GetMenuList(string ModuleName)
        //{
        //    MenuModels model = new MenuModels();

        //    PrepareMenu(model.MenuList, ModuleName);

        //    return PartialView("Menu", model);
        //}

        public PartialViewResult GetMenuList(string ModuleName)
        {
            MenuModels model = new MenuModels();
            var UID = User.Identity.Name;
            var list = _userAgent.GetMenuList(ModuleName, UID).ToList();
            foreach (var item in list)
            {
                model.MenuList.Add(item);
            }

            return PartialView("Menu", model);
        }

        public PartialViewResult GetSiteMapList(string ModuleName)
        {
            MenuModels model = new MenuModels();
            GetMenuList(ModuleName);
            //PrepareMenu(model.MenuList, ModuleName);
            return PartialView("SiteMap", model);
        }

        //[NonAction]
        //private void PrepareMenu(List<Menu> MenuList, string moduleName)
        //{
        //    List<Menu> allMenus = new List<Menu>();
        //    var selectedMenus = new List<Menu>();

        //    if (_menus != null && _menus.Count == 0)
        //    {
        //        allMenus = _userAgent.GetMenuList(moduleName, User.Identity.Name).ToList();

        //        selectedMenus = allMenus.Where(x => x.IsAssignedMenu).OrderBy(q => q.SerialNo).ToList();

        //        var tempItem = new List<AIMSSecurityService.Menu>();

        //        foreach (var item in selectedMenus)
        //        {
        //            var parentItem = allMenus.Find(x => x.MenuId == item.ParentMenuId);
        //            if (parentItem != null)
        //            {
        //                tempItem.Add(parentItem);
        //                if (parentItem.ParentMenuId != -1)
        //                {
        //                    parentItem = allMenus.Find(x => x.MenuId == parentItem.ParentMenuId);
        //                    if (parentItem != null)
        //                        tempItem.Add(parentItem);
        //                }
        //            }
        //        }
        //        selectedMenus.AddRange(tempItem.Distinct());

        //        _menus = selectedMenus;
        //    }
        //    else
        //        selectedMenus = _menus;

        //    foreach (var item in selectedMenus.Where(x => !x.MenuName.ToUpper().Contains("LEFT")).Distinct().ToList())
        //    {
        //        string strActionName = "";
        //        var url = item.PageUrl.Split('/');
        //        if (url.Count() >= 4)
        //        {

        //            for (int i = 3; i <= url.Count() - 1; i++)
        //            {
        //                strActionName += url[i] + "/";
        //            }
        //            var menu = new Menu()
        //            {
        //                ActionName = strActionName,
        //                ControllerName = url[2],

        //                ModuleName = moduleName, //ModuleName = AppConstant.AIMSModuleName,

        //                MenuId = item.MenuId,
        //                MenuName = item.MenuName,
        //                MenuCaption = item.MenuCaption ?? item.MenuName,
        //                ParentMenuId = item.ParentMenuId,
        //                ParentMenuName = item.ParentMenuName,

        //                PageUrl = item.PageUrl,

        //                IsAssignedMenu = item.IsAssignedMenu,
        //                IsAddAssigned = item.IsAddAssigned,
        //                IsEditAssigned = item.IsEditAssigned,
        //                IsDeleteAssigned = item.IsDeleteAssigned,
        //                IsCancelAssigned = item.IsCancelAssigned,
        //                IsPrintAssigned = item.IsPrintAssigned
        //            };
        //            MenuList.Add(menu);
        //        }
        //        else
        //        {
        //            var menu = new Menu()
        //            {
        //                ModuleName = moduleName, //ModuleName = AppConstant.AIMSModuleName,

        //                MenuId = item.MenuId,
        //                MenuName = item.MenuName,
        //                MenuCaption = item.MenuCaption ?? item.MenuName,
        //                ParentMenuId = item.ParentMenuId,
        //                ParentMenuName = item.ParentMenuName,

        //                PageUrl = item.PageUrl,

        //                IsAssignedMenu = item.IsAssignedMenu,
        //                IsAddAssigned = item.IsAddAssigned,
        //                IsEditAssigned = item.IsEditAssigned,
        //                IsDeleteAssigned = item.IsDeleteAssigned,
        //                IsCancelAssigned = item.IsCancelAssigned,
        //                IsPrintAssigned = item.IsPrintAssigned
        //            };
        //            MenuList.Add(menu);
        //        }
        //    }
        //}
    }
}
