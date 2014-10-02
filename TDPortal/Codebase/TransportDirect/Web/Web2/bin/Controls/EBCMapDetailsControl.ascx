<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EBCMapDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.EBCMapDetailsControl" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="CarSummaryControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="MapJourneyControl.ascx" %>
<div class="ebcMapDetailsBoxType1">
    <div class="ebcMapDetailsBoxType2">
        <div class="floatleftonly"> <asp:Label id="labelEBCMapControlTitle" runat="server" enableviewstate="false" CssClass="txteightb"></asp:Label> </div>
        <div class="floatrightonly"> <cc1:TDButton ID="buttonShowDetails" runat="server"></cc1:TDButton> </div>
        <div class="clearboth"></div>
    </div>
    <div class="ebcMapDetailsBoxType3">
        <div class="ebcMapDetailsBoxType4">
            <div class="boxtypetenfifteen">
                <uc1:CarSummaryControl id="carSummaryControl" runat="server"></uc1:CarSummaryControl>
            </div>
            <div class="clearboth"></div>
            <div class="ebcMapContainer">
                <uc1:MapJourneyControl ID="mapJourneyControl" runat="server"></uc1:MapJourneyControl>
            </div>
        </div>
        <div class="clearboth"></div>
    </div>
</div>
