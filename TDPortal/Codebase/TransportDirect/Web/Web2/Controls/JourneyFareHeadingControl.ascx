<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyFareHeadingControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyFareHeadingControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="false" %>
<a name="MainContent"></a>
<div id="boxtypeten">
	<table class="FaresSummaryTable" cellpadding="0" cellspacing="0">
		
		<tr>
			<td colspan="3"><h1><asp:label id="labelFaresTitle" runat="server">Fares found for</asp:label>
			<asp:label id="originName" runat="server">Wick</asp:label>
			<asp:label id="labelTo" runat="server">to</asp:label>
			<asp:label id="destinationName" runat="server">Glasgow</asp:label></h1></td>
			<td>&nbsp;</td>
			
		</tr>
		
		<tr>
			<td><asp:label id="labelRailcard" runat="server" cssclass="txtsevenb">Discount rail card</asp:label>&nbsp;&nbsp;
				<asp:label id="railcardName" runat="server" cssclass="txtseven">Senior Plus</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			<td><asp:label id="labelCoachcard" runat="server" cssclass="txtsevenb">Discount coach card</asp:label>&nbsp;&nbsp;
				<asp:label id="coachcardName" runat="server" cssclass="txtseven">Senior Gold</asp:label></td>
			<td>&nbsp;</td>
		</tr>
	</table>
</div>