<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RetailerListControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RetailerListControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:repeater id="retailersList" runat="server">
	<headertemplate>
		<table cellspacing="0" cellpadding="0" class="retailertable" summary="<%# ListTableSummary %>">
	</headertemplate>
	<footertemplate>
		</table>
	</footertemplate>
	<itemtemplate>
		<tr>
			<td class="retailernamecell" valign="middle" align="left">
				<asp:placeholder id="logoPlaceholder" runat="server"></asp:placeholder>
				<asp:hyperlink id="linkRetailerName" runat="server" cssclass="bluelink" enableviewstate="False"></asp:hyperlink>
				<asp:label id="labelRetailerName" runat="server" enableviewstate="False"></asp:label>
			</td>
			<td class="retailerbuycell" valign="middle" align="right">
				<cc1:tdbutton id="buyButton" runat="server"></cc1:tdbutton>
			</td>
		</tr>
	</itemtemplate>
</asp:repeater>
