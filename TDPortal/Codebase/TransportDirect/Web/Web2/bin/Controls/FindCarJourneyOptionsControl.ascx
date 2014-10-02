<%@ Register TagPrefix="uc1" TagName="FindViaLocationControl" Src="FindViaLocationControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindCarJourneyOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCarJourneyOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="tristateroadcontrol" Src="TriStateRoadControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="triStateLocationControl2" Src="TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="LocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<asp:panel id="panelJourneyOptions" runat="server">
	<div class="boxtypetwo"><strong>
			<asp:label id="labelJourneyOptions" runat="server" cssclass="txtsevenb"></asp:label></strong>
		<table cellspacing="0" width="100%">
			<tr>
				<td colspan="7">
					<asp:label id="roadsToAvoidLabel" runat="server" cssclass="txtseven"></asp:label>
					<asp:label id="displayAvoidLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="displayAvoidTollsLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="displayAvoidFerriesLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="displayAvoidMotorwayLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="displayBanLimitedAccessLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:checkbox id="avoidTollsCheckBox" runat="server" cssclass="txtseven"></asp:checkbox>
					<asp:checkbox id="avoidFerriesCheckBox" runat="server" cssclass="txtseven"></asp:checkbox>
					<asp:checkbox id="avoidMotorwayCheckBox" runat="server" cssclass="txtseven"></asp:checkbox>
					<asp:checkbox id="banLimitedAccessCheckBox" runat="server" cssclass="txtseven"></asp:checkbox></td>
			</tr>
			<tr>
				<td colspan="7">
					<asp:label id="roadAlertLabel" runat="server" cssclass="txtseven"></asp:label></td>
			</tr>
			<tr>
				<td valign="bottom" align="left" width="54%">
					<asp:label id="theseRoadsToAvoidLabel" runat="server" cssclass="txtseven"></asp:label>
					<asp:label id="displayTheseRoadsLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="avoidRoadsSRLabel" runat="server" cssclass="screenreader"></asp:label></td>
				<td valign="bottom" align="left" width="46%">
					<uc1:tristateroadcontrol id="avoidRoadSelectControl1" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="avoidRoadSelectControl2" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="avoidRoadSelectControl3" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="avoidRoadSelectControl4" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="avoidRoadSelectControl5" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="avoidRoadSelectControl6" runat="server"></uc1:tristateroadcontrol></td>
			</tr>
			<tr>
				<td colspan="7">
					<asp:label id="note1Label" runat="server" cssclass="txtnote"></asp:label></td>
			</tr>
			<tr>
				<td valign="bottom" align="left" width="54%">
					<asp:label id="theseRoadsToUseLabel" runat="server" cssclass="txtseven"></asp:label>
					<asp:label id="displayUseTheseRoadsLabel" runat="server" cssclass="txtsevenb"></asp:label>
					<asp:label id="useRoadsSRLabel" runat="server" cssclass="screenreader"></asp:label></td>
				<td valign="bottom" align="left" width="46%">
					<uc1:tristateroadcontrol id="useRoadSelectControl1" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="useRoadSelectControl2" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="useRoadSelectControl3" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="useRoadSelectControl4" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="useRoadSelectControl5" runat="server"></uc1:tristateroadcontrol>
					<uc1:tristateroadcontrol id="useRoadSelectControl6" runat="server"></uc1:tristateroadcontrol></td>
			</tr>
			<tr>
				<td colspan="7">
					<asp:label id="note2Label" runat="server" cssclass="txtnote"></asp:label></td>
			</tr>
			<tr>
				<td>
					<asp:label id="travelViaLabel" runat="server" cssclass="txtseven"></asp:label></td>
			</tr>
			<tr>
				<td colspan="7">
					<uc1:tristatelocationcontrol2 id="locationTristateControl" runat="server"></uc1:tristatelocationcontrol2>
					<div class="locationControlViaCar">
					    <uc1:LocationControl ID="locationControl" runat="server" />
					</div>
			    </td>
			</tr>
		</table>
		<uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
	</div>
</asp:panel>
