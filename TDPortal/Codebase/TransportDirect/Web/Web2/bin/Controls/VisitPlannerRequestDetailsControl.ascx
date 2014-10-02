<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="VisitPlannerRequestDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.VisitPlannerRequestDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<table class="requestvisittable" id="Requests" width="100%" align="center">
	<tr class="requestvisittableheader">
		<td align="left" colspan="2"><asp:label id="labelRequestedVisit" runat="server"></asp:label></td>
		<td align="right" colspan="2" class="txtseven">&nbsp;
			<cc1:tdbutton id="imageAmend" runat="server" imagealign="Top"></cc1:tdbutton>			
		</td>
	</tr>
	<tr class="requestvisittablerow" runat="server" id="row1">
		<td align="right" width="15%"><asp:label id="labelFrom" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelFromText" runat="server"></asp:label></td>
		<td colspan="2">&nbsp;</td>
	</tr>
	<tr class="requestvisittablerow" runat="server" id="row2">
		<td align="right"><asp:label id="labelFirstVisit" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelFirstVisitText" runat="server"></asp:label></td>
		<td align="right"><asp:label id="labelLengthFirstVisit" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelStayFirstTime" runat="server"></asp:label></td>
	</tr>
	<tr class="requestvisittablerow" runat="server" id="row3">
		<td align="right"><asp:label id="labelSecondVisit" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelSecondVisitText" runat="server"></asp:label></td>
		<td align="right"><asp:label id="labelLengthSecondVisit" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelStaySecondTime" runat="server"></asp:label></td>
	</tr>
	<tr class="requestvisittablerow" runat="server" id="row4">
		<td align="right"><asp:label id="labelVisitDateTime" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelVisitDateTimeText" runat="server"></asp:label></td>
		<td align="right"><asp:label id="labelReturnToOrigin" runat="server"></asp:label></td>
		<td align="left"><asp:label id="labelReturnToOriginText" runat="server"></asp:label></td>
	</tr>
	<tr class="requestvisittablerow" runat="server" id="row5">
		<td align="right"><asp:label id="labelOptions" runat="server"></asp:label></td>
		<td colspan="3">
			<asp:repeater id="repeaterOptions" runat="server">
				<itemtemplate>
					<asp:label runat="server" id="labelOption" />
				</itemtemplate>
				<separatortemplate>
					<br>
				</separatortemplate>
			</asp:repeater></td>
	</tr>
</table>
