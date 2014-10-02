<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="SimpleMapControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.SimpleMapControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="mapContainer">
    <table cellspacing="0" summary="Traffic Map">
        <tr>
            <td width="12">
                
                <cc1:TDImageButton ID="PanNorthWestButton" runat="server" Height="12px" Width="12px" />
            </td>
            <td align="center">
                  <div class="MapZoomLevelContainer">
                    <div class="MapZoomLevel">
                        <asp:Label ID="labelZoomLevel" runat="server" CssClass="txtseven"></asp:Label>
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
                    <div id= "panelZoomButtons" class="panelZoomButtons" runat="server"></div>  
                    <cc1:TDSimpleMapControl ID="theMap" runat="server" Height="554px" Width="786px"></cc1:TDSimpleMapControl>
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
               <div class="MapLegend">
                        <asp:HyperLink ID="hyperLinkMapKey" runat="server" CssClass="txtseven"></asp:HyperLink>
                    </div>
               
                <cc1:TDImageButton ID="PanSouthButton" runat="server" Height="12px" Width="36px" />
            </td>
            <td width="12">
                
                <cc1:TDImageButton ID="PanSouthEastButton" runat="server" Height="12px" Width="12px" />
            </td>
        </tr>
    </table>
</div>
