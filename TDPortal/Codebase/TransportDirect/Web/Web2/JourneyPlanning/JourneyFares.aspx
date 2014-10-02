<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyCostsControl" Src="../Controls/CarJourneyCostsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyFaresControl" Src="../Controls/JourneyFaresControl.ascx" %>
<%@ Page language="c#" Codebehind="JourneyFares.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyFares" SmartNavigation="False"%>
<%@ Register TagPrefix="uc1" TagName="FareDetailsTableSegmentControl" Src="../Controls/FareDetailsTableSegmentControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OutputNavigationControl" Src="../Controls/OutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <% /* More css gets added in code if page is showing plan to park and ride */  %>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ticketRetailers.css,ExtendAdjustReplan.css,expandablemenu.css,homepage.css,nifty.css,JourneyFares.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="JourneyFares" method="post" runat="server">
	        <div class="spotlighttag">
                <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Train input summary page. -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/JourneyPlanning/JourneySummary.aspx?cacheparam=3 -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:06/04/09 -->
                <script type="text/javascript" id="DoubleClickFloodlightTag551603">
                //<![CDATA[
                var axel = Math.random()+"";
                var a = axel * 10000000000000;
                var newFrame=document.createElement('iframe');
                newFrame.src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=train749;ord=1;num="+ a + "?";
                newFrame.width="1";
                newFrame.frameBorder="0";
                newFrame.height="1";
                var scriptNode=document.getElementById('DoubleClickFloodlightTag551603');
                scriptNode.parentNode.insertBefore(newFrame,scriptNode);
                //]]>
                </script>
                <noscript>
                <iframe src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=train749;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
		    </div>
			<a href="#MainContent">
				<asp:image id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a>
			<a href="#JourneyButtons">
				<asp:image id="imageJourneyButtonsSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a>
			<asp:panel id="panelOutwardJourneyPTSkipLink1" runat="server" visible="False">
				<a href="#OutwardDetails">
					<asp:image id="imageOutwardJourneyPTSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<asp:panel id="panelFindTransportToStartLocationSkipLink" runat="server" visible="False">
				<a href="#FindTransportToStartButton">
					<asp:image id="imageFindTransportToStartLocationSkipLink" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<asp:panel id="panelFindTransportFromEndLocationSkipLink" runat="server" visible="False">
				<a href="#FindTransportFromEndButton">
					<asp:image id="imageFindTransportFromEndLocationSkipLink" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<asp:panel id="panelReturnJourneyPTSkipLink1" runat="server" visible="False">
				<a href="#ReturnDetails">
					<asp:image id="imageReturnJourneyPTSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<!-- Homepage Outline Table -->
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:expandablemenucontrol>
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
                                                    <a name="ChangeJourney"></a>
                                                    <div id="boxjourneychangesearchcontrol">
			                                            <uc1:journeychangesearchcontrol id="theJourneyChangeSearchControl" runat="server"  HelpLabel="helpJourneyFaresLabelControl"></uc1:journeychangesearchcontrol>
		                                            </div>
                                                    <div>
		                                                <uc1:journeyssearchedforcontrol id="journeysSearchedControl" runat="server"></uc1:journeyssearchedforcontrol>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <uc1:journeyplanneroutputtitlecontrol id="JourneyPlannerOutputTitleControl1" runat="server"></uc1:journeyplanneroutputtitlecontrol>          
                                                        
                                                        <cc1:HelpLabelControl ID="helpJourneyFaresLabelControl" CssMainTemplate="helpboxinfaretable" Visible="False" runat="server"></cc1:HelpLabelControl>
                                                        
                                                        <table cellspacing="0" cellpadding="0" width="828">
				                                            <tr>
					                                            <td valign="bottom" align="right">
					                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td valign="bottom" align="right">
					                                                            <uc1:outputnavigationcontrol id="theOutputNavigationControl" runat="server"></uc1:outputnavigationcontrol>
					                                                        </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                			
		                                            <a name="outward"></a>
		                                            <asp:panel id="outwardPanel" runat="server">
			                                            <div class="boxtypeseventeen">
				                                            <table id="summaryOutwardTable" runat="server">
					                                            <tr>
						                                            <td width="610">
						                                                <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>    
						                                            </td>
						                                            <td align="right">
						                                                <a class="jptablelink" href="#return">
								                                            <asp:label id="hyperlinkReturnJourneys" runat="server"></asp:label>
								                                            <asp:image id="hyperlinkImageReturnJourneys" runat="server" AlternateText="Link to Return Journeys" ></asp:image></a></td>
					                                            </tr>
				                                            </table>
                                        					
				                                            <uc1:summaryresulttablecontrol id="summaryResultTableControlOutward" runat="server"></uc1:summaryresulttablecontrol>
				                                            <uc1:findsummaryresultcontrol id="findSummaryResultTableControlOutward" runat="server" visible="true"></uc1:findsummaryresultcontrol>
				                                        </div>
                                					
			                                            <a name="OutwardDetails"></a>
			                                            <uc1:journeyfarescontrol id="OutboundJourneyFaresControl" runat="server"></uc1:journeyfarescontrol>
			                                            <uc1:carjourneycostscontrol id="outwardCarJourneyCostsControl" runat="server"></uc1:carjourneycostscontrol>
		                                            </asp:panel>

		                                            <asp:panel id="panelJourneyButtonsSkipLink2" runat="server" visible="False">
			                                            <a href="#JourneyButtons">
				                                            <asp:image id="imageJourneyButtonsSkipLink2" runat="server" cssclass="skiptolinks"></asp:image></a>
		                                            </asp:panel>
		                                    
		                                            <a name="return"></a>
		                                            <asp:panel id="returnPanel" runat="server">
			                                            <asp:table id="faresReturnTable" runat="server">
				                                            <asp:tablerow>
					                                            <asp:tablecell id="cellButtonSummary">
					                                                <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
					                                            </asp:tablecell>
					                                            <asp:tablecell horizontalalign="Right">
						                                            <a class="jptablelink" href="#outward">
							                                            <asp:label id="hyperlinkOutwardJourneys" runat="server"></asp:label></a><a class="jptablelink" href="#outward">
							                                            <asp:image id="hyperlinkImageOutwardJourneys" runat="server" AlternateText="Link to Outward Journeys" ></asp:image></a>
					                                            </asp:tablecell>
				                                            </asp:tablerow>
			                                            </asp:table>
			                                            <uc1:summaryresulttablecontrol id="summaryResultTableControlReturn" runat="server"></uc1:summaryresulttablecontrol>
			                                            <uc1:findsummaryresultcontrol id="findSummaryResultTableControlReturn" runat="server" visible="true"></uc1:findsummaryresultcontrol>
		                                            </asp:panel>
		                                            <a name="ReturnDetails"></a>
		                                            <uc1:journeyfarescontrol id="ReturnJourneyFaresControl" runat="server"></uc1:journeyfarescontrol>
		                                            <uc1:carjourneycostscontrol id="returnCarJourneyCostsControl" runat="server"></uc1:carjourneycostscontrol>
		                                            <a name="discounts"></a><a name="amend"></a>

                                                    <div class="boxtypeeightstd">
			                                            <table id="jfooter" cellspacing="0">
				                                            <tr>
					                                            <td></td>
					                                            <td id="mprinter">
						                                            <cc1:tdbutton id="buttonRetailers" runat="server"></cc1:tdbutton>&nbsp;
					                                            </td>
				                                            </tr>
			                                            </table>
		                                            </div>
		                                            
		                                            <uc1:resultsfootnotescontrol runat="server" id="footnotesControl"></uc1:resultsfootnotescontrol>
		                                            <br />
		                                            <a href="#JourneyButtons">
			                                            <asp:image id="imageJourneyButtonsSkipLink3" runat="server" cssclass="skiptolinks"></asp:image></a>
		                                            <uc1:amendsavesendcontrol id="AmendSaveSendControl" runat="server"></uc1:amendsavesendcontrol>
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
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>
