<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyExtensionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyExtensionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table id="tableExtensionButtons" runat="server" width="765px" style="margin-left:5px" cellspacing="0" cellpadding="0" border="0" >
	<tr>
		<td valign="middle" height="20px" align="right" style="padding-bottom:5px">
			<a name="FindTransportToStartButton"></a>
			<cc1:tdbutton id="findTransportToStartButton" runat="server"></cc1:tdbutton>
		</td>
	</tr>
	<tr>
		<td valign="middle" height="25px" align="right">
			<a name="FindTransportFromEndButton"></a>
			<cc1:tdbutton id="findTransportFromEndButton" runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
