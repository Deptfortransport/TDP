<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LogoutControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LogoutControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:panel id="logoutPanel" runat="server">
	<div id="lbouthome">
		<div id="lbd">
			<div id="lbc">
				<table width="100%">
					<tr>
						<td colspan="2"><strong>
								<asp:label id="logoutTitleLabel" runat="server" enableviewstate="False"></asp:label></strong>
							<p>&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:label id="confirmLogoutLabel" runat="server" enableviewstate="False"></asp:label>
							<p>&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td>
							<cc1:tdbutton id="confirmCancelButton" runat="server" enableviewstate="False"></cc1:tdbutton>
						</td>
						<td align="right">
							<cc1:tdbutton id="confirmButton" runat="server" enableviewstate="False"></cc1:tdbutton>
						</td>
					</tr>
				</table>
			</div>
		</div>
	</div>
</asp:panel>
