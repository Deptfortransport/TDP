<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RetailerHandoffHeadingControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RetailerHandoffHeadingControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="boxtypeten">
	<table class="FaresSummaryTable" cellpadding="0" cellspacing="0">
		<tr>
			<td colspan="5"><asp:label id="labelDiscountsTitle" runat="server" cssclass="txtsevenb">Fares found for</asp:label>&nbsp;</td>
		</tr>
		<tr>
			<td><asp:label id="labelRailcard" runat="server" cssclass="txtsevenb">Discount rail card</asp:label>&nbsp;&nbsp;
				<asp:label id="railcardName" runat="server" cssclass="txtseven">Senior Plus</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:label id="labelCoachcard" runat="server" cssclass="txtsevenb">Discount coach card</asp:label>&nbsp;&nbsp;
				<asp:label id="coachcardName" runat="server" cssclass="txtseven">Senior Gold</asp:label>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
	</table>
</div>
