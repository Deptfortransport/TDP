<%@ Control Language="c#" AutoEventWireup="True" Codebehind="iFrameJourneyPlanningControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.iFrameJourneyPlanningControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="iFrameAmbiguousDateSelectControl" Src="../Controls/iFrameAmbiguousDateSelectControl.ascx" %>
<table class="HPControlLayoutTable">
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelFrom" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:textbox id="textBoxFrom" enableviewstate="False" runat="server" cssclass="HomepageControlColumn2TextBox"></asp:textbox>
		</td>
		<td rowspan="4" align="center" class="HomepageControlColumn3">
			<cc1:tdimage id="imageDoorToDoor" runat="server" width="50" height="65"></cc1:tdimage>
		</td>
	</tr>
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelFromPlaceTypeScreenReader" runat="server" cssclass="screenreader" enableviewstate="false"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:dropdownlist id="fromDropDownLocationGazeteerOptions" runat="server" enableviewstate="True" cssclass="HomepageControlColumn2DropDown"></asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelTo" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:textbox id="textBoxTo" runat="server" enableviewstate="False" cssclass="HomepageControlColumn2TextBox"></asp:textbox>
		</td>
	</tr>
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelToPlaceTypeScreenReader" runat="server" cssclass="screenreader" enableviewstate="false"></asp:label>
		</td>
		<td align="left" class="HomepageControlColumn2">
			<asp:dropdownlist id="toDropDownLocationGazeteerOptions" enableviewstate="True" runat="server" cssclass="HomepageControlColumn2DropDown"></asp:dropdownlist>
		</td>
	</tr>
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelLeave" runat="server" enableviewstate="False" cssclass="txtsevenb"></asp:label>
		</td>
		<td colspan="2" align="left" class="PlanAJourneyDateSelectionCell">
			<uc1:iFrameAmbiguousDateSelectControl id="ambiguousDateSelectControl" runat="server" width="100%" shortlayoutmode="true"
				monthlistresourceid="DateSelectControl.listShortMonths"></uc1:iFrameAmbiguousDateSelectControl>
		</td>
	</tr>
	<tr class="VertAlignTop">
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelShow" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td align="left" colspan="2" class="paddingtop2">
			<asp:checkbox id="checkBoxPublicTransport" runat="server" enableviewstate="False" checked="true"
				cssclass="txtsevenaligntop"></asp:checkbox>
			&nbsp;&nbsp;
			<asp:checkbox id="checkBoxCarRoute" runat="server" enableviewstate="False" checked="true" cssclass="txtsevenaligntop"></asp:checkbox>
		</td>
	</tr>
	<tr>
		<td colspan="3" align="right" class="HomepageButtonRow">&nbsp;&nbsp; 
			
			<asp:ImageButton ID="buttonSubmit" runat="server"></asp:ImageButton>
		</td>
	</tr>
	<tr>
		<td><input type="hidden" name="partnerId" id="partnerId" runat="server"></td>
	</tr>
</table>
