<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="CalendarControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindDateControl" Src="FindDateControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindLeaveReturnDatesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellspacing="0" cellpadding="0" width="100%">
	<tr>
		<td>
			<uc1:calendarcontrol id="calendar" runat="server" visible="False"></uc1:calendarcontrol>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:finddatecontrol id="theLeaveDateControl" runat="server"></uc1:finddatecontrol>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:finddatecontrol id="theReturnDateControl" runat="server"></uc1:finddatecontrol>
		</td>
	</tr>
	<tr>
		<td style="LEFT: 7px; VERTICAL-ALIGN: baseline; POSITION: relative; TEXT-ALIGN: left">
			<asp:label id="labelPlanningTip" runat="server" cssclass="txtseven"></asp:label>
		</td>
	</tr>
</table>
