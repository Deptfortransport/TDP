<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SummaryC02EmissionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SummaryC02EmissionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="../Controls/HyperlinkPostbackControl.ascx" %>
			<tr>
				<td class="cc5"><asp:Label id="labelCO2Emission" runat="server" enableviewstate="False"></asp:Label></td>
				<td><cc1:tdimage id="imageco2Emission" runat="server"></cc1:tdimage></td>
				<td class="ccPounds"><asp:Label id="labelCO2Amount" runat="server"></asp:Label></td>
				<td class="ccPence"><!-- Blank column no pence--></td>
				<td class="cc8"><uc1:hyperlinkpostbackcontrol id="hyperlinkJourneyEmission" runat="server"></uc1:hyperlinkpostbackcontrol></td>
			</tr>
