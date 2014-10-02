<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SearchTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SearchTypeControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<div class="boxtypetwo">
	<table cellspacing="0" cellpadding="0">
		<tr valign="middle">
			<td>
				<asp:label id="instructionLabel" runat="server" cssclass="txtsevenb"></asp:label>&nbsp;
			    <cc1:ScriptableGroupRadioButton id="timeRadioButton" runat="server" GroupName="searchType" ScriptName="TDAutoPostback" Action="TDAutoPostback();"></cc1:ScriptableGroupRadioButton>
			    <asp:label id="timeLabel" associatedcontrolid="timeRadioButton" runat="server" class="txtseven"></asp:label>
			    <cc1:TDImageButton ID="imageTime" runat="server" imagealign="Middle"></cc1:TDImageButton>&nbsp;
			    <cc1:ScriptableGroupRadioButton id="costRadioButton" runat="server" GroupName="searchType" ScriptName="TDAutoPostback" Action="TDAutoPostback();"></cc1:ScriptableGroupRadioButton>
			    <asp:label id="costLabel" associatedcontrolid="costRadioButton" runat="server" class="txtseven"></asp:label>
			    <cc1:TDImageButton ID="imageCost" runat="server" imagealign="Middle"></cc1:TDImageButton>
				<cc1:tdbutton id="okButton" runat="server"></cc1:tdbutton>
			</td>
		</tr>
	</table>
</div>
