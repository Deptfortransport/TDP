<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsDisplayControl" Src="../Controls/MapLocationIconsDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableJourneyMapInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyMapInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableJourneyMapInput.aspx.css">
    </cc2:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyMapInput" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <h1>
                    <asp:Label ID="labelMapTitle" runat="server"></asp:Label>&nbsp;
                    <asp:Label ID="labelLocationDescription" runat="server"></asp:Label></h1>
            </div>
            <uc1:PrintableMapControl ID="map" runat="server"></uc1:PrintableMapControl>
            <p>
                <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label><asp:Label ID="labelDateTime"
                    runat="server"></asp:Label></p>
            <p>
                <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername"
                    runat="server"></asp:Label></p>
        </form>
    </div>
</body>
</html>
