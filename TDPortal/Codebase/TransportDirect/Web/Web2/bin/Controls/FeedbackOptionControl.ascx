<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackOptionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackOptionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<table cellspacing="3" cellpadding="0" summary="FeedbackOptionTable" border="0">
	<tr>
		<td><strong><asp:label id="feedbackOptionTitle" runat="server" cssclass="txtseven"></asp:label></strong>&nbsp;&nbsp;<asp:label id="labelSRSelectOption" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label></td>			
	</tr>
	<tr>
		<td>
			<asp:RadioButtonList id="feedbackOptionsRadioButtonList" runat="server" AutoPostBack="True" repeatdirection="Vertical" cssclass="txtseven" repeatlayout="Flow"></asp:RadioButtonList>
			<uc1:hyperlinkpostbackcontrol id="hyperlinkNext" runat="server"></uc1:hyperlinkpostbackcontrol>
		</td>
	</tr>
</table>
