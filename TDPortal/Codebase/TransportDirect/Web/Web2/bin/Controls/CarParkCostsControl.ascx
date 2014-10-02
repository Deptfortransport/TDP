<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarParkCostsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarParkCostsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<tr>
	<td class="cc5">
		<asp:label id="otherCostsHeaderLabel" runat="server" visible="false" enableviewstate="False"></asp:label></td>
	<td align="right"><cc1:tdimage id="imageOtherCost" runat="server" ></cc1:tdimage></td>
	<td></td>
	<td></td>
	<td></td>
</tr>
<tr>
	<td class="cc5" align="left" valign="top" runat="server">
		<uc1:hyperlinkpostbackcontrol id="carParkInfoLinkControl" runat="server" visible="false"></uc1:hyperlinkpostbackcontrol>
	</td>
	<td align="right"><cc1:tdimage id="imageCarParkCost" runat="server" visible="False"></cc1:tdimage></td>
	<td class="ccPounds" valign="top"><asp:label id="labelCarParkPound" runat="server" visible="False" enableviewstate="False"></asp:label></td>
	<td class="ccPence" valign="top"><asp:label id="labelCarParkPence" runat="server" visible="False" enableviewstate="False"></asp:label></td>
	<td class="cc8" valign="top">
		<asp:label id="labelCarParkNote" runat="server" visible="False" enableviewstate="False"></asp:label></td>
</tr>
