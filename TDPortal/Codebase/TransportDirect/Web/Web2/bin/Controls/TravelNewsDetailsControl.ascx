<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TravelNewsDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TravelNewsDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class = "boxtypetravelincidents">
	<asp:repeater id="repeaterTravelIncidents" enableviewstate="false" runat="server">
		<headertemplate>
			<table class="TravelIncidentsTable" summary="Travel incidents" cellpadding="1" cellspacing="0" width="<%# PrinterFriendly ? "100%" : "810px"  %>">
				
					<tr>
						<th class="tnhIncident" id="headerIncident"><%# IncidentHeaderText %></th>
						<th class="tnhAffected" id="headerAffected"><%# AffectedHeaderText %></th>
						<th class="tnhDetails" id="headerDetails"><%# DetailsHeaderText %></th>
						<th class="tnhSeverity" id="headerSeverity"><%# SeverityHeaderText %></th>
						<th class="tnhOccurred" id="headerOccurred"><%# OccurredHeaderText %></th>
						<th class="tnhLastUpdated" id="headerUpdated"><%# LastUpdatedHeaderText %></th>
					</tr>
				
		</headertemplate>
		<itemtemplate>
			<tr class="travelIncident">
				<td headers="headerIncident">
					<asp:label id="lblIncident" runat="server"><%# IncidentTypeText(Container.DataItem) %></asp:label>
					<cc1:tdlinkbutton id="linkIncident" runat="server" oncommand="linkIncident_Command" commandargument="<%# CommandArgumentUid(Container.DataItem)%>"></cc1:tdlinkbutton>			
				</td>
				<td headers="headerAffected"><%# AffectedDescriptionText(Container.DataItem)%></td>
				<td headers="headerDetails"><%# DetailsItemText(Container.DataItem)%></td>
				<td headers="headerSeverity"><%# SeverityDescriptionText(Container.DataItem)%></td>
				<td headers="headerOccurred"><%# StartDateTimeText(Container.DataItem)%></td>
				<td headers="headerUpdated"><%# LastUpdatedText(Container.DataItem)%></td>
			</tr>
		</itemtemplate>
		<alternatingitemtemplate>
			<tr class="travelIncidentAlt">
				<td headers="headerIncident">
					<asp:label id="lblIncident" runat="server"><%# IncidentTypeText(Container.DataItem) %></asp:label>
					<cc1:tdlinkbutton id="linkIncident" runat="server" oncommand="linkIncident_Command" commandargument="<%# CommandArgumentUid(Container.DataItem)%>"></cc1:tdlinkbutton>
				</td>
				<td headers="headerAffected"><%# AffectedDescriptionText(Container.DataItem)%></td>
				<td headers="headerDetails"><%# DetailsItemText(Container.DataItem)%></td>
				<td headers="headerSeverity"><%# SeverityDescriptionText(Container.DataItem)%></td>
				<td headers="headerOccurred"><%# StartDateTimeText(Container.DataItem)%></td>
				<td headers="headerUpdated"><%# LastUpdatedText(Container.DataItem)%></td>
			</tr>
		</alternatingitemtemplate>
		<footertemplate>
			</table>
		</footertemplate>
	</asp:repeater>
	<br/>
	<table align="center">
		<tr>
			<td><asp:Label id="lblNoRecords" runat="server" cssclass="txtseven"></asp:Label></td>
		</tr>
	</table>
	
</div>
