<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindStationResultsLocationControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.FindStationResultsLocationControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<cc1:HelpLabelControl ID="FindStationResultsHelpLabel" Visible="False" runat="server"
    CssMainTemplate="helpboxoutput"></cc1:HelpLabelControl>
<div id="boxtypeeightstd">
    <table summary="Location Description" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td id="locationCell">
                <h1>
                    <asp:Label ID="labelLocationName" runat="server"></asp:Label>
                </h1>
            </td>
            <td valign="bottom">
                <cc1:TDButton ID="commandNewLocation" runat="server"></cc1:TDButton>
            </td>
        </tr>
    </table>
</div>
