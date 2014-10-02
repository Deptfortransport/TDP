<%@ Register TagPrefix="uc1" TagName="FeedbackJourneyInputControl" Src="FeedbackJourneyInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeedbackJourneyControl" Src="FeedbackJourneyControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackProblemControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackProblemControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellspacing="3" cellpadding="0" summary="FeedbackProblemTable" width="100%">
	<tr>
		<td>
			<asp:Panel id="PanelConfirmation" Runat="server" enableviewstate="False">
				<div class="spacer1">&nbsp;</div>
				<asp:label id="ProblemConfirmationLabel" enableviewstate="False" cssclass="txtseven" runat="server"></asp:label>
				<div align="right">
					<cc1:tdbutton id="NextButton" enableviewstate="False" runat="server"></cc1:tdbutton></div>
			</asp:Panel>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Panel id="PanelJourney" Runat="server" enableviewstate="False" visible="false">
				<asp:label id="ProblemWithJourneyLabel" enableviewstate="False" cssclass="txtsevenb" runat="server"></asp:label>&nbsp;&nbsp;<asp:label id="labelSRSelectOption1" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>				
				<br/>
			</asp:Panel>
			<asp:RadioButtonList id="feedbackJourneyConfirmationList" runat="server" AutoPostBack="True" repeatdirection="Horizontal"
				cssclass="txtseven" repeatlayout="Flow" visible="False"></asp:RadioButtonList>
				<uc1:hyperlinkpostbackcontrol id="hyperlinkNext1" runat="server"></uc1:hyperlinkpostbackcontrol>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:feedbackjourneycontrol id="feedbackJourneyControl" runat="server" visible="false"></uc1:feedbackjourneycontrol>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Panel id="PanelResult" Runat="server" enableviewstate="False" visible="false">
				<asp:label id="ProblemWithResultsLabel" enableviewstate="False" cssclass="txtsevenb" runat="server"></asp:label>&nbsp;&nbsp;<asp:label id="labelSRSelectOption2" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
				<br/>
			</asp:Panel>
			<asp:RadioButtonList id="feedbackResultsConfirmationList" runat="server" AutoPostBack="True" repeatdirection="Vertical"
				cssclass="txtseven" repeatlayout="Flow" visible="False"></asp:RadioButtonList>
			<uc1:hyperlinkpostbackcontrol id="hyperlinkNext2" runat="server"></uc1:hyperlinkpostbackcontrol>
		</td>
	</tr>
	<tr>
		<td>
			<asp:Panel id="PanelOtherProblem" Runat="server" enableviewstate="False" visible="false">
				<asp:label id="AnotherProblemLabel" enableviewstate="False" cssclass="txtsevenb" runat="server"></asp:label>&nbsp;&nbsp;<asp:label id="labelSRSelectOption3" runat="server" cssclass="screenreader" enableviewstate="False"></asp:label>
				<br/>
			</asp:Panel>
			<asp:RadioButtonList id="feedbackOtherProblemList" runat="server" AutoPostBack="True" repeatdirection="Vertical"
				cssclass="txtseven" repeatlayout="Flow" visible="False"></asp:RadioButtonList>
			<uc1:hyperlinkpostbackcontrol id="hyperlinkNext3" runat="server"></uc1:hyperlinkpostbackcontrol>
			<div class="spacer1">&nbsp;</div>
			<uc1:feedbackjourneyinputcontrol id="feedbackJourneyInputControl" runat="server"></uc1:feedbackjourneyinputcontrol>
		</td>
	</tr>
</table>
