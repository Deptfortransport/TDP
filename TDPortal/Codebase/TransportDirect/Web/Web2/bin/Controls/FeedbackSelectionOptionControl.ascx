<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackSelectionOptionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackSelectionOptionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="Table1" style="WIDTH: 473px; HEIGHT: 106px">
	<tr>
		<td colspan="2"><asp:label id="TableHeadingLabelFirst" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td></td>
		<td>
			<p><asp:table id="TableCommentSuggestion" runat="server"></asp:table></p>
		</td>
	</tr>
	<tr>
		<td colspan="2"><asp:label id="TableHeadingLabelSecond" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td style="HEIGHT: 20px"></td>
		<td style="HEIGHT: 20px"><asp:table id="TableFeedbackQuestion" runat="server"></asp:table></td>
	</tr>
	<tr>
		<td colspan="2"><asp:label id="TableHeadingLabelThird" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:table id="TableProblemSelection" runat="server"></asp:table></td>
	</tr>
	<tr>
		<td colspan="2"><asp:label id="TableHeadingLabelFourth" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td></td>
		<td><asp:table id="TableErrorSelection" runat="server"></asp:table></td>
	</tr>
	<tr>
		<td colspan="2"><asp:table id="TableTechnicalSelection" runat="server"></asp:table></td>
	</tr>
</table>
