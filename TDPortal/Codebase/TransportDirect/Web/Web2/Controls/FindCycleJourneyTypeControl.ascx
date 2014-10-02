<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindCycleJourneyTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCycleJourneyTypeControl" %>
<div class="cycleJourneyType">
    <div class="findafromcolumn">
        <asp:label id="labelTypeOfJourney" associatedcontrolid="listJourneyType" runat="server" cssclass="txtsevenb" EnableViewState="false"></asp:label>&nbsp;
    </div>
    <div class="cycleJourneyTypeSelect floatleftonly">
        <asp:label id="labelFind" runat="server" cssclass="txtseven" EnableViewState="false"></asp:label>&nbsp;
        <asp:dropdownlist id="listJourneyType" runat="server" EnableViewState="true" CssClass="cycleJourneyTypeDropDown"></asp:dropdownlist>&nbsp;
        <asp:label id="labelJourneys" runat="server" cssclass="txtseven" EnableViewState="false"></asp:label>
    </div>
    <div class="cycleJourneyTypeDisplay floatleftonly">
        <asp:label id="displayJourneyTypeLabel" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
    </div>
</div>
<div class="clearboth"></div>