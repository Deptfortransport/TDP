<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FeedbackClaimTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FeedbackClaimTypeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="5" cellPadding="0" summary="FeedbackClaimTypeTable">
	<tr>
		<td colSpan="2"><strong><asp:label id="FeedbackLabel" runat="server"></asp:label></strong></td>
	</tr>
	<tr>
		<td>
			<p><asp:imagebutton id="imageButtonProblemUnselected" runat="server" CausesValidation="False"></asp:imagebutton>&nbsp;<asp:label id="labelProblem" runat="server"></asp:label></p>
		</td>
		<td><span class="screenreader"><asp:Label id="labelSRProblem" runat="server"></asp:Label></span><asp:dropdownlist id="ProblemDropDownList" runat="server" AutoPostBack="True" style="width:275px"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td>
			<p><asp:imagebutton id="imageButtonInformationUnselected" runat="server" CausesValidation="False"></asp:imagebutton>&nbsp;<asp:label id="labelInformation" runat="server"></asp:label></p>
		</td>
		<td><span class="screenreader"><asp:Label id="labelSRInformation" runat="server"></asp:Label></span><asp:dropdownlist id="InformationDropDownList" runat="server" AutoPostBack="True" Enabled="False" style="width:275px"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td>
			<p><asp:imagebutton id="imageButtonSuggestionUnselected" runat="server" CausesValidation="False"></asp:imagebutton>&nbsp;<asp:label id="labelSuggestion" runat="server"></asp:label></p>
		</td>
		<td><span class="screenreader"><asp:Label id="labelSRSuggestion" runat="server"></asp:Label></span><asp:dropdownlist id="SuggestionDropDownList" runat="server" AutoPostBack="True" Enabled="False" style="width:275px"></asp:dropdownlist></td>
	</tr>
</table>
