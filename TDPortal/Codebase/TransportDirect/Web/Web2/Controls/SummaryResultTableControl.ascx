<%@ Import namespace="TransportDirect.UserPortal.Web.Adapters" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SummaryResultTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SummaryResultTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:repeater id="summaryRepeater" runat="server" OnItemDataBound="summaryRepeater_ItemDatabound">
	<headertemplate>
		<table cellspacing="0" class="jdetail" summary="Journey Summary" lang="en">
			<thead>
				<tr bgcolor="#ffffff">
					<th id="screenreaderheader1" align="left" runat="server">
						<%# HeaderOption %>
					</th>
					<th id="screenreaderheader2" align="left" runat="server">
						<%# HeaderTransport %>
					</th>
					<th id="screenreaderheader3" align="left" runat="server">
						<%# HeaderChanges %>
					</th>
					<th id="screenreaderheader4" align="left" runat="server">
						<%# HeaderLeave %>
					</th>
					<th id="screenreaderheader5" align="left" runat="server">
						<%# HeaderArrive %>
					</th>
					<th id="screenreaderheader6" align="left" runat="server">
						<%# HeaderDuration %>
					</th>
					<th id="screenreaderheader7" align="left" runat="server">
						<%# HeaderSelect %>
					</th>
				</tr>
			</thead>
			<tbody>
	</headertemplate>
	<itemtemplate>
			<tr>
				<th id="screenreaderchild1" scope="row" align="center" runat="server"><%#((FormattedJourneySummaryLine)Container.DataItem).GetFormattedDisplayNumber() %></th>
				<td id="screenreaderchild2" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem ).GetTransportModes() %></td>
				<td id="screenreaderchild3" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem ).InterchangeCount %></td>
				<td id="screenreaderchild4" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem ).GetDepartureTime() %></td>
				<td id="screenreaderchild5" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem ).GetArrivalTime() %></td>
				<td id="screenreaderchild6" runat="server"><%# ((FormattedJourneySummaryLine)Container.DataItem ).GetDuration() %></td>
				<td id="screenreaderchild7" align="right" runat="server">
					<asp:label runat="server" id="labelBlank" visible="<%# !RadioButtonsVisible %>">&nbsp;</asp:label>
					<asp:ImageButton runat="server" id="ImageButton" visible="<%# RadioButtonsVisible %>" commandname="<%# ((FormattedJourneySummaryLine)Container.DataItem ).GetButtonCommandName() %>" runat="server" imageurl="<%# GetButtonImageUrl(Container.ItemIndex) %>" AlternateText="<%# AlternateText(Container.ItemIndex) %>"></asp:ImageButton>
				</td>
			</tr>
	</itemtemplate>
	<footertemplate>
		</tbody></table>
	</footertemplate>
</asp:repeater>
