<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UserSurveyRadioMatrix.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.UserSurveyRadioMatrix" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="TransportDirect.UserPortal.DataServices" %>
<asp:repeater id="Q9RadioMatrix" runat="server">
	<headertemplate>
		<table border="0" cellspacing="0" id="Q9RadioTable">
			<tr class="txtsevenb">
				<td class="txtsevenb">
					&nbsp;
				</td>
				<td>
					&nbsp;
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(1) %>
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(2) %>
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(3) %>
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(4) %>
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(5) %>
				</td>
				<td align="center" width="50" class="txtnineb"><%# HeaderItem(6) %>
				</td>
			</tr>
	</headertemplate>
	<itemtemplate>
		<tr>
			<td class="txtsevenb">
				<asp:label id="InvalidMarker" runat="server" cssclass="txtsevenb" forecolor="#ff0000" visible="False">*</asp:label>
			</td>
			<td align="left" class="txtseven" width="210px">
				<asp:label runat="server" font-size="8">
					<%# GetQ9Statement( (DSDropItem)Container.DataItem ) %>
				</asp:label>
			</td>
			<td align="center">
				<asp:radiobutton runat="server" id="a1" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
			<td align="center">
				<asp:radiobutton runat="server" id="a2" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
			<td align="center">
				<asp:radiobutton  runat="server" id="a3" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
			<td align="center">
				<asp:radiobutton  runat="server" id="a4" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
			<td align="center">
				<asp:radiobutton runat="server" id="a5" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
			<td align="center">
				<asp:radiobutton  runat="server" id="a6" groupname='<%# String.Format("q9group{0}",Container.ItemIndex.ToString())  %>'>
				</asp:radiobutton></td>
		</tr>
	</itemtemplate>
	<separatortemplate>
		<tr>
			<td colspan="7" height="10"></td>
		</tr>
	</separatortemplate>
	<footertemplate>
		</table>
	</footertemplate>
</asp:repeater>
<p><asp:label id="LabelInvalid" runat="server" cssclass="txtsevenb" font-bold="True" forecolor="Red" font-size="8pt"></asp:label></p>
