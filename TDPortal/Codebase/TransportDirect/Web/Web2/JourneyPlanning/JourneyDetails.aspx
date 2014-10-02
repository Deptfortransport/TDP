<%@ Page Language="c#" Codebehind="JourneyDetails.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyDetails" SmartNavigation="False" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarAllDetailsControl" Src="../Controls/CarAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyDetailsTableControl" Src="../Controls/CarJourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarSummaryControl" Src="../Controls/CarSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareGotoTicketRetailerControl" Src="../Controls/FindFareGotoTicketRetailerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnalyticsControl" Src="../Controls/AnalyticsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyExtensionControl" Src="../Controls/JourneyExtensionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyMapControl" Src="../Controls/JourneyMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OutputNavigationControl" Src="../Controls/OutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="../Controls/MapJourneyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleWalkLinksControl" Src="../Controls/CycleWalkLinks.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <% // More css gets added in code if page is showing plan to park and ride  %>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="homepage.css,expandablemenu.css,nifty.css,setup.css,jpstd.css,ExtendAdjustReplan.css,emissions.css,JourneyDetails.aspx.css,Map.css,ModalPopupMessage.css">
    </cc1:HeadElementControl>
    <uc1:AnalyticsControl id="analyticsControl" runat="server"></uc1:AnalyticsControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#outward" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyDetails" method="post" runat="server">
            <div class="spotlighttag">
                <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Door 2 Door Details Page -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/JourneyPlanning/JourneyDetails.aspx?cacheparam=12 -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:06/04/09 -->
                <script type="text/javascript" id="DoubleClickFloodlightTag551601">
                //<![CDATA[
                var axel = Math.random()+"";
                var a = axel * 10000000000000;
                document.write('<iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=door2193;ord=1;num=' + a + '?" width="1" height="1" frameborder="0"></iframe>');
                //]]>
                </script>
                <noscript>
                <iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=door2193;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
            </div>
            
            <asp:ScriptManager runat="server" ID="scriptManager1">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
            <a href="#JourneyButtons">
                <cc1:TDImage ID="imageJourneyButtonsSkipLink1" runat="server" CssClass="skiptolinks">
                </cc1:TDImage></a>
            <asp:Panel ID="panelOutwardJourneyPTSkipLink1" runat="server" Visible="False">
                <a href="#OutwardJourneyDetails">
                    <cc1:TDImage ID="imageOutwardJourneyPTSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <asp:Panel ID="panelOutwardJourneyCarSkipLink1" runat="server" Visible="False">
                <a href="#OutwardJourneyDetails">
                    <cc1:TDImage ID="imageOutwardJourneyCarSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <asp:Panel ID="panelFindTransportToStartLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportToStartButton">
                    <cc1:TDImage ID="imageFindTransportToStartLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <asp:Panel ID="panelFindTransportFromEndLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportFromEndButton">
                    <cc1:TDImage ID="imageFindTransportFromEndLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <asp:Panel ID="panelReturnJourneyPTSkipLink1" runat="server" Visible="False">
                <a href="#ReturnJourneyDetails">
                    <cc1:TDImage ID="imageReturnJourneyPTSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <asp:Panel ID="panelReturnJourneyCarSkipLink1" runat="server" Visible="False">
                <a href="#ReturnJourneyDetails">
                    <cc1:TDImage ID="imageReturnJourneyCarSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc1:TDImage></a></asp:Panel>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <!-- Homepage Outline Table -->
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="SocialBookmark ExpandableMenu HomePageMenu">
                            <uc1:SocialBookMarkLinkControl ID="socialBookMarkLinkControl" runat="server" EnableViewState="False"></uc1:SocialBookMarkLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="JourneyChangeSearchControl1" runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <div>
                                                        <uc1:JourneysSearchedForControl ID="journeysSearchedControl" runat="server"></uc1:JourneysSearchedForControl>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <asp:Panel ID="errorMessagePanel" runat="server" Visible="False">
                                                            <div class="boxtypeerrormsgtwo">
                                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                            </div>
                                                        </asp:Panel>
                                                    
                                                        <asp:Panel ID="panelFindFareSteps" runat="server" Visible="false">
                                                            <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server" Visible="false"></uc1:FindFareStepsControl>
                                                        </asp:Panel>  
                                                                                                                
                                                        <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server"></uc1:JourneyPlannerOutputTitleControl>
                                                        
                                                        <cc1:HelpLabelControl ID="helpLabelJourneyDetails" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc1:HelpLabelControl>
                                                        
                                                        <table cellspacing="0" cellpadding="0"  width="828">
                                                            <tr id="selectedTicketRow" runat="server">
                                                                <td id="selectedTicketCell" width="100%" valign="bottom" runat="server">
                                                                    <uc1:FindFareSelectedTicketLabelControl ID="findFareSelectedTicketLabelControl" runat="server">
                                                                    </uc1:FindFareSelectedTicketLabelControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" align="right">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td valign="bottom" align="right">
                                                                                <uc1:OutputNavigationControl ID="theOutputNavigationControl" runat="server"></uc1:OutputNavigationControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    
                                                    <a name="outward"></a>
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <div class="boxtypeseventeen">
                                                            <table id="summaryOutwardTable" runat="server">
                                                                <tr>
                                                                    <td width="610">
                                                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                                                                    </td>
                                                                    <td align="right">
                                                                        <span id="hyperLinkTagReturnJourneys" runat="server"><a class="jptablelink" href="#return">
                                                                            <asp:Label ID="hyperLinkReturnJourneys" runat="server"></asp:Label>
                                                                            <cc1:TDImage ID="hyperLinkImageReturnJourneys" runat="server"></cc1:TDImage></a></span></td>
                                                                </tr>
                                                            </table>
                                                            <uc1:SummaryResultTableControl ID="summaryResultTableControlOutward" runat="server"></uc1:SummaryResultTableControl>
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server" Visible="true"></uc1:FindSummaryResultControl>
                                                        </div>
                                                        <uc1:CycleWalkLinksControl id="outwardCycleWalkLinks" runat="server" />
                                                        <uc1:JourneyExtensionControl ID="journeyExtensionControlOutward" runat="server"></uc1:JourneyExtensionControl>
                                                        <uc1:MapJourneyControl ID="mapJourneyControlOutward" runat="server" Visible="false"></uc1:MapJourneyControl>
                                                        <asp:Panel ID="outwardDetailPanel" runat="server">
                                                        <div class="boxtypetwelve">
                                                            <div class="dmtitle">
                                                                <a name="OutwardJourneyDetails"></a>
                                                                <div class="floatleftonly">
                                                                    <span class="txteightb">
                                                                        <asp:Label ID="labelDetailsOutwardJourney" runat="server"></asp:Label>&nbsp;
                                                                        <asp:Label ID="labelCarOutward" runat="server"></asp:Label></span>&nbsp;
                                                                    <cc1:TDButton ID="buttonShowMap" runat="server"></cc1:TDButton>&nbsp;
                                                                </div>
                                                                <div class="floatrightonly">
                                                                    <cc1:TDButton ID="buttonCheckCO2Outward" runat="server" Visible="false" />
                                                                    <cc1:TDButton ID="buttonShowTableOutward" runat="server" CausesValidation="False"></cc1:TDButton>
                                                                </div>
                                                                <br />
                                                            </div>
                                                            <uc1:JourneyDetailsControl ID="journeyDetailsControlOutward" runat="server"></uc1:JourneyDetailsControl>
                                                            <uc1:CarAllDetailsControl ID="carAllDetailsControlOutward" runat="server"></uc1:CarAllDetailsControl>
                                                            <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlOutward" runat="server"></uc1:JourneyDetailsTableControl>
                                                        </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="outwardEmissionsPanel" runat="server" Visible="false">
                                                            <uc1:JourneyEmissionsCompareControl id="journeyEmissionsCompareControlOutward" runat="server"></uc1:JourneyEmissionsCompareControl>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelJourneyButtonsSkipLink2" runat="server" Visible="False">
                                                        <a href="#JourneyButtons">
                                                            <cc1:TDImage ID="imageJourneyButtonsSkipLink2" runat="server" CssClass="skiptolinks">
                                                            </cc1:TDImage></a></asp:Panel>
                                                    <asp:Panel ID="panelReturnJourneyPTSkipLink2" runat="server" Visible="False">
                                                        <a href="#ReturnJourneyDetails">
                                                            <cc1:TDImage ID="imageReturnJourneyPTSkipLink2" runat="server" CssClass="skiptolinks">
                                                            </cc1:TDImage></a></asp:Panel>
                                                    <asp:Panel ID="panelReturnJourneyCarSkipLink2" runat="server" Visible="False">
                                                        <a href="#ReturnJourneyDetails">
                                                            <cc1:TDImage ID="imageReturnJourneyCarSkipLink2" runat="server" CssClass="skiptolinks">
                                                            </cc1:TDImage></a></asp:Panel>
                                                    <a name="return"></a>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <asp:Panel ID="summaryReturnPanel" runat="server">
                                                            <asp:Table ID="summaryReturnTable" runat="server">
                                                                <asp:TableRow>
                                                                    <asp:TableCell ID="cellButtonSummary" width="610">
                                                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                                                    </asp:TableCell>
                                                                    <asp:TableCell HorizontalAlign="Right">
                                                                        <a class="jptablelink" href="#outward">
                                                                            <asp:Label ID="hyperLinkOutwardJourneys" runat="server"></asp:Label>
                                                                            <cc1:TDImage ID="hyperLinkImageOutwardJourneys" runat="server"></cc1:TDImage></a>
                                                                    </asp:TableCell>
                                                                </asp:TableRow>
                                                            </asp:Table>
                                                            <uc1:SummaryResultTableControl ID="summaryResultTableControlReturn" runat="server"></uc1:SummaryResultTableControl>
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                                                                Visible="true"></uc1:FindSummaryResultControl>
                                                        </asp:Panel>
                                                        <uc1:CycleWalkLinksControl id="returnCycleWalkLinks" runat="server" />
                                                        <uc1:JourneyExtensionControl ID="journeyExtensionControlReturn" runat="server"></uc1:JourneyExtensionControl>
                                                        <uc1:MapJourneyControl ID="mapJourneyControlReturn" runat="server" Visible="false"></uc1:MapJourneyControl>
                                                        <cc1:HelpLabelControl ID="helpLabelDetailsReturn" runat="server" Visible="False"
                                                            CssMainTemplate="helpboxoutput"></cc1:HelpLabelControl>
                                                        <div class="boxtypetwelve">
                                                            <div class="dmtitle">
                                                                <a name="ReturnJourneyDetails"></a>
                                                                <div class="floatleftonly">
                                                                    <span class="txteightb">
                                                                        <asp:Label ID="labelDetailsReturnJourney" runat="server"></asp:Label>&nbsp;
                                                                        <asp:Label ID="labelCarReturn" runat="server"></asp:Label></span>&nbsp;
                                                                    <cc1:TDButton ID="buttonShowMapReturn" runat="server"></cc1:TDButton>&nbsp;
                                                                </div>
                                                                <div class="floatrightonly">
                                                                    <cc1:TDButton ID="buttonCheckCO2Return" runat="server" Visible="false" />
                                                                    <cc1:TDButton ID="buttonShowTableReturn" runat="server" CausesValidation="False"></cc1:TDButton>
                                                                </div>
                                                                <br />
                                                            </div>
                                                            <uc1:JourneyDetailsControl ID="journeyDetailsControlReturn" runat="server"></uc1:JourneyDetailsControl>
                                                            <uc1:CarAllDetailsControl ID="carAllDetailsControlReturn" runat="server" Visible="False">
                                                            </uc1:CarAllDetailsControl>
                                                            <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlReturn" runat="server">
                                                            </uc1:JourneyDetailsTableControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <a name="amend"></a>
                                                    <div class="boxtypesixteen">
                                                        <table lang="en" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <uc1:FindFareGotoTicketRetailerControl ID="findFareGotoTicketRetailerControl" runat="server">
                                                                    </uc1:FindFareGotoTicketRetailerControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <uc1:ResultsFootnotesControl runat="server" ID="footnotesControl"></uc1:ResultsFootnotesControl>
                                                    <br/>
                                                    <a href="#JourneyButtons">
                                                        <cc1:TDImage ID="imageJourneyButtonsSkipLink3" runat="server" CssClass="skiptolinks">
                                                        </cc1:TDImage></a>
                                                        
                                                    <a name="JourneyOptions"></a>
                                                    <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
