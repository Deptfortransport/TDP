<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapInputControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapInputControl" %>
<%@ Register TagPrefix="uc1" TagName="MapControl2" Src="MapControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSymbolsSelectControl" Src="MapSymbolsSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSelectLocationControl2" Src="MapSelectLocationControl2.ascx" %>
<div class="miMapInputControlContainer">
    <div class="mcMapControlsContainer">
        <div class="mcMapControlsAboveContainer">
            <uc1:MapSelectLocationControl2 ID="mapSelectLocationControl" runat="server"></uc1:MapSelectLocationControl2>
        </div>
        <uc1:MapControl2 id="mapControl" runat="server"></uc1:MapControl2>
    </div>
</div>