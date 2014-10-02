<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendCarDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendCarDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="0" cellpadding="3">
	<tr>
		<td style="WIDTH: 130px" align="left">
		<asp:label id="iHaveaLabel" cssclass="txtsevenb" runat="server"></asp:label></td>
		<td>
			<asp:dropdownlist id="listCarSize" runat="server"></asp:dropdownlist>
			<asp:label id="sizedLabel" cssclass="txtsevenb" runat="server"></asp:label>&nbsp;
			<asp:dropdownlist id="listFuelType" runat="server"></asp:dropdownlist>
			<asp:label id="carLabel" cssclass="txtsevenb" runat="server"></asp:label>
		</td>
		<td valign="bottom" rowspan="3">
			<cc1:tdbutton id="buttonOK" runat="server"></cc1:tdbutton>
		</td>
	</tr>
	<tr>
		<td style="WIDTH: 130px" valign="top">
		<asp:label id="myFuelConsumptionIsLabel" cssclass="txtsevenb" runat="server"></asp:label></td>
		<td>
		<asp:radiobuttonlist id="fuelConsumptionSelectRadio" cssclass="txtsevenb" runat="server" repeatdirection="Vertical"
			repeatlayout="Flow"></asp:radiobuttonlist>&nbsp;
		<asp:textbox id="textFuelConsumption" runat="server" columns="10"></asp:textbox>&nbsp;<asp:dropdownlist id="listFuelConsumptionUnit" runat="server"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td align="left" colspan="2"><asp:label id="displayFuelConsumptionErrorLabel" cssclass="txtsevenb" runat="server"></asp:label></td>
	</tr>
</table>
