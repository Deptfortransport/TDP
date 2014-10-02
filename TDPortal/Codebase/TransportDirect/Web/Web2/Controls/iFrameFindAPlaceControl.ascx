<%@ Control Language="c#" AutoEventWireup="True" Codebehind="iFrameFindAPlaceControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.iFrameFindAPlaceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table class="HPControlLayoutTable">
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelPlace" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:textbox id="textBoxPlace" runat="server" enableviewstate="False" cssclass="HomepageControlColumn2TextBox"></asp:textbox>
		</td>
		<td rowspan="2" align="center" class="HomepageControlColumn3">
			<asp:hyperlink id="hyperlinkFindAPlaceMore" runat="server" enableviewstate="false">
				<cc1:tdimage id="imageMap" runat="server" width="50" height="44"></cc1:tdimage>
			</asp:hyperlink>
		</td>
	</tr>
	<tr>
		<td align="right">
			<asp:label id="labelPlaceTypeScreenReader" runat="server" cssclass="screenreader" enableviewstate="false"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:dropdownlist id="dropDownLocationGazetteerOptions" runat="server" enableviewstate="True" cssclass="HomepageControlColumn2DropDown"></asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td align="right">
			<asp:label id="textBoxShowOption" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td colspan="2" align="left" class="HomepageControlColumn2Span2">
			<asp:dropdownlist id="dropDownLocationShowOptions" runat="server" enableviewstate="True" cssclass="HomepageControlColumn2DropDownSpan2"></asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td colspan="3" align="right" class="HomepageButtonRow2">
			<asp:ImageButton ID="buttonSubmit" runat="server"></asp:ImageButton>
			<input type="hidden" name="partnerId" id="partnerId" value="" runat="server"/>
		</td>
	</tr>
</table>
