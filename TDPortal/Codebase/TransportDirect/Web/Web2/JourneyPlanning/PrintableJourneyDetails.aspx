<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="../Controls/CarJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="../Controls/CarSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarAllDetailsControl" Src="../Controls/CarAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionSummaryControl" Src="../Controls/ExtensionSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>

<%@ Page Language="c#" Codebehind="PrintableJourneyDetails.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyDetails" %>

<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,MapPrint.css,emissionsprint.css">
    </cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyDetails" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl ID="journeysSearchedForControl" runat="server"></uc1:JourneysSearchedForControl>
            </div>
            <asp:Panel ID="panelFindFareSteps" runat="server" visible="false">
                <div class="boxtypeeightstd">
                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                </div>
            </asp:Panel>
            <uc1:ExtensionSummaryControl ID="extensionSummaryControl" runat="server"></uc1:ExtensionSummaryControl>
            <div class="boxtypeeightstd">
                <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                </uc1:JourneyPlannerOutputTitleControl>
            </div>
            <uc1:FindFareSelectedTicketLabelControl ID="findFareSelectedTicketLabelControl" runat="server">
            </uc1:FindFareSelectedTicketLabelControl>
            <asp:Panel ID="panelOutward" runat="server">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                        <asp:Label ID="labelOutwardJourneys" runat="server"></asp:Label><span class="txtseven">
                            <asp:Label ID="labelOutwardJourneysFor" runat="server"></asp:Label>
                            <asp:Label ID="labelOutwardBankHoliday" runat="server"></asp:Label></span></div>
                    <uc1:SummaryResultTableControl ID="SummaryResultTableControlOutward" runat="server">
                    </uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
                <div id="divMapOutward" runat="server" class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="PrintableOutwardMap" runat="server"></uc1:PrintableMapControl>
                </div>
                <asp:Panel ID="outwardDetailPanel" runat="server">
                    <div class="boxtypetwelve">
                        <div class="dmtitle">
                            <span class="txteightb">
                                <asp:Label ID="labelDetailsOutwardJourney" runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="labelDetailsOutwardDisplayNumber" runat="server"></asp:Label>&nbsp;
                                <asp:Label ID="labelCarOutward" runat="server"></asp:Label></span>&nbsp;
                        </div>
                        <p></p>
                    
                        <uc1:JourneyDetailsControl ID="JourneyDetailsControlOutward" runat="server"></uc1:JourneyDetailsControl>
                        <uc1:CarAllDetailsControl ID="CarAllDetailsControlOutward" runat="server"></uc1:CarAllDetailsControl>
                        <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlOutward" runat="server"></uc1:JourneyDetailsTableControl>
                    </div>
                </asp:Panel>
                <asp:Panel ID="outwardEmissionsPanel" runat="server" Visible="false">
                    <uc1:JourneyEmissionsCompareControl id="journeyEmissionsCompareControlOutward" runat="server"></uc1:JourneyEmissionsCompareControl>
                </asp:Panel>
            </asp:Panel>
            <asp:Literal ID="literalNewPage" runat="server" Visible="False" Text='<div class="NewPage"></div>'></asp:Literal>
            <asp:Panel ID="returnPanel" runat="server">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                    </div>
                    <uc1:SummaryResultTableControl ID="SummaryResultTableControlReturn" runat="server"></uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
                <div id="divMapReturn" runat="server" class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="PrintableReturnMap" runat="server"></uc1:PrintableMapControl>
                </div>
                <div class="boxtypetwelve">
                    <div class="dmtitle">
                        <span class="txteightb">
                            <asp:Label ID="labelDetailsReturnJourney" runat="server"></asp:Label>&nbsp;
                            <asp:Label ID="labelDetailsReturnDisplayNumber" runat="server"></asp:Label>&nbsp;
                            <asp:Label ID="labelCarReturn" runat="server"></asp:Label></span>&nbsp;
                    </div>

                    <uc1:JourneyDetailsControl ID="JourneyDetailsControlReturn" runat="server"></uc1:JourneyDetailsControl>
                    <uc1:CarAllDetailsControl ID="CarAllDetailsControlReturn" runat="server"></uc1:CarAllDetailsControl>
                    <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlReturn" runat="server">
                    </uc1:JourneyDetailsTableControl>

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
