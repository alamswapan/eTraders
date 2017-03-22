using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WebForms;
using System.ComponentModel;
using LILI_BMS.Web.Utility;

using LILI_BMS.DAL.Infrastructure;
using LILI_BMS.DAL.BMS;
using LILI_BMS.Domain.BMS;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using LILI_BMS.Web.Areas.BMS.ViewModel;
using LILI_BMS.DAL.BMS.CustomEntities;

namespace LILI_BMS.Web.Reports.BMS.viewers
{
    public partial class BudgetEntryCategorySummaryReport : ReportBase
    {


        public  BMSCommonSevice _bmsDailyWastage;
        public BMS_UnitOfWork _bmsUnit;



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateCombo();
            }
        }

        //protected void Page_Load(BMSCommonSevice bmsDailyWastage, BMS_UnitOfWork BMSUnit)
        //{
        //   this._bmsDailyWastage = bmsDailyWastage;
        //   this._bmsUnit = BMSUnit;
        //}


        public static class Global
        {
            public static DateTime? breakdownDate = null;
        }


        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            //if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "" && ddlBusiness.SelectedValue != "0" && ddlYear.SelectedValue != "0")
            //{
                GenerateReport();
            //}
        }

        public void GenerateReport()
        {
            rvBudgetEntry.Reset();
            rvBudgetEntry.ProcessingMode = ProcessingMode.Local;
            rvBudgetEntry.LocalReport.ReportPath = Server.MapPath("~/Reports/BMS/rdlc/BudgetEntryCategorySummaryReport.rdlc");



            #region Processing Report Data

            LILI_BMSEntities1 bEntities = new DAL.BMS.LILI_BMSEntities1();

            #endregion


            string strSubQuery = "";
            string strSubQuerySPLY = "";
            string strWhereQuery = "";

            if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
            {
                strSubQuery = " AND PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 = '" + ddlLevel1.SelectedValue + "'  ";
                strSubQuerySPLY = " AND GS.Business = '" + ddlBusiness.SelectedValue + "' AND  GS.ProductCode = PT.ProductCode AND GS.Level1 = '" + ddlLevel1.SelectedValue + "'";
            }

            else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
            {
                strSubQuery = " AND PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
                strSubQuerySPLY = " AND GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode AND GS.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "')";
            }

            else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
            {
                strSubQuery = " AND PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
                strSubQuerySPLY = " AND GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode AND GS.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
            }

            else
            {
                strSubQuery = " AND PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode  ";
                strSubQuerySPLY = " AND GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode ";
            }



            //string strQuery = "SELECT DISTINCT ProductCode, Product, " +
            //                  "(SELECT TOP 1 Cost FROM ProductCost PC WHERE PC.ProductCode = PT.ProductCode AND PC.Business = '" + ddlBusiness.SelectedValue + "') AS UnitCost, " +
            //                  "(SELECT TOP 1 Category FROM GetSales GS WHERE GS.ProductCode=PT.ProductCode) AS Category, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='01' AND " + strSubQuerySPLY + ") AS JanSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='02' AND " + strSubQuerySPLY + ") AS FebSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='03' AND " + strSubQuerySPLY + ") AS MarSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='04' AND " + strSubQuerySPLY + ") AS AprSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='05' AND " + strSubQuerySPLY + ") AS MaySPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='06' AND " + strSubQuerySPLY + ") AS JunSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='07' AND " + strSubQuerySPLY + ") AS JulSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='08' AND " + strSubQuerySPLY + ") AS AugSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='09' AND " + strSubQuerySPLY + ") AS SepSPLY, " +


            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='10' AND " + strSubQuerySPLY + ") AS OctSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='11' AND " + strSubQuerySPLY + ") AS NovSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTargetVal, " +
            //                  "(SELECT SUM(SalesQty) FROM GetSales GS WHERE Right(Period,2)='12' AND " + strSubQuerySPLY + ") AS DecSPLY " +

            //                  "FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT ON PTM.Id = PT.MasterId  " +
            //                  "WHERE PTM.Business = '" + ddlBusiness.SelectedValue + "' " +
            //                  "AND PTM.BudgetYear = '" + ddlYear.SelectedValue + "' "; 










            if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
            {
                strWhereQuery = " AND PTM.Level1 = '" + ddlLevel1.SelectedValue + "' ";
                //strQuery = strQuery + " AND PTM.Level1 = '" + ddlLevel1.SelectedValue + "' ";
            }

            else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
            {
                strWhereQuery = " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
                //strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
            }

            else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
            {
                strWhereQuery = " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
                //strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
            }

            //strQuery = strQuery + " group BY ProductCode, Product, PTM.Business, PTM.BudgetYear, PTM.Level1";
            //strQuery = strQuery + " Order By Category, Product";



            //string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
            //SqlConnection objConnection = new SqlConnection(strConn);
            //SqlCommand objCommand = new SqlCommand(strQuery, objConnection);
            //objCommand.CommandType = CommandType.Text;
            //SqlDataAdapter da = new SqlDataAdapter(objCommand);
            //DataSet ds = new DataSet();
            //objConnection.Open();
            //da.Fill(ds);
            //objConnection.Close();


            string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
            SqlConnection objConnection = new SqlConnection(strConn);
            SqlCommand objCommand = new SqlCommand();
            objCommand = new SqlCommand("sp_BudgetEntrySummaryReport", objConnection);
            objCommand.Parameters.Add("@Year", SqlDbType.VarChar).Value = ddlYear.SelectedValue;
            objCommand.Parameters.Add("@Business", SqlDbType.VarChar).Value = ddlBusiness.SelectedValue;
            objCommand.Parameters.Add("@strSubQuery", SqlDbType.VarChar).Value = strSubQuery;
            objCommand.Parameters.Add("@strSubQuerySPLY", SqlDbType.VarChar).Value = strSubQuerySPLY;
            objCommand.Parameters.Add("@strWhereQuery", SqlDbType.VarChar).Value = strWhereQuery;
            objCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            DataSet ds = new DataSet();
            objConnection.Open();
            da.Fill(ds);
            objConnection.Close();



            rvBudgetEntry.LocalReport.DataSources.Add(new ReportDataSource("DSBudgetTargetCOGSGPEntryDataSet", ds.Tables[0]));


            string searchParameter = "Business :" + ddlBusiness.SelectedItem;
            string shiftName = "Year :" + ddlYear.SelectedItem;

            ReportParameter p1 = new ReportParameter("param", searchParameter);
            ReportParameter p2 = new ReportParameter("paramShiftName", shiftName);

            rvBudgetEntry.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

            //this.rvBudgetEntry.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
            rvBudgetEntry.DataBind();
        }


