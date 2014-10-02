<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="TriStateLocationControl2.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="VisitPlannerLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.VisitPlannerLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="true" %>
<asp:panel id="locationPanel" runat="server">
	<table width="100%">
		<tr>
			<td class="findafromcolumn" align="right">
				<asp:label id="labelFrom" runat="server" cssclass="txtsevenb"></asp:label></td>
			<td>
				<uc1:tristatelocationcontrol2 id="triStateLocationControl" runat="server"></uc1:tristatelocationcontrol2></td>
		</tr>
		<tr>
			<td class="findafromcolumn" align="right">
				<asp:label id="labelReturnJourney" runat="server" cssclass="txtsevenb"></asp:label></td>
			<td>
				<asp:checkbox id="returnToStartingPoint" runat="server"></asp:checkbox>
				<asp:label id="labelRetunToStartPoint" runat="server" cssclass="txtseven"></asp:label></td>
		</tr>
		<tr>
			<td class="findafromcolumn" align="right">
				<asp:label id="labelLengthOfStay" runat="server" cssclass="txtsevenb"></asp:label></td>
			<td>
				<asp:dropdownlist id="listLengthOfStay" runat="server"></asp:dropdownlist>
				<asp:label id="labelHour" runat="server" cssclass="txtsevenb"></asp:label></td>
		</tr>
	</table>
</asp:panel>
