<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFareGotoTicketRetailerControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareGotoTicketRetailerControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<div id="boxfindfaregototicketretailercontrol">
<div style="padding-bottom: 3px; text-align: right">
			<cc1:tdbutton id="ticketRetailersButton" runat="server"></cc1:tdbutton>
</div>
<div style="padding-bottom: 3px; text-align: right">
			<cc1:tdbutton id="ticketRetailersOutwardSingleButton" runat="server"></cc1:tdbutton>
</div>
<div style="padding-bottom: 3px; text-align: right">
			<cc1:tdbutton id="ticketRetailersInwardSingleButton" runat="server"></cc1:tdbutton>
</div>
<div style="padding-bottom: 3px; text-align: right">
			<cc1:tdbutton id="ticketRetailersBothSingleButton" runat="server"></cc1:tdbutton>
</div>
</div>