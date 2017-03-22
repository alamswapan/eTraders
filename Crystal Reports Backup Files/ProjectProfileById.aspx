<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
    CodeBehind="ProjectProfileById.aspx.cs" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.ProjectProfileById" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Project Profile Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server">
    <div id="projectProfileByIdPage">
        <div id="projectProfileByIdMess" class="row-fluid">
            <asp:Label ID="lblMess" runat="server" Visible="false" Text="" CssClass="alert alert-info label-message"></asp:Label>
        </div>
        <div id="projectProfileByIdCrystalReport">
            <CR:CrystalReportViewer ID="crptProjectProfile" runat="server" AutoDataBind="true"
                Width="100%" Height="100%" />
        </div>
    </div>
    </form>
</asp:Content>
