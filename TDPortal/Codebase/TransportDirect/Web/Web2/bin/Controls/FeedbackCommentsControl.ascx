<%@ Import namespace="TransportDirect.Web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackCommentsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackCommentsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="PageContent" cellSpacing="5" cellPadding="0" align="left">
	<tr>
		<td><asp:label id="commentsLabel" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><asp:textbox id="txtBoxComments" runat="server" Columns="50" Rows="5" TextMode="MultiLine" MaxLength="10"></asp:textbox></td>
	</tr>
	<tr>
		<td><asp:requiredfieldvalidator id="commentsRequiredFieldValidator" runat="server" ControlToValidate="txtBoxComments"></asp:requiredfieldvalidator></td>
	</tr>
	<tr>
		<td><asp:customvalidator id="commentsLengthValidator" runat="server" ControlToValidate="txtBoxComments"></asp:customvalidator></td>
	</tr>
</table>
