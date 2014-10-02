<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JourneyOverview.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyOverview" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindOverviewResultControl" Src="../Controls/FindOverviewResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,JourneyOverview.aspx.css"></cc1:headelementcontrol>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
		<form id="JourneyOverview" method="post" runat="server">
			<div class="spotlighttag">
				<!-- Start of DoubleClick Spotlight Tag: Please do not remove--> <!-- Activity Name for this tag is:Results (Journey Overview) -->  <!-- Web site URL where tag should be placed: http://www.transportdirect.info/TransportDirect/en/journeyplanning/journeyoverview.aspx -->  <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->  <!-- Creation Date:7/25/2005 -->
				<script  type="text/javascript" language="javascript">
				    //<![CDATA[
					var axel = Math.random()+"";
					var a = axel * 10000000000000;
					document.write('<img src="http://ad.uk.doubleclick.net/activity;src=988398;type=trans425;cat=resul380;ord=1;num='+ a + '?" width="1" height="1" border="0" alt=" ">');
					//]]>
				</script>
				<noscript>
					<img height="1" alt=" " src="http://ad.uk.doubleclick.net/activity;src=988398;type=trans425;cat=resul380;ord=1;num=1?"
						width="1" border="0" />
				</noscript> <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
			</div>
			<div class="spotlighttag">
				<!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
				<!-- Activity Name for this tag is:Transport Direct Journey Overview -->	
				<!-- Web site URL where tag should be placed: http://www.transportdirect.info/TransportDirect/en/JourneyPlanning/JourneyOverview.aspx?cacheparam=6 -->
				<!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
				<!-- Creation Date:16/01/2008 -->
				<script type="text/javascript" language="JavaScript">
				    //<![CDATA[
					var axel = Math.random()+"";
					var a = axel * 10000000000000;
					document.write('<img src="http://ad.uk.doubleclick.net/activity;src=1501791;type=trans203;cat=trans893;ord=1;num='+ a + '?" width="1" height="1" border="0" alt=" ">');
					//]]>
				</script>
				<noscript>
					<img src="http://ad.uk.doubleclick.net/activity;src=1501791;type=trans203;cat=trans893;ord=1;num=1?" width="1" height="1" border="0" alt=" " />
				</noscript>
				<!-- End of DoubleClick Spotlight Tag: Please do not remove-->
			</div>
			<a href="#MainContent">
				<asp:image id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a>
			<a href="#JourneyButtons">
				<asp:image id="imageJourneyButtonsSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a>
			<asp:panel id="panelOutwardJourneysSkipLink1" runat="server" visible="False">
				<a href="#OutwardJourneys">
					<asp:image id="imageOutwardJourneysSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<asp:panel id="panelReturnJourneysSkipLink1" runat="server" visible="False">
				<a href="#ReturnJourneys">
					<asp:image id="imageReturnJourneysSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>

        <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
            <tr valign="top">
                <!-- Left Hand Navigaion Bar -->
                <td class="LeftHandNavigationBar" valign="top">
                    <uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False"
                        CategoryCssClass="HomePageMenu">
                    </uc1:expandablemenucontrol>
                
                    <div class="HomepageBookmark">
                        <uc1:clientlinkcontrol id="clientLink" runat="server" EnableViewState="False" Visible="false">
                        </uc1:clientlinkcontrol></div>
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
                                    <a name="ChangeJourney"></a>
                                    <!-- Journey Planning Controls -->
                                    <asp:Panel ID="panelSubHeading" runat="server">
                                        <div class="boxtypeeightalt">
                                            <asp:Label ID="labelFromToTitle" runat="server" CssClass="txtseven"></asp:Label></div>
                                    </asp:Panel>
                                    <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                        <div id="boxtypeeightalt">
                                            <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label></div>
                                    </asp:Panel>
                                    <table lang="en" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <% /* Start: Content to be replaced when white labelling */ %>
                                                
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:journeychangesearchcontrol id="theJourneyChangeSearchControl" runat="server" helplabel="journeySummaryPageHelpLabel"></uc1:journeychangesearchcontrol>
                                                    </div>
			                                        <div>
			                                            <uc1:journeyssearchedforcontrol id="theJourneysSearchedForControl1" runat="server"></uc1:journeyssearchedforcontrol>
			                                        </div>
			                                        <div class="boxtypesixteen">
			                                            <asp:panel id="errorMessagePanel" runat="server" visible="False">
				                                            <div class="boxtypeerrormsgtwo">
					                                            <uc1:errordisplaycontrol id="errorDisplayControl" runat="server" visible="False"></uc1:errordisplaycontrol></div>
                                                        </asp:panel>
			                                        
				                                        <uc1:journeyplanneroutputtitlecontrol id="JourneyPlannerOutputTitleControl1" runat="server"></uc1:journeyplanneroutputtitlecontrol>
				                                        <div id="panelInstructions" style="MARGIN-BOTTOM: 5px" runat="server">
				                                            <asp:label id="labelInstructions" runat="server" cssclass="txtseven"></asp:label></div>				    
			                                        </div>
			                                        <asp:panel id="outwardPanel" runat="server">
				                                        <a name="OutwardJourneys"></a>
				                                        <div class="boxtypejourneyoverviewcontrol">
				                                            <asp:label id="labelJourneyOptionsTableDescription" runat="server" cssclass="screenreader"></asp:label>
					                                        <table id="overviewOutwardTable" runat="server" class="jpsumoutfinda">
						                                        <tr>
							                                        <td>
								                                        <uc1:resultstabletitlecontrol id="resultsTableTitleControlOutward" runat="server"></uc1:resultstabletitlecontrol></td>
						                                        </tr>
					                                        </table>
					                                        <uc1:findoverviewresultcontrol id="findOverviewResultTableControlOutward" runat="server" visible="true"></uc1:findoverviewresultcontrol></div>				
			                                        </asp:panel>
			                                        <asp:panel id="returnPanel" runat="server">
			                                            <a name="ReturnJourneys"></a>
			                                            <div class="boxtypejourneyoverviewcontrol">
				                                            <asp:table id="overviewReturnTable" runat="server" cssclass="jpsumoutfinda">
					                                            <asp:tablerow>
						                                            <asp:tablecell id="cellButtonSummary">
							                                            <uc1:resultstabletitlecontrol id="resultsTableTitleControlReturn" runat="server"></uc1:resultstabletitlecontrol>
						                                            </asp:tablecell>
					                                            </asp:tablerow>
				                                            </asp:table>
				                                        <uc1:findoverviewresultcontrol id="findOverviewResultTableControlReturn" runat="server" visible="true"></uc1:findoverviewresultcontrol>
				                                        </div>
			                                        </asp:panel>
			                                        <div class="boxtypesixteen" style="MARGIN-BOTTOM: 5px">
			                                            <asp:Label id="findOverviewResultsTableNotes" runat="server" cssclass="txtseven"></asp:Label>
			                                        </div>
			                                        <uc1:resultsfootnotescontrol id="footnotesControl" runat="server"></uc1:resultsfootnotescontrol>
			                                        <br />
			                                        <a name="amend"></a>
			                                        <uc1:amendsavesendcontrol id="amendSaveSendControl" runat="server"></uc1:amendsavesendcontrol>
			                                        <br />
                                                <% /* End: Content to be replaced when white labelling */ %>
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