//        public void GenerateReport()
//        {
//            rvBudgetEntry.Reset();
//            rvBudgetEntry.ProcessingMode = ProcessingMode.Local;
//            rvBudgetEntry.LocalReport.ReportPath = Server.MapPath("~/Reports/BMS/rdlc/BudgetEntryCategorySummaryReport.rdlc");



//            #region Processing Report Data

//            LILI_BMSEntities1 bEntities = new DAL.BMS.LILI_BMSEntities1();

//            #endregion


//            string strSubQuery = "";
//            string strSubQuerySPLY = "";

//            if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
//            {
//                strSubQuery = " PTM.Business = '" + ddlBusiness.SelectedValue + "' AND PTM.BudgetYear = '" + ddlYear.SelectedValue + "' AND PT.ProductCode = PT.ProductCode AND PTM.Level1 = '" + ddlLevel1.SelectedValue + "'  ";
//                strSubQuerySPLY = " GS.Business = '" + ddlBusiness.SelectedValue + "' AND  GS.ProductCode = PT.ProductCode AND GS.Level1 = '" + ddlLevel1.SelectedValue + "'";
//            }

//            else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
//            {
//                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
//                strSubQuerySPLY = " GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode AND GS.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "')";
//            }

//            else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
//            {
//                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
//                strSubQuerySPLY = " GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode AND GS.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
//            }

//            else
//            {
//                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode  ";
//                strSubQuerySPLY = " GS.Business = '" + ddlBusiness.SelectedValue + "' AND GS.ProductCode = PT.ProductCode ";
//            }


