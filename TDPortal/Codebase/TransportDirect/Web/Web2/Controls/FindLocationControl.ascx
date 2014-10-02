<%@ Register TagPrefix="uc1" TagName="FindPlaceDropDownControl" Src="FindPlaceDropDownControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ParkAndRideSelectionControl" Src="ParkAndRideSelectionControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table lang="en" cellspacing="0" width="100%" style="MARGIN-BOTTOM:10px">
	<tr>
		<td nowrap="nowrap" ><asp:label id="stationTypeLabel" runat="server" cssclass="txtsevenb"></asp:label></td>
		<td><asp:checkboxlist id="stationTypesCheckList" runat="server" cssclass="txtseven" repeatdirection="Horizontal"></asp:checkboxlist></td>
	</tr>
	<tr>
		<td class="findafromcolumn" align="right"><asp:label id="directionLabel" runat="server" cssclass="txtsevenb"></asp:label></td>
		<td><uc1:findplacedropdowncontrol id="placeControl" runat="server"></uc1:findplacedropdowncontrol>
			<uc1:tristatelocationcontrol2 id="tristateLocationControl" runat="server"></uc1:tristatelocationcontrol2><uc1:parkandrideselectioncontrol id="parkAndRideControl" runat="server" />
		</td>
	</tr>
	<tr>
		<td colspan="2" align="right" class="FindNearestButtonCell"><cc1:tdbutton id="findNearestButton" runat="server"></cc1:tdbutton></td>
	</tr>
</table>
<asp:Table id="spaceTable" Visible="False" Runat="server" EnableViewState="False">
	<asp:TableRow>
		<asp:TableCell Height="15px"></asp:TableCell>
	</asp:TableRow>
</asp:Table>
