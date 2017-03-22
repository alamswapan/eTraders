<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reports/PCV/viewers/ReportMaster.master"
CodeBehind="PettyCashVoucherReport.aspx.cs" Inherits="LILI_CRM.Web.Reports.PCV.viewers.PettyCashVoucherReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>  

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="GroupBox" style="max-height: 15px" id="message">
                <asp:Label ID="lblMsg" runat="server">        
                    </asp:Label>
            </div>


            <div class="GroupBox" style="width:99%; ">
                

                <div class="row">
                    <span class="label"> 
                        <asp:Label ID="Label5" Text="From Date " runat="server" />
                        <label class="required-field">
                            *</label>
                    
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="datePicker date" Width="100px"></asp:TextBox>
                    </span>  

                    <span class="label"> 
                        <asp:Label ID="Label6" Text="To Date " runat="server" />
                        <label class="required-field">
                            *</label>
                    
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="datePicker date" Width="100px"></asp:TextBox>
                    </span>  


                    <span class="label">
                        <div class="button-crude button-left">
                            <asp:Button Text="Load Voucher" runat="server" ID="btnLoadVoucherNo" ValidationGroup="view"
                                type="submit" OnClick="btnLoadVoucherNo_Click" />
                        </div>
                    </span>

                    <span class="label">
                        <asp:Label ID="lblVoucherNo" Text="Voucher No. " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlVoucherNo" >
                        </asp:DropDownList>
                     </span> 
                </div>


                <div class="row">

                    
                
                </div>

                




                <div class="row">
                    <div class="button-crude button-left">
                        <asp:Button Text="View Report" runat="server" ID="btnViewReport" ValidationGroup="view"
                            OnClientClick="ShowLoadingImage();" type="submit" OnClick="btnViewReport_Click" />
                    </div>
                    </span>
                </div>


                <div id="divLoading" style="margin: 0px; padding: 0px; position: fixed; right: 0px;
                    top: 0px; width: 100%; height: 100%; background-color: #666666; z-index: 30001;
                    opacity: .8; filter: alpha(opacity=70);display:none" >
                    <p style="position: absolute; top: 30%; left: 40%; color: White;">
                        <img src="../../../../Content/Images/ajax-loader.gif" />
                    </p>
                </div>


                <div class="clear">
                </div>
            </div>


            <div class="GroupBox">
                <%--<rsweb:ReportViewer ID="rvBudgetEntry" runat="server" Width="100%" 
                    Height="100%" onreportrefresh="rvBudgetEntry_ReportRefresh">
                </rsweb:ReportViewer>--%>

                <rsweb:ReportViewer ID="rvBudgetEntry" runat="server" width="100%" height="100%"
                ZoomMode="PageWidth" ShowBackButton="true" SizeToReportContent="True" onreportrefresh="rvBudgetEntry_ReportRefresh">
                </rsweb:ReportViewer>

                <div class="clear">
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="rvBudgetEntry" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">

        function ShowLoadingImage() {

            $("#divLoading").show();

        }
    </script>

</asp:Content>
