<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EBCAllDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.EBCAllDetailsControl" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="CarSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="CarJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="EBCCalculationDetailsTableControl" Src="EBCCalculationDetailsTableControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="ebcAllDetailsBoxType1">
    <div class="ebcAllDetailsBoxType2">
        <div class="floatleftonly"> <asp:Label id="labelEBCAllDetailsControlTitle" runat="server" enableviewstate="false" CssClass="txteightb"></asp:Label> </div>
        <div class="floatrightonly"> <cc1:TDButton ID="buttonShowMap" runat="server"></cc1:TDButton> </div>
        <div class="clearboth"></div>
    </div>
    <div class="ebcAllDetailsBoxType3">
        <div class="ebcAllDetailsBoxType4">
            <div class="boxtypetenfifteen">
                <uc1:CarSummaryControl id="carSummaryControl" runat="server"></uc1:CarSummaryControl>
            </div>
            <div class="clearboth"></div>
            <div class="floatleftonly">
                <uc1:CarJourneyDetailsTableControl id="carJourneyDetailsTableControl" runat="server"></uc1:CarJourneyDetailsTableControl>
            </div>
            <div class="floatrightonly">
                <uc1:EBCCalculationDetailsTableControl id="ebcCalculationDetailsTableControl" runat="server"></uc1:EBCCalculationDetailsTableControl>                
            </div>
        </div>
        <div class="clearboth"></div>
    </div>
</div>
