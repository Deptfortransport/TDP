<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyCostsControl" Src="../Controls/CarJourneyCostsControl.ascx" %>
<%@ Page language="c#" Codebehind="PrintableJourneyFares.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.PrintableJourneyFares" %>
<%@ Register TagPrefix="uc1" TagName="JourneyFaresControl" Src="../Controls/JourneyFaresControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionSummaryControl" Src="../Controls/ExtensionSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" stylesheets="setup.css,jpstdprint.css,ticketRetailersPrint.css,ExtendAdjustReplanPrintable.css"
			runat="server"></cc1:headelementcontrol>
	</head>
	<body>
	<div class="CenteredContent">
		<form id="PrintableJourneyFares" method="post" runat="server">
			<uc1:printableheadercontrol id="printableHeaderControl" runat="server"></uc1:printableheadercontrol>
			<div class="boxtypeeightstd">
				<p class="txtsevenb"><asp:label id="labelPrinterFriendly" runat="server" cssclass="onscreen"></asp:label></p>
				<p class="txtsevenb"><asp:label id="labelInstructions" runat="server" cssclass="onscreen"></asp:label></p>
			</div>
			<div class="boxtypeeightstd">
			    <uc1:journeyssearchedforcontrol id="JourneysSearchedForControl1" runat="server"></uc1:journeyssearchedforcontrol>
			</div>
			<uc1:extensionsummarycontrol id="theExtensionSummaryControl" runat="server"></uc1:extensionsummarycontrol>
			<div class="boxtypeeightstd">
				<p>&nbsp;</p>
				<uc1:journeyplanneroutputtitlecontrol id="JourneyPlannerOutputTitleControl1" runat="server"></uc1:journeyplanneroutputtitlecontrol>
			</div>
			<asp:panel id="outwardPanel" runat="server">
				<div class="boxtypeeleven">
					<div class="jpsumout"><span class="txteightb"><asp:label id="labelOutwardJourneys" runat="server"></asp:label>&nbsp; 
							</span><span class="txteight">
							<asp:label id="labelOutwardJourneysFor" runat="server"></asp:label>
						</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<span class="txtseven">
							<asp:label id="labelOutwardBankHoliday" runat="server"></asp:label>
						</span></div>
					<uc1:summaryresulttablecontrol id="SummaryResultTableControlOutward" runat="server"></uc1:summaryresulttablecontrol>
					<uc1:findsummaryresultcontrol id="findSummaryResultTableControlOutward" runat="server" visible="true"></uc1:findsummaryresultcontrol></div>
				<asp:Panel id="outwardJourneyFaresPanel" runat="server">
					<div class="boxtypeeleven">
						<uc1:journeyfarescontrol id="OutboundJourneyFaresControl" runat="server"></uc1:journeyfarescontrol></div>
				</asp:Panel>
				<uc1:CarJourneyCostsControl id="outwardCarJourneyCostsControl" runat="server"></uc1:CarJourneyCostsControl>
				<p>&nbsp;</p>
			</asp:panel>
			<asp:literal id="literalNewPage" runat="server" visible="False" text='<DIV class="NewPage"></DIV>'></asp:literal>
			<asp:panel id="returnPanel" runat="server">
				<div class="boxtypeeleven">
					<div class="jpsumout"><span class="txteightb">
<asp:label id="labelReturnJourneys" runat="server"></asp:label>&nbsp;</span>
						<span class="txteight">
							<asp:label id="labelReturnJourneysFor" runat="server"></asp:label>
						</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<span class="txtseven">
							<asp:label id="labelReturnBankHoliday" runat="server"></asp:label>
						</span></div>
					<uc1:summaryresulttablecontrol id="SummaryResultTableControlReturn" runat="server"></uc1:summaryresulttablecontrol>
					<uc1:findsummaryresultcontrol id="findSummaryResultTableControlReturn" runat="server" visible="true"></uc1:findsummaryresultcontrol></div>
				<asp:panel id="returnJourneyFaresPanel" runat="server">
				<div class="boxtypeeleven">
					<uc1:journeyfarescontrol id="ReturnJourneyFaresControl" runat="server"></uc1:journeyfarescontrol></div>
				</asp:panel>
				<uc1:carjourneycostscontrol id="returnCarJourneyCostsControl" runat="server"></uc1:carjourneycostscontrol>
			</asp:panel>
			<div class="boxtypeeightstd">
				<p class="txtseven"><asp:label id="labelDateTitle" runat="server"></asp:label><asp:label id="labelDate" runat="server"></asp:label></p>
				<p class="txtseven"><asp:label id="labelUsernameTitle" runat="server"></asp:label><asp:label id="labelUsername" runat="server"></asp:label></p>
				<p class="txtseven"><asp:label id="labelReferenceNumberTitle" runat="server"></asp:label><asp:label id="labelJourneyReferenceNumber" runat="server"></asp:label></p>
			</div>
		</form>
		</div>
	</body>
</html>
