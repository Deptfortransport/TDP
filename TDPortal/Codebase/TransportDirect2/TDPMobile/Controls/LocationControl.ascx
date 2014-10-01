<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.LocationControl" %>
<%@ Register Namespace="TDP.UserPortal.TDPMobile.Controls" TagPrefix="cc1" assembly="tdp.userportal.tdpmobile" %> 
<%@ Register Namespace="TDP.Common.Web" TagPrefix="cc2" assembly="tdp.common.web" %>
<%@ Register src="~/Controls/VenueSelectControl.ascx" tagname="VenueSelectControl" tagprefix="uc1" %>

<div class="locationControl venues">
    <div class="jssettings">
        <asp:HiddenField ID="scriptId" runat="server" />
        <asp:HiddenField ID="scriptPath" runat="server" />
        <asp:HiddenField ID="jsEnabled" Value="false" runat="server" />
        <asp:HiddenField ID="venueOnly" runat="server" />
    </div>
    
    <asp:HiddenField ID="locationInput_hdnValue"  runat="server" Value="" />
    <asp:HiddenField ID="locationInput_hdnType"  runat="server" Value="" />

    <div id="locationDirectionDiv" runat="server" class="screenReaderOnly" >
        <asp:Label ID="locationDirectionLbl" CssClass="locationLabel" runat="server" EnableViewState="false" />
    </div>

    <div id="locationDescriptionDiv" runat="server" class="screenReaderOnly">
        <asp:Label ID="locationInput_Discription"  runat="server" EnableViewState="false" />
    </div>

    <div id="ambiguityRow" runat="server" class="ambiguityrow" visible="false">
        <asp:DropDownList ID="ambiguityDrop" runat="server" CssClass="ambiguityDrop" data-role="none"></asp:DropDownList>
    </div>
    
    <div id="locationInputDiv" runat="server" class="locationInputDiv">
        <asp:TextBox ID="locationInput" CssClass="locationInput watermark watermarkNonJS" runat="server" autocorrect="off" autocapitalize="off" aria-live="polite"></asp:TextBox>
        <asp:Button ID="clearLocationButton" runat="server" CssClass="locationClearButton hide" />   
        <noscript>
            <cc2:GroupDropDownList ID="venueDropdownNonJS" CssClass="venueDropdownNonJS" runat="server" Visible="false"></cc2:GroupDropDownList>
        </noscript>
    </div>
    
    
    <asp:HyperLink ID="venueInputPageLnk" runat="server" CssClass="venuesLink jshide" data-transition='none' data-role='button' data-icon='none'></asp:HyperLink>
    
    <div id="resetDiv" runat="server" class="resetDiv">
        <asp:LinkButton ID="resetButton" runat="server" CssClass="resetLink jshide" OnClick="ResetLocation" data-role='button' data-icon='none'/>
        <noscript>
            <asp:Button ID="resetButtonNonJS" runat="server" CssClass="resetLocationNonJS" OnClick="ResetLocation" />
        </noscript>
    </div>

    <div id="currentLocationDiv" runat="server" class="mylocation jshide">
        <asp:Button ID="currentLocationButton" runat="server" CssClass="hide locationCurrent" />
    </div>

    <div id="travelFromToToggleDiv" runat="server" class="travelFromToDiv jshide" visible="false" data-role="none">
        <asp:Button ID="travelFromToToggle" CssClass="travelFromTo" runat="server" OnClick="TravelFromToToggle_Click" OnClientClick="return toggleLocation();" data-role="none" />
    </div>

</div>

<uc1:VenueSelectControl ID="venueSelectControl" runat="server"></uc1:VenueSelectControl>

