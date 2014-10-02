<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendAmendControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendAmendControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellspacing="0" summary="Amend date and time table">
	<tr>
		<td class="txtsevenb"><asp:label id="labelLeaving" runat="server"></asp:label></td>
		<td><asp:label id="labelSRLeavingDate" associatedcontrolid="dropDownListLeavingDate" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListLeavingDate" runat="server" width="45" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRLeavingMonthYear" associatedcontrolid="dropDownListLeavingMonthYear" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListLeavingMonthYear" runat="server" width="120px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRLeavingConstraint" associatedcontrolid="dropDownListLeavingTimeConstraint" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListLeavingTimeConstraint" runat="server" width="110px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRLeavingHoursPre" associatedcontrolid="dropDownListLeavingHour" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListLeavingHour" runat="server" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRLeavingHoursPost" runat="server" cssclass="screenreader"></asp:label>
			<asp:label id="labelSRLeavingMinutesPre" associatedcontrolid="dropDownListLeavingMinute" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListLeavingMinute" runat="server" width="40px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRLeavingMinutesPost" runat="server" cssclass="screenreader"></asp:label></td>
		
	</tr>
	<tr>
		<td><asp:label cssclass="txtsevenb" id="labelReturning" runat="server"></asp:label></td>
		<td><asp:label id="labelSRReturningDate" associatedcontrolid="dropDownListReturningDate" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListReturningDate" runat="server" width="45px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRReturningMonthYear" associatedcontrolid="dropDownListReturningMonthYear" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListReturningMonthYear" runat="server" width="120px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRReturningConstraint" associatedcontrolid="dropDownListReturningTimeConstraint" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListReturningTimeConstraint" runat="server" cssclass="txtseven" Width="110px"></asp:dropdownlist>
			<asp:label id="labelSRReturningHoursPre" associatedcontrolid="dropDownListReturningHour" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListReturningHour" runat="server"  cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRReturningHoursPost" runat="server" cssclass="screenreader"></asp:label>
			<asp:label id="labelSRReturningMinutesPre" associatedcontrolid="dropDownListReturningMinute" runat="server" cssclass="screenreader"></asp:label>
			<asp:dropdownlist id="dropDownListReturningMinute" runat="server" width="40px" cssclass="txtseven"></asp:dropdownlist>
			<asp:label id="labelSRReturningMinutesPost" runat="server" cssclass="screenreader"></asp:label></td>
        <td valign="bottom" rowspan="2">
			<cc1:tdbutton id="buttonOK" runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