//            _bmsUnit = new BMS_UnitOfWork();
//            _bmsDailyWastage = new BMSCommonSevice(_bmsUnit);

//            var PSavedTargetlist = GetReportSalesListSavedTarget(strSubQuery).ToList();

//            var strQuery = @"SELECT Category, ProductCode, Product, ProductCost, ISNULL(January,0) AS JanTarget, ISNULL(February,0) AS FebTarget, ISNULL(March,0) AS MarTarget, ISNULL(April,0) AS AprTarget,  
//                        ISNULL(May,0) AS MayTarget, ISNULL(June,0) AS JunTarget, ISNULL(July,0) AS  JulTarget, ISNULL(August,0) AS AugTarget,
//                        ISNULL(September,0) AS SepTarget, ISNULL(October,0) AS OctTarget, ISNULL(November,0) AS NovTarget, ISNULL(December,0) AS DecTarget
//                        FROM 
//                        (SELECT PTM.Business, PTM.Level1, PT.ProductCode, 
//		                (SELECT TOP 1 Cost FROM ProductCost PC WHERE PC.ProductCode = PT.ProductCode) AS ProductCost,
//		                (SELECT TOP 1 Category FROM GetSales GS WHERE GS.ProductCode=PT.ProductCode) AS Category,
//		                Product, Pack, left(PT.Period,4) as [Year], 
//                            DateName( month , DateAdd( month , Convert(INT, right(PT.Period,2)) , 0 ) - 1 ) as [Month],
//                            Target
//                            FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT  ON PTM.Id = PT.MasterId
//                            WHERE " + strSubQuery + "  ) as q" +
//                        " PIVOT( SUM(Target) FOR [month] in (January, February, March, April, May, June, July, August, September, October, November, December)) as pvot";

//            //string strQuery = "SELECT '" + PSavedTargetlist.All(.ProductCode + "' AS ProductCode, '" + PSavedTargetlist.FirstOrDefault().ProductName + "' AS Product," +
//            //                  "'" + PSavedTargetlist.FirstOrDefault().UnitPrice + "' AS UnitCost, " +
//            //                  "'" + PSavedTargetlist.FirstOrDefault().Category + "' AS Category, " +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().January + "' AS JanTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().January + "' AS JanTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().February + "' AS FebTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().March + "' AS MarTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().April + "' AS AprTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().May + "' AS MayTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().June + "' AS JunTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().July + "' AS JulTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().August + "' AS AugTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().September + "' AS SepTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().October + "' AS OctTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().November + "' AS NovTarget," +
//            //                  " '" + PSavedTargetlist.FirstOrDefault().December + "' AS DecTarget," +

//            //                  "0 AS JanTargetVal, " +
//            //                  "0 AS JanSPLY, " +

//            //                  "0 AS FebTargetVal, " +
//            //                  "0 AS FebSPLY, " +

//            //                  "0 AS MarTargetVal, " +
//            //                  "0 AS MarSPLY, " +

//            //                  "0 AS AprTargetVal, " +
//            //                  "0 AS AprSPLY, " +

//            //                  "0 AS MayTargetVal, " +
//            //                  "0 AS MaySPLY, " +

//            //                  "0 AS JunTargetVal, " +
//            //                  "0 AS JunSPLY, " +


//            //                  "0 AS JulTargetVal, " +
//            //                  "0 AS JulSPLY, " +

//            //                  "0 AS AugTargetVal, " +
//            //                  "0 AS AugSPLY, " +

//            //                  "0 AS SepTargetVal, " +
//            //                  "0 AS SepSPLY, " +

//            //                  "0 AS OctTargetVal, " +
//            //                  "0 AS OctSPLY, " +

//            //                  "0 AS NovTargetVal, " +
//            //                  "0 AS NovSPLY, " +

//            //                  "0 AS DecTargetVal, " +
//            //                  "0 AS DecSPLY " +

//            //                  "FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT ON PTM.Id = PT.MasterId  " +
//            //                  "WHERE PTM.Business = '" + ddlBusiness.SelectedValue + "' " +
//            //                  "AND PTM.BudgetYear = '" + ddlYear.SelectedValue + "' "; // +
//            ////"group BY ProductCode, Product";









