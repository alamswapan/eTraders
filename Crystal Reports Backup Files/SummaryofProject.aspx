<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryofProject.aspx.cs"
    MasterPageFile="~/Views/Shared/Site.Master" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.SummaryofProject" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Summary of Project
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="Form1" runat="server" >
    <div class="widget-box">
        <div class="widget-header">
            <h4> Summary of Projects/ Programs supported by Development Partners</h4>
        </div>
        <div class="widget-body">
        <div class="widget-main">
            <div class="row-fluid">
                <div class="span6">
                    <span class="control-label span4">
                       Calendar Type
                    </span>
                    <span class="span8">
                        <asp:RadioButtonList ID="rbSelectYear" runat="server" RepeatDirection="Horizontal" Width="100%" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="rbSelectYear_OnSelectedIndexChanged">
                            <asp:ListItem Text="Calender Year"  Value="1" Selected="True" ></asp:ListItem>
                            <asp:ListItem Text="Fiscal Year" Value="2" ></asp:ListItem>
                            <asp:ListItem Text="Date" Value="3" ></asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </div>
                <div class="span6">
                <span class="control-label span4">
                    Developement Partner
                </span>
                <div class="controls span8">
                    <asp:DropDownList ID="ddlDP" runat="server"></asp:DropDownList>
                </div>
                </div>
                
            </div>
            <div class="row-fluid">
                <div class="span6 divCalYear">
                    <span class="control-label span4">
                       Calendar Year
                    </span>
                    <div class="controls span8">
                        <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="span6 divFisYear" style="float: left; margin-left: -0px;">
                    <span class="control-label span4">
                         Fiscal Year
                    </span>
                    <div class="controls span8">
                         <asp:DropDownList ID="ddlFiscalYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
             <div class="row-fluid divDateWise">
                <div class="span6">
                    <span class="control-label span4">
                        Start Date
                    </span>
                    <div class="controls span8">
                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="date" Width="100px"></asp:TextBox>
                    </div>
                </div>
                <div class="span6">
                    <span class="control-label span4">
                        End Date
                    </span>
                    <div class="controls span8">
                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="date" Width="100px"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row-fluid" style=" text-align:left">
                <div class="span6">
                    <span class="control-label span4">
                             
                    </span>
                    <div class="controls span8" style="float: left; margin-left: -0px;" >
                        <asp:Button ID="btnShow" runat="server" CssClass="btn btn-mini btn-primary" SkinID="btn-small" Font-Bold="true"
                        Text="Show" OnClick="btnShow_OnClick" />  
                    </div>
                </div>
                <div class="span6">
                        <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="true" ForeColor="Red" ></asp:Label>
                </div>
            </div>
        </div>
        </div>
        <div id="projectProfileMess" class="row-fluid">
            <asp:Label ID="lblMess" runat="server" Visible="false" Text="" CssClass="alert alert-info label-message"></asp:Label>
        </div>
        <div class="span6">
            <div>
                <CR:CrystalReportViewer ID="crpt" runat="server" AutoDataBind="true" Width="100%"/>
            </div>
        </div>
       
       </div>
    </form>
     <script type="text/javascript">
         $(document).ready(function () {
             var selectedValue = $('#<%= rbSelectYear.ClientID %> input:checked').val();

             if (selectedValue == 1) {
                 $('.divCalYear').show();
                 $('.divFisYear').hide();
                 $('.divDateWise').hide();
             }
             else if (selectedValue == 2) {
                 $('.divCalYear').hide();
                 $('.divFisYear').show();
                 $('.divDateWise').hide();
             }
             else {
                 $('.divCalYear').hide();
                 $('.divFisYear').hide();
                 $('.divDateWise').show();
             }
         });
    </script>
</asp:Content>
