<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AvoidSelectControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AvoidSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="JPTable" cellspacing="0" lang="en" summary="Please enter your travelling From details">
	<tr>
		<td valign="top"><asp:label id="labelAvoidTitle" runat="server" cssclass="txtseven"></asp:label>&nbsp;</td>
		<td><asp:label id="labelSRAvoid" runat="server" cssclass="screenreader"></asp:label>
			<asp:textbox id="textAvoid" runat="server" width="301px"></asp:textbox><br>
			<asp:label id="labelEg" runat="server" cssclass="txtnote"></asp:label></td>
	</tr>
</table>
