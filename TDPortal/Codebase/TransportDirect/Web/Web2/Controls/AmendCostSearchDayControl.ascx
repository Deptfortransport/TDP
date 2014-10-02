<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendCostSearchDayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendCostSearchDayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:label id="infoLabel" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label>
<table>
	<tr id="outwardDateRow" runat="server">
		<td id="outwardLabelCell" runat="server"><asp:label id="outwardLabel" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
		<td class="amenddaybuttoncell"><cc1:tdbutton id="prevOutwardDayButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
		<td class="amenddaylabelcell" align="center"><asp:label id="currentOutwardDateLabel" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
		<td class="amenddaybuttoncell"><cc1:tdbutton id="nextOutwardDayButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
	</tr>
	<tr id="inwardDateRow" runat="server">
		<td id="inwardLabelCell" runat="server"><asp:label id="inwardLabel" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
		<td class="amenddaybuttoncell"><cc1:tdbutton id="prevInwardDayButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
		<td class="amenddaylabelcell" align="center"><asp:label id="currentInwardDateLabel" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
		<td class="amenddaybuttoncell"><cc1:tdbutton id="nextInwardDayButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
	</tr>
</table>
