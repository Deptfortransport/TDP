<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindPageOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="boxtypesevenalt">
	<table lang="en" cellspacing="0" width="605" border="0">
		<tr>
			<td valign="top" align="left" width="50%">
				<p><cc1:tdbutton id="commandBack" runat="server"></cc1:tdbutton>
					<cc1:tdbutton id="commandShowAdvancedOptions" runat="server"></cc1:tdbutton>
					<cc1:tdbutton id="commandHideAdvancedOptions" runat="server"></cc1:tdbutton></p>
			</td>
			<td valign="top" align="left" width="25%"><cc1:tdbutton id="commandClear" runat="server"></cc1:tdbutton></td>
			<td valign="top" align="right" width="25%"><cc1:tdbutton id="commandSubmit" runat="server"></cc1:tdbutton></td>
		</tr>
		
	</table>
</div>
