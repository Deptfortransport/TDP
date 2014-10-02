<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AdjustOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AdjustOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="txtseven">&nbsp;
	<asp:label id="adjustPreText" runat="server" enableviewstate="false"></asp:label>
	<cc1:scriptabledropdownlist id="adjustTimings" runat="server" enableclientscript="true" scriptname="JourneyAdjustElementSelection"
		enableviewstate="true"></cc1:scriptabledropdownlist>
	<cc1:scriptabledropdownlist id="adjustLocations" runat="server" enableclientscript="true" scriptname="JourneyAdjustElementSelection"
		enableviewstate="true"></cc1:scriptabledropdownlist>
</div>
<br/>
<div>
	<span class="floatright">
		<cc1:tdbutton id="nextButton" runat="server" enableviewstate="false"></cc1:tdbutton></span>&nbsp;
</div>
