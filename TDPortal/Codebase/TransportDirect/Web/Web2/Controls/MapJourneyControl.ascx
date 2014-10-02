<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapJourneyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapJourneyControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="MapControl2" Src="MapControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSymbolsSelectControl" Src="MapSymbolsSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="MapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyDisplayDetailsControl" Src="MapJourneyDisplayDetailsControl.ascx" %>
<div class="mjMapJourneyControlContainer">
    <div id="scrollToMap" runat="server"></div>
    <asp:Panel ID="mapTitleArea" runat="server">
        <div class="mjTitleAreaBox">
            <div class="floatleftonly">
                <div class="mjMapJourneyTitleLabels">
                    <asp:Label ID="labelMaps" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                    <asp:Label ID="labelJourney" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                    <asp:Label ID="labelDisplayNumber" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                    <asp:Label ID="labelMapsCar" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                </div>
            </div>
            <div class="floatrightonly">
                <asp:Panel ID="panelJourneySelectButtons" runat="server">
                    <div class="mjMapJourneySelectButtons">
                        <asp:Label ID="labelShowOutwardJourney" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                        <cc1:tdbutton id="buttonShowOutwardJourney" runat="server"></cc1:tdbutton>
                        <asp:Label ID="labelShowReturnJourney" runat="server" EnableViewState="false" CssClass="txteightb"></asp:Label>
                        <cc1:tdbutton id="buttonShowReturnJourney" runat="server"></cc1:tdbutton>                        
                    </div>
                </asp:Panel>
                <uc1:MapJourneyDisplayDetailsControl ID="mapJourneyDisplayDetailsControl" runat="server"></uc1:MapJourneyDisplayDetailsControl>
            </div>
            <div class="clearleft">
                <asp:Label ID="labelOptions" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
            </div>
        </div>
    </asp:Panel>
    <div class="mcMapControlsContainer">
        <uc1:MapControl2 id="mapControl" runat="server"></uc1:MapControl2>
        <div class="clearboth"></div>
        <div class="mcMapControlsBelowContainer">
            <uc1:MapKeyControl ID="mapKeyControl" runat="server"></uc1:MapKeyControl>
            <uc1:MapSymbolsSelectControl ID="mapSymbolsSelectControl" runat="server"></uc1:MapSymbolsSelectControl>
        </div>
    </div>
</div>
<div class="clearboth"></div>
