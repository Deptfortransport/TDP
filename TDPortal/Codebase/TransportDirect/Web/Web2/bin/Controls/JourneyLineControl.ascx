<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyLineControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyLineControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<table cellpadding="0" cellspacing="0" class="txtseven" width="100%">
	<asp:repeater id="ballAndLine" runat="server">
		<headertemplate>
			<tr>
		</headertemplate>
		<itemtemplate>
			<td class="bglinehorizontal" align="<%# GetAlignment(Container.ItemIndex) %>"><asp:image id="image1" runat="server"></asp:image>&nbsp;</td>
			<td class="bglinehorizontal" align="<%# GetAlignment(Container.ItemIndex) %>"><asp:image id="image2" runat="server"></asp:image>&nbsp;</td>
			<td class="bglinehorizontal" align="<%# GetAlignment(Container.ItemIndex) %>"><asp:image id="image3" runat="server"></asp:image>&nbsp;</td>
		</itemtemplate>
		<footertemplate>
			</tr>
		</footertemplate>
	</asp:repeater>
	<asp:repeater id="locations" runat="server">
		<headertemplate>
			<tr>
		</headertemplate>
		<itemtemplate>
			<td colspan="3" align="<%# GetAlignment(Container.ItemIndex) %>" class="<%# GetClass(Container.ItemIndex) %>">
				<asp:label runat="server" id="labelText"><%# GetDescription(Container.DataItem) %></asp:label>
			</td>
		</itemtemplate>
		<footertemplate>
			</tr>
		</footertemplate>
	</asp:repeater>
</table>
