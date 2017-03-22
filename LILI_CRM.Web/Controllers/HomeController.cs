using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LILI_CRM.Web.Controllers
{
    public class HomeController : Controller
    {
        //private AIMSUserManagementServiceClient _userAgent;

        public ActionResult Index()
        {
            ViewBag.Message = "Dashboard";
            return View();

            //LILI_CRM.Web.Models.Menu model = new LILI_CRM.Web.Models.Menu();
            //model.ModuleName = GetModuleAuthentication();
            //return View(model);
        }

        //public ActionResult Unauthorized()
        //{
        //    //return View();
        //    return RedirectToAction("Account", "LogOn");
        //}

        //public string GetModuleAuthentication()
        //{
        //    int UID = 0;
        //    string moduleName = "";
        //    _userAgent = new AIMSUserManagementServiceClient();
        //    if (LILI_CRM.Web.Utility.AppConstant.NumLoginId != null)
        //    {
        //        UID = Convert.ToInt32(LILI_CRM.Web.Utility.AppConstant.NumLoginId.ToString());
        //    }
        //    var role = _userAgent.GetRolesList();
        //    var sss1 = _userAgent.GetUserList();

        //    var userRole = _userAgent.GetUserRole(UID);

        //    //var userRoleModule = from rol in role
        //    //                     join ur in userRole on rol.RoleId equals ur.RoleId
        //    //                     select rol.ModuleId;
        //    var userRoleModule = from ur in userRole
        //                         join rol in role on ur.RoleId equals rol.RoleId
        //                         select rol.ModuleId;


        //    foreach (var item in userRoleModule)
        //    {
        //        var module = _userAgent.GetModuleById(item);

        //        moduleName = moduleName + module.ModuleName + ",";
        //    }

        //    return moduleName;

        //}

      
    }
}
