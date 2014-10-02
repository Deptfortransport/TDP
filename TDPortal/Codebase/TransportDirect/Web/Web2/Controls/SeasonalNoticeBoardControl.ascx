<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SeasonalNoticeBoardControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SeasonalNoticeBoardControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="SeasonalNoticeboardHeading" style="WIDTH: 833px"> <%--Changed from 755px to allow right hand menu to fit in SeasonalNoticeBoard.aspx--%>
	&nbsp;<asp:label id="lblHeadingText" visible="true" runat="server" width="100%"></asp:label>
</div>
<p>&nbsp;</p>
<asp:panel id="seasonalNoticeBoardPanel" runat="server" enableviewstate="False">
	<div id="boxtypeeleven" style="OVERFLOW: auto; WIDTH: 755px; HEIGHT: 281px"> <%--Changed width from 755px to allow right hand menu to fit in SeasonalNoticeBoard.aspx--%>
		<asp:repeater id="repeaterSeasonalNews" runat="server">
			<itemtemplate>
				<tr>
					<td class="txtseven" headers="tnhRegion"><%#  DataBinder.Eval(Container.DataItem, "Region") %></td>
					<td class="txtseven" headers="tnhTransportMode"><%# DataBinder.Eval(Container.DataItem, "TransportMode") %></td>
					<td class="txtseven" headers="tnhInformation"><%# DataBinder.Eval(Container.DataItem, "InformationDetail") %></td>
					<td class="txtseven" headers="tnhlblEffectedDates"><%# DataBinder.Eval(Container.DataItem, "EffectedDates") %></td>
					<td class="txtseven" headers="tnhLastUpdated"><%# DataBinder.Eval(Container.DataItem, "LastUpdated") %></td>
				</tr>
			</itemtemplate>
			<alternatingitemtemplate>
				<tr bgcolor="#cccccc">
					<td class="txtseven" headers="tnhRegion"><%#  DataBinder.Eval(Container.DataItem, "Region") %></td>
					<td class="txtseven" headers="tnhTransportMode"><%# DataBinder.Eval(Container.DataItem, "TransportMode") %></td>
					<td class="txtseven" headers="tnhInformation"><%# DataBinder.Eval(Container.DataItem, "InformationDetail") %></td>
					<td class="txtseven" headers="tnhlblEffectedDates"><%# DataBinder.Eval(Container.DataItem, "EffectedDates") %></td>
					<td class="txtseven" headers="tnhLastUpdated"><%# DataBinder.Eval(Container.DataItem, "LastUpdated") %></td>
				</tr>
			</alternatingitemtemplate>
			<headertemplate>
				<table id="SeasonalNewsgrid" summary="<%# TableSummaryText%>" cellspacing="0" cellpadding="1" width="736px" border="1">
					<thead>
						<tr>
							<th id="tnhRegion">
								<asp:label id="lblRegion" runat="server">
									<%# RegionText %>
								</asp:label></th>
							<th id="tnhTransportMode">
								<asp:label id="lblTransportMode" runat="server">
									<%# TransportModeText %>
								</asp:label></th>
							<th id="tnhInformation">
								<asp:label id="lblInformation" runat="server">
									<%# InformationText %>
								</asp:label></th>
							<th id="tnhlblEffectedDates">
								<asp:label id="lblEffectedDates" runat="server">
									<%# EffectedDatesText %>
								</asp:label></th>
							<th id="tnhLastUpdated">
								<asp:label id="lblLastUpdated" runat="server">
									<%# LastUpdatedText %>
								</asp:label></th>
						</tr>
					</thead>
					<tbody>
			</headertemplate>
			<footertemplate>
				</tbody> </table>
			</footertemplate>
		</asp:repeater><br />
	</div>
</asp:panel>
<div id="SeasonalNews_NoData">
	&nbsp;<asp:label id="lblSeasonalNews_NoData" runat="server" visible="False"></asp:label>
	<p>&nbsp;</p>
</div>
