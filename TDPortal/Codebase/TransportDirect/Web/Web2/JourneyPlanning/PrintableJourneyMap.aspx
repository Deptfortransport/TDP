<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="PrintableJourneyMap.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyMap" %>

<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableMapControl" Src="../Controls/PrintableMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsDisplayControl" Src="../Controls/MapLocationIconsDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionSummaryControl" Src="../Controls/ExtensionSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstdprint.css,PrintableJourneyMap.aspx.css,MapPrint.css">
    </cc2:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="PrintableJourneyMap" method="post" runat="server">
            <uc1:PrintableHeaderControl ID="printableHeaderControl" runat="server"></uc1:PrintableHeaderControl>
            <div class="boxtypeeightstd">
                <p class="txtsevenb">
                    <asp:Label ID="labelPrinterFriendly" CssClass="onscreen" runat="server"></asp:Label></p>
                <p class="txtsevenb">
                    <asp:Label ID="labelInstructions" CssClass="onscreen" runat="server"></asp:Label></p>
            </div>
            <div class="boxtypeeightstd">
                <uc1:JourneysSearchedForControl ID="JourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
                <uc1:ExtensionSummaryControl ID="theExtensionSummaryControl" runat="server"></uc1:ExtensionSummaryControl>
            </div>
            <asp:Panel ID="panelFindFareSteps" runat="server" Visible="false">
                <div class="boxtypeeightstd">
                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p>&nbsp;</p>
                <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server">
                </uc1:JourneyPlannerOutputTitleControl>
                <uc1:FindFareSelectedTicketLabelControl ID="findFareSelectedTicketLabelControl" runat="server">
                </uc1:FindFareSelectedTicketLabelControl>
            </div>
            <asp:Panel ID="panelMapOutward" runat="server" Visible="False">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                    </div>
                    <uc1:SummaryResultTableControl ID="summaryResultTableControlOutward" runat="server">
                    </uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
                <div class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="mapOutward" runat="server"></uc1:PrintableMapControl>
                </div>
            </asp:Panel>
            <asp:Literal ID="literalNewPage" runat="server" Visible="False" Text='<div class="NewPage"></div>'></asp:Literal><asp:Panel
                ID="panelMapReturn" runat="server" Visible="False">
                <div class="boxtypeeleven">
                    <div class="jpsumout">
                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                    </div>
                    <uc1:SummaryResultTableControl ID="summaryResultTableControlReturn" runat="server"></uc1:SummaryResultTableControl>
                    <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                        Visible="true"></uc1:FindSummaryResultControl>
                </div>
                <div class="boxtypeeightstd">
                    <uc1:PrintableMapControl ID="mapReturn" runat="server"></uc1:PrintableMapControl>
                </div>
            </asp:Panel>
            <div class="boxtypeeightstd">
                <p>
                    <asp:Label ID="labelDateTimeTitle" runat="server"></asp:Label><asp:Label ID="labelDateTime"
                        runat="server"></asp:Label></p>
                <p>
                    <asp:Label ID="labelUsernameTitle" runat="server"></asp:Label><asp:Label ID="labelUsername"
                        runat="server"></asp:Label></p>
                <p>
                    <asp:Label ID="labelReferenceTitle" runat="server"></asp:Label><asp:Label ID="labelReference"
                        runat="server"></asp:Label></p>
                <p>
                </p>
            </div>
        </form>
    </div>
</body>
</html>
