<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LegDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LegDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="LocalZonalFaresControl" Src="LocalZonalFaresControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocalZonalOpertatorFaresControl" Src="LocalZonalOpertatorFaresControl.ascx" %>
<asp:table id="legDetailsTable" runat="server" cellspacing="0" cellpadding="5" CssClass="<%# GetCssClass() %>">
	<asp:tablerow id="modeRow" runat="server">
		<asp:tablecell id="modeCell" runat="server" columnspan="2">
			<strong>
				<asp:label runat="server" id="labelMode"></asp:label></strong>
			<asp:label runat="server" id="labelLegDescription"></asp:label>
		</asp:tablecell>
	</asp:tablerow>
	<asp:tablerow id="legRetailerRow" runat="server">
		<asp:tablecell runat="server" CssClass ="<%# GetRowCssClass() %>">
			<asp:label runat="server" id="labelLegService"></asp:label>
<asp:Label Runat="server" ID="labelNotHaveFare"></asp:Label>
			<uc1:LocalZonalFaresControl id="LocalZonalFaresControl1" runat="server"></uc1:LocalZonalFaresControl>
			<uc1:LocalZonalOpertatorFaresControl id="LocalZonalOpertatorFaresControl1" runat="server"></uc1:LocalZonalOpertatorFaresControl></asp:tablecell></asp:TableRow>
	<asp:tablerow id="dummyRow1" runat="server"></asp:tablerow>
	<asp:tablerow id="dummyRow2" runat="server"></asp:tablerow>
	<asp:tablerow id="faresIncludedRow" runat="server">
		<asp:tablecell id="faresIncludedCell" columnspan="2" runat="server">
			<asp:label runat="server" id="labelFaresAbove" enableviewstate="False"></asp:label>
		</asp:tablecell>
	</asp:tablerow>
</asp:table>
