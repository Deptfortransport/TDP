<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JourneyDetailsShowControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyDetailsShowControl" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<asp:Panel ID="panelTravelInfo" runat="server">
    <div class="boxtype11">
        <div class="txteightb">
            <cc2:TDButton ID="buttonDirectionsRoad" runat="server" Visible="false"></cc2:TDButton>
            <cc2:TDButton ID="buttonDirectionsCycle" runat="server" Visible="false"></cc2:TDButton>
        </div>
        <asp:Panel ID="panelTravelInfoLabels" runat="server">
            <div>
                <asp:Label ID="labelTotalDistance" runat="server" CssClass="txtsevenb"></asp:Label>&nbsp;
                <asp:Label ID="labelTotalMiles" runat="server" CssClass="txtseven"></asp:Label>
            </div>
            <div>
                <asp:Label ID="labelTotalDuration" runat="server" CssClass="txtsevenb"></asp:Label>&nbsp;
                <asp:Label ID="labelTotalTime" runat="server" CssClass="txtseven"></asp:Label>
            </div>
        </asp:Panel>
    </div>
</asp:Panel>
