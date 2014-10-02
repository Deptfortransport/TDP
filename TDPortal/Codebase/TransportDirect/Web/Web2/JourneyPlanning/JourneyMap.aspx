<%@ Page Language="c#" Codebehind="JourneyMap.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyMap" %>

<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OutputNavigationControl" Src="../Controls/OutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapKeyControl" Src="../Controls/MapKeyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyMapControl" Src="../Controls/JourneyMapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareGotoTicketRetailerControl" Src="../Controls/FindFareGotoTicketRetailerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarAllDetailsControl" Src="../Controls/CarAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleAllDetailsControl" Src="../Controls/CycleAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping" Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="../Controls/MapJourneyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsShowControl" Src="../Controls/JourneyDetailsShowControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <% /* More css gets added in code if page is showing plan to park and ride or car journey */  %>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,JourneyMap.aspx.css,Map.css">
    </cc2:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyMap" method="post" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" >
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <a href="#MainContent">
                <cc2:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc2:TDImage></a>
            <a href="#JourneyButtons">
                <cc2:TDImage ID="imageJourneyButtonsSkipLink1" runat="server" CssClass="skiptolinks">
                </cc2:TDImage></a>
            <asp:Panel ID="panelOutwardJourneyMapSkipLink1" runat="server" Visible="False">
                <a href="#OutwardJourneyMap">
                    <cc2:TDImage ID="imageOutwardJourneyMapSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc2:TDImage></a></asp:Panel>
            <asp:Panel ID="panelFindTransportToStartLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportToStartButton">
                    <cc2:TDImage ID="imageFindTransportToStartLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </cc2:TDImage></a></asp:Panel>
            <asp:Panel ID="panelFindTransportFromEndLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportFromEndButton">
                    <cc2:TDImage ID="imageFindTransportFromEndLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </cc2:TDImage></a></asp:Panel>
            <asp:Panel ID="panelReturnJourneyMapSkipLink1" runat="server" Visible="False">
                <a href="#ReturnJourneyMap">
                    <cc2:TDImage ID="imageReturnJourneyMapSkipLink1" runat="server" CssClass="skiptolinks">
                    </cc2:TDImage></a></asp:Panel>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <!-- Homepage Outline Table -->
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
 
                        <div class="SocialBookmark ExpandableMenu HomePageMenu">
                            <uc1:SocialBookMarkLinkControl ID="socialBookMarkLinkControl" runat="server" EnableViewState="False"></uc1:SocialBookMarkLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc2:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="theJourneyChangeSearchControl" runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False" enableviewstate="false">
                                                            <div class="boxtypeerrormsgfour">
                                                                <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                                            </div>
                                                        </asp:Panel>
                                                    </div>
                                                    <div>
                                                        <uc1:JourneysSearchedForControl ID="JourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>                                            
                                                    </div>
                                                    
                                                    <div class="boxtypesixteen">
                                                        <asp:Panel ID="panelFindFareSteps" runat="server" Visible="false">
                                                            <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server" Visible="false"></uc1:FindFareStepsControl>
                                                        </asp:Panel>
                                                        
                                                        <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server"></uc1:JourneyPlannerOutputTitleControl>
                                                        
                                                        <cc2:HelpLabelControl ID="helpLabelJourneyMap" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                        
                                                        <table cellspacing="0" cellpadding="0" width="828" summary="Journey map menu">
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
                                                                            <cc2:TDImage ID="hyperLinkImageReturnJourneys" runat="server"></cc2:TDImage></a></span></td>
                                                                </tr>
                                                            </table>
                                                            <uc1:SummaryResultTableControl ID="summaryResultTableControlOutward" runat="server"
                                                                Visible="false"></uc1:SummaryResultTableControl>
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server"
                                                                Visible="false"></uc1:FindSummaryResultControl>
                                                        </div>
                                                        
                                                        <!-- Outward Map -->
                                                        <a name="OutwardJourneyMap"></a>
                                                        <asp:Panel ID="panelMapJourneyControlOutward" runat="server">
                                                            <uc1:MapJourneyControl ID="mapJourneyControlOutward" runat="server"></uc1:MapJourneyControl>
                                                            <uc1:JourneyDetailsShowControl ID="journeyDetailsShowControlOutward" runat="server"></uc1:JourneyDetailsShowControl>
                                                                                                                                                    
                                                            <asp:Panel ID="directionsPanel" runat="server">
                                                                <div class="boxtypetwelve">
                                                                    <div id="dmtitle">
                                                                        <div style="float: right">
                                                                            <cc2:HelpCustomControl ID="DetailsHelpCustomControl" runat="server" HelpLabel="helpLabelDetails"
                                                                                scrolltohelp="False"></cc2:HelpCustomControl></div>
                                                                        <div>
                                                                            <span class="txteightb">
                                                                                <asp:Label ID="labelDetailsOutwardJourney" runat="server"></asp:Label>&nbsp;
                                                                                <asp:Label ID="labelCarOutward" runat="server"></asp:Label></span>&nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <uc1:CarAllDetailsControl ID="carAllDetailsControlOutward" runat="server"></uc1:CarAllDetailsControl>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="directionsCyclePanel" runat="server">
                                                                <div class="cycleJourneyDetailsPanel">
                                                                    <uc1:CycleAllDetailsControl ID="cycleAllDetailsControlOutward" runat="server" Visible="false"></uc1:CycleAllDetailsControl>
                                                                </div>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                    
                                                    <a name="return"></a>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <asp:Table ID="summaryReturnTable" runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell ID="cellButtonSummary" width="610" Style="width: 610px">
                                                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                                                </asp:TableCell>
                                                                <asp:TableCell HorizontalAlign="Right">
                                                                    <a class="jptablelink" href="#outward">
                                                                        <asp:Label ID="hyperLinkOutwardJourneys" runat="server"></asp:Label>
                                                                        <cc2:TDImage ID="hyperLinkImageOutwardJourneys" runat="server"></cc2:TDImage></a>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <uc1:SummaryResultTableControl ID="summaryResultTableControlReturn" runat="server"
                                                            Visible="false"></uc1:SummaryResultTableControl>
                                                        <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                                                            Visible="false"></uc1:FindSummaryResultControl>
                                                    </asp:Panel>
                                                    <asp:Panel ID="returnPanel2" runat="server">
                                                        <a name="ReturnJourneyMap"></a>
                                                        <asp:Panel ID="panelMapJourneyControlReturn" runat="server">
                                                            <uc1:MapJourneyControl ID="mapJourneyControlReturn" runat="server"></uc1:MapJourneyControl>
                                                            <uc1:JourneyDetailsShowControl ID="journeyDetailsShowControlReturn" runat="server"></uc1:JourneyDetailsShowControl>
                                                            
                                                            <asp:Panel ID="directionsPanelReturn" runat="server">
                                                                <div class="boxtypetwelve">
                                                                    <div id="dmtitle">
                                                                        <div style="float: right">
                                                                            <cc2:HelpCustomControl ID="DetailsReturnHelpCustomControl" runat="server" HelpLabel="helpLabelDetailsReturn"
                                                                                scrolltohelp="False"></cc2:HelpCustomControl></div>
                                                                        <div>
                                                                            <span class="txteightb">
                                                                                <asp:Label ID="labelDetailsJourneyReturn" runat="server"></asp:Label>&nbsp;
                                                                                <asp:Label ID="labelReturnCar" runat="server"></asp:Label></span>&nbsp;
                                                                        </div>
                                                                    </div>
                                                                    <uc1:CarAllDetailsControl ID="carAllDetailsControlReturn" runat="server"></uc1:CarAllDetailsControl>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="directionsCyclePanelReturn" runat="server">
                                                                <div class="cycleJourneyDetailsPanel">
                                                                    <uc1:CycleAllDetailsControl ID="cycleAllDetailsControlReturn" runat="server" Visible="false"></uc1:CycleAllDetailsControl>
                                                                </div>
                                                            </asp:Panel>
                                                        </asp:Panel>
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
                                                    <br />
                                                    <p>
                                                        <a href="#JourneyButtons">
                                                            <cc2:TDImage ID="imageJourneyButtonsSkipLink2" runat="server" CssClass="skiptolinks">
                                                            </cc2:TDImage></a>
                                                    </p>
                                                        <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                        &nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                                        
                        </cc2:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
