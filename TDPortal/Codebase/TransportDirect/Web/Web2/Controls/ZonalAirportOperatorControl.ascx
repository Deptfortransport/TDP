<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZonalAirportOperatorControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ZonalAirportOperatorControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Label id="comments" runat="server" cssclass="txtseven" enableviewstate="False"></asp:Label>

<ul class="ZonalAirportOperatorLinksList">
	<asp:repeater id="ZonalAirportOperatorRepeater" runat="server" enableviewstate="False">
		<itemtemplate>
			<li class="zonalLinksList">
				<asp:hyperlink id="zonalAirportOperatorHyperLink" runat="server" enableviewstate="False" Visible="False"></asp:hyperlink>
				<asp:Label id="zonalAirportOperatorLabel" runat="server" enableviewstate="False" Visible="False"></asp:Label>
			</li>
		</itemtemplate>
	</asp:repeater>
</ul>
