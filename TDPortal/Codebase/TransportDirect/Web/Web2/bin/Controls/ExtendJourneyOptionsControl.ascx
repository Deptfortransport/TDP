<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ExtendJourneyOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ExtendJourneyOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="extensionControlTable" runat="server">
	
	<tr>
		<td align="center" width="100" rowspan="3">
			<cc1:tdimage id="extendImage" runat="server" alternatetext=" "></cc1:tdimage></td>
		<td colspan="5">
			<asp:label id="labelPromptText" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></td>
	</tr>
	<tr>
		<td><cc1:scriptablegroupradiobutton id="scriptableRadioButtonExtendDirection1" runat="server" groupname="direction"></cc1:scriptablegroupradiobutton></td>
		<td><asp:label id="labelExtendStartPreText" cssclass="txtseven" runat="server" enableviewstate="false"></asp:label>
			<asp:label id="labelExtendStartPostText" cssclass="txtseven" runat="server" enableviewstate="false"></asp:label></td>
	</tr>
	<tr>
		<td><cc1:scriptablegroupradiobutton id="scriptableRadioButtonExtendDirection2" runat="server" groupname="direction"></cc1:scriptablegroupradiobutton></td>
		<td><asp:label id="labelExtendStartPreText2" cssclass="txtseven" runat="server" enableviewstate="false"></asp:label>
			<asp:label id="labelExtendStartPostText2" cssclass="txtseven" runat="server" enableviewstate="false"></asp:label></td>
	</tr>
	
</table>
