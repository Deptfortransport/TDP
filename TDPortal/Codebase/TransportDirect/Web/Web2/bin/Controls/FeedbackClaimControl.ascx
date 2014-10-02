<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackClaimControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackClaimControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="PageContent" id="feedbackTypeTable" cellSpacing="5" cellPadding="0">
	<tr>
		<td colSpan="3">
			<asp:Label id="initialSelectionLabel" runat="server"></asp:Label>
		</td>
	<tr>
		<td colspan="1"></td>
		<td colspan="1">
			<asp:RadioButtonList id="feedbackClaimSelection" runat="server"></asp:RadioButtonList></td>
		<td colspan="1">
			<asp:RequiredFieldValidator id="feedbackClaimTypeValidator" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="feedbackClaimSelection"></asp:RequiredFieldValidator></td>
	</tr>
</table>
