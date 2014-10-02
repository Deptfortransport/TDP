<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ItineraryViasControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ItineraryViasControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table>
	<tr>
		<td valign="top"><asp:label id="labelTitle1Text" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
		<td rowspan="2"><asp:repeater id="locationsList" runat="server" enableviewstate="False">
				<itemtemplate>
					<p class="txtsevenb"><%# (string)Container.DataItem %></p>
				</itemtemplate>
			</asp:repeater><asp:repeater id="returnList" runat="server" enableviewstate="False">
				<itemtemplate>
					<p class="txtsevenb"><%# (string)Container.DataItem %></p>
				</itemtemplate>
			</asp:repeater></td>
	</tr>
	<tr>
		<td valign="top"><asp:label id="labelTitle2Text" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
	</tr>
</table>
