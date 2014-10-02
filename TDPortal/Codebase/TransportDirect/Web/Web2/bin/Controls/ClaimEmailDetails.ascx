<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ClaimEmailDetails.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ClaimEmailDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div><A href="#"></A></div>
<table border="0" width="550">
	<tr>
		<td width="1%"><p><asp:label id="labelMandatoryEmail" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td width="30%" align="right"><p><asp:label id="labelEmail" Runat="server"></asp:label></p>
		</td>
		<td width="1%"><asp:image id="imgErrorEmail" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></td>
		<td><asp:textbox id="textEmail" Runat="server" Width="100%" Columns="60"></asp:textbox></td>
	</tr>
	<tr>
		<td><p><asp:label id="labelMandatoryConfirmEmail" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td align="right"><p><asp:label id="labelConfirmEmail" Runat="server"></asp:label></p>
		</td>
		<td><asp:image id="imgErrorConfirmEmail" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></td>
		<TD><asp:textbox id="textConfirmEmail" Runat="server" Width="100%" Columns="60"></asp:textbox></TD>
	</tr>
</table>
<div style="FONT-SIZE: 0.6em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif">
	<asp:Label id="labelNote" runat="server"></asp:Label></div>
<p>&nbsp;</p>
