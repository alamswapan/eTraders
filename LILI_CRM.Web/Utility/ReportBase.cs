using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Web.Utility;

namespace LILI_CRM.Web.Utility
{
    public class ReportBase : Page
    {
        #region Fields
        //private readonly EmployeeService _empService;
        internal readonly LILI_CRMEntities1 context;
        internal readonly Common commonList;
        #endregion

        public ReportBase()
        {
            //_empService = empService;
            context = new LILI_CRMEntities1();
            commonList = new Common();
            
        }
    }
}