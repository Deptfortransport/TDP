<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZonalAccessibilityLinksControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ZonalAccessibilityLinksControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableviewstate="False"%>
<ul class="zonalLinksList">
	<asp:repeater id="zonalAccessibilityLinks" runat="server" enableviewstate="False">
		<itemtemplate>
		<li class="zonalLinksList">
				<asp:hyperlink id="zonalAccessibilityLink" runat="server" enableviewstate="False"></asp:hyperlink>
			</li>
		
		</itemtemplate>
	</asp:repeater>
</ul>