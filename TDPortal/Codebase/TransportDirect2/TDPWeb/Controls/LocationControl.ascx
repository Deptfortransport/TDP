<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationControl.ascx.cs" Inherits="TDP.UserPortal.TDPWeb.Controls.LocationControl" %>
<%@ Register Namespace="TDP.UserPortal.TDPWeb.Controls" TagPrefix="cc1" assembly="tdp.userportal.tdpweb" %> 
<%@ Register Namespace="TDP.Common.Web" TagPrefix="cc2" assembly="tdp.common.web" %> 


<div class="locationControl">
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
        <cc2:GroupDropDownList ID="venueDropdown" CssClass="venueDropdown" runat="server" Visible="false"></cc2:GroupDropDownList>
    </div>
        
    <div id="resetDiv" runat="server" class="resetDiv">
        <asp:Button ID="resetButton" CssClass="resetButton locationreset linkButton" runat="server" OnClick="ResetLocation"/>
    </div>
</div>
