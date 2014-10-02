<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackTypeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table class="PageContent" id="feedbackTypeTable" cellSpacing="5" cellPadding="0">
	<tr>
		<td><asp:label id="feedbackTypeLabel" runat="server"></asp:label></td>
		<td><asp:dropdownlist id="dropDownFeedback" runat="server" Height="34px" Width="215px"></asp:dropdownlist></td>
		<td><asp:customvalidator id="feedbackTypeValidator" runat="server" ControlToValidate="dropDownFeedback"></asp:customvalidator></td>
	</tr>
</table>
