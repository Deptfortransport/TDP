<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="incidentDetails" class="incidentDetails">
</div>
<div id="mapContainer">
    <table cellspacing="0" summary="Journey Map" lang="en">
        <tr>
            <td width="12">
                
                <cc1:TDImageButton ID="PanNorthWestButton" runat="server" Height="12px" Width="12px" />
            </td>
            <td align="center">
                <div class="MapZoomLevelContainer">
                    <div class="MapZoomLevel">
                        <asp:Label ID="labelZoomLevel" runat="server" CssClass="TDMapLabel"></asp:Label>
                    </div>
                    
                    
                    <cc1:TDImageButton ID="PanNorthButton" runat="server" Height="12px" Width="36px" />
                </div>
            </td>
            <td width="12">
                
                <cc1:TDImageButton ID="PanNorthEastButton" runat="server" Height="12px" Width="12px" />
            </td>
        </tr>
        <tr>
            <td width="12">
               
                <cc1:TDImageButton ID="PanWestButton" runat="server" Height="36px" Width="12px" />
            </td>
            <td>
                <div>
                    <div id="maph">
                        <div id="panelZoomButtons" class="panelZoomButtons" runat="server">
                        </div>
                        <cc1:TDMap ID="theMap" runat="server" Height="554px" Width="786px"></cc1:TDMap>
                    </div>
                </div>
            </td>
            <td width="12">
                
                <cc1:TDImageButton ID="PanEastButton" runat="server" Height="36px" Width="12px" />
            </td>
        </tr>
        <tr>
            <td width="12">
               
                <cc1:TDImageButton ID="PanSouthWestButton" runat="server" Height="12px" Width="12px" />
            </td>
            <td align="center">
               <div class="MapLegendContainer">
                   <div class="OverviewMapLink">
                        <cc1:TDButton ID="hyperLinkOverviewMap" CssClass="TDMapLink" CssClassMouseOver="TDMapLinkMouseOver" runat="server"></cc1:TDButton>
                   </div>
                   <div class="MapLegend">
                            <asp:HyperLink ID="hyperLinkMapKey" runat="server" CssClass="TDMapLink"></asp:HyperLink>
                        </div>
                   
                    <cc1:TDImageButton ID="PanSouthButton" runat="server" Height="12px" Width="36px" />
                </div>
            </td>
            <td width="12">
                
                <cc1:TDImageButton ID="PanSouthEastButton" runat="server" Height="12px" Width="12px" />
            </td>
        </tr>
    </table>
</div>
