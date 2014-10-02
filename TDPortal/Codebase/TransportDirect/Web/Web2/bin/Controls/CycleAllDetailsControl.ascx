<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CycleAllDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CycleAllDetailsControl" %>
<%@ Register TagPrefix="uc1" TagName="CycleJourneyDetailsTableControl" Src="CycleJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleSummaryControl" Src="CycleSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleJourneyGraphControl" Src="CycleJourneyGraphControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleJourneyGPXControl" Src="CycleJourneyGPXControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="cycleAllDetailsBoxType1">
    <div class="cycleAllDetailsBoxType2">
        <div class="floatleftonly"> <asp:Label id="labelCycleAllDetailsControlTitle" runat="server" enableviewstate="false" CssClass="txteightb"></asp:Label> </div>
        <div class="floatrightonly"> <cc1:TDButton ID="buttonShowMap" runat="server"></cc1:TDButton> </div>
        <div class="clearboth"></div>
    </div>
    <div class="cycleAllDetailsBoxType3">
        <div class="cycleAllDetailsBoxType4">        
            <uc1:CycleSummaryControl id="cycleSummaryControl" runat="server"></uc1:CycleSummaryControl>
            <div class="clearboth"></div>
            <div class="floatleftonly">
                <uc1:CycleJourneyDetailsTableControl id="cycleJourneyDetailsTableControl" runat="server"></uc1:CycleJourneyDetailsTableControl>
            </div>
            <div class="floatrightonly">
                <uc1:CycleJourneyGraphControl id="cycleJourneyGraphControl" runat="server"></uc1:CycleJourneyGraphControl>
                <div class="clearboth"></div>
                <uc1:CycleJourneyGPXControl id="cycleJourneyGPXControl" runat="server"></uc1:CycleJourneyGPXControl>
            </div>
        </div>
        <div class="clearboth"></div>
        <div class="cycleAllDetailsBoxType5">
            <cc1:tdimage id="imageCycleEngland" runat="server" enableviewstate="false"></cc1:tdimage>&nbsp;
            <asp:HyperLink ID="hyperlinkCycleEngland" runat="server" enableviewstate="false" CssClass="txtseven"></asp:HyperLink>
        </div>
    </div>
</div>