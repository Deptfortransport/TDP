<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RegisterControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RegisterControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="TransportDirect.Web" %>
<asp:panel id="registerPanel" runat="server">
	<div id="lbouthome">
		<div id="lbd">
			<div id="lbc">
				<table width="100%">
					<tr>
						<td>
							<h1>
								<asp:label id="registerLabel" runat="server" enableviewstate="False"></asp:label></h1>
						</td>
						<td>
							<div id="helpimg">&nbsp;</div>
						</td>
						<td align="right">							
						</td>
					</tr>
					<tr>
						<td>
							<asp:label id="emailLabel" associatedcontrolid="emailTxtBox" runat="server" enableviewstate="False"></asp:label><br/>
							<asp:regularexpressionvalidator id="emailValidator" runat="server" enableclientscript="False" display="Dynamic"
								controltovalidate="emailTxtBox" validationexpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></td>

						<td>
							<asp:textbox id="emailTxtBox" width="200px" runat="server" maxlength="255"></asp:textbox></td>
						<td></td>
					</tr>
					<tr>
						<td>
							<asp:label id="pWordLabel" associatedcontrolid="passwordTxtBox" runat="server" enableviewstate="False"></asp:label><br/>
							<asp:label id="passwordInfoLabel" runat="server" enableviewstate="False"></asp:label><br/>
							<asp:regularexpressionvalidator id="passwordValidator" runat="server" enableclientscript="False" display="Dynamic"
								controltovalidate="passwordTxtBox" validationexpression="^([a-zA-Z0-9]{4,12})$" cssclass="txterror"></asp:regularexpressionvalidator></td>
						<td>
							<asp:textbox id="passwordTxtBox" width="134px" runat="server" maxlength="12" textmode="Password"></asp:textbox></td>
						<td></td>
					</tr>
					<tr>
						<td>
							<div>
								<asp:label id="pWordConfirmationLabel" associatedcontrolid="confirmationTxtBox" runat="server" enableviewstate="False"></asp:label></div>
							<div>
								<asp:regularexpressionvalidator id="pwordConfirmationValidator" runat="server" enableclientscript="False" display="Dynamic"
									controltovalidate="confirmationTxtBox" validationexpression="^([a-zA-Z0-9]{4,12})$" cssclass="txterror"></asp:regularexpressionvalidator></div>
						</td>
						<td>
							<p></p>
							<div>
								<asp:textbox id="confirmationTxtBox" width="134px" runat="server" maxlength="12" textmode="Password"></asp:textbox></div>
						</td>
						<td></td>
					</tr>
					<tr>
						<td colspan="3">
							<asp:label id="messageLabel" runat="server" enableviewstate="False"></asp:label></td>
					</tr>
					<tr>
						<td><cc1:tdbutton id="cancelButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
						<td align="right"></td>
						<td align="right">
							<cc1:tdbutton id="registerButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
					</tr>
				</table>
			</div>
		</div>
	</div>
</asp:panel>
<asp:panel id="registerLogoutPanel" runat="server">
	<div id="lbouthome">
		<div id="lbd">
			<div id="lbc">
				<table width="100%">
					<tr>
						<td><strong>
							<asp:label id="registerTitleLabel" runat="server" enableviewstate="False"></asp:label></strong></td>
						<td></td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:label id="emailEnteredLabel" runat="server" enableviewstate="False"></asp:label></td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:label id="emailAddressLabel" runat="server" cssclass="txtmonospace" enableviewstate="False"></asp:label>
							<p>&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<asp:label id="confirmCancelLabel" runat="server" enableviewstate="False"></asp:label>
							<p>&nbsp;</p>
						</td>
					</tr>
					<tr>
						<td>
							<cc1:tdbutton id="confirmCancelButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
						<td align="right">
							<cc1:tdbutton id="confirmButton" runat="server" enableviewstate="False"></cc1:tdbutton></td>
					</tr>
				</table>
			</div>
		</div>
	</div>
</asp:panel>
<div>
	<cc1:helplabelcontrol id="registerHelpLabel" runat="server" visible="False" cssmaintemplate="helpboxLogin"></cc1:helplabelcontrol></div>
<div>
	<cc1:helplabelcontrol id="registerConfirmHelpLabel" runat="server" cssmaintemplate="helpboxLogin" visible="False"></cc1:helplabelcontrol></div>
