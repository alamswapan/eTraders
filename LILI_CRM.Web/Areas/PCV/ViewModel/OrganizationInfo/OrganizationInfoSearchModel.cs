using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using LILI_CRM.Web.Areas.PCV.ViewModel;

namespace LILI_CRM.Web.Areas.PCV.ViewModel.OrganizationInfo
{
    public class OrganizationInfoSearchModel : BaseViewModel
    {
        #region Properties
        public int? Id { get; set; }
        public string OrganizationName { get; set; }
        #endregion

        #region Method
        public string GetFilterExpression()
        {
            StringBuilder filterExpressionBuilder = new StringBuilder();
            if (Id.HasValue)
                filterExpressionBuilder.Append(String.Format("id = {0} AND ", Id));
            if (!String.IsNullOrWhiteSpace(OrganizationName))
                filterExpressionBuilder.Append(String.Format("OrganizationName like {0} AND ", OrganizationName));

            if (filterExpressionBuilder.Length > 0)
                filterExpressionBuilder.Remove(filterExpressionBuilder.Length - 5, 5);
            return filterExpressionBuilder.ToString();
        }
        #endregion
    }
}