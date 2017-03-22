<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewlySignedAidAgreements.aspx.cs"
    MasterPageFile="~/Views/Shared/Site.Master" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.NewlySignedAidAgreements" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Newly Signed Aid Agreements
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="Form1" runat="server" >
     <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <Triggers>
            <asp:PostBackTrigger ControlID="crptNewlySignedAidAgreements" />
            <asp:AsyncPostBackTrigger ControlID="btnShow" />
        </Triggers>
     <ContentTemplate>--%>
    <div class="widget-box">
   
        <div class="widget-header">
            <h4> Newly Signed Aid Agreements</h4>
        </div>
        <div class="widget-body">
        <div class="widget-main">
            <div class="row-fluid">
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</asp:Content>
