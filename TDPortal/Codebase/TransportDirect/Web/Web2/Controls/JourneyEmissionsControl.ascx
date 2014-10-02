<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsDashboardControl" Src="JourneyEmissionsDashboardControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCarInputControl" Src="JourneyEmissionsCarInputControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyEmissionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="boxtypetwelve">
	<div id="dmtitletwo">
		<table cellspacing="0" cellpadding="0" width="100%" summary="Journey Emissions">
			<tr>
				<td><asp:panel id="panelDashboard" runat="server">
						<uc1:journeyemissionsdashboardcontrol id="journeyEmissionsDashboard" runat="server"></uc1:journeyemissionsdashboardcontrol>
						<asp:Panel id="panelHorizontalRuleOne" runat="server">
							<table cellspacing="0" cellpadding="0" width="100%">
								<tr bgcolor="#ccccff">
									<td><img height="5" src="/web2/images/gifs/spacer.gif" width="5" alt='<%# GetSpacerText() %>'/></td>
								</tr>
								<tr bgcolor="#ffffff">
									<td><img height="5" src="/web2/images/gifs/spacer.gif" width="5" alt='<%# GetSpacerText() %>'/></td>
								</tr>
							</table>
						</asp:Panel>
					</asp:panel></td>
			</tr>
			<tr>
				<td>
					<div id="boxtypetwentyeight">
						<table cellspacing="0" cellpadding="0" width="100%">
							<tr>
								<td><uc1:journeyemissionscarinputcontrol id="journeyEmissionsCarInput" runat="server"></uc1:journeyemissionscarinputcontrol></td>
								<td valign="top" align="right">
									<asp:Panel ID="panelNextButton" Runat="server" EnableViewState="False"></asp:Panel>
									<cc1:tdbutton id="commandNext" runat="server" enableviewstate="false"></cc1:tdbutton>
								</td>
							</tr>
						</table>
						<table cellspacing="0" cellpadding="0" width="100%" summary="Journey Emissions More Next Buttons">
							<tr>
								<td width="186"></td>
								<td align="left"><cc1:tdbutton id="commandMoreDetails" runat="server" enableviewstate="false"></cc1:tdbutton></td>
								<td align="right"></td>
							</tr>
						</table>
					</div>
				</td>
			</tr>
		</table>
	</div>
</div>
<div>
	<asp:panel id="panelNotes" runat="server">
	<br/>
	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td width="10"></td>
			<td colspan="2"><asp:Label ID="notes" Runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></td>
		</tr>
		<tr>
			<td></td>
			<td><span class="txtseven">•&nbsp;</span></td>
			<td><asp:Label ID="dialColours" Runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></td>
		</tr>
	</table>
	</asp:panel>
</div>
