<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="JourneyDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyDetailsCompareControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsCompareControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="boxtypetwelverefine">
    <table lang="en" id="Table1" cellspacing="0" cellpadding="0" summary="Journey comparison table" border="0">
        <tr>
            <td>
                <div class="dmtitlecompare"><span class="txteightb"><asp:label id="originalJourneyTitle" runat="server"></asp:label></span></div>
            </td>
            <td>
                <div class="dmtitlecompare"><span class="txteightb"><asp:label id="adjustedJourneyTitle" runat="server"></asp:label></span></div>
            </td>
        </tr>
        <tr id="dmview">
            <td style="BORDER-RIGHT: #c8c8c8 3px dotted" valign="top" width="50%"><uc1:journeydetailscontrol id="journeyDetailsControlOne" runat="server"></uc1:journeydetailscontrol></td>
            <td valign="top"><uc1:journeydetailscontrol id="journeyDetailsControlTwo" runat="server"></uc1:journeydetailscontrol></td>
        </tr>
    </table>
</div>
<div class="boxtypeJourneyBuilder">
	<span class="floatleft"></span>
	<span class="floatright"><cc1:tdbutton id="buttonSaveJourney" runat="server"></cc1:tdbutton></span>
</div>
