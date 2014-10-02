<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyChangeSearchControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyChangeSearchControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div id="backButtons" class="boxjourneychangesearchback" runat="server">
	<cc1:tdbutton id="backToInitialResultsSummaryButton" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="backToExtensionSummaryButton" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="buttonAmendExtension" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<!--<cc1:tdbutton id="buttonBackJourneyOverview" runat="server" style="vertical-align: top"></cc1:tdbutton>-->
	
</div>
<div class ="boxjourneychangesearchchangetwo">
    <cc1:TDButton ID="backButton" runat="server" style="vertical-align:top" Visible="false" />
    <cc1:tdbutton id="buttonNewSearch" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="buttonAmendJourney" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:TDButton ID="buttonReturnJourney" runat="server" style="vertical-align:top" Visible="false"></cc1:TDButton>
</div>
<div class="boxjourneychangesearchchange">
	
	<cc1:tdbutton id="undoLastExtensionButton" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="undoThisExtensionButton" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="undoAllExtensionsButton" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<cc1:tdbutton id="buttonRedoExtension" runat="server" style="vertical-align: top"></cc1:tdbutton>
	<uc1:printerfriendlypagebuttoncontrol id="printerFriendlyPageButton" runat="server"></uc1:printerfriendlypagebuttoncontrol>
	<cc1:helpcustomcontrol id="pageHelpCustomControl" runat="server" style="vertical-align: top"></cc1:helpcustomcontrol>
	<cc1:helpbuttoncontrol id="pageHelpButton" runat="server" Visible="true" EnableViewState="false" ></cc1:helpbuttoncontrol>
</div>
