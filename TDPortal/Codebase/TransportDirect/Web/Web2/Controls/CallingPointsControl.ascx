<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CallingPointsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CallingPointsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<asp:repeater id="serviceDetailsRepeater" runat="server" EnableViewState="False">
	<HeaderTemplate>
		<table  class="<%# TableId() %>" lang="en" summary="Train service details">
			<thead>
				<tr class="<%# HeaderRowId() %>">
					<th class="<%# HeaderColId(0) %>">
						<h4><%# HeaderDescription() %></h4>
					</th>
					<th class="<%# HeaderColId(1) %>">
						<%# HeaderArrival() %>
					</th>
					<th class="<%# HeaderColId(2) %>">
						<%# HeaderDeparture() %>
					</th>
				</tr>
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="<%# DetailRowId() %>">
			<td class="<%# DetailColId(Container.ItemIndex, 0) %>">
				<%# DetailDescription(Container.ItemIndex) %>
			</td>
			<td class="<%# DetailColId(Container.ItemIndex, 1) %>">
				<%# DetailArrival(Container.ItemIndex) %>
			</td>
			<td class="<%# DetailColId(Container.ItemIndex, 2) %>">
				<%# DetailDeparture(Container.ItemIndex) %>
			</td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</tbody> </table>
	</FooterTemplate>
</asp:repeater>
