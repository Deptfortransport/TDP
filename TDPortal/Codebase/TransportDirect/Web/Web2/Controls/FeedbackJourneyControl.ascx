<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackJourneyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackJourneyControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="spacer1">&nbsp;</div>
<div id="outerDiv" runat="server">
	<div id="boxtypeten" style="WIDTH: 560px">
		<div class="txtsevenb"><asp:literal id="literalTitle" runat="server"></asp:literal></div>
		<asp:table runat="server" cellspacing="0" id="tableJourneyInfo" width="100%">
			<asp:tablerow runat="server" id="rowLocation">
				<asp:tablecell runat="server" id="cellDepartLocation" width="40%" cssclass="txtseven"></asp:tablecell>
				<asp:tablecell runat="server" id="cellTo" horizontalalign="center" cssclass="txtsevenb"></asp:tablecell>
				<asp:tablecell runat="server" id="cellArriveLocation" horizontalalign="left" width="40%" cssclass="txtseven"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowSpacer1">
				<asp:tablecell runat="server" id="cellSpacer1" Height="5" columnspan="3" horizontalalign="left"
					cssclass="txtseven"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowOutwardTime">
				<asp:tablecell runat="server" id="cellOutwardTime" columnspan="3" horizontalalign="left"
					cssclass="txtseven"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowSpacer2">
				<asp:tablecell runat="server" id="cellSpacer2" Height="2" columnspan="3" horizontalalign="left"
					cssclass="txtseven"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowReturnTime">
				<asp:tablecell runat="server" id="cellReturnTime" columnspan="3" horizontalalign="left"
					cssclass="txtseven"></asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowTransportModes">
				<asp:tablecell runat="server" id="cellTransportModes" columnspan="3" cssclass="txtseven" horizontalalign="left"></asp:tablecell>
			</asp:tablerow>
		</asp:table>
	</div>
</div>
