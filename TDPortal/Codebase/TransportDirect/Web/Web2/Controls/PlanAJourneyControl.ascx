<%@ Register TagPrefix="uc1" TagName="AmbiguousDateSelectControl" Src="../Controls/AmbiguousDateSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="LocationControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PlanAJourneyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.PlanAJourney" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table class="HPControlLayoutTable">
    <tr>
        <td align="right" class="HomepageControlColumn1 HomepageControlColumn1PJ">
            <asp:label id="labelFrom" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
        </td>
		<td class="HomepageControlColumn2">
            <uc1:LocationControl ID="originLocationControl" runat="server" />
        </td>
        <td rowspan="2" align="center" class="HomepageControlColumn3">
            <asp:hyperlink id="hyperlinkDoorToDoor" runat="server" enableviewstate="false">
				<cc1:tdimage id="imageDoorToDoor" runat="server" width="50" height="65"></cc1:tdimage>
			</asp:hyperlink>
        </td>
    </tr>
    <tr>
        <td align="right" class="HomepageControlColumn1 HomepageControlColumn1PJ">
			<asp:label id="labelTo" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
        </td>
        <td class="HomepageControlColumn2">
            <uc1:LocationControl ID="destinationLocationControl" runat="server" />
        </td>
    </tr>
    <tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelLeave" runat="server" enableviewstate="False" cssclass="txtsevenb"></asp:label>
		</td>
		<td colspan="2" align="left" class="PlanAJourneyDateSelectionCell">
			<uc1:ambiguousdateselectcontrol id="ambiguousDateSelectControl" runat="server" width="100%"
				shortlayoutmode="true" monthlistresourceid="DateSelectControl.listShortMonths"></uc1:ambiguousdateselectcontrol>
		</td>
	</tr>
	<tr>
		<td align="right" class="HomepageControlColumn1">
			<asp:label id="labelShow" runat="server" enableviewstate="false" cssclass="txtsevenb"></asp:label>
		</td>
		<td align="left" colspan="2" class="paddingtop2">
			<asp:checkbox id="checkBoxPublicTransport" runat="server" enableviewstate="False" checked="true" cssclass="txtsevenaligntop"></asp:checkbox>
			&nbsp;&nbsp;
			<asp:checkbox id="checkBoxCarRoute" runat="server" enableviewstate="False" checked="true" cssclass="txtsevenaligntop"></asp:checkbox>
		</td>
	</tr>
	<tr>
		<td colspan="3" align="right" class="HomepageButtonRow">
			<cc1:tdbutton id="buttonAdvanced" runat="server"></cc1:tdbutton>
			&nbsp;&nbsp;
			<cc1:tdbutton id="buttonSubmit" runat="server"></cc1:tdbutton>
		</td>
	</tr>
	<tr><td></td></tr>
</table>