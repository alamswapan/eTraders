using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LILI_CRM.Web.Areas.PCV.ViewModel.Report;

namespace LILI_CRM.Web.Areas.PCV.Controllers
{
    public class ReportController : Controller
    {
        #region Fields
        private readonly ReportViewerViewModel _model;
        #endregion

        #region Ctor
        public ReportController()
        {
            _model = new ReportViewerViewModel();
        }
        #endregion
                            

        public ActionResult PettyCashVoucherReport()
        {
            _model.ReportPath = Url.Content("~/Reports/PCV/viewers/PettyCashVoucherReport.aspx");
            return View("ReportViewer", _model);
        }
    }
}
