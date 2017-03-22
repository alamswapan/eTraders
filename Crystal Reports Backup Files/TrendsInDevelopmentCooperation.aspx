﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrendsInDevelopmentCooperation.aspx.cs"
    MasterPageFile="~/Views/Shared/Site.Master" Inherits="AIMS_UNDP.Web.Areas.AIMS.ReportViews.TrendsInDevelopmentCooperation" %>

   <%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="Server">
    Trends in Development Cooperation
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="Form1" runat="server" >
    <div class="widget-box">
        <div class="widget-header">
            <h4>Trends in Development Cooperation</h4>
        </div>
        <div class="widget-body">
        <div class="widget-main">
            <div class="row-fluid">
                
                <div class="span6">
                    <span class="control-label span4">
                       Calendar Type
                    </span>
                    <div class="span8">
                    <asp:RadioButtonList ID="rbSelectYear" runat="server" RepeatDirection="Horizontal" Width="100%" 
                            AutoPostBack="true" OnSelectedIndexChanged="rbSelectYear_OnSelectedIndexChanged">
                        <asp:ListItem Text="Calender Year" Value="1" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="Financial Year" Value="2" ></asp:ListItem>
                    </asp:RadioButtonList>

                    </div>
                </div>
                <div class="span6">
                     <span class="control-label span4">
                      Budget Type
                    </span>
                    <div class="span8">
                    <asp:RadioButtonList ID="rbSelectBudget" runat="server" RepeatDirection="Horizontal" Width="100%" >
                        <asp:ListItem Text="On-Budget" Value="1" Selected="True" ></asp:ListItem>
                        <asp:ListItem Text="Off-Budget" Value="0" ></asp:ListItem>
                        <asp:ListItem Text="Both" Value="2" ></asp:ListItem>
                    </asp:RadioButtonList>

                    </div>
                </div>
            </div>
            
            <div class="row-fluid">
                <div class="span6 divCalYear">
                    <span class="control-label span4">
                       Start Calendar Year
                    </span>
                    <div class="controls span8">
                        <asp:DropDownList ID="ddlStartCalYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="span6 divCalYear">
                    <span class="control-label span4">
                       End Calendar Year
                    </span>
                    <div class="controls span8">
                        <asp:DropDownList ID="ddlEndCalYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                
            </div>
            <div class="row-fluid">
                <div class="span6 divFisYear" >
                    <span class="control-label span4">
                         Start Financial Year
                    </span>
                    <div class="controls span8">
                         <asp:DropDownList ID="ddlStartFisYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <div class="span6 divFisYear" >
                    <span class="control-label span4">
                         End Financial Year
                    </span>
                    <div class="controls span8">
                         <asp:DropDownList ID="ddlEndFisYear" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="span6">
                    <span class="control-label span4">
                         Project
                    </span>
                   <div class="controls span8" >
                        <asp:DropDownList ID="ddlProject" runat="server"></asp:DropDownList>
                   </div>
                </div>
                <div class="span6">
                    <span class="control-label span4">
                        Amount Type
                    </span>
                   <div class="controls span8" >
                        <asp:DropDownList ID="ddlAmountCategory" runat="server">
                            <asp:ListItem Value="1" Text="Actual Disbursement Amount" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Plan Disbursement Amount" ></asp:ListItem>
                            <asp:ListItem Value="3" Text="Funding Commitment Amount"></asp:ListItem>
                        </asp:DropDownList>
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
                <CR:CrystalReportViewer ID="crpt" runat="server" AutoDataBind="true" Width="100%"
                ReuseParameterValuesOnRefresh="True" />
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
            } else {
                $('.divCalYear').hide();
                $('.divFisYear').show();
            }
        });
    </script>
</asp:Content>
