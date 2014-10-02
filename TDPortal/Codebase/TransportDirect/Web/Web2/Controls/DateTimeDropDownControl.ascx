<%@ Control Language="c#" AutoEventWireup="True" Codebehind="DateTimeDropDownControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.DateTimeDropDownControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="1" cellpadding="1" border="0">
	<tr>
		<td><p><asp:label id="labelDay" runat="server"></asp:label></p>
		</td>
		<td><p><asp:label id="labelMonthYear" runat="server"></asp:label></p>
		</td>
		<td><p><asp:label id="labelHours" runat="server"></asp:label></p>
		</td>
		<td><p><asp:label id="labelMinutes" runat="server"></asp:label></p>
		</td>
	</tr>
	<tr>
		<td><asp:label id="labelSRDay" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="DropDownListDay" runat="server"></asp:dropdownlist></td>
		<td><asp:label id="labelSRMonthYear" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="DropDownListMonthYear" runat="server"></asp:dropdownlist></td>
		<td><asp:label id="labelSRHoursPre" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="DropDownListHours" runat="server"></asp:dropdownlist><asp:label id="labelSRHoursPost" runat="server" cssclass="screenreader"></asp:label></td>
		<td><asp:label id="labelSRMinutesPre" runat="server" cssclass="screenreader"></asp:label><asp:dropdownlist id="DropDownListMinutes" runat="server"></asp:dropdownlist><asp:label id="labelSRMinutesPost" runat="server" cssclass="screenreader"></asp:label></td>
		<td><cc1:tdimagebutton id="commandCalendar" runat="server" Visible="false" ImageUrl="/web2/app_themes/transportdirect/images/gifs/JourneyPlanning/en/calendar.gif"></cc1:tdimagebutton></td>
		<td><cc1:tdimagebutton id="commandOk" runat="server" Visible="false"></cc1:tdimagebutton></td>
	</tr>
</table>
