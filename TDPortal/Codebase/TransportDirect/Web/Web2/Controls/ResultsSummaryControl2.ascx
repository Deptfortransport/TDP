<%@ Import namespace="TransportDirect.UserPortal.Web.Adapters" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TransportModesControl" Src="TransportModesControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultsSummaryControl2.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:repeater id="journeyRows" runat="server" enableviewstate="false">
	<headertemplate>
		<table id="<%# GetTableId %>" class="ejdetail" cellspacing="0" summary="<%# GetTableSummary %>">
			<thead>
				<tr>
					<th id="tableheaderOrig" runat="server" class="ejporighd">
						<asp:label id="headerlabelFrom" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderDest" runat="server" class="ejpdesthd">
						<asp:label id="headerlabelTo" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderTransport" runat="server" class="ejptranhd">
						<asp:label id="headerlabelTransport" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderLeaveTime" runat="server" class="ejpleavhd">
						<asp:label id="headerlabelLeaveTime" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderArriveTime" runat="server" class="ejparrihd">
						<asp:label id="headerlabelArriveTime" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderDuration" runat="server" class="ejpdurahd">
						<asp:label id="headerlabelDuration" runat="server" enableviewstate="false" /></th>
				</tr>
			</thead>
			<tbody>
	</headertemplate>
	<itemtemplate>
		<tr id="row" runat="server" class="<%# GetRowClass1(Container.ItemIndex)%>">
			<td id="tablecellOrigin" class="ejporig" runat="server">
				<asp:label id="labelFrom" runat="server" enableviewstate="false"></asp:label></td>
			<td id="tablecellDestination" runat="server" class="ejpdest">
				<asp:label id="labelTo" runat="server" enableviewstate="false"></asp:label></td>
			<td id="tablecellTransport" runat="server" class="ejptran">
				<uc1:transportmodescontrol runat="server" id="itmTransportMode" enableviewstate="false" /></td>
			<td id="tablecellLeaveTime" runat="server" class="ejpleav">
				<asp:label id="labelLeaveTime" runat="server" enableviewstate="false"></asp:label></td>
			<td id="tablecellArriveTime" runat="server" class="ejparri">
				<asp:label id="labelArriveTime" runat="server" enableviewstate="false"></asp:label></td>
			<td id="tablecellDuration" runat="server" class="ejpdura">
				<asp:label id="labelDuration" runat="server" enableviewstate="false"></asp:label></td>
		</tr>
		<tr id="rowTransport" runat="server" class="<%# GetRowClass2(Container.ItemIndex)%>">
			<td id="tablecellTransport2" runat="server" class="ejptranwide" colspan="5">
				<table cellspacing="0" cellpadding="0">
					<tr>
						<td>
							<asp:label id="labelTransport" runat="server" enableviewstate="false" />&nbsp;
						</td>
						<td>
							<%# ((FormattedJourneySummaryLine)Container.DataItem ).GetTransportModes() %>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</tbody> </table>
	</footertemplate>
</asp:repeater>
