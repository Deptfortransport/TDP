<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="CarJourneyDetailsTableControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyDetailsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableGridControl" Src="JourneyDetailsTableGridControl.ascx" %>
<asp:repeater id="detailsRepeater" runat="server">
	<headertemplate>
		<table class="jdt" cellpadding="0" cellspacing="0">
	</headertemplate>
	<itemtemplate>
		<tr>
			<td>
			    <asp:Panel ID="panelDetailsTableGrid" runat="server" Visible="false">
				    <uc1:journeydetailstablegridcontrol id="detailsPublicControl" runat="server" visible="false"></uc1:journeydetailstablegridcontrol>
                </asp:Panel>
            </td>
		</tr>
	</itemtemplate>
	<footertemplate>
		</table>
	</footertemplate>
</asp:repeater>
