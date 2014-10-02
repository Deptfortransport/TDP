<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapLocationIconSelectControlTransportOnly.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapLocationIconSelectControlTransportOnly"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<div id="panelTransportKeysBox">
    <asp:Panel ID="panelKeys" runat="server">
        
        <div id="panelKeysOtherOptions">
                <table lang="en" cellspacing="0" summary="Transport">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandTransport" runat="server" Height="13px" Width="14px" /></td>
                        <td class="locals">
                            <asp:Label ID="labelTransportTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Accommodation">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandAccommodation" runat="server" Height="13px" Width="14px" />
                            </td>
                        <td id="Td1">
                            <asp:Label ID="labelAccommodationTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Sport and Entertainment">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandSport" runat="server" Height="13px" Width="14px" /></td>
                        <td id="Td2">
                            <asp:Label ID="labelSportTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Attractions">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandAttractions" runat="server" Height="13px" Width="14px" /></td>
                        <td id="Td3">
                            <asp:Label ID="labelAttractionsTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Health">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandHealth" runat="server" Height="13px" Width="14px" /></td>
                        <td id="Td4">
                            <asp:Label ID="labelHealthTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Education">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandEducation" runat="server" Height="13px" Width="14px" /></td>
                        <td id="Td5">
                            <asp:Label ID="labelEducationTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Public Buildings &amp; Services">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandInfrastructure" runat="server" Height="13px" Width="14px" />
                            </td>
                        <td id="Td6">
                            <asp:Label ID="labelInfrastructureTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        <div id="panelKeysTransport1">
            
            <asp:Table ID="tableTransport" runat="server" CssClass="mtbl"
                CellSpacing="0">
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="RLY" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image2" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="AIR" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image3" runat="server" />
                    </asp:TableCell>
                     <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="CPK" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image31" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="MET" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image28" runat="server" />
                        
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="FER" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image29" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="BCX" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image1" runat="server" />
                    </asp:TableCell>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="TXR" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image4" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                
            </asp:Table>
        </div>
        
    </asp:Panel>
</div>
<div id="panelKeysButton">
    <cc1:TDButton ID="buttonOk" runat="server"></cc1:TDButton></div>