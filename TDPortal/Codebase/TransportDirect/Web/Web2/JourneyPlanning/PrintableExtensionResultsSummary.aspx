<%@ Page Language="c#" Codebehind="PrintableExtensionResultsSummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableExtensionResultsSummary" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyLineControl" Src="../Controls/ExtendJourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,ExtendAdjustReplanPrintable.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableExtensionResultsSummary" method="post" runat="server">
            <!-- header -->
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypesixteen">
                <h1>
                    <asp:Label ID="labelTitle" runat="server" CssClass="ExtendedLabels" EnableViewState="false"></asp:Label></h1>
                <br/>
                <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven" EnableViewState="false"></asp:Label>
            </div>
            <div class="boxtypewhitebackground">
                <uc1:ExtendJourneyLineControl ID="extendJourneyLineControl" runat="server" EnableViewState="false" />
            </div>
            <asp:Panel ID="summaryPanel" CssClass="boxtypelargetwo" runat="server">
                <asp:Panel ID="outwardSummaryPanel" runat="server">
                    <div class="boxtypetwentyfive">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"
                            EnableViewState="false"></uc1:ResultsTableTitleControl>
                    </div>
                    <div class="boxtypetwentysix">
                        <uc1:ResultsSummaryControl ID="outwardResultsSummaryControl" runat="server" EnableViewState="false">
                        </uc1:ResultsSummaryControl>
                    </div>
                </asp:Panel>
                <asp:Panel ID="returnSummaryPanel" runat="server">
                    <div class="boxtypetwentyfive">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"
                            EnableViewState="false"></uc1:ResultsTableTitleControl>
                    </div>
                    <div class="boxtypetwentysix">
                        <uc1:ResultsSummaryControl ID="returnResultsSummaryControl" runat="server" EnableViewState="false">
                        </uc1:ResultsSummaryControl>
                    </div>
                </asp:Panel>
                <div class="spacer2">
                    &nbsp;</div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="labelDateTitle" runat="server" EnableViewState="false"></asp:Label>
                    <asp:Label ID="labelDate" runat="server" EnableViewState="false"></asp:Label></p>
            </div>
        </form>
    </div>
</body>
</html>
