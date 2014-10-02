<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LocalZonalOpertatorFaresControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocalZonalOpertatorFaresControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Repeater id="operatorFaresLinkRepeater" runat="server">
	<ItemTemplate>
		<asp:Label id="labelCheckFor" runat="server"></asp:Label>
		<asp:HyperLink id="operatorFaresHyperLink" runat="server"></asp:HyperLink>
	</ItemTemplate>
</asp:Repeater>
