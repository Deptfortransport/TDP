<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapFindInformationLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapFindInformationLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="mapFindInformationLocationControl" runat="server">
	<table width="100%" border="0">
		<tr>
			<td valign="top" align="left">
				<asp:image id="imageMapError" runat="server"></asp:image>
			</td>
			<td width="100%" align="left">
				<asp:literal id="literalInstructions" runat="server"></asp:literal>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="right">
				<cc1:tdbutton id="buttonSelectNewLocation" runat="server" causesvalidation="false" action="return SelectLocation();" scriptname="MapLocationControl"></cc1:tdbutton>
				<cc1:tdbutton id="buttonFindCancel" runat="server" causesvalidation="false" action="return FindInformation_Cancel();" scriptname="MapLocationControl"></cc1:tdbutton>
			</td>
		</tr>
	</table>
</div>
