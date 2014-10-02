<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapLocationIconsSelectControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapLocationIconsSelectControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="mha">
    <asp:Panel ID="panelOnlyView" runat="server">
         <div class="panelOnlyViewBox">
                    <div class="panelOnlyInfo">
                        <asp:Label ID="labelOnlyView" runat="server"></asp:Label>
                    </div>
                
                    <div class="panelOnlySymbols">
                        <asp:Label ID="labelSymbolsKey" runat="server"></asp:Label>
                        <ul class="mapicons">
                            <li>
                                <asp:Label ID="labelTransport" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelAccommodation" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelSport" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelAttractions" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelHealth" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelEducation" runat="server"></asp:Label></li>
                            <li>
                                <asp:Label ID="labelPublicBuildings" runat="server"></asp:Label></li>
                        </ul>
                    </div>
          </div>      
    </asp:Panel>
</div>
<div id="panelKeysBox" runat="server">
    <asp:Panel ID="panelKeys" runat="server">
        <div class="panelKeysTransport">
            <table lang="en" cellspacing="0" summary="Transport">
                <tr>
                    <td valign="top">
                        <cc1:TDImageButton ID="commandTransport" runat="server" Height="13px" Width="14px" /></td>
                    <td class="locals">
                        <asp:Label ID="labelTransportTitle" runat="server"></asp:Label></td>
                </tr>
            </table>
            <asp:Table ID="tableTransport" runat="server" Height="65px" Width="160px" CssClass="mtbl"
                CellSpacing="0">
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="RLY" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image2" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="MET" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image28" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="BCX" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image1" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="AIR" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image3" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="FER" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image29" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="TXR" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="image4" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell VerticalAlign="Top">
                        <asp:CheckBox ID="CPK" runat="server"></asp:CheckBox>
                    </asp:TableCell>
                    <asp:TableCell HorizontalAlign="right">
                        <cc1:TDImage ID="Image31" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
        <div class="panelKeysOther">
            <div class="panelKeysOtherOptions">
                <table lang="en" cellspacing="0" summary="Accommodation">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandAccommodation" runat="server" Height="13px" Width="14px" />
                            </td>
                        <td class="locals">
                            <asp:Label ID="labelAccommodationTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Sport and Entertainment">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandSport" runat="server" Height="13px" Width="14px" /></td>
                        <td class="locals">
                            <asp:Label ID="labelSportTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Attractions">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandAttractions" runat="server" Height="13px" Width="14px" /></td>
                        <td class="locals">
                            <asp:Label ID="labelAttractionsTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Health">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandHealth" runat="server" Height="13px" Width="14px" /></td>
                        <td class="locals">
                            <asp:Label ID="labelHealthTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Education">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandEducation" runat="server" Height="13px" Width="14px" /></td>
                        <td class="locals">
                            <asp:Label ID="labelEducationTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <table lang="en" cellspacing="0" summary="Public Buildings &amp; Services">
                    <tr>
                        <td valign="top">
                            <cc1:TDImageButton ID="commandInfrastructure" runat="server" Height="13px" Width="14px" />
                            </td>
                        <td class="locals">
                            <asp:Label ID="labelInfrastructureTitle" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div class="panelKeysSelectedCategory">
                <div class="mhd">
                    <asp:Label ID="lblSelectedCategory" runat="server"></asp:Label></div>
                <div style="margin-top: 3px;">
                    <asp:Table ID="tableAccommodation" runat="server" Height="58px" CssClass="mtbl" CellSpacing="0"
                        Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="ETDR" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image5" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="HTGH" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image6" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="CCMH" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image7" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="YHST" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image8" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tableSport" runat="server" Height="77px" CssClass="mtbl" CellSpacing="0"
                        Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="ODPT" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image9" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="SPCM" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image10" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="VSSC" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image11" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="RETL" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="Image30" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tableAttractions" runat="server" Height="57px" CssClass="mtbl" CellSpacing="0"
                        Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="BTZL" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image12" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="HSTC" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image13" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="RECS" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image14" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="TOUR" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image15" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tableHealth" runat="server" Height="65px" CssClass="mtbl" CellSpacing="0"
                        Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="SURG" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image16" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="HCLC" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image17" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="NRCH" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image18" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="CPHO" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image19" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tableEducation" runat="server" Height="69px" CssClass="mtbl" CellSpacing="0"
                        Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="PRNR" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image20" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="SMIN" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image21" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="CLUN" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image22" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="RECR" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image23" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Table ID="tableInfrastructure" runat="server" Height="47px" CssClass="mtbl"
                        CellSpacing="0" Visible="False">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="POLC" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image24" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="LOCS" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image25" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="OTGV" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image26" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="PBFC" runat="server"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image27" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
        </div>
    </asp:Panel>
</div>
<div class="panelKeysButton">
    <cc1:TDButton ID="buttonOk" runat="server"></cc1:TDButton></div>
