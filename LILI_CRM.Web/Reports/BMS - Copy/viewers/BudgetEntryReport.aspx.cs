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

namespace LILI_BMS.Web.Reports.BMS.viewers
{
    public partial class BudgetEntryReport : ReportBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateCombo();
            }
        }

        public static class Global
        {
            public static DateTime? breakdownDate = null;
        }


        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        public void GenerateReport()
        {
            rvBudgetEntry.Reset();
            rvBudgetEntry.ProcessingMode = ProcessingMode.Local;
            rvBudgetEntry.LocalReport.ReportPath = Server.MapPath("~/Reports/BMS/rdlc/BudgetEntryReport.rdlc");

            //if (txtFromDate.Text.Trim() != "")
            //{
            //    Global.breakdownDate = Convert.ToDateTime(txtFromDate.Text.Trim());
            //}

            #region Processing Report Data

            LILI_BMSEntities1 bEntities = new DAL.BMS.LILI_BMSEntities1();

            #endregion


            string strSubQuery = "";

            if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
            {
                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 = '" + ddlLevel1.SelectedValue + "'  ";
            }

            else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
            {
                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
            }

            else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
            {
                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode AND PTMJ.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
            }

            else
            {
                strSubQuery = " PTMJ.Business = '" + ddlBusiness.SelectedValue + "' AND PTMJ.BudgetYear = '" + ddlYear.SelectedValue + "' AND PTJ.ProductCode = PT.ProductCode  ";
            }


            string strQuery = "SELECT DISTINCT ProductCode, Product, " +
                              "(SELECT TOP 1 Category FROM GetSales GS WHERE GS.ProductCode=PT.ProductCode) AS Category, " +

                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTargetVal, " +
                              
                              "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTarget, " +
                              "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTargetVal " +
                              
                              "FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT ON PTM.Id = PT.MasterId " +
                              "WHERE PTM.Business = '" + ddlBusiness.SelectedValue + "' " +
                              "AND PTM.BudgetYear = '" + ddlYear.SelectedValue + "' "; // +
            //"group BY ProductCode, Product";



            //string strQuery = "SELECT DISTINCT ProductCode, Product, " +
            //                  "(SELECT TOP 1 Category FROM GetSales GS WHERE GS.ProductCode=PT.ProductCode) AS Category, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='01' AND " + strSubQuery + ") AS JanGr, " +
            //                  "0 AS JanSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND  " + strSubQuery + ") AS FebTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='02' AND " + strSubQuery + ") AS FebGr, " +
            //                  "0 AS FebSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND  " + strSubQuery + ") AS MarTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='03' AND " + strSubQuery + ") AS MarGr, " +
            //                  "0 AS MarSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND  " + strSubQuery + ") AS AprTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='04' AND " + strSubQuery + ") AS AprGr, " +
            //                  "0 AS AprSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND  " + strSubQuery + ") AS MayTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='05' AND " + strSubQuery + ") AS MayGr, " +
            //                  "0 AS MaySPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND  " + strSubQuery + ") AS JunTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='06' AND " + strSubQuery + ") AS JunGr, " +
            //                  "0 AS JunSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND  " + strSubQuery + ") AS JulTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='07' AND " + strSubQuery + ") AS JulGr, " +
            //                  "0 AS JulSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND  " + strSubQuery + ") AS AugTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='08' AND " + strSubQuery + ") AS AugGr, " +
            //                  "0 AS AugSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND  " + strSubQuery + ") AS SepTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='09' AND " + strSubQuery + ") AS SepGr, " +
            //                  "0 AS SepSPLY, " +


            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND  " + strSubQuery + ") AS OctTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='10' AND " + strSubQuery + ") AS OctGr, " +
            //                  "0 AS OctSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND  " + strSubQuery + ") AS NovTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='11' AND " + strSubQuery + ") AS NovGr, " +
            //                  "0 AS NovSPLY, " +

            //                  "(SELECT SUM(Target) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTarget, " +
            //                  "(SELECT SUM(TargetVal) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND  " + strSubQuery + ") AS DecTargetVal, " +
            //                  "(SELECT SUM(Gr) FROM ProductTarget PTJ INNER JOIN ProductTargetMaster PTMJ ON PTMJ.Id = PTJ.MasterId WHERE Right(Period,2)='12' AND " + strSubQuery + ") AS DecGr, " +
            //                  "0 AS DecSPLY " +

            //                  "FROM ProductTargetMaster PTM INNER JOIN ProductTarget PT ON PTM.Id = PT.MasterId " +
            //                  "WHERE PTM.Business = '" + ddlBusiness.SelectedValue + "' " +
            //                  "AND PTM.BudgetYear = '" + ddlYear.SelectedValue + "' "; // +
            //                  //"group BY ProductCode, Product";


            if (ddlLevel1.SelectedValue != "0" && ddlLevel1.SelectedValue != "")
            {
                strQuery = strQuery + " AND PTM.Level1 = '" + ddlLevel1.SelectedValue + "' ";
            }

            else if (ddlLevel2.SelectedValue != "0" && ddlLevel2.SelectedValue != "")
            {
                strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 = '" + ddlLevel2.SelectedValue + "') ";
            }

            else if (ddlLevel3.SelectedValue != "0" && ddlLevel3.SelectedValue != "")
            {
                strQuery = strQuery + " AND PTM.Level1 IN (SELECT Level1 FROM Level1 WHERE Level2 IN (SELECT Level2 FROM Level2 WHERE Level3 = '" + ddlLevel3.SelectedValue + "') ) ";
            }

            strQuery = strQuery + " group BY ProductCode, Product, PTM.Business, PTM.BudgetYear, PTM.Level1";
            strQuery = strQuery + " Order By Category, Product";



            string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
            SqlConnection objConnection = new SqlConnection(strConn);
            SqlCommand objCommand = new SqlCommand(strQuery, objConnection);
            objCommand.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            DataSet ds = new DataSet();
            objConnection.Open();
            da.Fill(ds);
            objConnection.Close();


            rvBudgetEntry.LocalReport.DataSources.Add(new ReportDataSource("DSBudgetTargetEntryDataSet", ds.Tables[0]));


            //var generationDate = txtFromDate.Text.Trim();
            //DateTime effectiveDate;
            //if (DateTime.TryParse(generationDate, out effectiveDate))
            //{
            //    string searchParameter = "Business :" + ddlBusiness.SelectedItem;
            //    string shiftName = "Year :" + ddlYear.SelectedItem;

            //    ReportParameter p1 = new ReportParameter("param", searchParameter);
            //    ReportParameter p2 = new ReportParameter("paramShiftName", shiftName);

            //    rvBudgetEntry.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });
            //}

            string searchParameter = "Business :" + ddlBusiness.SelectedItem;
            string shiftName = "Year :" + ddlYear.SelectedItem;

            ReportParameter p1 = new ReportParameter("param", searchParameter);
            ReportParameter p2 = new ReportParameter("paramShiftName", shiftName);

            rvBudgetEntry.LocalReport.SetParameters(new ReportParameter[] { p1, p2 });

            //this.rvBudgetEntry.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
            rvBudgetEntry.DataBind();
        }

        //void localReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        //{

        //    dynamic data = null;
        //    var dsName = string.Empty;

        //    string strVarRV = "SELECT ProductionTime, ProductionTimeUnit, ProductionCapacityPH, ProductionCapacityPHUnit ,TotalBreakdownTime, TotalBreakdownTimeUnit, TotalProduction, TotalProductionUnit, CostPerHour, CostPerHourUnit " +
        //                "FROM BMS_tblDailyBreakdown db " +
        //                "WHERE db.ShiftId = '" + ddlShiftName.SelectedValue + "'" +
        //                "AND db.DateOfBreakdown= '" + Global.breakdownDate + "'";


        //    string strConRV = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
        //    SqlConnection objConnectionRV = new SqlConnection(strConRV);
        //    SqlCommand objCommandRV = new SqlCommand(strVarRV, objConnectionRV);
        //    objCommandRV.CommandType = CommandType.Text;
        //    SqlDataAdapter daRV = new SqlDataAdapter(objCommandRV);
        //    DataSet dsRV = new DataSet();
        //    objConnectionRV.Open();
        //    daRV.Fill(dsRV);


        //    objConnectionRV.Close();

        //    dsName = "DSDailyBreakdownReportSub";
        //    e.DataSources.Add(new ReportDataSource(dsName, dsRV.Tables[0]));
        //}

        protected void rvBudgetEntry_ReportRefresh(object sender, CancelEventArgs e)
        {
            btnViewReport_Click(null, null);
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