<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZonalFareDetailsTableSegmentControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ZonalFareDetailsTableSegmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="LocalZonalFaresControl" Src="LocalZonalFaresControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocalZonalOpertatorFaresControl" Src="LocalZonalOpertatorFaresControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LegDetailsControl" Src="LegDetailsControl.ascx" %>
<asp:repeater id="zonalfares" enableviewstate="False" runat="server">
	<itemtemplate>
		<asp:placeholder id="faresTablePlaceholder" runat="server"></asp:placeholder>
		<uc1:legdetailscontrol id="legDetails" runat="server"></uc1:legdetailscontrol>
	</itemtemplate>
</asp:repeater>
