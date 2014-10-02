<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultsFootnotesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ResultsFootnotesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false"%>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="../Controls/HyperlinkPostbackControl.ascx" %>
<div class="boxtypeeightstd">
	<asp:table runat="server" cssclass="txtseven" id="tableFootnotes">
		<asp:tablerow ID="rowFootnote" runat="server">
			<asp:tablecell runat="server" columnspan="2">
				<asp:label id="labelFootnote" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow ID="rowAccessibilltyNotes" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelAccessibillityFootnoteLine1" runat="server"></asp:label>
				<uc1:hyperlinkpostbackcontrol runat="server" id="linkControl"></uc1:hyperlinkpostbackcontrol>
			</asp:tablecell>
		</asp:tablerow>
		<asp:TableRow ID="rowAccessibilityNotes1" runat="server">
		    <asp:TableCell runat="server">•</asp:TableCell>
		    <asp:TableCell runat="server">
		        <asp:Label ID="labelAccessibilityFootnoteLine2" runat="server"></asp:Label>
		        <asp:HyperLink ID="hyperlinkAccessibilityFootnoteLine2" runat="server"></asp:HyperLink>
		    </asp:TableCell>
		</asp:TableRow>
		<asp:TableRow ID="rowJourneyTimesNote1" runat="server" Visible="false">
		    <asp:TableCell runat="server">•</asp:TableCell>
		    <asp:TableCell runat="server">
		       <asp:label id="labelJourneyTimesNote1" runat="server" EnableViewState="false"></asp:label>
		    </asp:TableCell>
		</asp:TableRow>
		<asp:TableRow ID="rowInternationalNotes1" runat="server" Visible="false">
		    <asp:TableCell runat="server">•</asp:TableCell>
		    <asp:TableCell runat="server">
		       <asp:label id="labelInternationalFootnote1" runat="server" EnableViewState="false"></asp:label>
		    </asp:TableCell>
		</asp:TableRow>
		<asp:TableRow ID="rowInternationalNotes2" runat="server" Visible="false">
		    <asp:TableCell runat="server">•</asp:TableCell>
		    <asp:TableCell runat="server">
		       <asp:label id="labelInternationalFootnote2" runat="server" EnableViewState="false"></asp:label>
		    </asp:TableCell>
		</asp:TableRow>
		<asp:tablerow id="rowFindFareTrainFootnoteLine1" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelFindFareTrainFootnoteLine1" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowFlightFootnoteLine1" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelFlightFootnoteLine1" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
	
		<asp:tablerow id="rowFlightFootnoteLine2" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelFlightFootnoteLine2" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowFootnoteLine1" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelFootnoteLine1" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowReturnFootnoteLine1" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelReturnFootnoteLine1" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowInternationalReturnFootnoteLine1" runat="server" Visible="false">
			<asp:tablecell ID="Tablecell3" runat="server">•</asp:tablecell>
			<asp:tablecell ID="Tablecell4" runat="server">
				<asp:label id="labelInternationalReturnFootnoteLine1" runat="server" EnableViewState="false"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowFootnoteLine2" runat="server">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelFootnoteLine2" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
	<asp:tablerow id="rowFootnoteJourneyEmissionsLine1" runat="server">
			<asp:tablecell ID="Tablecell1" runat="server">•</asp:tablecell>
			<asp:tablecell ID="Tablecell2" runat="server">
				<asp:label id="labelJourneyEmissionsFootnoteLine1" runat="server"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow id="rowInternationalTransferFootnoteLine1" runat="server" Visible="false">
			<asp:tablecell runat="server">•</asp:tablecell>
			<asp:tablecell runat="server">
				<asp:label id="labelInternationalTransferFootnoteLine1" runat="server" EnableViewState="false"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
	</asp:table>
</div>
