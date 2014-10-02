<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationMapControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationMapControl" %>

<%@ Register TagPrefix="uc1" TagName="BasicMapControl" Src="MapBasicControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="StopInformationMapTitle" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_nopadding">
        <uc1:BasicMapControl ID="theMap" height="224px" Width="391px" runat="server" />
        <asp:Panel ID="panelExpandMapButton" runat="server">
            <div class="stopInformationMapExpandMap">
                <cc1:TDButton ID="ExpandMap" runat="server" />
            </div>
        </asp:Panel>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
