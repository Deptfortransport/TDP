<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RequiredTicketsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RequiredTicketsTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div>
	<asp:repeater id="requiredTicketTable" runat="server" enableviewstate="False">
		
		<headertemplate>
		
			<table cellspacing="0" cellpadding="0" id="requiredticketstable" summary="<%# RequiredTicketsTableSummary %>">
				
				<tbody>
					<tr>
						<th id="colTickets" scope="col" class="requiredticketstableheading">
							<asp:label id="labelTicket" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colDiscounted" scope="col" class="requiredticketstableheading">
							<asp:label id="labelDiscounted" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colAdultFare" scope="col" class="requiredticketstableheading">
							<asp:label id="labelAdultFare" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colChildFare" scope="col" class="requiredticketstableheading">
							<asp:label id="labelChildFare" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colNoOfTickets" scope="col" class="requiredticketstableheading">
							<asp:label id="labelNoOfTickets" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colTotal" align="right" scope="col" class="requiredticketstableheading">
							<asp:label id="labelTotal" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></th>

						<th id="colSpacer" scope="col" class="requiredticketstableheading"></th>
					</tr>
		</headertemplate>
		<itemtemplate>
			<tr>
				<td headers="colTickets" class="requiredticketstablecell">
					<asp:label id="labelTicket" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colDiscounted" class="requiredticketstablecell">
					<asp:label id="labelDiscounted" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colAdultFare" align="right" class="requiredticketstablecell">
					<asp:label id="labelAdultFare" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colChildFare" align="right" class="requiredticketstablecell">
					<asp:label id="labelChildFare" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colNoOfTickets" align="center" class="requiredticketstablecell">
					<asp:label id="labelNoOfTickets" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colTotal" align="right" class="requiredticketstablecell">
					<asp:label id="labelTotal" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></td>
				
				<td headers="colSpacer" align="right" class="requiredticketstablecell"></td>
			</tr>
		</itemtemplate>
		<footertemplate>
				<tr>
					<td colspan="5" class="requiredticketstablecell"></td>
					<td headers="colTotal" align="right" class="requiredticketstabletotalcell">
						<asp:label id="labelTotal" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label></td>
					<td headers="colSpacer" align="right" class="requiredticketstablecell"></td>
				</tr>
			</tbody>
			</table>
		</footertemplate>
	</asp:repeater>
	
</div>
