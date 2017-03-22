<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SectorProfile.aspx.cs"
    MasterPageFile="~/Views/Shared/Site.Master" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.SectorProfile" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sector Profile Information
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <form id="Form1" runat="server">
    <div class="widget-box">
        <div class="widget-header">
            <h4> Sector Profile Information</h4>
        </div>
        <div class="widget-body">
        <div class="widget-main">
            <div class="row-fluid">
                <div class="span6">
                    <span class="control-label span4">
                       Financial Year
                    </span>
                    <div class="controls span8">
                        <asp:DropDownList ID="ddlYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="span6">
                    <span class="control-label span4">
                         Sector
                    </span>
                    <div class="controls span8">
                         <asp:DropDownList ID="ddlSector" runat="server"></asp:DropDownList>
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
       
        <div class="span6">
            <div>
                <CR:CrystalReportViewer ID="crpt" runat="server" AutoDataBind="true" Width="100%" />
                
            </div>
        </div>
       
       </div>
    </form>
</asp:Content>
