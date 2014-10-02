<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationLocalityControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationLocalityControl" %>

<%@ Register TagPrefix="uc1" TagName="ZonalAccessibilityLinksControl" Src="ZonalAccessibilityLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ZonalServiceLinksControl" Src="../Controls/ZonalServiceLinksControl.ascx" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="localInformation" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_clear">
        
        <div class="stopInformationLinks">
            <uc1:ZonalAccessibilityLinksControl ID="ZonalAccessibilityLinksControl1" runat="server"
                Visible="True">
            </uc1:ZonalAccessibilityLinksControl>
       
            <uc1:ZonalServiceLinksControl ID="ZonalServiceLinksControl1" runat="server" Visible="True">
            </uc1:ZonalServiceLinksControl>
        </div>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
