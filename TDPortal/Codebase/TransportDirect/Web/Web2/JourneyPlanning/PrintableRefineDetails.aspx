<%@ Page Language="c#" Codebehind="PrintableRefineDetails.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableRefineDetails" %>

<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css,PrintableRefineDetails.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableRefineDetails" method="post" runat="server">
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
                <div class="boxtypetwelve">
                    <div class="dmtitle">
                        <span class="txteightb">
                            <asp:Label ID="labelOutwardDirection" runat="server"></asp:Label></span> <span class="txteight">
                                <asp:Label ID="labelOutwardSummary" runat="server"></asp:Label></span>
                    </div>
                    <div class="dmview">
                        <uc1:JourneyDetailsControl ID="journeyDetailsControlOutward" runat="server"></uc1:JourneyDetailsControl>
                        <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlOutward" runat="server">
                        </uc1:JourneyDetailsTableControl>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="returnPanel" runat="server">
                <div class="boxtypetwelve">
                    <div class="dmtitle">
                        <span class="txteightb">
                            <asp:Label ID="labelReturnDirection" runat="server"></asp:Label></span> <span class="txteight">
                                <asp:Label ID="labelReturnSummary" runat="server"></asp:Label></span>
                    </div>
                    <div class="dmview">
                        <uc1:JourneyDetailsControl ID="journeyDetailsControlReturn" runat="server"></uc1:JourneyDetailsControl>
                        <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlReturn" runat="server">
                        </uc1:JourneyDetailsTableControl>
                    </div>
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
