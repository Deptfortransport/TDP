<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyTicketCostsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyTicketCostsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyCostsControl" Src="CarJourneyCostsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyFaresControl" Src="JourneyFaresControl.ascx" %>
<div class="JourneyTicketCostsItem">
	<asp:repeater id="ticketsCostsRepeater" runat="server" enableviewstate="true">
		<itemtemplate>
			<h2>
				<asp:label id="segmentHeader" runat="server"></asp:label>
			</h2>
			<div class="JourneyTicketCostsCarItem">
				<uc1:carjourneycostscontrol id="carJourneyCostsControl" runat="server"></uc1:carjourneycostscontrol>
			</div>
			<div class="JourneyTicketCostsPublicItem">
				<uc1:journeyfarescontrol id="journeyFaresControl" runat="server"></uc1:journeyfarescontrol>
			</div>
		</itemtemplate>
	</asp:repeater>
</div>