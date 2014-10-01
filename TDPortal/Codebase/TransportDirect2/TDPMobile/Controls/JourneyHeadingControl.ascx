<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JourneyHeadingControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.JourneyHeadingControl" %>
<div id="journeyHeadingContainer" runat="server" class="journeyHeadingContainer">
    <div id="journeyHeadingDiv" runat="server" class="journeyHeadingDiv" aria-atomic="true" aria-live="polite">
        <asp:HyperLink ID="journeyHeadingLnk" runat="server" CssClass="journeyHeadingLnk">
            <asp:Label ID="lblHeadingJourney" CssClass="headingJourney" runat="server" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblHeadingDate" CssClass="headingDate" runat="server" EnableViewState="false"></asp:Label>
        </asp:HyperLink>
    </div>
    <div id="journeyDurationDiv" runat="server" class="journeyDurationDiv">
        <div class="leave">
            <asp:Label ID="leave" CssClass="leaveHeading" runat="server" EnableViewState="false" />
            <asp:Label ID="leaveTime" CssClass="leaveTime" runat="server" EnableViewState="false" />
        </div>
        <div class="arrive">
            <asp:Label ID="arrive" CssClass="arriveHeading" runat="server" EnableViewState="false" />
            <asp:Label ID="arriveTime" CssClass="arriveTime" runat="server" EnableViewState="false" />
        </div>       
        <div class="changes">
            <asp:Label ID="changes" runat="server" EnableViewState="true" />
        </div>  
        <div class="duration">
            <asp:Label ID="duration" CssClass="durationHeading" runat="server" EnableViewState="false" />
            <asp:Label ID="durationSR" CssClass="screenReaderOnly" runat="server" EnableViewState="true" />
            <asp:Label ID="durationTime" CssClass="durationTime" runat="server" EnableViewState="false" />
        </div>  
    </div>
</div>