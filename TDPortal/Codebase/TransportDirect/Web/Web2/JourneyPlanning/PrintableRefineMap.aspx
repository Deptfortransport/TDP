<%@ Page Language="c#" Codebehind="PrintableRefineMap.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableRefineMap" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css,MapPrint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableRefineMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div id="boxtypelargeeight">
                <h1>
                    <asp:Label ID="labelTitle" CssClass="ExtendedLabels" runat="server"></asp:Label>
                </h1>
                <br />
                <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
            </div>
            <asp:Panel ID="outwardPanel" runat="server">
                <div class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="journeyMapControlOutward" runat="server"></uc1:PrintableMapControl>
                </div>
            </asp:Panel>
            <asp:Panel ID="returnPanel" runat="server">
                <div class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="journeyMapControlReturn" runat="server"></uc1:PrintableMapControl>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelDate" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
