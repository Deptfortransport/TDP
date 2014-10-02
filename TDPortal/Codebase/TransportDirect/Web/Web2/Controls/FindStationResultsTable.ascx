<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindStationResultsTable.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindStationResultsTable" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="System.Data" %>
<asp:repeater id="repeaterResultTable" runat="server">
	<headertemplate>
		<div id="boxtypestationresults">
			<table id="stationResultsTable" lang="en" summary="stations found results table" cellpadding="0" cellspacing="0" width="100%">
				<tr id="underline">
					<td class='tdstationheader<%# OptionHeaderStyle%>'>
						<asp:ImageButton ID="commandSortByOption" Runat="server" OnClick="CommandSortByOptionClick" ImageUrl='<%# OptionSortSymbolUrl %>' AlternateText='<%# SortByOptionAlternateText %>'>
						</asp:imagebutton> <%# OptionTitle %></td>
					<td class='tdstationheader<%# TransportHeaderStyle%>'>
						<div style="<%# IsTransportVisible %>">
							<asp:ImageButton id="commanSortByTransport" runat="server" onclick="CommandSortByTransportClick" imageurl='<%# TransportSortSymbolUrl %>' alternatetext='<%# SortByTransportAlternateText %>'>
							</asp:imagebutton> <%# TransportTitle %></div>
					</td>
					<td class='tdstationheader<%# StationNameHeaderStyle%>'>
						<asp:ImageButton ID="commandSortByAirportName" Runat="server" OnClick="CommandSortByAirportNameClick" ImageUrl='<%# AirportNameSortSymbolUrl %>' AlternateText='<%# SortByAirportNameAlternateText %>'>
						</asp:imagebutton> <%# AirportStationTitle %></td>
					<td class='tdstationheader<%# DistanceHeaderStyle%>' align="left">
						<asp:ImageButton ID="commandSortByDistance" Runat="server" OnClick="CommandSortByDistanceClick" ImageUrl='<%# DistanceSortSymbolUrl %>' AlternateText='<%# SortByDistanceAlternateText %>'>
						</asp:imagebutton> <%# DistanceTitle %></td>
					<td valign="middle" class="tdstationheader">
						<div id="panelSelectHeader" style="<%# IsSelectVisible %>"><%# SelectTitle %></div>
					</td>
				</tr>
	</headertemplate>
	<itemtemplate>
		<tr class='jptype<%# GetTextCssClass(Container.ItemIndex)%>'>
			<td class="txtseven" align="left" id="rowIndexCell" visible="<%# !IsRowSelectLinkVisible %>"  runat="server">&nbsp;&nbsp;<%# GetRowIndex((DataRow) Container.DataItem) %></td>
			<td class="txtseven" align="left" id="rowIndexLink" visible="<%# IsRowSelectLinkVisible %>" runat="server">&nbsp;&nbsp;<a href=" " onclick="<%# GetShowOnMapScript((DataRow) Container.DataItem) %>"><%# GetRowIndex((DataRow) Container.DataItem) %></a></td>
			<td class="txtseven">
				<div  style="<%# IsTransportVisible %>">
					<%# GetTransport((DataRow) Container.DataItem)%>
				</div>
			</td>
			<td class="txtseven">
                <cc1:tdbutton ID="commandInfo" runat="server" CssClass="TDLocationLink" CssClassMouseOver="TDLocationLinkMouseOver" CommandArgument='<%# GetInfoArgument((DataRow) Container.DataItem) %>' OnClick="CommandInfoClick" Text='<%# GetAirportName((DataRow) Container.DataItem)%>' Visible='<%# IsStationLinkVisible() %>' ></cc1:tdbutton>
                <asp:Label ID="labelInfo" runat="server" Text='<%# GetAirportName((DataRow) Container.DataItem)%>' Visible='<%# IsStationLabelVisible() %>'></asp:Label>
			</td>		
			<td class="txtseven" align="left"><%# GetDistance((DataRow) Container.DataItem)%></td>
			<td class="stationResultsSelect">
				<div  style="<%# IsSelectVisible %>" >
					<asp:checkBox ID="checkAirportSelect" Runat="server" Checked='<%# GetSelected((DataRow) Container.DataItem)%>' OnCheckedChanged="AirportCheckedChanged">
					</asp:checkBox>
				</div>
			</td>
		</tr>
	</itemtemplate>


	<footertemplate>
		</table> </div>
	</footertemplate>
</asp:repeater>
