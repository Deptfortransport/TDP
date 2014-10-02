<%@ Register TagPrefix="uc1" TagName="ZonalFareDetailsTableSegmentControl" Src="ZonalFareDetailsTableSegmentControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FareDetailsTableSegmentControl" Src="FareDetailsTableSegmentControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FareDetailsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FareDetailsTableControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<asp:Repeater id="fareDetailsTableSegmentControlRepeater" runat="server" EnableViewState="false">

    <HeaderTemplate>
        <table cellspacing="0" cellpadding="5" width="100%">
    </HeaderTemplate>

	<itemtemplate>
			<tr>
				<td>
					<uc1:faredetailstablesegmentcontrol id="detailsSegment" runat="server"></uc1:faredetailstablesegmentcontrol></td>
			</tr>
	</itemtemplate>

    <FooterTemplate>
        </table>
    </FooterTemplate>

</asp:Repeater>

<fieldset>
    <asp:Repeater ID="fareDetailsZonalSegmentControlRepeater" runat="server" EnableViewState="false">

        <HeaderTemplate>
            <table cellspacing="0" cellpadding="5" width="100%">
        </HeaderTemplate>

	    <ItemTemplate>
			    <tr>
				    <td>
					    <uc1:zonalfaredetailstablesegmentcontrol id="zonalSegment" runat="server"></uc1:zonalfaredetailstablesegmentcontrol></td>
			    </tr>
        </ItemTemplate>

        <FooterTemplate>
            </table>
        </FooterTemplate>

    </asp:Repeater>
</fieldset>


