<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendStopoverControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendStopoverControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!--<table border="0" cellpadding="0" cellspacing="0">-->

<table border="0" cellpadding="0" cellspacing="0" runat="server" id="tableAmendStopoverControl">	
	<tr>
		<td colspan="3">
			<div class="txtseven"><asp:label id="labelTitleOutward" runat="server"></asp:label></div>
		</td>
	</tr>
	<tr>
		<td class="findafromcolumn" align="right">
			<asp:label id="labelOutward" runat="server" cssclass="txtsevenb"></asp:label>
		</td>
		<td valign="bottom">
			<table runat="server" id="tableOutwardStopover" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="middle">
						<asp:label id="labelOutwardReadonly" runat="server" cssclass="txtseven"></asp:label>
						<asp:label id="labelSROutwardDays" associatedcontrolid="dropListOutwardDays" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListOutwardDays" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelOutwardDays" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
					<td valign="middle" >
						<asp:label id="labelSROutwardHours" associatedcontrolid="dropListOutwardHours" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListOutwardHours" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelOutwardHours" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
					<td valign="middle">
						<asp:label id="labelSROutwardMinutes" associatedcontrolid="dropListOutwardMinutes" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListOutwardMinutes" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelOutwardMinutes" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
				</tr>
			</table>
		</td>
		<td valign="bottom" align="center" width="13%" rowspan="2"><cc1:tdbutton id="buttonSearch" runat="server"></cc1:tdbutton></td>
	</tr>
	<tr runat="server" id="returnTitleRow">
		<td colspan="3">
			<div class="txtseven"><asp:label id="labelTitleReturn" runat="server"></asp:label></div>
		</td>
	</tr>
	<tr runat="server" id="returnRow">
		<td class="findafromcolumn" align="right">
			<asp:label id="labelReturn" runat="server" cssclass="txtsevenb"></asp:label>
		</td>
		<td valign="bottom">
			<table runat="server" id="tableReturnStopover" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="middle">
						<asp:label id="labelReturnReadonly" runat="server" cssclass="txtseven"></asp:label>
						<asp:label id="labelSRReturnDays" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListReturnDays" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelReturnDays" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
					<td valign="middle">
						<asp:label id="labelSRReturnHours" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListReturnHours" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelReturnHours" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
					<td valign="middle">
						<asp:label id="labelSRReturnMinutes" runat="server" cssclass="screenreader"></asp:label>
						<asp:dropdownlist id="dropListReturnMinutes" runat="server" cssclass="txtseven" width="40px"></asp:dropdownlist>
						<asp:label id="labelReturnMinutes" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
