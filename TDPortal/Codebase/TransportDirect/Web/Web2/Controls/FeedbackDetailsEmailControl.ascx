<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackDetailsEmailControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackDetailsEmailControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellspacing="3" cellpadding="0" summary="FeedbackDetailsEmailTable">
	<tr>
		<td>
			<p><strong><asp:label id="DetailsLabel" associatedcontrolid="CommentsTextBox" runat="server" enableviewstate="False"></asp:label></strong></p>
			<asp:label id="labelSRFeedback" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
			<p><asp:textbox id="CommentsTextBox" runat="server" columns="67" rows="8" textmode="MultiLine" Width="570" MaxLength="500"></asp:textbox></p>
			<p><asp:label id="FillDetailsLabel" runat="server" visible="False" cssclass="txterror" enableviewstate="False"></asp:label></p>
		</td>
	</tr>
	<tr>
		<td>
			<p><asp:label id="EmailAddressLabel" associatedcontrolid="EmailTextBox" runat="server" enableviewstate="False"></asp:label>
				<br/>
				<asp:textbox id="EmailTextBox" runat="server" width="225px" ></asp:textbox>
				<br/>
				<asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" validationexpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
					controltovalidate="EmailTextBox"></asp:regularexpressionvalidator>
			</p>
		</td>
	</tr>
</table>
