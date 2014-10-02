<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OtherCostsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.OtherCostsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:repeater id="OtherCostsRepeater" Runat="server">
	<HeaderTemplate>
		<tr>
			<td><%# HeaderText%></td>
			<td align="right"><cc1:tdimage id="imageOtherCost" imageUrl="<%# imageOtherCostsURL%>" AlternateText="<%# imageOtherCostsAltText%>" runat="server"></cc1:tdimage></td>
		</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<asp:TableRow id="Row" runat="server">
			<asp:TableCell id="Cell" CssClass="cc5" runat="server"><%# ((string[])Container.DataItem)[0] %><br /></asp:TableCell>
			<asp:TableCell id="BlankCell" runat="server" visible="False"></asp:TableCell>
			<asp:TableCell id="ImageCell" runat="server" visible="False" HorizontalAlign="right"><cc1:tdimage id="imageCarParkCost" imageUrl="<%# imageCarParkCostsURL%>" AlternateText="<%# imageCarParkCostsAltText%>" runat="server" ></cc1:tdimage></asp:TableCell>
			<asp:TableCell id="PoundsCell" CssClass="ccPounds" runat="server" visible="False"><%# ((string[])Container.DataItem)[1] %></asp:TableCell>
			<asp:TableCell id="PenceCell"  CssClass="ccPence" runat="server" visible="False"><%# ((string[])Container.DataItem)[2] %></asp:TableCell>
			<asp:TableCell id="UnknownCell" CssClass="ccUnknown" runat="server" visible="False" colspan="2"><%# ((string[])Container.DataItem)[1] %></asp:TableCell>
			<asp:TableCell id="NoteCell" class="cc8" runat="server"><%# ((string[])Container.DataItem)[3] %></asp:TableCell>
		</asp:TableRow>
	</ItemTemplate>
</asp:repeater>

