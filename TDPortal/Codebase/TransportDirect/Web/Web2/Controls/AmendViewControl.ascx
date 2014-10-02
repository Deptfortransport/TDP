<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendViewControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendViewControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="2" width="100%">
	<tr>
		<td class="txtseven" colspan="2"><asp:label id="labelInfo" runat="server"></asp:label>&nbsp;</td>
	</tr>
	<tr>
		<td align="left" class="txtseven"><asp:label id="labelPreference" runat="server" enableviewstate="False"></asp:label>
			<asp:dropdownlist id="dropListPartition" runat="server" enableviewstate="True"></asp:dropdownlist>
			<asp:label id="labelResults" runat="server" enableviewstate="False"></asp:label></td>
		<td valign="bottom" rowspan="1">
			<cc1:tdbutton id="submitButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
	</tr>
</table>
