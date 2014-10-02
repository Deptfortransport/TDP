<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapNearestControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapNearestControl" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="MapControl2" Src="MapControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSymbolsSelectControl" Src="MapSymbolsSelectControl.ascx" %>
<div class="mnMapNearestControlContainer">
    <div id="scrollToMap" runat="server"></div>
    <asp:Panel ID="panelMapLocationTitle" runat="server">
        <div class="mnMapLocationTitleArea">
            <div class="floatleftonly">
                <h1><asp:label id="labelLocationName" runat="server" EnableViewState="false"></asp:label></h1>
            </div>
            <div class="floatrightonly">
                <cc2:TDButton id="commandHideMap" runat="server" EnableViewState="false"></cc2:TDButton>
            </div>
            <div class="clearboth"></div>
        </div>
    </asp:Panel>
    <div class="mcMapControlsContainer">
        <uc1:MapControl2 id="mapControl" runat="server"></uc1:MapControl2>
        <div class="clearboth"></div>
        <div class="mcMapControlsBelowContainer">
            <uc1:MapSymbolsSelectControl ID="mapSymbolsSelectControl" runat="server"></uc1:MapSymbolsSelectControl>
        </div>
    </div>
</div>