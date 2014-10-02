<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapDisabledControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapDisabledControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div id="boxtypeonem">
    <table lang="en" cellspacing="0" summary="Journey Map">
        <tr>
            <td width="12"><asp:image id="PanNorthWestButton" imageurl="/web2/images/gifs/JourneyPlanning/PanNorthWest.gif" width="12px" height="12px" runat="server"></asp:image></td>
            <td align="center"><asp:image id="PanNorthButton" imageurl="/web2/images/gifs/JourneyPlanning/PanNorth.gif" width="36px" height="12px" runat="server" imagealign="Middle"></asp:image></td>
            <td width="12"><asp:image id="PanNorthEastButton" imageurl="/web2/images/gifs/JourneyPlanning/PanNorthEast.gif" width="12px" height="12px" runat="server"></asp:image></td>
        </tr>
        <tr>
            <td width="12"><asp:image id="PanWestButton" imageurl="/web2/images/gifs/JourneyPlanning/PanWest.gif" width="12px" height="36px" runat="server"></asp:image></td>
            <td>
                <div id="maphdisabled">
                    <div id="mapa">
                        <asp:label id="labelMapAppear" cssclass="txttenb" runat="server"></asp:label>
                    </div>
                </div>
            </td>
            <td width="12"><asp:image id="PanEastButton" imageurl="/web2/images/gifs/JourneyPlanning/PanEast.gif" width="12px" height="36px" runat="server"></asp:image></td>
        </tr>
        <tr>
            <td width="12"><asp:image id="PanSouthWestButton" imageurl="/web2/images/gifs/JourneyPlanning/PanSouthWest.gif" width="12px" height="12px" runat="server"></asp:image></td>
            <td align="center"><asp:image id="PanSouthButton" imageurl="/web2/images/gifs/JourneyPlanning/PanSouth.gif" width="36px" height="12px" runat="server"></asp:image></td>
            <td width="12"><asp:image id="PanSouthEastButton" imageurl="/web2/images/gifs/JourneyPlanning/PanSouthEast.gif" width="12px" height="12px" runat="server"></asp:image></td>
        </tr>
    </table>
</div>
