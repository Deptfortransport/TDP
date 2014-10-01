<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="Direction.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.Direction" %>
<%@ Register Namespace="TDP.UserPortal.TDPMobile.Controls" TagPrefix="uc1" assembly="tdp.userportal.tdpmobile" %> 
<%@ Register src="~/Controls/DetailsCycleControl.ascx" tagname="CycleLegControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
    
    <div class="legCycle" id="legCycle" runat="server" visible="false">
         <uc1:CycleLegControl ID="cycleLeg" runat="server" />
    </div>

</asp:Content>