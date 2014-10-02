<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PtJourneyChangesOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PtJourneyChangesOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:panel id="panelPreferences" runat="server">
	<asp:panel id="panelChanges" runat="server">
		<div class="boxtypetwo">
			<table>
				<tr>
					<td align="left" colspan="2"><span class="jpt">
							<uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol></span></td>
					<td style="HEIGHT: 5px" align="left"><span class="jpt"></span></td>
				</tr>
				<tr>
					<td style="WIDTH: 10px; HEIGHT: 5px" align="left">
						<asp:label id="labelChanges" runat="server" cssclass="txtsevenb"></asp:label></td>
				</tr>
				
				<tr>
					<td align="left" colspan="2">
						<asp:label id="labelChangesShowTitle" associatedcontrolid="listChangesShow" runat="server" cssclass="txtseven"></asp:label>
						<asp:dropdownlist id="listChangesShow" runat="server"></asp:dropdownlist>
						<asp:label id="listChangesShowFixed" runat="server" cssclass="txtsevenb"></asp:label></td>
					<td align="left" colspan="2"></td>
				</tr>
				<tr>
					<td style="WIDTH: 30px" align="left"></td>
					<td align="left">
						<asp:label id="labelChangesShowNote" runat="server" cssClass="txtnote"></asp:label></td>
				</tr>
				<tr>
					<td align="left" colspan="2">
						<asp:label id="labelChangesSpeedTitle" associatedcontrolid="listChangesSpeed" runat="server" cssclass="txtseven"></asp:label>
						<asp:dropdownlist id="listChangesSpeed" runat="server"></asp:dropdownlist>
						<asp:label id="listChangesSpeedFixed" runat="server" cssclass="txtsevenb"></asp:label></td>
					<td align="left" colspan="2"></td>
				</tr>
				<tr>
					<td style="WIDTH: 30px" align="left"></td>
					<td align="left" colspan="2">
						<asp:label id="labelChangesSpeedNote" runat="server" cssclass="txtnote"></asp:label></td>
				</tr>
			</table>
		</div>
	</asp:panel>
</asp:panel>
