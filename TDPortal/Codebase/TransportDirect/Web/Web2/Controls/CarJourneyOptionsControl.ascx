<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarJourneyOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarJourneyOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table id="tableCarJourneyOptions" cellspacing="2" cellpadding="2" summary="Summary of car journey options used" width="100%" border="0">
	<tr>
		<td><asp:label id="labelCarJourneyOptionsTitle" runat="server" CssClass="txtsevenb"></asp:label></td>
	</tr>	
	<tr>
		<td>
			<asp:Panel ID="pnlRow1" Runat="server" Visible="False">
				<asp:label id="labelMaxSpeed" runat="server" CssClass="txtseven"></asp:label>
				<asp:label id="labelMotorways" runat="server" CssClass="txtseven"></asp:label>
				<asp:label id="labelAvoid" runat="server" CssClass="txtseven"></asp:label>
			</asp:Panel>
			<asp:Panel ID="pnlRow2" Runat="server" Visible="False">
				<asp:label id="labelUseRoads" runat="server" CssClass="txtseven"></asp:label>
			</asp:Panel>
			<asp:Panel ID="pnlRow3" Runat="server" Visible="False">
				<asp:label id="labelAvoidRoads" runat="server" CssClass="txtseven"></asp:label>
			</asp:Panel>
			<asp:Panel ID="pnlRow4" Runat="server" Visible="False">
				<asp:label id="labelViaLocation" runat="server" CssClass="txtseven"></asp:label>
			</asp:Panel>
		</td>
	</tr>
</table>
