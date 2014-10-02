<%@ Import namespace="TransportDirect.UserPortal.SessionManager" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFareTicketSelectionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareTicketSelectionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:panel id="ticketsPanel" runat="server">
	<div class="<%= GetTicketBoxHeaderTitleClass()%>">
		<asp:label id="headerTitleLabel" runat="server" cssclass="txtsevenb" enableviewstate="False"></asp:label>
		<asp:label id="headerDatesLabel" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label></div>
	<asp:repeater id="repeaterResultTable" runat="server" enableviewstate="False">
		<headertemplate>
			<div class="<%# GetTicketBoxHeaderClass()%>">
				<table lang="en" summary="Table header for table containing ticket types for the date and transport you selected."
					cellpadding="0" cellspacing="0" width="100%">
					<tr id="ticketTableHeader" runat="server">
						<th class="tdttypecolumnheader" id="HeaderType">
							<%# GetTicketTypeColumnHeaderText() %>
						</th>
						<th class="tdtflexibilitycolumnheader" id="flexHeader">
							<%# GetFlexibilityColumnHeaderText() %>
						</th>
						<th class="tdtfarecolumnheader" id="fareHeader">
							<%# GetFareColumnHeaderText() %>
						</th>
						<th class="tdtprobabilitycolumnheader" align="left" id="probHeader">
							<%# GetProbabilityColumnHeaderText() %>
						</th>
						<th class="tdtselectcolumnheader" id="selectHeader">
							<%# GetSelectColumnHeaderText() %>
						</th>
					</tr>
				</table>
			</div>
			<div class="<%# GetTicketBoxClass()%>">
				<table id="<%# GetTableId()%>"  lang="en" summary="Ticket types for the date and transport you selected.  Each ticket is listed with its expected availability." cellpadding="0" cellspacing="0">
				<thead><tr>
				<th id="<%# GetHeaderId(1)%>" class="screenreader" >
					<asp:Label id="labelSRTicketType" runat="server" ><%# GetTicketTypeColumnHeaderText() %></asp:Label></th>
				<th id="<%# GetHeaderId(2)%>" class="screenreader" >
					<asp:Label id="labelSRFlexibility" runat="server"><%# GetFlexibilityColumnHeaderText() %></asp:Label></th>
				<th id="<%# GetHeaderId(3)%>" class="screenreader">
					<asp:Label id="labelSRFare" runat="server"><%# GetFareColumnHeaderText() %></asp:Label></th>
				<th id="<%# GetDiscountHeaderId()%>" class="<%# CellCss() %>" ><asp:Label id="labelSRDiscount" runat="server"><%# GetDiscountColumnHeaderText() %></asp:Label></th>
				<th id="<%# GetHeaderId(4)%>" class="screenreader"  >
					<asp:Label id="labelSRProbability" runat="server"><%# GetProbabilityColumnHeaderText() %></asp:Label></th>
				<th id="<%# GetHeaderId(5)%>" class="screenreader">
					<asp:Label id="labelSRSelect" runat="server"><%# GetSelectColumnHeaderText() %></asp:Label></th></tr>
		</thead>
		</headertemplate>
		<itemtemplate>
			<tr class ="<%# GetRowClass() %>" id="ticketTableRow" runat="server">
				<td class="tdttypeitem" runat="server" id="ticketName"></td>
				<td class="tdtflexibilityitem"  ><%# GetResource(((DisplayableCostSearchTicket)Container.DataItem).Flexibility)%></td>
				<td class="tdtfareitem"><span class="pound">£</span><%# ((DisplayableCostSearchTicket)Container.DataItem).Fare%></td>
				<td class="tdtprobabilityitem"><%# GetResource(((DisplayableCostSearchTicket)Container.DataItem).Probability)%></td>
				<td class="<%# GetScrollBarColumnClass()%>">
					<cc1:scriptablegroupradiobutton id="ticketRadioButton" runat="server" groupname="<%# GetGroupName()%>" enableclientscript="<%# GetEnableClientScript() %>" action="<%# GetAction() %>" scriptname="<%# GetScriptName() %>" enabled="<%# GetEnabled((DisplayableCostSearchTicket)Container.DataItem) %>">
					</cc1:scriptablegroupradiobutton>
				</td>
			</tr>
		</itemtemplate>
		<footertemplate>
			</table> </div>
		</footertemplate>
	</asp:repeater>
</asp:panel>
<asp:panel id="noTicketsPanel" runat="server">
	<div id="boxtypeten">
		<asp:label id="noTicketsLabel" runat="server" cssclass="txtseven"></asp:label></div>
</asp:panel>
