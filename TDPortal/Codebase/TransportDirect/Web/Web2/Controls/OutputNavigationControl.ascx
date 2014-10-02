<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OutputNavigationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.OutputNavigationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<a name="JourneyButtons"></a>
<div class="outputnavdiv">
	<cc1:tdbutton id="buttonDetails" runat="server"></cc1:tdbutton>
	<asp:image ImageAlign="Top" id="imageDetails" runat="server" visible="false"></asp:image>
	<cc1:tdbutton id="buttonSummary" runat="server"></cc1:tdbutton>
	<asp:image ImageAlign="Top" id="imageSummary" runat="server" visible="false"></asp:image>	
	<cc1:tdbutton id="buttonMaps" runat="server"></cc1:tdbutton>
	<asp:image ImageAlign="Top" id="imageMaps" runat="server" visible="false"></asp:image>
	<cc1:tdbutton id="buttonCosts" runat="server"></cc1:tdbutton>
	<asp:image ImageAlign="Top" id="imageCosts" visible="false" runat="server"></asp:image>
	<cc1:tdbutton id="buttonRefineJourney" runat="server"></cc1:tdbutton>
	<cc1:tdbutton id="buttonCheckC02" runat="server"></cc1:tdbutton>
</div>
