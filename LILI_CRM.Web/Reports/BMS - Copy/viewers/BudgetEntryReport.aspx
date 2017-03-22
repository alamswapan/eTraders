<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reports/BMS/viewers/ReportMaster.master"
CodeBehind="BudgetEntryReport.aspx.cs" Inherits="LILI_BMS.Web.Reports.BMS.viewers.BudgetEntryReport" %>

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
                        <asp:Label ID="Label1" Text="Business " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlBusiness" onselectedindexchanged="ddlBusiness_SelectedIndexChanged">
                        </asp:DropDownList>
                     </span> 
                
                    <span class="label" style="padding-left:30px;">
                        <asp:Label ID="Label3" Text="Level3 " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlLevel3" 
                        onselectedindexchanged="ddlLevel3_SelectedIndexChanged">
                        </asp:DropDownList>
                        </span> 
                
                    <span class="label" style="padding-left:30px;">
                        <asp:Label ID="Label4" Text="Level2 " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList AutoPostBack="true" runat="server" ID="ddlLevel2" 
                        onselectedindexchanged="ddlLevel2_SelectedIndexChanged">
                        </asp:DropDownList>
                    </span> 
                
                    <span class="label" style="padding-left:30px;">
                        <asp:Label ID="Label6" Text="Territory  " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList runat="server" ID="ddlLevel1">
                        </asp:DropDownList>
                    </span> 
                
                    <span class="label" style="padding-left:30px;">
                        <asp:Label ID="Label7" Text="Year " runat="server" />
                        <label class="required-field">
                            *</label>
                        <asp:DropDownList runat="server" ID="ddlYear">
                        </asp:DropDownList>
                    </span> 
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
