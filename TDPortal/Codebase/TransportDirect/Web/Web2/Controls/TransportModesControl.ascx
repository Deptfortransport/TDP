<%@ Import namespace="TransportDirect.JourneyPlanning.CJPInterface" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TransportModesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TransportModesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<asp:repeater id="repeaterTransportModesControl" runat="server">
	<itemtemplate>
	<img title="<%# GetResource("TransportModesControl.image" + ((ModeType)Container.DataItem).ToString() + "AltText")%>"  alt="<%# GetResource("TransportModesControl.image" + ((ModeType)Container.DataItem).ToString() + "AltText")%>" src="<%# GetResource("TransportModesControl.image" + ((ModeType)Container.DataItem).ToString() + "URL")%>">
	</itemtemplate>
	<separatortemplate>
	<img title="<%# GetResource("TransportModesControl.crossSymbol.altText")%>" alt="<%# GetResource("TransportModesControl.crossSymbol.altText")%>" src="<%# GetResource("TransportModesControl.crossSymbol")%>">
	</separatortemplate>
</asp:repeater>
