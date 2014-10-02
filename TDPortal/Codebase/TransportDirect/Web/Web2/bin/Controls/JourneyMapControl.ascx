<%@ Register TagPrefix="uc1" TagName="CarAllDetailsControl" Src="CarAllDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyMapControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.JourneyMapControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationControl" Src="MapLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="CarJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapControl" Src="MapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyDisplayDetailsControl" Src="MapJourneyDisplayDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapZoomControl" Src="MapZoomControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="MapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationIconsSelectControl" Src="MapLocationIconsSelectControl.ascx" %>

<cc2:HelpLabelControl ID="helpLabelMapToolsOutward" runat="server" Visible="False"
    CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl ID="helpLabelMapToolsReturn"
        runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
            ID="journeyMapControlPrivateHelpLabel" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                ID="journeyMapControlPublicHelpLabel" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                    ID="journeyMapControlItineraryHelpLabel" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                        ID="helpLabelMapTools" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                            ID="helpLabelMapIcons" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl><cc2:HelpLabelControl
                                ID="helpLabelMapKey" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>

 
    

<table class="mapcontrol" cellspacing="0" cellpadding="0">
    <tr>
        <td valign="top">
            <div id="mapTitleArea" runat="server">
            <div class="mboxtypetwonoborder">
        <table width="100%">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td style="padding-right: 30px" valign="top" nowrap="nowrap" align="left">
                                <span class="txteightb">
                                    <asp:Label ID="labelMaps" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="labelJourney" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="labelDisplayNumber" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="labelMapsCar" runat="server"></asp:Label></span></td>
                            <td valign="top" align="right">
                                <span class="txteightb">
                                    <asp:Label ID="labelCurrentLocation" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="labelSelectedLocation" runat="server"></asp:Label></span></td>
                        </tr>
                    </table>
                </td>
                <td align="right">
                    <table cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td>
                                    <uc1:MapJourneyDisplayDetailsControl ID="theMapJourneyDisplayDetailsControl" runat="server">
                                    </uc1:MapJourneyDisplayDetailsControl>
                                </td>
                                <td>
                                   <!-- <div id="helpimg">
                                        <cc2:HelpCustomControl ID="journeySegmentHelpCustomControl" runat="server" scrolltohelp="False"
                                            HelpLabel="journeySegmentHelpLabel" Visible="false"></cc2:HelpCustomControl>&nbsp;</div> -->
                                </td>
                            </tr>
                        </table>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="labelOptions" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
            </div>
            <uc1:MapControl ID="theMapControl" runat="server"></uc1:MapControl>
            
        </td>
    </tr>
    <tr>
        <td class="mapnav" valign="top">
            <div class="MapLocationSelect" id="MapLocationSelect" runat="server">
                <div class="mapOverviewBox">
                    <div class="mhe">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="labelKey" runat="server"></asp:Label></td>
                                <td align="right">
                                   <!-- <cc2:HelpCustomControl ID="HelpControlMapKey" runat="server" scrolltohelp="False"
                                        HelpLabel="helpLabelMapKey" Visible="false"></cc2:HelpCustomControl> --></td>
                            </tr>
                        </table>
                    </div>
                    <uc1:MapKeyControl ID="theMapKeyControl" runat="server"></uc1:MapKeyControl>
                    <br />
                </div>
               
                <div class="findamapbox">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                <div class="mheSymbols">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="labelMapSymbols" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>    
                                </div>
                            </td>
                            <td>
                                <div class="mapSymbolsDisclaimer">
                                    <asp:Label ID="labelMapSymbolsDisclaimer" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <uc1:MapLocationIconsSelectControl ID="theMapLocationIconsSelectControl" runat="server">
                    </uc1:MapLocationIconsSelectControl>
                </div>
                
            </div>
           
        </td>
    </tr>
</table>

 <div class="mapOverviewContainer" runat="server" id="mapOverviewContainer" visible="false">
   <div id="overviewBox">
        <div class="mhd"><asp:label id="labelOverviewMap" runat="server"></asp:label></div>
	    <div id="msmlm"><asp:image id="imageSummaryMap" runat="server"></asp:image></div>
    </div>
</div>
<div id="travelInfoDiv" runat="server">
    <div class="boxtypeeightstd">
        <div class="txteightb">
            <cc2:TDButton ID="buttonDirections" runat="server" Visible="false"></cc2:TDButton>
            <cc2:TDButton ID="buttonDirectionsCycle" runat="server" Visible="false"></cc2:TDButton></div>
        <div id="travelInfoLabelsDiv" runat="server" visible="true">
            <div>
                <asp:Label ID="labelTotalDistance" runat="server" CssClass="txtsevenb"></asp:Label>&nbsp;
                <asp:Label ID="labelTotalMiles" runat="server" CssClass="txtseven"></asp:Label></div>
            <div>
                <asp:Label ID="labelTotalDuration" runat="server" CssClass="txtsevenb"></asp:Label>&nbsp;
                <asp:Label ID="labelTotalTime" runat="server" CssClass="txtseven"></asp:Label></div>
        </div>
    </div>
</div>
