using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LILI_BMS.DAL.PCV;
using LILI_CRM.DAL.Infrastructure;
using LILI_CRM.Domain.BMS;
using LILI_CRM.DAL.CRM;
using LILI_CRM.Domain.PCV;

namespace LILI_BMS.Domain.BMS
{
    public class ResourceCategoryService:BMSCommonSevice
    {
   
        #region Ctor

        public ResourceCategoryService(BMS_UnitOfWork uow):base(uow)
        {
        }

        #endregion

        #region Workflow methods

        //public List<string> GetBusinessLogicValidation(PRM_ResourceCategory ResourceCategory)
        //{
        //    List<string> errorMessage = new List<string>();

        //    if (ResourceCategory.ActualRate <=0)
        //    {
        //        errorMessage.Add("Actual Rate should be greater than zero.");
        //    }
        //    if (ResourceCategory.BudgetRate <= 0)
        //    {
        //        errorMessage.Add("Budget Rate should be greater than zero.");
        //    }
        //    return errorMessage;

        //}
        #endregion
    }
}
