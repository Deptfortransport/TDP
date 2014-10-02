<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapTravelNewsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapTravelNewsControl" %>
<%@ Register TagPrefix="uc1" TagName="MapControl2" Src="MapControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSymbolsSelectControl" Src="MapSymbolsSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsMapKeyControl" Src="TravelNewsMapKeyControl.ascx"%>
<div class="mtMapTravelNewsContainer">
    <div class="mcMapControlsContainer">
        <uc1:MapControl2 id="mapControl" runat="server"></uc1:MapControl2>
        <div class="clearboth"></div>
        <div class="mcMapControlsBelowContainer">
            <uc1:TravelNewsMapKeyControl id="keyControl" runat="server"></uc1:TravelNewsMapKeyControl>
        </div>       
    </div>
</div>