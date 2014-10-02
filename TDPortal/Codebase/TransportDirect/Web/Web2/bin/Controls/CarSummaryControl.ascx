<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CarSummaryControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarSummaryControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table lang="en" class="sumdetail" cellspacing="0" border="0">
	<tbody>
		<tr id="adjustedJourneyRow" runat="server">
			<td class="sumd" style="WIDTH: 178px"><span class="txteightb"><asp:label id="labelSummaryOfDirections" runat="server"></asp:label></span></td>
			<td class="sumd" style="WIDTH: 200px"><span class="txtsevenb"><asp:label id="labelAdjustedTotalDistance" runat="server"></asp:label>&nbsp;</span><span class="txtseven"><asp:label id="labelAdjustedTotalDistanceNumber" runat="server"></asp:label></span></td>
			<td class="sumd" style="WIDTH: 209px"><span class="txtsevenb"><asp:label id="labelAdjustedTotalDuration" runat="server"></asp:label>&nbsp;</span><span class="txtseven"><asp:label id="labelAdjustedTotalDurationNumber" runat="server"></asp:label></span></td>
			<td class="sumd righttextonly" ><asp:label id="lblUnits" associatedcontrolid="dropdownlistCarSummary" runat="server" CssClass="txtseven" Visible="False"></asp:label></td>
			<td class="sumd sumdUnitsDropDown righttextonly">
			    <cc1:scriptabledropdownlist id="dropdownlistCarSummary" runat="server" scriptname="UnitsSwitch" Visible="False"></cc1:scriptabledropdownlist>
				<input id="<%# GetHiddenInputId %>" type="hidden" value="<%# UnitsState%>" name="<%# GetHiddenInputId %>" />
			</td>
		</tr>
		<tr>
			<td colspan="5"><span class="txtseven"><asp:label id="labelAdjustedRoads" runat="server"></asp:label></span></td>
		</tr>
	</tbody>
</table>
