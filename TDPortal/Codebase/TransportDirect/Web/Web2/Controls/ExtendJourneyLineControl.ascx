<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ExtendJourneyLineControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:Panel id="hideIfEmpty" runat="server">
<div align="center">
<table cellpadding="0" cellspacing="0" class="txtseven" summary="<%= GetTableSummary() %>">
	<thead align="center">
	<tr>
		<th class="ejpColumnWidth2"></th>
		<th class="ejpColumnWidth1"></th>
		<th class="ejpColumnWidth2"></th>
		<th class="ejpColumnWidth2"></th>
		<th class="ejpColumnWidth1"></th>
		<th class="ejpColumnWidth2"></th>
		<th id="layoutColumn7" runat="server" class="ejpColumnWidth2"></th>
		<th id="layoutColumn8" runat="server" class="ejpColumnWidth1"></th>
		<th id="layoutColumn9" runat="server" class="ejpColumnWidth2"></th>
		<th id="layoutColumn10" runat="server" class="ejpColumnWidth2"></th>
		<th id="layoutColumn11" runat="server" class="ejpColumnWidth1"></th>
		<th id="layoutColumn12" runat="server" class="ejpColumnWidth2"></th>
	</tr>
	</thead>
	
	<tbody>

	<tr>
		<td id="topRowStartExtensionCell" class="ejpLocationDescriptionCell" colspan="3" runat="server"><asp:label id="topRowStartExtensionLocation" runat="server" enableviewstate="false"></asp:label></td>
		
		<asp:repeater id="topRowText" runat="server">
			<itemtemplate>
				<td class="ejpLocationDescriptionCell" colspan="3"><asp:label id="topRowItineraryLocation" runat="server" enableviewstate="false"></asp:label></td>
			</itemtemplate>
		</asp:repeater>
		
		<td id="topRowEndExtensionCell" class="ejpLocationDescriptionCell" colspan="3" runat="server"><asp:label id="topRowEndExtensionLocation" runat="server" enableviewstate="false"></asp:label></td>
		<td>&nbsp;</td>
	</tr>
	
	<tr>
		<td class="ejpPaddingCell"></td>
		<td id="startExtensionCircleCell" class="ejpCircleCell" runat="server"><cc1:tdimage id="startExtensionCircle" runat="server" enableviewstate="false"></cc1:tdimage></td>
		<td id="startExtensionLineCell" class="ejpLineCell" colspan="2" runat="server"><cc1:tdimage id="startExtensionLine" runat="server" enableviewstate="false"></cc1:tdimage></td>
		
		<asp:repeater id="middleRowIcons" runat="server">
			<itemtemplate>
				<td class="ejpCircleCell"><cc1:tdimage id="itineraryCircle" runat="server" enableviewstate="false"></cc1:tdimage></td>
				<td id="itineraryLineCell" class="ejpLineCell" colspan="2" runat="server"><cc1:tdimage id="itineraryLine" runat="server" enableviewstate="false"></cc1:tdimage></td>
			</itemtemplate>
		</asp:repeater>
		
		<td id="endExtensionLineCell" class="ejpLineCell" colspan="2" runat="server"><cc1:tdimage id="endExtensionLine" runat="server" enableviewstate="false"></cc1:tdimage></td>
		<td id="endExtensionCircleCell" class="ejpCircleCell" runat="server"><cc1:tdimage id="endExtensionCircle" runat="server" enableviewstate="false"></cc1:tdimage></td>
		<td class="ejpPaddingCell"></td>
	</tr>
	
	<tr>
		<td id="bottomRowStartExtensionCell" class="ejpLocationDescriptionCell" colspan="3" runat="server"><asp:label id="bottomRowStartExtensionLocation" runat="server" enableviewstate="false"></asp:label></td>
		
		<asp:repeater id="bottomRowText" runat="server">
			<itemtemplate>
				<td class="ejpLocationDescriptionCell" colspan="3"><asp:label id="bottomRowItineraryLocation" runat="server" enableviewstate="false"></asp:label></td>
			</itemtemplate>
		</asp:repeater>
		
		<td id="bottomRowEndExtensionCell" class="ejpLocationDescriptionCell" colspan="3" runat="server"><asp:label id="bottomRowEndExtensionLocation" runat="server" enableviewstate="false"></asp:label></td>
	</tr>
	</tbody>
	
</table>
</div>
</asp:Panel>