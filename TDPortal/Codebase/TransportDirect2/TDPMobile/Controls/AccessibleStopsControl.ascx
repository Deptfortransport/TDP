<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessibleStopsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.AccessibleStopsControl" %>

<div id="accessibility">

    <h3 id="accessibleStopsHeading" runat="server" enableviewstate="false" />

<div id="accessibilityStops" runat="server" class="accessibilityStops">
    
    <div class="accessibleStopsInfoDiv">
        <asp:Label ID="accessibleStopsInfo" runat="server" EnableViewState="false"></asp:Label>
    </div>

    <div class="clear"></div>

    <div id="originLabelDiv" class="accessibleStopDiv" runat="server" visible="false">
        <fieldset>
            <asp:Label ID="originLabel" CssClass="accessibleStopsLabel" runat="server" enableviewstate="false"/>
        </fieldset>
    </div>
    
    <div id="originAccessibleStopDiv" class="accessibleStopDiv" runat="server">
        <fieldset data-role="controlgroup">
            <asp:Label ID="originHeading" CssClass="accessibleStopsLabel" runat="server" enableviewstate="false"/>
            <asp:DropDownList ID="originStopList" runat="server" OnSelectedIndexChanged="Stop_Click" CssClass="accessibleStopsDrop" runat="server" />
        </fieldset>
    </div>
    
    <div id="destinationLabelDiv" class="accessibleStopDiv" runat="server" visible="false">
        <fieldset>
            <asp:Label ID="destinationLabel" CssClass="accessibleStopsLabel" runat="server" />
        </fieldset>
    </div>
    
    <div id="destinationAccessibleStopDiv" class="accessibleStopDiv" runat="server">
        <fieldset data-role="controlgroup">
            <asp:Label ID="destinationHeading" CssClass="accessibleStopsLabel" runat="server" enableviewstate="false"/>
            <asp:DropDownList ID="destinationStopList" runat="server" OnSelectedIndexChanged="Stop_Click" CssClass="accessibleStopsDrop" runat="server" />
        </fieldset>
    </div>
    
</div>
</div>

<div class="clear"></div>

<div class="submittab">
    <div class="jshide">
        <asp:LinkButton ID="planJourneyBtn" runat="server" OnClick="planJourneyBtn_Click" EnableViewState="false" CssClass="buttonMag" ></asp:LinkButton>
    </div>
    <noscript>
        <asp:Button ID="planJourneyBtnNonJS" runat="server" OnClick="planJourneyBtn_Click" EnableViewState="false" CssClass="buttonMag buttonMagNonJS" ></asp:Button>
    </noscript>
</div>
