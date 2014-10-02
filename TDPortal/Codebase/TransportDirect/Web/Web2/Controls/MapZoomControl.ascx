<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapZoomControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapZoomControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div lang="en" summary="Zooming controls for the map" style="padding-top: 5px">
    <table>
    <tr>
    <td>
    <div>
        <cc1:helpcustomcontrol id="MapZoomControlHelp" runat="server" scrolltohelp="False"></cc1:helpcustomcontrol>
    </div>
    <div style="width: 100%; display: block;">
        <div style="float: left">
            <cc1:tdbutton id="buttonZoomIn" runat="server"></cc1:tdbutton>
        </div>
        <div style="float: right;padding-right:5px;">
            <cc1:tdbutton id="buttonZoomOut" runat="server"></cc1:tdbutton>
        </div>
    </div>
    <div>
        <cc1:tdbutton id="buttonPreviousView" runat="server" visible="False"></cc1:tdbutton>
        <cc1:tdbutton id="buttonFindNewMap" runat="server" visible="false"></cc1:tdbutton>
    </div>
    </td>
    </tr>
    <tr>
    <td>
    <div style="display:block;">
        <table>
            <tr>
                <td id="panelZoomButtons" runat="server" colspan="3">
                </td>
            </tr>
            <tr>
                <td align="left" colspan="3">
                    <asp:Label ID="labelZoomControlInstructions1" runat="server" CssClass="txtnote"></asp:Label><asp:Label
                        ID="labelZoomControlInstructions2" runat="server" CssClass="txtnote" Visible="False"></asp:Label></td>
            </tr>
        </table>
    </div>
    </td>
    </tr>
    </table>
</div>
