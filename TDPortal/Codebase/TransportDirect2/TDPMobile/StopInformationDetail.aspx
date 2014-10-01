<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="StopInformationDetail.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.StopInformationDetail" %>
<%@ Register src="~/Controls/ServiceDetailsControl.ascx" tagname="ServiceDetailsControl" tagprefix="uc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="contentMain" runat="server">
    <asp:Panel ID="stopInfoDetailContainer" runat="server" CssClass="stopInfoDetail">

        <uc1:ServiceDetailsControl ID="serviceDetailsControl" runat="server" />

    </asp:Panel>
</asp:Content>
