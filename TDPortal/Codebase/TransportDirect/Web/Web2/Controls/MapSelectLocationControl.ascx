<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapSelectLocationControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapSelectLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<div id="<%# GetDivId %>">
    <table width="100%">
        <tr>
            
            <td align="right">
                <div class="mapSelectResolveLocationContainer">
                <asp:Panel ID="panelInitial" runat="server">
                    <asp:Label ID="labelSelectInstructions" runat="server"></asp:Label>
                </asp:Panel>
                <asp:Panel ID="panelResolveLocation" runat="server">
                    <div id="alertwarning" style="padding: 3px;">
                        <asp:Label ID="labelSelectLocation" runat="server"></asp:Label>
                        <asp:Label ID="labelSRLocation" runat="server" CssClass="screenreader"></asp:Label>
                        <asp:DropDownList ID="dropDownListLocation" runat="server">
                        </asp:DropDownList></div>
                </asp:Panel>
                <asp:Panel ID="panelZoomLevel" runat="server">
                    <table width="100%">
                        <tr>
                            <td valign="top" align="left">
                                <asp:Image ID="imageMapError" runat="server"></asp:Image></td>
                            <td align="left">
                                <asp:Label ID="labelZoomInstructions" runat="server"></asp:Label><br/>
                                <asp:Label ID="labelZoomInstructions2" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </asp:Panel>
                </div>
            </td>
            <td>
                <asp:Table ID="tableOkCancel" runat="server" Width="100%">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="left">
                            <cc1:TDButton ID="buttonOK" runat="server"></cc1:TDButton>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="right">
                            <cc1:TDButton ID="buttonSelectCancel"
                                runat="server" CausesValidation="false" Action="return SelectLocation_Cancel();"
                                ScriptName="MapLocationControl" Text=""></cc1:TDButton>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
    </table>
</div>
