<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectProfile.aspx.cs"
    MasterPageFile="~/Views/Shared/Site.Master" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.ProjectProfile" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Project Profile Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server">
    <div id="projectProfilePage">
        <div id="projectProfileFilter">
            <div class="widget-box">
                <div class="widget-header">
                    <h5>
                        Selection Criteria</h5>
                </div>
                <div class="widget-body">
                    <div class="widget-main">
                        <div class="row-fluid form">
                            <div class="row-fluid">
                                <div class="content">
                                    <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="row-fluid">
                                <div class="span2">
                                    <label class="control-label" for="ProjectId">
                                        Project</label></div>
                                <div class="span4">
                                    <asp:DropDownList ID="ddlProject" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="hr">
                            </div>
                            <div class="row-fluid">
                                <div class="pull-right">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-mini btn-primary" SkinID="btn-small"
                                        Font-Bold="true" Text="Show" OnClick="btnShow_OnClick" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="projectProfileMess" class="row-fluid">
            <asp:Label ID="lblMess" runat="server" Visible="false" Text="" CssClass="alert alert-info label-message"></asp:Label>
        </div>
        <div id="projectProfileCrystalReport">
            <CR:CrystalReportViewer ID="crptProjectProfile" runat="server" AutoDataBind="true"
                Width="100%" Height="100%" />
        </div>
    </div>
    </form>
</asp:Content>
