<%@ Control Language="c#" AutoEventWireup="True" Codebehind="NetworkMapLinksControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.NetworkMapLinksControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Repeater Runat="server" ID="networkLinkRepeater">
	<ItemTemplate>
		<asp:label id="labelTitle" runat="server"></asp:label>
		<asp:hyperlink id="linkNetworkMap" runat="server"></asp:hyperlink>
		<br />
	</ItemTemplate>
</asp:Repeater>
