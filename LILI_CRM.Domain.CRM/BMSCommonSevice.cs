using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LILI_CRM.DAL.CRM;
using LILI_CRM.DAL.CRM.CustomEntities;
using LILI_BMS.DAL.PCV.CustomEntities;

namespace LILI_CRM.Domain.PCV
{
    public class BMSCommonSevice
    {      
        #region Fields

         BMS_UnitOfWork _bmsUnit { get; set; }      
     
        #endregion

        #region Ctor

        public BMSCommonSevice(BMS_UnitOfWork uow)
        {
            _bmsUnit =uow;
        }

        public BMSCommonSevice()
        {
            
        }      
        
        #endregion

        public BMS_UnitOfWork BMSUnit { get { return _bmsUnit; } }

        public List<MenuList> GetMenuList(string ModuleName, string UID)
         {
             var query = @"SELECT ML.ModuleName, ML.MenuName, ML.ParentMenuName, ML.PageUrl, ML.ControllerName, ML.ActionName, UI.UserName
                                            FROM BMS_tblMenuList ML JOIN BMS_tblUserWiseMenuAssign UMA ON ML.Id = UMA.MenuId
                                            JOIN BMS_tblUserInfo UI ON UMA.UserId = UI.Id
                                            WHERE  ML.ModuleName='" + ModuleName + "' AND UI.UserName='" + UID + "' ORDER BY ML.ID ASC ";

            var data = BMSUnit.MenuList.GetWithRawSql(query);

            return data.ToList();
        }

        public IList<UserMenuSearch> GetUserMenuSearchList(string userName = "")
        {
            var query = @"Select UI.Id, UI.UserName
                            FROM INV_tblUserInfo UI";
            if (!string.IsNullOrEmpty(userName))
                query += " WHERE UI.UserName LIKE '%" + userName + "%'";

            var data = BMSUnit.UserMenuSearch.GetWithRawSql(query);

            return data.ToList();
        }

        public List<TransactionRefCustomEntity> GenerateCustomInvoiceId(int MasterId =0)
        {
            var query = @"SELECT CAST(CAST(RIGHT(YEAR(GETDATE()),2) AS VARCHAR(2)) 
                            +CAST(REPLICATE('0',6-LEN(ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1)) AS VARCHAR(5))
                            +CAST(ISNULL(MAX(RIGHT(InvoiceNo,6)),0)+1 AS VARCHAR(6))  AS VARCHAR(20)) AS  InvoiceId
                          FROM [tblInvoice]
                          WHERE LEFT(InvoiceNo,2)=RIGHT(YEAR(GETDATE()),2)";

            var data = BMSUnit.GetNewTransactionId.GetWithRawSql(query);

            //var query1 = @"SELECT top 1 InvoiceNo FROM tblInvoice";
            //var maxId1 = _invInvoiceService.INVUnit.InvoiceRepository.GetWithRawSql(query);

            return data.ToList();
        }

    }
}
