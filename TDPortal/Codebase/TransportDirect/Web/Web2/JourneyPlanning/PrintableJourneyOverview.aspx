<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PrintableJourneyOverview.aspx.cs"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyOverview" %>

<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindOverviewResultControl" Src="../Controls/FindOverviewResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css">
    </cc1:HeadElementControl>
   
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneySummary" runat="server">
            <div>
                <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
                <div class="boxtypeeightstd">
                    <p class="txtsevenb">
                        <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                    <p class="txtsevenb">
                        <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
                </div>
                <div>
                    <div class="boxtypeeightstd">
                        <uc1:JourneysSearchedForControl ID="theJourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
                    </div>
                    <div id="boxtypesixteen">
                        <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                        </uc1:JourneyPlannerOutputTitleControl>
                        <div id="panelInstructions" style="margin-bottom: 5px" runat="server">
                            <asp:Label ID="label1" runat="server" CssClass="txtseven"></asp:Label></div>
                    </div>
                    <asp:Panel ID="outwardPanel" runat="server">
                        <a name="OutwardJourneys"></a>
                        <div class="boxtypejourneyoverviewcontrol">
                            <asp:Label ID="labelJourneyOptionsTableDescription" runat="server" CssClass="screenreader"></asp:Label>
                            <table id="overviewOutwardTable" runat="server" class="jpsumoutfinda">
                                <tr>
                                    <td>
                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                                    </td>
                                </tr>
                            </table>
                            <uc1:FindOverviewResultControl ID="findOverviewResultTableControlOutward" runat="server"
                                Visible="true"></uc1:FindOverviewResultControl>
                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel ID="returnPanel" runat="server">
                        <a name="ReturnJourneys"></a>
                        <div class="boxtypejourneyoverviewcontrol">
                            <asp:Table ID="overviewReturnTable" runat="server" CssClass="jpsumoutfinda">
                                <asp:TableRow>
                                    <asp:TableCell ID="cellButtonSummary">
                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <uc1:FindOverviewResultControl ID="findOverviewResultTableControlReturn" runat="server"
                                Visible="true"></uc1:FindOverviewResultControl>
                        </div>
                    </asp:Panel>
                </div>
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
            </div>
        </form>
    </div>
</body>
</html>
