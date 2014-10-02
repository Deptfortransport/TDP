<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZonalServiceLinksControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ZonalServiceLinksControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableviewstate="False"%>
<ul class="zonalLinksList">
	<asp:repeater id="zonalServiceLinks" runat="server" enableviewstate="False">
		<itemtemplate>
			<li class="zonalLinksList">
				<asp:hyperlink id="zonalServiceLink" runat="server" enableviewstate="False"></asp:hyperlink>
			</li>
		</itemtemplate>
	</asp:repeater>
</ul>