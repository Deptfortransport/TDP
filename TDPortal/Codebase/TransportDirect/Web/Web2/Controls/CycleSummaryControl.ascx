<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CycleSummaryControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CycleSummaryControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="cycleJourneySummaryBoxType1">
    <div class="floatleftonly">
        <asp:label id="labelSummaryOfDirections" runat="server" enableviewstate="false" CssClass="txteightb"></asp:label>&nbsp;        
        <asp:label id="labelTotalDistance" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:label>&nbsp;<asp:label id="labelTotalDistanceNumber" runat="server" CssClass="txtseven"></asp:label>
        <asp:label id="labelTotalDuration" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:label>&nbsp;<asp:label id="labelTotalDurationNumber" runat="server" CssClass="txtseven"></asp:label>
    </div>
    <div class="floatrightonly">
        <asp:label id="labelDistanceUnits" runat="server" enableviewstate="false" CssClass="txtseven" Visible="False"></asp:label>
        
        <cc1:scriptabledropdownlist id="dropdownlistCycleSummary" runat="server" scriptname="UnitsSwitchCycle" Visible="False"></cc1:scriptabledropdownlist>      
    </div>
    <div class="clearboth"></div>
    <div>
        <asp:label id="labelJourneyOptions" runat="server" enableviewstate="false" CssClass="txtseven"></asp:label>
        <asp:label id="labelCalorieCount" runat="server" enableviewstate="false" CssClass="txtseven"></asp:label>    
    </div>
    <div id="divJourneyAdditionalOptions" runat="server" visible="false" enableviewstate="false">
        <asp:label id="labelJourneyAdditionalOptions" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:label>
    </div>
</div>
<input id="<%# GetHiddenInputId %>" type="hidden" value="<%# UnitsState%>"  name="<%# GetHiddenInputId %>" />