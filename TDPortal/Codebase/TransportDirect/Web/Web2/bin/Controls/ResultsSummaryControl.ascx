<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ResultsSummaryControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ResultsSummaryControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TransportModesControl" Src="TransportModesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<asp:repeater id="journeyRows" runat="server" enableviewstate="false">
	<headertemplate>
		<table id="<%# GetTableId %>" class="ejdetail" cellspacing="0" summary="<%# GetTableSummary %>">
			<thead>
				<tr>
					<th id="tableheaderDelete" runat="server" class="ejpdelehd"></th>
					<th id="tableheaderOrig" runat="server" class="ejporighd">
						<asp:label id="headerlabelFrom" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderDest" runat="server" class="ejpdesthd">
						<asp:label id="headerlabelTo" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderTransport" runat="server" class="ejptranhd">
						<asp:label id="headerlabelTransport" runat="server" enableviewstate="false" /></th>
					<th class="ejpleavhd">
						<asp:label id="headerlabelLeaveTime" runat="server" enableviewstate="false" /></th>
					<th class="ejparrihd">
						<asp:label id="headerlabelArriveTime" runat="server" enableviewstate="false" /></th>
					<th class="ejpdurahd">
						<asp:label id="headerlabelDuration" runat="server" enableviewstate="false" /></th>
					<th id="tableheaderSelect" runat="server" class="ejpselehd">
						<asp:label id="headerlabelSelect" runat="server" enableviewstate="false" /></th>
				</tr>
			</thead>
			<tbody>
	</headertemplate>
	<itemtemplate>
				<tr id="row" runat="server">
					<td id="tablecellDelete" class="ejpdele" runat="server">
						<uc1:hyperlinkpostbackcontrol id="hyperlinkDelete" runat="server" enableviewstate="false"></uc1:hyperlinkpostbackcontrol></td>
					<td id="tablecellOrigin" class="ejporig" runat="server">
						<asp:label id="labelFrom" runat="server" enableviewstate="false"></asp:label></td>
					<td class="ejpdest">
						<asp:label id="labelTo" runat="server" enableviewstate="false"></asp:label></td>
					<td class="ejptran">
						<asp:Label ID="labelTransport" runat="server" EnableViewState="false"></asp:Label> </td>
					<td class="ejpleav">
						<asp:label id="labelLeaveTime" runat="server" enableviewstate="false"></asp:label></td>
					<td class="ejparri">
						<asp:label id="labelArriveTime" runat="server" enableviewstate="false"></asp:label></td>
					<td class="ejpdura">
						<asp:label id="labelDuration" runat="server" enableviewstate="false"></asp:label></td>
					<td id="tablecellSelect" class="ejpsele" runat="server">
						<cc1:ScriptableGroupRadioButton enableclientscript="true" scriptname="RowHighlighter" action="<%# GetAction %>" id="journeyRadioButton" runat="server" enableviewstate="false">
						</cc1:scriptablegroupradiobutton></td>
				</tr>
	</itemtemplate>
	<footertemplate>
			</tbody>
		</table>
	</footerTemplate>
</asp:repeater>
