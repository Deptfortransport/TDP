<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsMapKeyControl" Src="../Controls/TravelNewsMapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapRegionControl" Src="../Controls/MapRegionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnalyticsControl" Src="../Controls/AnalyticsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="../Controls/MapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapTravelNewsControl" Src="../Controls/MapTravelNewsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Page Language="c#" Codebehind="TravelNews.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.TravelNews"
    EnableViewState="True" %>

<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="../Controls/CalendarControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsDetailsControl" Src="../Controls/TravelNewsDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ShowNewsControl" Src="../Controls/ShowNewsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/LiveTravel/TravelNews.aspx" />
    <meta name="description" content="Keep up to date with live travel and traffic news for Britain's road and rail networks. Brought to you by Transport Direct." />
    <cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="MapIncidents.css,setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,TravelNews.aspx.css,Map.css"></cc1:headelementcontrol>
    <uc1:AnalyticsControl id="analyticsControl" runat="server"></uc1:AnalyticsControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#PrinterFriendly" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="TravelNews" method="post" runat="server">
        <asp:ScriptManager runat="server" ID="scriptManager1">
            <Services>
                <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
            </Services>
        </asp:ScriptManager>
        <uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
        <table id="pagelayout" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="top" class="LeftHandNavigationBar">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top">
                                 <div style="height:1px; width:1px;">
                                        <table>
                                            <tr>

                                                <td align="left">
                                                    <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                                                    <!-- Activity Name for this tag is:Transport Direct Live Travel -->
                                                    <!-- Web site URL where tag should be placed: http://www.transportdirect.info/TransportDirect/en/LiveTravel/Home.aspx?cacheparam=0 -->
                                                    <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                                                    <!-- Creation Date:7/11/2007 -->
                                                    <script language="JavaScript" type="text/javascript">
                                                    //<![CDATA[
										                    var axel = Math.random()+"";
										                    var a = axel * 10000000000000;
										                    document.write('<img src="http://ad.uk.doubleclick.net/activity;src=1501791;type=trans203;cat=trans362;ord=1;num='+ a + '?" width="1" height="1" border="0" alt=" " />');
										            //]]>       
                                                    </script>
                                                    <noscript>
                                                        <img src="http://ad.uk.doubleclick.net/activity;src=1501791;type=trans203;cat=trans362;ord=1;num=1?"
                                                            width="1" height="1" border="0" alt=" " />
                                                    </noscript>
                                                    <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
                                                </td>
                                            </tr>
                                         </table>
                                         </div>
                                         
                                <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                                        CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                                        
                            </td>
                        </tr>
                                        
                        <tr>
                            <td valign="bottom">
                                
                            </td>
                        </tr>
                        
                    </table>
                    
                    
                    
                </td>
                  <td id="contentarea" valign="top">
                     <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                         <table style="width: 840px">
                            <tr>
                                <td align="left">
                                    
                                </td>
                                <td align="right">
                                    <a name="PrinterFriendly"></a>
                                    <div class="boxprinterfriendly">
                                        <uc1:printerfriendlypagebuttoncontrol id="PrinterFriendlyPageButtonControl1" runat="server"></uc1:printerfriendlypagebuttoncontrol>
                                        <cc1:helpcustomcontrol id="travelNewsHelp" runat="server" style="vertical-align: top"></cc1:helpcustomcontrol>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div class="toptitlediv">
                                        <table>
                                            <tr>
                                                <td>
                                                     <cc1:TDImage ID="imageTravelNews" runat="server" CssClass="titleImage"></cc1:TDImage>
                                                </td>
                                                <td>
                                                    <h1>
                                                        <asp:Label ID="lblLiveTravelNews" runat="server"></asp:Label>
                                                
                                                    &nbsp;&nbsp;<asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                                    </h1>
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </div>
                                
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="topInstructions">
                                    <asp:Label ID="lblTopInstruction" runat="server" CssClass="txtseven"></asp:Label></td>
                            </tr>
                            
                        </table>
                        <asp:Panel ID="dateErrorPanel" Style="padding-left: 10px;display:none;" runat="server" Width="827">
                            <uc1:errordisplaycontrol id="dateErrorControl" runat="server" style="display:none;"></uc1:errordisplaycontrol>
                            <br/>
                        </asp:Panel>
                        <asp:Panel ID="searchPhraseErrorPanel" Style="padding-left: 10px;display:none;" runat="server" Width="827">
                            <uc1:errordisplaycontrol id="searchPhraseErrorControl" runat="server" style="display:none;"></uc1:errordisplaycontrol>
                            <br/>
                        </asp:Panel>
                        <asp:Panel ID="errorPanel" Style="padding-left: 10px" runat="server" Width="827">
                            <uc1:errordisplaycontrol id="errorControl" runat="server"></uc1:errordisplaycontrol>
                            <br/>
                        </asp:Panel>
                        <asp:Panel ID="helpPanelTravelNews" runat="server" EnableViewState="false">
                            <cc1:helplabelcontrol id="helpLabelTravelNews" runat="server" cssmaintemplate="helpboxtravel" Visible="false"></cc1:helplabelcontrol>
                        </asp:Panel>
                        <asp:Panel ID="helpPanelTravelNewsNonMap" runat="server" EnableViewState="false">
                            <cc1:helplabelcontrol id="helpLabelTravelNewsNonMap" runat="server"
                                cssmaintemplate="helpboxtravel2" Visible="false"></cc1:helplabelcontrol>
                        </asp:Panel>
                        <div class="boxtypetravelnewsswitch">
                            <table class="TravelNewsSwitch" cellpadding="0" cellspacing="0">
                                <tr class="TravelNewsSwitchHeader">
                                    <td class="txtnineb" align="left">
                                        <h2>
                                            <asp:Label ID="lblCurrentView" runat="server"></asp:Label>
                                        </h2>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblHoverMsg" runat="server" Visible=False CssClass="txtseven"></asp:Label>
                                    </td>
                                    <td align="right">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                         <uc1:shownewscontrol id="ShowNewsControl" runat="server"></uc1:shownewscontrol>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="boxtypetravelnewsswitchbuttons">
                            <cc1:tdbutton id="ButtonSwitchView" runat="server" ></cc1:tdbutton>
                        </div>
                       <%-- ESRI map doesn't like the style as display as 'none' or 'block'. One of the problem is when map get double clicked
                        instead of centering map, the map pans weird. As a solution travelnews detail and map view are put in the container div
                        with position as relative. Also, position as absolute with left position 0 and top position 0 applied to the detail view
                        and map view child containers.--%>
                        <div class="travelNewsContainer">
                        <div id="travelNewsDetailsContainer" runat="server" class="travelNewsDetail">
                            <uc1:travelnewsdetailscontrol id="TravelNewsDetails" runat="server"></uc1:travelnewsdetailscontrol>
                        </div>
                        <asp:Label ID="labelNoTravelNews" runat="server" CssClass="txtseven"></asp:Label>
                        <table id="mapTable" runat="server" cellpadding="0" cellspacing="0" class="travelNewsMap">
                            <tr>
                                <td>
                                    <uc1:maptravelnewscontrol id="MapTravelNews" runat="server" Visible ="false"></uc1:maptravelnewscontrol>
                                    <div class="mcMapControlsContainer" id="newsContainer" runat="server">
                                        <div class="mcMapControlContainer">
                                            <div class="mcMapControl">
                                                <div id="travelNewsMap">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearboth"></div>
                                        <div class="mcMapControlsBelowContainer">
                                            <uc1:TravelNewsMapKeyControl id="keyControl" runat="server"></uc1:TravelNewsMapKeyControl>
                                        </div>  
                                        <asp:HiddenField ID="singleIncidentId" runat="server" />
                                        <asp:HiddenField ID="singleIncidentEasting" runat="server" />
                                        <asp:HiddenField ID="singleIncidentNorthing" runat="server" />
                                        <asp:HiddenField ID="singleIncidentScale" runat="server" />
                                    </div>
                                </td>
                            </tr>
                       </table>
                       </div>
                    </cc1:RoundedCornerControl>
                </td>
                
            </tr>
            
            
        </table>
        <div class="mapRegionContainer">
             <uc1:mapregioncontrol id="regionSelector" runat="server"></uc1:mapregioncontrol>
        </div>  
        <uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
        
    </form>
    </div>
</body>
</html>
