<%@ Page Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TDP.UserPortal.TDPMobile._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" Runat="Server">
    
    <div class="divPTLogo">
        <asp:Button id="publicTransportLogoBtn" runat="server" OnClick="publicTransportModeBtn_Click" CssClass="buttonPTLogo" EnableViewState="false" />
    </div>

    <div id="divPublicTransportMode" runat="server" class="submittab">
        <asp:Button id="publicTransportModeBtn" runat="server" OnClick="publicTransportModeBtn_Click" CssClass="buttonMag" EnableViewState="false" />
    </div>
    <div id="divCycleMode" runat="server" class="submittab">
        <asp:Button id="cycleModeBtn" runat="server" OnClick="cycleModeBtn_Click" CssClass="buttonMag" EnableViewState="false" />
    </div>
    <div id="divTravelNews" runat="server" class="submittab">
        <asp:Button id="travelNewsBtn" runat="server" OnClick="travelNewsBtn_Click" CssClass="buttonBlue" EnableViewState="false" />
    </div>

</asp:Content>