<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnalyticsControl" Src="Controls/AnalyticsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsHeadlineControl" Src="Controls/TravelNewsHeadlineControl.ascx"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindAPlaceControl" Src="Controls/FindAPlaceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PlanAJourneyControl" Src="Controls/PlanAJourneyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="Controls/ExpandableMenuControl.ascx" %>
<%@ Page language="c#" Codebehind="Home.aspx.cs" validateRequest = "false" AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.Templates.HomeDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <link rel="canonical" href="http://www.transportdirect.info/Web2/Home.aspx" />
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css,Home.aspx.css"></cc1:headelementcontrol>
		<meta name="ROBOTS" content="NOODP" />
		<uc1:AnalyticsControl id="analyticsControl" runat="server"></uc1:AnalyticsControl>	
	</head>

	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#PlanAJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
		<form id="HomeDefault" method="post" runat="server">
			<a href="#SideNavigation">
				<cc1:tdimage id="imageSideNavigationSkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<a href="#QuickPlanners">
				<cc1:tdimage id="imageQuickPlannersSkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<a href="#PlanAJourney">
				<cc1:tdimage id="imagePlanAJourneySkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<a href="#FindAPlace">
				<cc1:tdimage id="imageFindAPlaceSkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<a href="#TravelNews">
				<cc1:tdimage id="imageTravelNewsSkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<a href="#TipsAndTools">
				<cc1:tdimage id="imageTipsAndToolsSkipLink1" runat="server" class="skiptolinks"></cc1:tdimage></a>
			<!-- header -->
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<table class="HomepageOutlineTable" cellpadding="0" cellspacing="0">
				<tr>
					<td valign="top" class="LeftHandNavigationBar">
						<a name="SideNavigation"></a>
						<uc1:expandablemenucontrol id="expandableMenuControl" runat="server" CategoryCssClass="HomePageMenu"></uc1:expandablemenucontrol>
						<uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
						<div class="HomepageBookmark">
							<uc1:clientlinkcontrol id="clientLink" runat="server"></uc1:clientlinkcontrol>
						</div>
					</td>
					
					<td valign="top">
						<cc1:roundedcornercontrol id="Roundedcornercontrol1" cssclass="bodyArea" runat="server" corners="TopLeft" outercolour="#CCECFF"
							innercolour="White">
							<a name="QuickPlanners"></a>
							<div class="clearboth"></div>
							<div class="FindALayoutTable">
							        <div class="floatleftonly FindATableCell">
										<asp:hyperlink id="hyperLinkFindTrain" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindTrain" class="FindATableImage" runat="server" width="62" height="36"></cc1:tdimage>
											<asp:literal id="literalFindTrain" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
								    </div>
								    <div class="floatleftonly FindATableCell">
										<asp:hyperlink id="hyperLinkFindFlight" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindFlight" class="FindATableImage" runat="server" width="62" height="36"></cc1:tdimage>
											<asp:literal id="literalFindFlight" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
								    </div>
								    <div class="floatleftonly FindATableCell">
								    	<asp:hyperlink id="hyperLinkFindCar" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindCar" class="FindATableImage" runat="server" width="62" height="36"></cc1:tdimage>
											<asp:literal id="literalFindCar" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
									</div>
									<div class="floatleftonly FindATableCell">
									    <asp:hyperlink id="hyperLinkFindCoach" runat="server" enableviewstate="false">
									        <cc1:tdimage id="imageFindCoach" class="FindATableImage" runat="server" width="62" height="36"></cc1:tdimage>
											<asp:literal id="literalFindCoach" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
									</div>
									<div class="floatleftonly FindATableCell" id="findAFareQuickLink" runat="server">
									    <asp:hyperlink id="hyperLinkFindAFare" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindAFare" class="FindATableImage" runat="server" width="70" height="36"></cc1:tdimage>
											<asp:literal id="literalFindAFare" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
									</div>
									<div class="floatleftonly FindATableCell">										
									    <asp:hyperlink id="hyperLinkCarPark" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindCarPark" class="FindATableImage" runat="server" width="44" height="36"></cc1:tdimage>
											<asp:literal id="literalFindCarPark" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
									</div>
                                    <div class="floatleftonly FindATableCell">
										<asp:hyperlink id="hyperLinkFindCycle" runat="server" enableviewstate="false">
											<cc1:tdimage id="imageFindCycle" class="FindATableImage" runat="server" width="62" height="36"></cc1:tdimage>
											<asp:literal id="literalFindCycle" runat="server" enableviewstate="false"></asp:literal>
										</asp:hyperlink>
									</div>	
							</div>
							<div class="clearboth"></div>
							<table class="HomepageLayoutTable" cellspacing="0" cellpadding="0">
							    <tr>
									<td class="WhiteSpaceBetweenColumns"></td>
									<td class="HomepageMainLayoutColumn1"><a name="PlanAJourney"></a>
										<div class="Column1Header" id="PlanAJourneyHeader" runat="server">
											<h1>
											<asp:hyperlink cssclass="Column1HeaderLink" id="hyperLinkPlanAJourney" runat="server" enableviewstate="false">
											<asp:label cssclass="txtsevenbwl" id="labelPlanAJourney" runat="server" enableviewstate="false"></asp:label>
											</asp:hyperlink>
											</h1>
											<asp:hyperlink class="txtsevenbwrlink" id="hyperLinkPlanAJourneyMore" runat="server" enableviewstate="false"></asp:hyperlink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size --> 
											&nbsp;&nbsp;&nbsp;
										</div>
										<div class="clearboth"></div>
										<div class="Column1Content1" id="PlanAJourneyDetail" runat="server">
											<uc1:planajourneycontrol id="planAJourneyControl" runat="server"></uc1:planajourneycontrol>
										</div>
										<a name="FindAPlace"></a>
										<div class="Column1Header" id="FindAPlaceHeader" runat="server">
											<h1>
											<asp:hyperlink cssclass="Column1HeaderLink" id="hyperLinkFindAPlace" runat="server" enableviewstate="false">
											<asp:label cssclass="txtsevenbwl" id="labelFindAPlace" runat="server" enableviewstate="false"></asp:label>
											</asp:hyperlink>
											</h1>
											<asp:hyperlink class="txtsevenbwrlink" id="hyperLinkFindAPlaceMore" runat="server" enableviewstate="false"></asp:hyperlink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size --> 
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										</div>
										<div class="clearboth"></div>
										<div class="Column1Content2" id="FindAPlaceDetail" runat="server">
											<uc1:findaplacecontrol id="findAPlaceControl1" runat="server"></uc1:findaplacecontrol>
										</div>
									</td>
									<td class="WhiteSpaceBetweenColumns"></td>
									<td class="HomepageMainLayoutColumn2"><a name="TravelNews"></a>
										<div class="Column2Header" id="TravelNewsHeader" runat="server">
											<h1>
											<asp:hyperlink cssclass="Column2HeaderLink" id="hyperLinkLiveTravel" runat="server" enableviewstate="false">
											<asp:label cssclass="txtsevenbwl" id="labelLiveTravel" runat="server" enableviewstate="false"></asp:label>
											</asp:hyperlink>
											</h1>
											<asp:hyperlink class="txtsevenbwrlink" id="hyperLinkLiveTravelMore" runat="server" enableviewstate="false"></asp:hyperlink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size --> 
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										</div>
										<div class="clearboth"></div>
										<div class="Column2Content1" id="TravelNewsDetail" runat="server">
											<div class="TravelNewsHeadlines">
												<uc1:travelnewsheadlinecontrol id="travelNewsHeadlines" runat="server" enableviewstate="false"></uc1:travelnewsheadlinecontrol>
											</div>
											<div class="TextAlignRight">
												<asp:label class="txtsevenbb" id="labelStatusAt" runat="server" enableviewstate="false"></asp:label>
												<asp:label class="txtsevenbn" id="labelCurrentTime" runat="server" enableviewstate="false"></asp:label>
											</div>
										</div>
										<a name="TipsAndTools"></a>
										<div >
										    <asp:Panel ID="TDTipsHtmlPlaceholderDefinition" runat="server"></asp:Panel>
										    <br />
										</div>
									</td>
									<td class="WhiteSpaceBetweenColumns"></td>
									<td class="HomepageMainLayoutColumn3">
									<asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>	
									<asp:Panel ID="TDAdditionalInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
									<asp:Panel ID="TDNewInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel></td>
								</tr>
								<tr class="HomePageLevel1Heading">
								    <td class="WhiteSpaceBetweenColumns"></td>
								    <td colspan="3">
								        <div><h1><asp:literal id="literalPageHeading" runat="server" enableviewstate="False"></asp:literal></h1></div>
								    </td>
								    <td class="WhiteSpaceBetweenColumns"></td>
								    <td></td>
								</tr>
							</table>
						</cc1:roundedcornercontrol>
						</td>
						
						<td align="left" style="width: 2px">
								<!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
								<!-- Activity Name for this tag is:Transport Direct -->
								<!-- Web site URL where tag should be placed: http://www.transportdirect.info/ -->

								<script language="JavaScript" type="text/javascript">
								//<![CDATA[

								var axel = Math.random()+"";
								var a = axel * 10000000000000;
								document.write('<iframe title="Not user content" style="display:none" src="http://fls.doubleclick.net/activityi;src=1501791;type=trans203;cat=trans762;ord=1;num=' + a + '?" width="1" height="1" frameborder="0"></iframe>');
								//]]>
								</script>
								<noscript>
								<iframe title="Not user content" style="display:none" src="http://fls.doubleclick.net/activityi;src=1501791;type=trans203;cat=trans762;ord=1;num=0123456789?" width="1" height="1" frameborder="0"></iframe>
								</noscript>

								<!-- End of DoubleClick Spotlight Tag: Please do not remove-->
						</td>
				</tr>
				<tr>
					<td class="LeftHandNavigationBar"></td>
					<td ><cc1:roundedcornercontrol id="cornerBottomLeft" cssclass="BodyArea" runat="server" corners="BottomLeft" 
						outercolour="#CCECFF" innercolour="White"></cc1:roundedcornercontrol></td>
					<td></td>
					<td style="width: 2px"></td>
				</tr>

			</table>
			<uc1:footercontrol id="FooterControl1" runat="server" enableviewstate="false"></uc1:footercontrol>
        </form>			
    </div>
		
	</body>
</html>
