<%@ Page Language="c#" Codebehind="PrintableAdjustFullItinerarySummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableAdjustFullItinerarySummary" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css,PrintableAdjustFullItinerarySummary.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableAdjustFullItinerarySummary" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypesixteen">
                <h1>
                    <asp:Label ID="labelTitle" CssClass="ExtendedLabels" runat="server"></asp:Label></h1>
                <br />
                <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
            </div>
            <div class="spacer1">
                &nbsp;</div>
            <asp:Panel ID="summaryPanel" CssClass="boxtypelargetwo" runat="server">
                <div>
                    <div class="boxtypetwentyfive">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server" />
                    </div>
                    <div class="boxtypetwentysix">
                        <uc1:ResultsSummaryControl ID="combinedResultsSummaryControl" runat="server" />
                    </div>
                    <div class="spacer2">
                        &nbsp;</div>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelDate" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelUsername" runat="server"></asp:Label></p>
                <p class="txtseven">
                    <asp:Label ID="labelReferenceNumberTitle" runat="server"></asp:Label>
                    <asp:Label ID="labelJourneyReferenceNumber" runat="server"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
