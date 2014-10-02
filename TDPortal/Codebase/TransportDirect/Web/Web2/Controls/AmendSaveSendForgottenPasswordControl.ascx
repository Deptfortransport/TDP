<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmendSaveSendForgottenPasswordControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmendSaveSendForgottenPasswordControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="txtsevenb"><strong><asp:Label id="labelEnterEmailMessage" Height="20px" runat="server" CssClass="BlkGenTxtBld7em"></asp:Label></strong></div>
<table summary="AmendSaveSendForgottenPasswordControlTable">
	<tr>
		<td class="txtsevenb" colspan="1"><asp:Label id="labelTypeEmailAddressLabel" runat="server" CssClass="BlkGenTxtBld7em"></asp:Label>
		</td>
		<td colspan="1"><asp:TextBox id="textEmailAddress" runat="server" Width="195px" CssClass="BlkGenTxt7em" MaxLength="255"></asp:TextBox>
		</td>
		<td colspan="1"><cc1:tdbutton ID="buttonSend" Runat="server"></cc1:tdbutton>
		</td>
	</tr>
</table>
