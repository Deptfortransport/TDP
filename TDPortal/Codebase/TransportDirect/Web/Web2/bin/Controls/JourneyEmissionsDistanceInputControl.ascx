<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyEmissionsDistanceInputControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionsDistanceInputControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div>
	<table cellspacing="5" cellpadding="0" width="100%" summary="Journey Emissions Distance Input">
		<tr>
			<td colspan="3">
				<asp:label id="labelTitle" runat="server" cssclass="txtsevenb"></asp:label>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<table cellspacing="0" cellpadding="0" width="100%" summary="Journey Emissions Transport Images">
					<tr>
						<td width="25"></td>
						<td align="center"><cc1:tdimage id="imageCar" runat="server"></cc1:tdimage></td>
						<td align="center"><cc1:tdimage id="imageTrain" runat="server"></cc1:tdimage></td>
						<td align="center"><cc1:tdimage id="imagePlane" runat="server"></cc1:tdimage></td>
						<td align="center"><cc1:tdimage id="imageBusCoach" runat="server"></cc1:tdimage></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td align="right">
				<asp:label id="labelJourneyDistance" associatedcontrolid="textJourneyDistance" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;&nbsp;&nbsp;
			</td>
			<td>
				<asp:textbox id="textJourneyDistance" runat="server" columns="10" enableviewstate="false"></asp:textbox>
			</td>
			<td>
				<asp:DropDownList id="listJourneyDistanceUnit" runat="server"></asp:DropDownList>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<asp:label id="labelDisplayJourneyDistanceError" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
			</td>
		</tr>
	</table>
</div>
