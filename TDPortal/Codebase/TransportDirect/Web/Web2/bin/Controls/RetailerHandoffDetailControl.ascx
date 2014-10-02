<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RetailerHandoffDetailControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RetailerHandoffDetailControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="RequiredTicketsTableControl" Src="RequiredTicketsTableControl.ascx" %>
<asp:repeater id="repeaterTickets" runat="server">
	<itemtemplate>
		<div id="requiredticketsheading">
			<asp:label id="labelMode" runat="server" enableviewstate="False" cssclass="txtsevenb">Mode</asp:label>&nbsp;&nbsp;&nbsp;
			<asp:label id="labelJourney" runat="server" enableviewstate="False" cssclass="txtseven">Journey</asp:label>
		</div>
		<uc1:requiredticketstablecontrol id="requiredTickets" runat="server"></uc1:requiredticketstablecontrol>
	</itemtemplate>
</asp:repeater>
