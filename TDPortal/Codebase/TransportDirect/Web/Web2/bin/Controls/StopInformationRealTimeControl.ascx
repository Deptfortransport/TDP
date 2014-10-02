<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationRealTimeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationRealTimeControl" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="realTimeInfo" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_clear">
        <div class="stopInformationLinks">
            <asp:HyperLink ID="DepartureBoardHyperLink" CssClass="realTimeLink" runat="server" Visible ="false">
            <asp:Label ID="labelDepartureBoardNavigation" runat="server"></asp:Label></asp:HyperLink>
            
            <asp:HyperLink ID="ArrivalsBoardHyperlink" CssClass="realTimeLink" runat="server" Visible ="false">
            <asp:Label ID="labelArrivalsBoardNavigation" runat="server"></asp:Label></asp:HyperLink>
         </div>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
