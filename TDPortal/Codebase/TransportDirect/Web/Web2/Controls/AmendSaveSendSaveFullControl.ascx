<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendSaveFullControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendSaveFullControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="txtsevenb"><strong><asp:label id="labelJourneyListFullMsg" runat="server" enableviewstate="False"></asp:label></strong></div>
<table summary="AmendSendSaveFullControlTable">
	<tr>
		<td class="txtsevenb"><asp:label id="labelReplaceJourneyLabel" runat="server" enableviewstate="False"></asp:label>
		</td>
		<td><asp:dropdownlist id="ddlJourney" runat="server" Width="120px"></asp:dropdownlist>
		</td>
		<td class="txtsevenb"><asp:label id="labelWithNewJourneyLabel" runat="server" enableviewstate="False"></asp:label>
		</td>
		<td><asp:textbox id="txtNewJourneyName" runat="server" MaxLength="50"></asp:textbox>
		</td>
		<td><cc1:tdbutton id="btnOK" runat="server" enableviewstate="False"></cc1:tdbutton>
		</td>
	</tr>
	<tr>
		<td class="txtnote" colspan="4"><asp:label id="labelJourneyPlanSaveNote" runat="server" enableviewstate="False"></asp:label></td>
	</tr>
	<tr>
		<td class="txtseven" colspan="4"><asp:label id="labelJourneyNotReplace" runat="server" enableviewstate="False"></asp:label></td>
	</tr>
</table>
