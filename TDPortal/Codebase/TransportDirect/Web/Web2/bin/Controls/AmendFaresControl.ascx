<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendFaresControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendFaresControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="0" width="100%">
	<tr>
		<td class="txtseven" colspan="5" height="20"><asp:label id="labelInfo" runat="server"></asp:label>&nbsp;</td>
	</tr>
	<tr>
		<td class="txtseven"><asp:label id="labelShowType" associatedcontrolid="dropDownListItineraryTypeSelect" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven"><asp:dropdownlist id="dropDownListItineraryTypeSelect" runat="server"></asp:dropdownlist>
		<asp:label id="labelFaresItinerary" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven" align="right"><asp:label id="labelRailcard" associatedcontrolid="dropDownListRailcardSelect" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven"><asp:dropdownlist id="dropDownListRailcardSelect" runat="server"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td class="txtseven"><asp:label id="labelShowAge" associatedcontrolid="dropDownListAgeSelect" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven"><asp:dropdownlist id="dropDownListAgeSelect" runat="server"></asp:dropdownlist>
		<asp:label id="labelFaresAge" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven" align="right"><asp:label id="labelCoachCard" associatedcontrolid="dropDownListCoachCardSelect" runat="server" enableviewstate="False"></asp:label></td>
		<td class="txtseven"><asp:dropdownlist id="dropDownListCoachCardSelect" runat="server"></asp:dropdownlist></td>
		<td valign="bottom" rowspan="1"><cc1:tdbutton id="buttonOK" runat="server" enableviewstate="False"></cc1:tdbutton></td>
	</tr>
</table>
