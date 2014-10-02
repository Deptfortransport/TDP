<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="SimpleDateControl" Src="SimpleDateControl.ascx" %>

<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapSymbolsSelectControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapSymbolsSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="msMapSymbolContainer floatleftonly">
    <div class="msMapSymbolContainer2">
        <div class="msMapSymbolsHeading">
            <asp:Label ID="labelMapSymbols" runat="server" CssClass="txtnineb"></asp:Label>&nbsp;
          
        </div>
        
        <asp:Panel ID="panelOnlyView" runat="server" CssClass="hide">
            <div class="msPanelOnlyViewBox">
                <div class="msPanelOnlyInfoHeading">
                    <asp:Label ID="labelOnlyView" runat="server" CssClass="txtsevenb"></asp:Label>
                </div>
                       
                <div class="msPanelOnlySymbols">
                    <asp:Label ID="labelSymbolsKey" runat="server" CssClass="txtseven"></asp:Label>
                    <ul class="listerdisc">
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

        <asp:Panel ID="panelKeys" runat="server" CssClass="hide">
            <div class="msPanelKeysTransport floatleftonly">
                <div class="msTransportSymbolsHeading">
                    <table lang="en" cellspacing="0" summary="Transport">
                        <tr>
                            <td valign="top">
                                <cc1:TDImage ID="commandTransport" runat="server" Height="13px" Width="14px" />
                            </td>
                            <td class="locals">
                                <asp:Label ID="labelTransportTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
               
                <div class="msTransportSymbols">
                     
                    <asp:Table ID="tableTransport" runat="server" CellSpacing="0">
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="RLY" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image2" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="MET" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="Image28" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="BCX" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image1" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="AIR" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image3" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="FER" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="Image29" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="TXR" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="image4" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell VerticalAlign="Top">
                                <asp:CheckBox ID="CPK" runat="server" CssClass="txtseven"></asp:CheckBox>
                            </asp:TableCell>
                            <asp:TableCell HorizontalAlign="right">
                                <cc1:TDImage ID="Image31" runat="server" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>
            
           
                    
            <div class="msPanelKeysOther floatleftonly">
                <asp:Panel id="panelTravelNewsToggle" runat="server">
                    <div class="msTravelNewsToggle floatrightonly">
                        <cc1:TDButton ID="buttonToggleTravelNewsAndSymbols" runat="server"></cc1:TDButton>
                    </div>
                    <div class="clearboth"></div>
                </asp:Panel>
                <div id="pointXKeysContainer" class="msPointXKeysContainer show" runat="server">
                    <div class="msPanelKeysOtherOptions  floatleftonly">
                        <table lang="en" cellspacing="0" summary="Accommodation" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandAccommodation" runat="server" Height="13px" Width="14px" />
                                    </td>
                                <td class="locals">
                                    <asp:Label ID="labelAccommodationTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table lang="en" cellspacing="0" summary="Sport and Entertainment" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandSport" runat="server" Height="13px" Width="14px" /></td>
                                <td class="locals">
                                    <asp:Label ID="labelSportTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table lang="en" cellspacing="0" summary="Attractions" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandAttractions" runat="server" Height="13px" Width="14px" /></td>
                                <td class="locals">
                                    <asp:Label ID="labelAttractionsTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table lang="en" cellspacing="0" summary="Health" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandHealth" runat="server" Height="13px" Width="14px" /></td>
                                <td class="locals">
                                    <asp:Label ID="labelHealthTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table lang="en" cellspacing="0" summary="Education" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandEducation" runat="server" Height="13px" Width="14px" /></td>
                                <td class="locals">
                                    <asp:Label ID="labelEducationTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                        <table lang="en" cellspacing="0" summary="Public Buildings &amp; Services" class="msPanelKeysOtherOptionsTable">
                            <tr>
                                <td valign="top">
                                    <cc1:TDImage ID="commandInfrastructure" runat="server" Height="13px" Width="14px" />
                                    </td>
                                <td class="locals">
                                    <asp:Label ID="labelInfrastructureTitle" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                    
                    <div class="msPanelKeysSelectedCategory floatleftonly">
                        
                        <div class="msPanelKeysSelectedCategoryHeading">
                            <asp:Label ID="lblSelectedCategory" runat="server" CssClass="txtsevenb"></asp:Label>
                        </div>
                        
                        <div class="msPanelKeysSelectedOtherOption">
                            <asp:Table ID="tableAccommodation" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="ETDR" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image5" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="HTGH" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image6" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="CCMH" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image7" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="YHST" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image8" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tableSport" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="ODPT" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image9" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="SPCM" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image10" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="VSSC" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image11" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="RETL" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="Image30" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tableAttractions" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="BTZL" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image12" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="HSTC" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image13" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="RECS" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image14" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="TOUR" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image15" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tableHealth" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="SURG" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image16" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="HCLC" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image17" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="NRCH" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image18" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="CPHO" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image19" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tableEducation" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="PRNR" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image20" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="SMIN" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image21" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="CLUN" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image22" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="RECR" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image23" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <asp:Table ID="tableInfrastructure" runat="server" CssClass=" hide" CellSpacing="0">
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="POLC" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image24" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="LOCS" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image25" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="OTGV" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image26" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell VerticalAlign="Top">
                                        <asp:CheckBox ID="PBFC" runat="server" CssClass="txtseven"></asp:CheckBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="right">
                                        <cc1:TDImage ID="image27" runat="server" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </div>
                    </div>
                    
                    <div class="clearboth"></div>
                
                    <div class="msPanelKeysButton floatrightonly">
                        <cc1:TDButton ID="buttonOk" runat="server"></cc1:TDButton>
                    </div>
                </div>
                <div id="travelNewsContainer" class="msTravelNewsContainer floatleftonly hide" runat="server">
                    <div class="msPanelTravelNewsHeading">
                        <asp:Label ID="labelTravelNews" runat="server" CssClass="txtsevenb"></asp:Label>
                    </div>
                    <br />
                    <div class="msTravelNewsOptions  floatleftonly">
                        <table lang="en" cellspacing="0" summary="Accommodation" class="msPanelKeysTravelNewsOptionsTable">
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="roadIncidentsVisible" runat="server" CssClass="txtseven" Checked="true"></asp:CheckBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:CheckBox ID="publicIncidentsVisible" runat="server" CssClass="txtseven" Checked="true"></asp:CheckBox>
                                </td>
                                
                            </tr>
                        </table>
                        
                    </div>
                    <div class="msPanelTravelNewsDateSelector floatleftonly">
                        <uc1:simpledatecontrol id="dateSelect" runat="server"></uc1:simpledatecontrol>
                    </div>
                    <asp:Panel ID="dateErrorPanel" Style="display:none;" runat="server">
                        <asp:Label ID="lblDateError" runat="server" Text="" ForeColor="Red" Width="100%" Font-Size="X-Small"></asp:Label>    
                    </asp:Panel>
                    <div class="clearboth"></div>
                
                    <div class="msPanelKeysButton floatrightonly">
                        <cc1:TDButton ID="buttonShowNews" runat="server"></cc1:TDButton>
                    </div>
                </div>
                
            </div>
        </asp:Panel>
    </div>
</div>