//            //if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
//            //{
//            //    strQuery = strQuery + " AND PTM.Level1 = '" + ddlLevel1.SelectedValue + "' ";
//            //}

//            //else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
//            //{
//            //    strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
//            //}

//            //else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
//            //{
//            //    strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
//            //}

//            //strQuery = strQuery + " group BY ProductCode, Product, PTM.Business, PTM.BudgetYear, PTM.Level1";
//            //strQuery = strQuery + " Order By Category, Product";



//            string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
//            SqlConnection objConnection = new SqlConnection(strConn);
//            SqlCommand objCommand = new SqlCommand(strQuery, objConnection);
//            objCommand.CommandType = CommandType.Text;
//            SqlDataAdapter da = new SqlDataAdapter(objCommand);
//            DataSet ds = new DataSet();
//            objConnection.Open();
//            da.Fill(ds);
//            objConnection.Close();


//            rvBudgetEntry.LocalReport.DataSources.Add(new ReportDataSource("DSBudgetTargetCOGSGPEntryDataSet", ds.Tables[0]));


//            string searchParameter = "Business :" + ddlBusiness.SelectedItem;
//            string shiftName = "Year :" + ddlYear.SelectedItem;

//            ReportParameter p1 = new ReportParameter("param", searchParameter);
//            ReportParameter p2 = new ReportParameter("paramShiftName", shiftName);

//            rvBudgetEntry.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

