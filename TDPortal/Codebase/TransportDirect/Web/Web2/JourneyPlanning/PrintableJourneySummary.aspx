<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionSummaryControl" Src="../Controls/ExtensionSummaryControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="PrintableJourneySummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneySummary" %>

<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneySummary" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <!-- End of Transport Direct Header info -->
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl ID="theJourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            <asp:Panel ID="panelFindFareSteps" runat="server" visible="false">
                <div class="boxtypeeightstd">
                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                </div>
            </asp:Panel>
            <uc1:ExtensionSummaryControl ID="theExtensionSummaryControl" runat="server"></uc1:ExtensionSummaryControl>
            <div class="boxtypeeightstd">
                <p>
                    &nbsp;</p>
                
                    <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                    </uc1:JourneyPlannerOutputTitleControl>
                
            </div>
            <div id="boxtypesixteen">
                <uc1:FindFareSelectedTicketLabelControl ID="findFareSelectedTicketLabelControl" runat="server">
                </uc1:FindFareSelectedTicketLabelControl>
            </div>
            <asp:Panel ID="outwardPanel" runat="server">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <table id="summaryOutwardTable" runat="server">
                            <tr>
                                <td>
                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc1:SummaryResultTableControl ID="summaryResultTableControlOutward" runat="server">
                    </uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
            </asp:Panel>
            <asp:Panel ID="returnPanel" runat="server">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <table id="summaryReturnTable" runat="server">
                            <tr>
                                <td>
                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <uc1:SummaryResultTableControl ID="summaryResultTableControlReturn" runat="server"></uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p class="txtseven">
                    <asp:Label ID="LabelFootnote" runat="server"></asp:Label></p>
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
