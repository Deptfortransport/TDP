<%@ Control Language="c#" AutoEventWireup="True" Codebehind="VisitPlannerJourneyOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.VisitPlannerJourneyOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="JourneyLineControl" Src="JourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RouteSelectionControl" Src="RouteSelectionControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table class="boxtypeten874x768blue" id="tblMain" width="100%">
	<tr>
		<td align="center" class="boxtypewhite">
			<table id="tblControls">
				<tr>
					<td align="center" colspan="3"><uc1:journeylinecontrol runat="server" id="journeyLineVisitPlanner" EnableViewState="false" /></td>
				</tr>
				<tr align="center">
					<td valign="top">
						<uc1:routeselectioncontrol runat="server" id="routeSelectionControl1" />
					</td>
					<td valign="top">
						<uc1:routeselectioncontrol runat="server" id="routeSelectionControl2"  />
					</td>
					<td valign="top">
						<uc1:routeselectioncontrol runat="server" id="routeSelectionControl3"  />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="right" valign="middle" class="txtsevenb">
			<asp:label runat="server" id="labelShow" />&nbsp;
			<asp:dropdownlist id="ShowSelectionDropDown" runat="server" />&nbsp;			
			<cc1:tdbutton id="OK" runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