//            //this.rvBudgetEntry.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
//            rvBudgetEntry.DataBind();
//        }


        protected void rvBudgetEntry_ReportRefresh(object sender, CancelEventArgs e)
        {
            btnViewReport_Click(null, null);
        }


        public List<TargetEntryDetailsCustomEntity> GetReportSalesListSavedTarget(string strSubQuery)
        {
            TargetEntryDetailsCustomEntity tedc = new TargetEntryDetailsCustomEntity();
            _bmsUnit = new BMS_UnitOfWork();
            LILI_BMSEntities1 bEntities = new DAL.BMS.LILI_BMSEntities1();

//            var query = @"SELECT ProductCode, ISNULL(January,0) AS January, ISNULL(February,0) AS February, ISNULL(March,0) AS March, ISNULL(April,0) AS April,  
//                        ISNULL(May,0) AS May, ISNULL(June,0) AS June, ISNULL(July,0) AS  July, ISNULL(August,0) AS August,
//                        ISNULL(September,0) AS September, ISNULL(October,0) AS October, ISNULL(November,0) AS November, ISNULL(December,0) AS December
//                        FROM 
//                        (SELECT PTM.Business, PTM.Level1, ProductCode, Product, Pack, left(Period,4) as [Year], 
//                            DateName( month , DateAdd( month , Convert(INT, right(Period,2)) , 0 ) - 1 ) as [Month],
//	                        Target
//	                        FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT  ON PTM.Id = PT.MasterId
//                            WHERE " + strSubQuery + "  ) as q" +
//                        " PIVOT( SUM(Target) FOR [month] in (January, February, March, April, May, June, July, August, September, October, November, December)) as pvot";

            var query = @"SELECT Category, ProductCode, Product, ProductCost, ISNULL(January,0) AS January, ISNULL(February,0) AS February, ISNULL(March,0) AS March, ISNULL(April,0) AS April,  
                        ISNULL(May,0) AS May, ISNULL(June,0) AS June, ISNULL(July,0) AS  July, ISNULL(August,0) AS August,
                        ISNULL(September,0) AS September, ISNULL(October,0) AS October, ISNULL(November,0) AS November, ISNULL(December,0) AS December
                        FROM 
                        (SELECT PTM.Business, PTM.Level1, PT.ProductCode, 
		                (SELECT TOP 1 Cost FROM ProductCost PC WHERE PC.ProductCode = PT.ProductCode) AS ProductCost,
		                (SELECT TOP 1 Category FROM GetSales GS WHERE GS.ProductCode=PT.ProductCode) AS Category,
		                Product, Pack, left(PT.Period,4) as [Year], 
                            DateName( month , DateAdd( month , Convert(INT, right(PT.Period,2)) , 0 ) - 1 ) as [Month],
                            Target
                            FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT  ON PTM.Id = PT.MasterId
                            WHERE " + strSubQuery + "  ) as q" +
                        " PIVOT( SUM(Target) FOR [month] in (January, February, March, April, May, June, July, August, September, October, November, December)) as pvot";

            BMS_GenericRepository<TargetEntryDetailsCustomEntity> GetSalesDataList = new BMS_GenericRepository<TargetEntryDetailsCustomEntity>(bEntities);

            var data = GetSalesDataList.GetWithRawSql(query);// BMSUnit.GetSalesDataList.GetWithRawSql(query);


            return data.ToList();
        }


        #region Others

        private void PopulateCombo()
        {
            //---[Populate Business Combo]
            var userId = User.Identity.Name;

            ddlBusiness.DataSource = (from x in context.UserBusiness
                        join b in context.Business
                        on x.Business equals b.Business1
                        where x.UserId == userId
                        select new { b.Business1, b.BusinessName }).ToList();

            ddlBusiness.DataValueField = "Business1";
            ddlBusiness.DataTextField = "BusinessName";
            ddlBusiness.DataBind();
            ddlBusiness.Items.Insert(0, new ListItem("<--Select Business-->", "0"));


            //---[Populate Year Combo]           
            ddlYear.DataSource = (from x in context.BudgetYear 
                                  select new { x.BudgetYear1}).ToList();
            ddlYear.DataValueField = "BudgetYear1";
            ddlYear.DataTextField = "BudgetYear1";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, new ListItem("<--Select Year-->", "0"));

            //ddlYear.DataSource = TargetEntryViewModel.AllMonths.ToList();
            //ddlYear.DataValueField = "Index";
            //ddlYear.DataTextField = "Name";
            //ddlYear.DataBind();
            //ddlYear.Items.Insert(0, new ListItem("<--Select Year-->", "0"));

        }

        #endregion

        protected void ddlBusiness_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dlBusiness = ddlBusiness.SelectedValue;
            
            var userId = User.Identity.Name;

            var userDetails = (from x in context.UserManager.Where(q => q.UserId == userId) select x).SingleOrDefault();

            if (userDetails.UserLevel == "All")
            {
                ddlLevel3.DataSource = (from x in context.Level3.Where(q => q.Business == dlBusiness) select new { x.Level31, x.Level3Name }).Distinct().ToList();                
            }
            else
            {
                ddlLevel3.DataSource = (from x in context.Level3.Where(q => q.Level31 == userDetails.LevelCode && q.Business == dlBusiness)
                                     select new { x.Level31, x.Level3Name }).Distinct().ToList();                
            }

            ddlLevel3.DataValueField = "Level31";
            ddlLevel3.DataTextField = "Level3Name";
            ddlLevel3.DataBind();
            ddlLevel3.Items.Insert(0, new ListItem("<--Select Level3-->", "0"));
        }

        protected void ddlLevel3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = User.Identity.Name;
            var level3 = ddlLevel3.SelectedValue;
            var business = ddlBusiness.SelectedValue;

            ddlLevel2.DataSource = (from x in context.Level2.Where(q => q.Level3 == level3 && q.Business == business)
                                 select new { x.Level21, x.Level2Name }).Distinct().ToList();

            ddlLevel2.DataValueField = "Level21";
            ddlLevel2.DataTextField = "Level2Name";
            ddlLevel2.DataBind();
            ddlLevel2.Items.Insert(0, new ListItem("<--Select Level2-->", "0"));
        }

        protected void ddlLevel2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var userId = User.Identity.Name;
            var level2 = ddlLevel2.SelectedValue;
            var business = ddlBusiness.SelectedValue;

            ddlLevel1.DataSource = (from x in context.Level1.Where(q => q.Level2 == level2 && q.Business == business)
                                 select new { x.Level11, x.Base }).Distinct().ToList();

            ddlLevel1.DataValueField = "Level11";
            ddlLevel1.DataTextField = "Base";
            ddlLevel1.DataBind();
            ddlLevel1.Items.Insert(0, new ListItem("<--Select Territory-->", "0"));
        }


    }
}