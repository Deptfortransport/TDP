<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationFacilityControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationFacilityControl" %>
    
<%@ Register TagPrefix="uc1" TagName="ZonalAccessibilityLinksControl" Src="ZonalAccessibilityLinksControl.ascx" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="stationFacilities" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_clear">
        <p>
        <asp:Label ID="labelStationFacilitiesText" runat="server" />
        </p>
        <br />
        <div class="stopInformationLinks">
            <div>
                <uc1:zonalaccessibilitylinkscontrol id="StationZonalAccessibilityLinks" runat="server"
                    visible="True">
                </uc1:zonalaccessibilitylinkscontrol>
            </div>
            
            <asp:HyperLink ID="StationAccessibilityLink" CssClass="facilityLink" runat="server" Text="Accessibility information"></asp:HyperLink>
           
            <asp:HyperLink ID="FurtherDetailsHyperLink" CssClass="facilityLink" runat="server" Visible="false">
                <asp:Label ID="labelFurtherDetailsNavigation" runat="server" />
            </asp:HyperLink>
         </div>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
