<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyDetailsTableGridControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableGridControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VehicleFeaturesControl" Src="VehicleFeaturesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LegInstructionsControl" Src="LegInstructionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="NetworkMapLinksControl" Src="NetworkMapLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WalkitLinkControl" Src="WalkitLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="CJPUserInfoControl" Src="CJPUserInfoControl.ascx" %>

<asp:repeater id="detailsRepeater" runat="server">
	<headertemplate>
		<table class="jdtgrid" cellspacing="0" cellpadding="5px" lang="en" summary="Journey details table">
			<thead>
				<tr>
					<%# HeaderItem(1) %>
					<%# HeaderItem(2) %>
					<%# HeaderItem(3) %>
					<%# HeaderItem(4) %>
					<%# HeaderItem(5) %>
					<%# HeaderItem(6) %>
					<%# HeaderItem(7) %>
					<%# HeaderItem(8) %>
					<%# HeaderItem(9) %>
					<%# HeaderItem(10) %>
				</tr>
			</thead>
			<tbody>
	</headertemplate>
	<itemtemplate>
		<tr>
			<%# CellStart(1) %>
			<uc1:HyperlinkPostBackControl id="modeLinkControl" runat="server" CommandName="<%# GetCommandName(Container.ItemIndex) %>" CommandArgument="<%# GetCommandArgument((JourneyLeg)Container.DataItem) %>"></uc1:HyperlinkPostBackControl>
			<%# DetailsItem(1, Container.ItemIndex) %>
			<uc1:CJPUserInfoControl id="cjpUserInfoWalkLength" runat="server" InfoType="WalkLength" newLineBefore="true" />
			<uc1:CJPUserInfoControl id="cjpUserInfoJourneyLegSource" runat="server" InfoType="DataSource" newLineBefore="true" />
			<%# CellEnd(1) %>

			<%# CellStart(2) %>
			<asp:label id="startLocationLabelControl" runat="server"></asp:label>
			<asp:HyperLink id="startLocationInfoLink" runat="server" Target="_blank" visible="false"/>
			<uc1:HyperlinkPostBackControl id="startLocationInfoLinkControl" runat="server"></uc1:HyperlinkPostBackControl>
			<uc1:CJPUserInfoControl id="cjpUserLocationNaptanInfoStart" runat="server" InfoType="NaPTAN" NewLineBefore="true" />
			<uc1:CJPUserInfoControl ID="cjpUserLocationCoordinateInfoStart" runat="server" InfoType="Coordinate" NewLineBefore="true" />				
			<%# CellEnd(2) %>

			<%# CellStart(3) %>
			<%# DetailsItem(3, Container.ItemIndex) %>

			<%# CellEnd(3) %>

			<%# CellStart(4) %>
			<%# DetailsItem(4, Container.ItemIndex) %>
			<%# DetailsItem(10, Container.ItemIndex)%>
            <cc1:tdbutton id="departButton" runat="server" enableclientscript="true"  causesvalidation="False" style="vertical-align:top;"></cc1:tdbutton>
            <noscript>
            <asp:HyperLink id="hyperlinkDepartureBoard" runat="server" 
                Text="<%# DepartureBoardButtonText %>" Visible="<%# DepartureBoardButtonVisible(Container.ItemIndex) %>" />
            </noscript>

			<%# CellEnd(4) %>

			<%# CellStart(5) %>
			<asp:label id="endLocationLabelControl" runat="server"></asp:label>
			<asp:HyperLink id="endLocationInfoLink" runat="server" Target="_blank" visible="false"/>
			<uc1:HyperlinkPostBackControl id="endLocationInfoLinkControl" runat="server"></uc1:HyperlinkPostBackControl>
			<uc1:CJPUserInfoControl id="cjpUserLocationNaptanInfoEnd" runat="server" InfoType="NaPTAN" NewLineBefore="true" />
			<uc1:CJPUserInfoControl ID="cjpUserLocationCoordinateInfoEnd" runat="server" InfoType="Coordinate" NewLineBefore="true" />				
			<%# CellEnd(5) %>

			<%# CellStart(6) %>
			<%# DetailsItem(6, Container.ItemIndex) %>
			<%# DetailsItem(10, Container.ItemIndex)%>
            <cc1:tdbutton id="arriveButton" runat="server" enableclientscript="true"  causesvalidation="False" style="vertical-align:top;"></cc1:tdbutton>
            <noscript>
            <asp:HyperLink ID="hyperlinkArrivalBoard" runat="server" Text="<%# ArrivalBoardButtonText %>" Visible="<%# ArrivalBoardButtonVisible(Container.ItemIndex) %>" />
            </noscript>
			<%# CellEnd(6) %>

			<%# CellStart(7) %>
			<%# DetailsItem(7, Container.ItemIndex) %>
			<%# CellEnd(7) %>

			<%# CellStart(8) %>
			<uc1:LegInstructionsControl id="legInstructionsControl" runat="server"></uc1:LegInstructionsControl>
			<uc1:VehicleFeaturesControl id="vehicleFeaturesControl" runat="server"></uc1:Vehiclefeaturescontrol>
			<uc1:NetworkMapLinksControl id="networkMapLink" runat="server"></uc1:NetworkMapLinksControl>
			<uc1:WalkitLinkControl ID="walkitLink" runat="server" />
			<%# DetailsItem(8, Container.ItemIndex) %>
			<%# CellEnd(8) %>

			<%# CellStart(9) %>
			<%# DetailsItem(9, Container.ItemIndex) %>
			<%# CellEnd(9) %>

			<%# CellStart(10) %>
			<cc1:tdbutton id="buttonMap" Runat="server" Text="<%# MapButtonText %>"  CommandName="<%# Container.ItemIndex %>" />
            <asp:hyperlink runat="server" id="walkitImageLink" Target="_blank" Visible ="false" CssClass="TDButtonHyperlink">
            </asp:hyperlink>                                                         
            <%# CellEnd(10) %>
		</tr>
	</itemtemplate>
	<footertemplate>
		</tbody> </table>
	</footertemplate>
</asp:repeater>

