<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RetailerMatrixControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RetailerMatrixControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<%@ Register TagPrefix="uc1" TagName="RetailerListControl" Src="RetailerListControl.ascx" %>
<div class="matrixborder">
	<div class="matrixheading">
		<h2><asp:label id="labelTicketRetailers" runat="server"></asp:label></h2>
	</div>
	<asp:panel id="onlinePanel" runat="server">
		<h3><asp:label id="labelOnline" runat="server"></asp:label></h3>
		<br />
		<uc1:retailerlistcontrol id="onlineList" runat="server"></uc1:retailerlistcontrol>
	</asp:panel>
	<asp:panel id="offlinePanel" runat="server">
		<h3><asp:label id="labelOffline" runat="server"></asp:label></h3>
		<br />
		<uc1:retailerlistcontrol id="offlineList" runat="server"></uc1:retailerlistcontrol>
	</asp:panel>
</div>
