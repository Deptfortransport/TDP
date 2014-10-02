<%@ Register TagPrefix="uc1" TagName="JourneyDetailsSegmentsControl" Src="JourneyDetailsSegmentsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="TransportDirect.UserPortal.JourneyControl" %>
<div class="dmview"><!-- This comment to fix IE DIV bug. Do not remove! -->
<table border="0" class="jdetail2" cellpadding="0" cellspacing="0">
	<tbody>
		<tr>
			<td>
				<asp:repeater id="detailsRepeater" runat="server">
					<itemtemplate>
						<uc1:journeydetailssegmentscontrol id="journeyDetailsSegment" runat="server"></uc1:journeydetailssegmentscontrol>
					</itemtemplate>
				</asp:repeater>
			</td>
		</tr>
	</tbody>
</table>
</div>