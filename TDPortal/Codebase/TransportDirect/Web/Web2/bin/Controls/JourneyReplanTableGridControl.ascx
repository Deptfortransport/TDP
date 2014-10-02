<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyReplanTableGridControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyReplanTableGridControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VehicleFeaturesControl" Src="VehicleFeaturesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LegInstructionsControl" Src="LegInstructionsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:repeater id="detailsRepeater" runat="server">
	<headertemplate>
		<table class="jdtgrid" cellspacing="0" cellpadding="5px" lang="en" summary="Journey details table">
			<thead>
				<tr>
					<%# HeaderItem(0) %>
					<%# HeaderItem(1) %>
					<%# HeaderItem(2) %>
					<%# HeaderItem(3) %>
					<%# HeaderItem(4) %>
					<%# HeaderItem(5) %>
					<%# HeaderItem(6) %>
					<%# HeaderItem(7) %>
					<%# HeaderItem(8) %>
					<%# HeaderItem(9) %>
				</tr>
			</thead>
			<tbody>
	</headertemplate>
	<itemtemplate>
		<tr id="<%# GetHighlightRowId( Container.ItemIndex) %>" class="<%# GetHighlightRowClass( Container.ItemIndex) %>">
			<%# CellStart(0) %>
				<cc1:scriptablecheckbox id="checkJourneyElement" EnableClientScript="true" ScriptName="JourneyReplanElementSelection" Action="HandleReplanTableChecks()" Value="<%# GetItemIndex( Container.ItemIndex ) %>" Checked="<%# GetCheckedStatus( Container.ItemIndex ) %>" runat="server"></cc1:scriptablecheckbox>
			<%# CellEnd(0) %>
			
			<%# CellStart(1) %>
			<asp:label id="modeLinkLabel" runat="server" enableviewstate="false"></asp:label>
			<%# DetailsItem(1, Container.ItemIndex) %>
			<%# CellEnd(1) %>

			<%# CellStart(2) %>
			<asp:label id="startLocationLabelControl" runat="server" enableviewstate="false"></asp:label>
			<%# CellEnd(2) %>

			<%# CellStart(3) %>
			<%# DetailsItem(3, Container.ItemIndex) %>
			<%# CellEnd(3) %>

			<%# CellStart(4) %>
			<%# DetailsItem(4, Container.ItemIndex) %>
			<%# CellEnd(4) %>

			<%# CellStart(5) %>
			<asp:label id="endLocationLabelControl" runat="server" enableviewstate="false"></asp:label>
			<%# CellEnd(5) %>

			<%# CellStart(6) %>
			<%# DetailsItem(6, Container.ItemIndex) %>
			<%# CellEnd(6) %>

			<%# CellStart(7) %>
			<%# DetailsItem(7, Container.ItemIndex) %>
			<%# CellEnd(7) %>

			<%# CellStart(8) %>
			<uc1:leginstructionscontrol id="legInstructionsControl" runat="server" enableviewstate="false"></uc1:leginstructionscontrol>
			<uc1:vehiclefeaturescontrol id="vehicleFeaturesControl" runat="server" enableviewstate="false"></uc1:vehiclefeaturescontrol>
			<%# DetailsItem(8, Container.ItemIndex) %>
			<%# CellEnd(8) %>

			<%# CellStart(9) %>
			<%# DetailsItem(9, Container.ItemIndex) %>
			<%# CellEnd(9) %>

		</tr>
	</itemtemplate>
	<footertemplate>
		</tbody> </table>
	</footertemplate>
</asp:repeater>
