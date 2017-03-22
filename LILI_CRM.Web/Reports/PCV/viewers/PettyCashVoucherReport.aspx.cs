using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.ComponentModel;
using LILI_CRM.Web.Utility;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using LILI_CRM.DAL.CRM;

namespace LILI_CRM.Web.Reports.PCV.viewers
{
    public partial class PettyCashVoucherReport : ReportBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                PopulateCombo();
                txtFromDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtToDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }
        }

        public static class Global
        {
            public static DateTime? DateFrom = null;
            public static DateTime? DateTo = null;
        }


        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            if (ddlVoucherNo.SelectedIndex == 0)
            {
                string script = "alert(\"Please Select Voucher No.!\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                                      "ServerControlScript", script, true);
                return;
            }

            GenerateReport();
        }

        public void GenerateReport()
        {
            rvBudgetEntry.Reset();
            rvBudgetEntry.ProcessingMode = ProcessingMode.Local;
            rvBudgetEntry.LocalReport.ReportPath = Server.MapPath("~/Reports/PCV/rdlc/PettyCashVoucher.rdlc");
            
            if (txtFromDate.Text.Trim() != "")
            {
                Global.DateFrom = Convert.ToDateTime(txtFromDate.Text.Trim());
                Global.DateTo = Convert.ToDateTime(txtToDate.Text.Trim());
            }

            #region Processing Report Data

            LILI_CRMEntities1 bEntities = new DAL.CRM.LILI_CRMEntities1();

            #endregion

            //string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
            //SqlConnection objConnection = new SqlConnection(strConn);
            //SqlCommand objCommand = new SqlCommand();
            //objCommand = new SqlCommand("sp_ComplainInfo", objConnection);
            //objCommand.Parameters.Add("@ComplainType", SqlDbType.VarChar).Value = ddlComplainType.SelectedValue;
            //objCommand.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = ddlProductType.SelectedValue;
            //objCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Global.DateFrom;
            //objCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Global.DateTo;
            //objCommand.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter da = new SqlDataAdapter(objCommand);
            //DataSet ds = new DataSet();
            //objConnection.Open();
            //da.Fill(ds);
            //objConnection.Close();


            string strVar = "SELECT VoucherNo, VoucherDate, CompanyCode, PayTo, StaffID, UserId, Narration, Business, " +
            " Department, Debit, 0 AS Credit, AMCode, AMDetails FROM vwWebPettyCashVoucher vw" +
            " WHERE vw.VoucherNo= " + ddlVoucherNo.SelectedItem + " ";
            


            string strConn = ConfigurationManager.ConnectionStrings["LILI_BMSConnectionString"].ConnectionString;
            SqlConnection objConnection = new SqlConnection(strConn);
            SqlCommand objCommand = new SqlCommand(strVar, objConnection);
            objCommand.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(objCommand);
            DataSet ds = new DataSet();
            objConnection.Open();
            da.Fill(ds);
            objConnection.Close();




            rvBudgetEntry.LocalReport.DataSources.Add(new ReportDataSource("DSPettyCashVoucher", ds.Tables[0]));


            //string paramComplainType = "Complain Type : " + ddlComplainType.SelectedItem;
            //string paramProductName = "Product Name : " + ddlProductType.SelectedItem;
            //string paramDateRange = "Date From " + txtFromDate.Text + " To " + txtToDate.Text;
            //ReportParameter p1 = new ReportParameter("paramComplainType", paramComplainType);
            //ReportParameter p2 = new ReportParameter("paramProductName", paramProductName);
            //ReportParameter p3 = new ReportParameter("paramDateRange", paramDateRange);

            //rvBudgetEntry.LocalReport.SetParameters(new ReportParameter[] { p1, p2, p3 });

            rvBudgetEntry.DataBind();
        }

        protected void rvBudgetEntry_ReportRefresh(object sender, CancelEventArgs e)
        {
            btnViewReport_Click(null, null);
        }


        protected void btnLoadVoucherNo_Click(object sender, EventArgs e)
        {
            PopulateVoucherComboDateRange();
        }

        #region Others

        private void PopulateCombo()
        {
            //---[Populate Voucher No. Combo]
            var userId = User.Identity.Name;

            ddlVoucherNo.DataSource = (from x in context.WebVoucherMaster
                                       where x.UserId == userId && x.Posted == "N"
                                       select new { x.Id}).ToList();
            ddlVoucherNo.DataValueField = "Id";
            ddlVoucherNo.DataTextField = "Id";
            ddlVoucherNo.DataBind();
            ddlVoucherNo.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }

        private void PopulateVoucherComboDateRange()
        {
            //---[Populate Voucher No. Combo]
            var userId = User.Identity.Name;
            Global.DateFrom = Convert.ToDateTime(txtFromDate.Text.Trim());
            Global.DateTo = Convert.ToDateTime(txtToDate.Text.Trim());

            ddlVoucherNo.DataSource = (from x in context.WebVoucherMaster
                                       where x.VoucherDate >= Global.DateFrom && x.VoucherDate <= Global.DateTo && x.UserId == userId && x.Posted == "N"
                                       select new { x.Id}).ToList();
            ddlVoucherNo.DataValueField = "Id";
            ddlVoucherNo.DataTextField = "Id";
            ddlVoucherNo.DataBind();
            ddlVoucherNo.Items.Insert(0, new ListItem("<--Select-->", "0"));

        }

        #endregion

    }
}