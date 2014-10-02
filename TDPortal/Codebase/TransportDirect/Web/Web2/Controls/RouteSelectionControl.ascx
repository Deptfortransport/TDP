<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TransportModesControl" Src="TransportModesControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RouteSelectionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RouteSelectionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<table class="routeselectmain" cellpadding="0" cellspacing="0" title="tblMainRouteControl"  >
	
		<tr valign="top">
			<td colspan="2">
				<asp:repeater id="RouteSelectionRepeater" runat="server">
					<headertemplate>
						<table cellspacing="0" border="0" id="<%# GetTableId%>"  summary="<%# GetTableSummary %>"
							class="txtseven">
							<thead>
								<tr>
									<th class="rowheader" align="left" width="150" colspan="2">
										<asp:label runat="server" id="labelSelectRoute" />
									</th>
									<th class="rowheader" align="left" width="50">
										<asp:label id="labelDepTime" runat="server" />
									</th>
									<th class="rowheader" width="50" align="left">
										<asp:label id="labelArrTime" runat="server" />
									</th>
								</tr>
							</thead>
							<tbody>
					</headertemplate>
					<itemtemplate>
						<tr  id="row" runat="server">
							<td valign="middle">
								<cc1:ScriptableGroupRadioButton EnableClientScript="true" scriptname="RowHighlighter" action="<%# GetAction %>" id="routeRadioButton" runat="server" GroupName="routeType">
								</cc1:ScriptableGroupRadioButton>
							</td>
							<td valign="top">
								<asp:Label ID="labelTrasportMode" runat="server"></asp:Label>
							</td>
							<td valign="middle">
								<asp:label id="labelDepart" runat="server"></asp:label></td>
							<td valign="middle">
								<asp:label id="labelArrive" runat="server"></asp:label></td>
						</tr>
					</itemtemplate>
					<footertemplate></tbody>
</table>
</FooterTemplate> </asp:repeater></td></tr>
<tr>
	<td class="rowheader" align="left">
		<span class="txtsevenb">
			<uc1:hyperlinkpostbackcontrol id="earlierHyperlink" runat="server"></uc1:hyperlinkpostbackcontrol>
		</span>
	</td>
	<td class="rowheader" align="right">
		<span class="txtsevenb">
			<uc1:hyperlinkpostbackcontrol id="laterHyperlink" runat="server"></uc1:hyperlinkpostbackcontrol>
		</span>
	</td>
</tr>
</table>
