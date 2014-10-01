<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ServiceDetailsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.ServiceDetailsControl" %>

<div id="serviceUnavailableDiv" runat="server" class="serviceUnavailable">
    <asp:Label ID="serviceUnavailableLbl" runat="server" CssClass="error" />
</div>


<div id="serviceDiv" runat="server" class="serviceContainer">

    <div class="journeyHeadingContainer serviceHeadingContainer">
        <div class="journeyHeadingDiv serviceTitleDiv">
            <asp:Label ID="serviceTitle" runat="server" CssClass="headingJourney headingService"></asp:Label>
        </div>
    </div>

    <div id="serviceMessageDiv" runat="server" class="serviceMessageDiv">
        <asp:Label ID="serviceMessageLbl" runat="server" CssClass="serviceMessage"/>
    </div>

    <asp:Repeater ID="serviceLegRptr" runat="server" OnItemDataBound="serviceLegRptr_DataBound">
        <HeaderTemplate>
            <div class="callingPointsDiv">
        </HeaderTemplate>
        <ItemTemplate>
                <div class="row">
                    <div class="columnTime">
                        <div class="timeDepart">
                            <asp:Label ID="pointScheduledTime" runat="server" EnableViewState="false" CssClass="timeScheduled" />                
                            <asp:Label ID="pointActualTime" runat="server" EnableViewState="false" CssClass="timeActual" />                
                        </div>
                    </div>
                    <div id="columnNode" runat="server" enableviewstate="false" class="columnNode">
                        <div class="nodeImage">
                            <asp:Image ID="legNodeImage" runat="server" EnableViewState="false" />
                            <asp:Image ID="pointImage" runat="server" EnableViewState="false" CssClass="pointImage" />
                        </div>
                    </div>
                    <div class="columnLocation">
                        <asp:Label ID="pointLocation" runat="server" EnableViewState="false" CssClass="station" />
                        <asp:Label ID="pointReport" runat="server" EnableViewState="false" CssClass="report" />
                    </div>
                </div>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>

    <div class="serviceOperatedDiv">
        <asp:Label ID="serviceOperatedLbl" runat="server" CssClass="serviceOperated"/>
    </div>

    <div class="serviceLastUpdated">
        <asp:Label ID="serviceLastUpdatedLbl" runat="server" CssClass="headingDate"/>
        <asp:HyperLink ID="serviceUpdateLink" runat="server" CssClass="serviceUpdate"></asp:HyperLink>
    </div>
</div>