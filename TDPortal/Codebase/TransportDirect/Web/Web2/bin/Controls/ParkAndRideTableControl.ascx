<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ParkAndRideTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ParkAndRideTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="TransportDirect.UserPortal.LocationService" %>
<div id="boxparkandride"><asp:repeater id="repeaterResultTable" runat="server">
		<headertemplate>
			<table id="parkandridegrid" summary="<%# TableSummary() %>" cellspacing="0" cellpadding="5" width="<%# PrinterFriendly ? "100%" : "724px"  %>">
				<thead>
					<tr>
						<th id="tnhButtonColumnHeader" runat="server" class="tnhButtonColumnHeader"></th>
						
						<th id="tnhLocation"><asp:label id="labelLocation" runat="server" enableviewstate="False"><%# LabelLocationText %></asp:label> </th>
						
						<th id="tnhComments"><asp:label id="labelComments" runat="server" enableviewstate="False"><%# LabelCommentText %></asp:label> </th>
					</tr>
				</thead>
				<tbody>
		</headertemplate>
		<itemtemplate>
			<tr class="parkrow">
				<td id="buttonColumn" align="center" runat="server"><cc1:TDButton id="driveToButton" runat="server" text="Drive To" commandname="<%# GetCommandName( (ParkAndRideInfo)Container.DataItem) %>"></cc1:tdbutton></td>
				<td class="tnbLocation">
					<asp:hyperlink id="ParkAndRideHyperLink1" enableviewstate="False" runat="server" target="_blank" tooltip="<%# UrlToolTip(Container.DataItem) %>" NavigateUrl="<%# LocationUrl(Container.DataItem) %>"><%# LocationText(Container.DataItem) %></asp:hyperlink>
				</td>
				<td class="tnbComments"><%# CommentsText(Container.DataItem) %></td>
			</tr>
		</itemtemplate>
		<alternatingitemtemplate>
			<tr class="parkaltrow">
				<td id="buttonColumn" align="center" runat="server"><cc1:TDButton id="driveToAltButton" runat="server" text="Drive To" commandname="<%# GetCommandName( (ParkAndRideInfo)Container.DataItem) %>"></cc1:tdbutton></td>
				<td class="tnbLocation">
					<asp:hyperlink id="ParkAndRideHyperLink2" enableviewstate="False" runat="server" target="_blank" tooltip="<%# UrlToolTip(Container.DataItem) %>" NavigateUrl="<%# LocationUrl(Container.DataItem) %>"><%# LocationText(Container.DataItem) %></asp:hyperlink>
				</td>
				<td class="tnbComments"><%# CommentsText(Container.DataItem) %></td>
			</tr>
		</alternatingitemtemplate>
		<footertemplate>
			</tbody>
			</table>
		</footertemplate>
	</asp:repeater>
	<br/>
	<table align="center">
		<tr>
			<td>
				<asp:label id="labelNoParkAndRides" runat="server" cssclass="txtseven" enableviewstate="False"></asp:label>
			</td>
		</tr>
	</table>
</div>
