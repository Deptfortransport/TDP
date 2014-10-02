<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFareSelectedTicketLabelControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareSelectedTicketLabelControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<table cellspacing="0" cellpadding="0" summary="tableSummaryTicketSelected">
	<tr valign="top">
		<td runat="server" id="outwardTicketDirectionColumn">
			<asp:label id="outwardTicketLabelDirection" class="selectedticketlabel" runat="server"></asp:label>
		</td>
		<td>
			<asp:label id="outwardTicketLabel" class="selectedticketlabel" runat="server"></asp:label>
		</td>
		<td>
			<asp:label id="outwardTicketPrice" class="selectedticketlabel" runat="server"></asp:label>
		</td>
	</tr>
	<tr valign="top" runat="server" id="outwardRouteRow">
	    <td runat="server" id="outwardTicketRouteDirectionColumn">
		</td>
		<td colspan="2">
			<asp:label id="labelOutwardRoute" class="selectedticketlabel" runat="server"></asp:label>
		</td>
	</tr>
	<tr valign="top" runat="server" id="inwardTicketRow">
		<td>
			<asp:label id="inwardTicketLabelDirection" class="selectedticketlabel" runat="server"></asp:label>
		</td>
		<td>
			<asp:label id="inwardTicketLabel" class="selectedticketlabel" runat="server"></asp:label>
		</td>
		<td>
			<asp:label id="inwardTicketPrice" class="selectedticketlabel" runat="server"></asp:label>
		</td>
	</tr>
	<tr valign="top" runat="server" id="inwardRouteRow">
	    <td>
		</td>
		<td colspan="2">
			<asp:label id="labelInwardRoute" class="selectedticketlabel" runat="server"></asp:label>
		</td>
	</tr>
</table>
