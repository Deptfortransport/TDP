<%@ Register TagPrefix="uc1" TagName="TriStateDateControl" Src="TriStateDateControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindDateControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindDateControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:panel id="dateControlPanel" runat="server">
	<table cellspacing="0" cellpadding="0" width="100%">
		<tr>
			<td class="findafromcolumn" align="right"></td>
			<td><asp:label id="errorMessageLabel" runat="server" cssclass="txtseven"></asp:label></td>
		</tr>
		<tr>
			<td class="findafromcolumn" align="right"><asp:label id="directionEntryLabel" runat="server" cssclass="txtsevenb"></asp:label></td>
			<td><uc1:tristatedatecontrol id="triDateControl" runat="server"></uc1:tristatedatecontrol></td>
		</tr>
	</table>
</asp:panel>
