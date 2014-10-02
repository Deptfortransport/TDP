<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PlanAJourneyControl" Src="../Controls/PlanAJourneyControl.ascx" %>

<%@ Page Language="C#" Codebehind="Home.aspx.cs" ValidateRequest="false" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyPlanning.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="description" content="Plan your journey with Transport Direct. Get train times, car routes, flight details and more." />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,HomepagePlanaJourney.aspx.css,nifty.css,expandablemenu.css, JourneyPlannerHome.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#PlanAJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="HomePlanAJourney" method="post" runat="server">
            <a href="#SideNavigation">
                <cc1:TDImage ID="imageSideNavigationSkipLink1" runat="server" class="skiptolinks" /></a>
            <a href="#DoorToDoor">
                <cc1:TDImage ID="imageDoorToDoorSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindTrain">
                <cc1:TDImage ID="imageFindTrainSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindFlight">
                <cc1:TDImage ID="imageFindFlightSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindCar">
                <cc1:TDImage ID="imageFindCarSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindCoach">
                <cc1:TDImage ID="imageFindCoachSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindTrainCost">
                <cc1:TDImage ID="imageFindTrainCostSkipLink" runat="server" class="skiptolinks" /></a>
            <a id="FindFareSkipLink" href="#FindAFare" runat="server">
                <cc1:TDImage ID="imageFindAFareSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#CompareCityToCity">
                <cc1:TDImage ID="imageCompareCityToCitySkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#VisitPlanner">
                <cc1:TDImage ID="imageVisitPlannerSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#ParkAndRide">
                <cc1:TDImage ID="imageParkAndRideSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindBus">
                <cc1:TDImage ID="imageFindBusSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindCarPark">
                <cc1:TDImage ID="imageFindCarParkSkipLink" runat="server" class="skiptolinks" /></a>
            <a href="#FindCycle">
                <cc1:TDImage ID="imageFindCycleSkipLink" runat="server" class="skiptolinks" /></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <% /* Start: Region to copy to include LH menu when white labelling */ %>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table class="HomepageLayoutTable" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                    
                                        <table cellpadding="0" cellspacing="0" width="536px">
                                            <tr>
                                                <td colspan="3"><%--<td colspan="6">--%>
                                                    <div id="boxtypeeightstd">
                                                        <h1>
                                                            <asp:Literal ID="literalPageHeading" runat="server" EnableViewState="False"></asp:Literal></h1>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr valign="middle">
                                                <td>
                                                    <table>
                                                        <tr>                                        
                                            
           							                        <td class="WhiteSpaceBetweenColumns"></td>
                        									<td class="HomepageMainLayoutColumn1"><a name="PlanAJourney"></a>
										<div class="Column1Header" id="PlanAJourneyHeader" runat="server">
											<h2>
											<asp:hyperlink cssclass="Column1HeaderLink" id="hyperLinkPlanAJourney" runat="server" enableviewstate="false">
											<asp:label cssclass="txtsevenbwl" id="labelPlanAJourney" runat="server" enableviewstate="false"></asp:label>
											</asp:hyperlink>
											</h2>
											<asp:hyperlink class="txtsevenbwrlink" id="hyperLinkPlanAJourneyMore" runat="server" enableviewstate="false"></asp:hyperlink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size --> 
											&nbsp;&nbsp;&nbsp;
										</div>
										<div class="clearboth"></div>
										<div class="Column1Content1" id="PlanAJourneyDetail" runat="server">
											<uc1:planajourneycontrol id="planAJourneyControl" runat="server"></uc1:planajourneycontrol>
										</div>                                
                                     </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top">                                       
                                                    <table>
                                                        <tr>
									                        <td>
            									            						            
									                           <div class="clearboth"></div>
                                                                
                                                                <div class="floatleftonly HyperlinkTableCell HyperlinkDoorToDoor">
                                                                    <a name="DoorToDoor"></a>
                                                                    <asp:HyperLink ID="hyperlinkDoorToDoor" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageDoorToDoor" runat="server" Width="50" Height="65"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalDoorToDoor" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
            									            
                                                                <div class="floatleftonly HyperlinkTableCell HyperlinkFindTrain">
                                                                    <a name="FindTrain"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindTrain" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindTrain" runat="server" Width="62" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindTrain" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell HyperlinkFindFlight">
                                                                    <a name="FindFlight"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindFlight" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindFlight" runat="server" Width="62" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindFlight" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindCar"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindCar" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindCar" runat="server" Width="62" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindCar" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindCoach"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindCoach" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindCoach" runat="server" Width="62" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindCoach" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindTrainCost"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindTrainCost" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindTrainCost" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindTrainCost" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell" id="findAFareQuickLink" runat="server">
                                                                    <a name="FindAFare"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindAFare" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindAFare" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindAFare" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="CompareCityToCity"></a>
                                                                    <asp:HyperLink ID="hyperlinkCompareCityToCity" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageCompareCityToCity" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalCompareCityToCity" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="VisitPlanner"></a>
                                                                    <asp:HyperLink ID="hyperlinkVisitPlanner" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageVisitPlanner" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalVisitPlanner" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="ParkAndRide"></a>
                                                                    <asp:HyperLink ID="hyperlinkParkAndRide" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageParkAndRide" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalParkAndRide" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindBus"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindBus" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindBus" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindBus" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindCarPark"></a>
                                                                    <asp:HyperLink ID="hyperlinkFindCarPark" runat="server" EnableViewState="false">
                                                                        <cc1:TDImage ID="imageFindCarPark" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                                        <br/>
                                                                        <asp:Literal ID="literalFindCarPark" runat="server" EnableViewState="false"></asp:Literal>
                                                                    </asp:HyperLink>
                                                                </div>
                                                                <div class="floatleftonly HyperlinkTableCell">
                                                                    <a name="FindCycle"></a>
										                            <asp:hyperlink id="hyperlinkFindCycle" runat="server" enableviewstate="false">
											                            <cc1:tdimage id="imageFindCycle" runat="server"></cc1:tdimage>
											                            <br/>
											                            <asp:literal id="literalFindCycle" runat="server" enableviewstate="false"></asp:literal>
										                            </asp:hyperlink>
									                            </div>	
                                                            </td>
                                                        </tr>
									                </table>
									            </td>
							                </tr>
									            
                                            <tr>
                                                <td colspan="3" style="overflow:auto" align="center">
                                                    <asp:Panel ID="PlanAJourneyInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
				
                                                    <br/>
                                                </td>
                                            </tr>
                                        </table>
                                    
                                    </td>
                                    <td class="HomepageMainLayoutColumn3" rowspan="3">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
                                        <asp:Panel ID="TDAdditionalInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                                                               
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="false"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
