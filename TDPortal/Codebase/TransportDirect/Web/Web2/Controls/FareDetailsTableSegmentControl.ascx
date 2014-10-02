<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FareDetailsTableSegmentControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FareDetailsTableSegmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<table class="fdtctable" id="<%# GetTableId %>" cellspacing="0" cellpadding="5" 
summary="<%# GetTableSummary %>">
		<tr class="fdtrowheader">
			<td class="fdtcellheader" id="journeyDescriptionCell" runat="server">
				<strong>
					<asp:label id="faresLabel" runat="server" enableviewstate="False"></asp:label>
				</strong>
				&nbsp;
				<asp:label id="journeySegmentDescription" runat="server" enableviewstate="False"></asp:label>
			</td>
			<td class="fdtcellcheaper" id="viewFaresFromCell" align="right" colspan="3" runat="server">
				<strong>
					<asp:label id="faresViewLabel" runat="server" EnableViewState="False"></asp:label>
				</strong>
				<strong>
					<uc1:hyperlinkpostbackcontrol id="otherFaresLinkControl" runat="server" enableviewstate="false"></uc1:hyperlinkpostbackcontrol><asp:hyperlink id="hyperlinkCheaper" runat="server" EnableViewState="False"></asp:hyperlink>&nbsp;
				</strong>
			</td>
		</tr>
		<tr class="fdtrowheader">
			<th class="fdtcellheaderb" style="WIDTH: 388px">
				<asp:label id="noFaresFound" runat="server" enableviewstate="False"></asp:label><asp:label id="noTicketsAvailable" runat="server" enableviewstate="False"></asp:label><asp:label id="noThroughFares" EnableViewState="False" Runat="server"></asp:label><asp:label id="ticketTypeLabel" runat="server" enableviewstate="False"></asp:label><br />
				&nbsp;</th>
			<th class="fdtcellheaderb">
				<asp:label id="flexibilityLabel" runat="server" enableviewstate="False"></asp:label>&nbsp;</th>
			<th class="fdtcellheaderb">
				<asp:label id="adultFareLabel" runat="server" enableviewstate="False"></asp:label><asp:label id="childFareLabel" runat="server"></asp:label><asp:label id="childAgeLabel" runat="server" cssclass="txteight"></asp:label>&nbsp;</th>
			<th class="fdtcellheaderb" id="headerCellDiscounts" runat="server">
				&nbsp;
			</th>
			<th class="fdtcellheaderb" id="headerCellUpgrades" runat="server">
				&nbsp;
			</th>
			<th class="fdtcellheaderb" id="headerCellSelect" runat="server">
				&nbsp;</th></tr>

		<asp:repeater id="fareTicketRepeater" runat="server" enableviewstate="False">
			<itemtemplate>
			</itemtemplate>
		</asp:repeater></table>
