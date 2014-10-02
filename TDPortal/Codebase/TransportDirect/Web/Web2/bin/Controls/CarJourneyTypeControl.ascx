<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarJourneyTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarJourneyTypeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="2" cellpadding="2" width="100%" border="0">
	<tr>
		<td><asp:label id="labelCarJourneyType" runat="server" CssClass="txtsevenb"></asp:label></td>
		<td align="right" rowspan="2"><cc1:tdimage id="imageCar" runat="server" ></cc1:tdimage>&nbsp;&nbsp;&nbsp;&nbsp;</td>
	</tr>
	<tr>
		<td><asp:label id="labelFuelConsumption" runat="server" CssClass="txtseven"></asp:label>
			&nbsp;
			<asp:label id="labelFuelCost" runat="server" CssClass="txtseven"></asp:label>
		</td>
	</tr>
</table>
