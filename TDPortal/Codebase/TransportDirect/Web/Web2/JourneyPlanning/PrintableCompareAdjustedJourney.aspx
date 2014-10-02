<%@ Page Language="c#" Codebehind="PrintableCompareAdjustedJourney.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableCompareAdjustedJourney" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsCompareControl" Src="../Controls/JourneyDetailsCompareControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css,PrintableCompareAdjustedJourney.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableCompareAdjustedJourney" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <h1>
                    <asp:Label ID="headerLabel" runat="server"></asp:Label></h1>
            </div>
            <uc1:JourneyDetailsCompareControl ID="JourneyDetailsCompareControl1" runat="server">
            </uc1:JourneyDetailsCompareControl>
            <p>
            </p>
            <p class="txtseven">
                <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                <asp:Label ID="labelDate" runat="server"></asp:Label></p>
            <p class="txtseven">
                <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
            <p class="txtseven">
                <asp:Label ID="labelReferenceNumberTitle" runat="server"></asp:Label>
                <asp:Label ID="labelJourneyReferenceNumber" runat="server"></asp:Label></p>
        </form>
    </div>
</body>
</html>
