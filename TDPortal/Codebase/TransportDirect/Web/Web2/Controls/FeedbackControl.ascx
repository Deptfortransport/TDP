<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FeedbackOptionControl" Src="FeedbackOptionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeedbackProblemControl" Src="FeedbackProblemControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeedbackDetailsEmailControl" Src="FeedbackDetailsEmailControl.ascx" %>
<div id="boxtypetwo">
	<asp:label id="labelSRFeedbackStart" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
		<uc1:feedbackoptioncontrol id="feedbackOptionControl" runat="server"></uc1:feedbackoptioncontrol>
		<uc1:feedbackproblemcontrol id="feedbackProblemControl" runat="server" visible="false"></uc1:feedbackproblemcontrol>
		<uc1:feedbackdetailsemailcontrol id="feedbackDetailsEmailControl" runat="server" visible="false"></uc1:feedbackdetailsemailcontrol>
	<asp:Panel id="PanelSubmit" Runat="server" enableviewstate="False" visible="false">
		<div align="right">
			<cc1:tdbutton id="SubmitButton" runat="server" enableviewstate="False" visible="true"></cc1:tdbutton>
		</div>
	</asp:Panel>
	<asp:Panel id="PanelFeedbackConfirmation" Runat="server" enableviewstate="False" visible="false">
		<div style="PADDING-TOP: 10px; PADDING-LEFT: 10px">
		<asp:label id="feedbackConfirmationLabel" runat="server" enableviewstate="False"  cssclass="txtseven" ></asp:label>
		</div>
		<div align="right" style="PADDING-TOP: 5px; PADDING-RIGHT: 5px">
			<cc1:tdbutton id="AnotherFeedbackButton" runat="server" enableviewstate="False" visible="true"></cc1:tdbutton>
		</div>
	</asp:Panel>
	<asp:label id="labelSRFeedbackEnd" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
</div>

