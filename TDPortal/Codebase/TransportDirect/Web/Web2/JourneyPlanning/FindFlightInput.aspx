<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnalyticsControl" Src="../Controls/AnalyticsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="../Controls/CalendarControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AirportDisplayControl" Src="../Controls/AirportDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Page Language="c#" Codebehind="FindFlightInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindFlightInput" %>

<%@ Register TagPrefix="uc1" TagName="OperatorSelectionControl" Src="../Controls/OperatorSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AirStopOverControl" Src="../Controls/AirStopOverControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AirportBrowseControl" Src="../Controls/AirportBrowseControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/FindFlightInput.aspx" />
    <meta name="description" content="Find flights, plan air travel within England, Scotland and Wales with Transport Direct." />
    <meta name="keywords" content="flight route, plane journey planner. flight planner, flight times, flight schedules, air schedules, airport map" />
    <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/softcontent/en/finda_plane.gif" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,CalendarSS.css, expandablemenu.css, nifty.css,FindFlightInput.aspx.css">
    </cc1:HeadElementControl>
    <uc1:AnalyticsControl id="analyticsControl" runat="server"></uc1:AnalyticsControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindAFlight" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindFlightInput" method="post" runat="server">
            <div class="spotlighttag">
                <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Transport Direct - Find a Flight -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/JourneyPlanning/FindFlightInput.aspx?repeatingloop=Y -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:1/8/2010 -->
                <script type="text/javascript" id="DoubleClickFloodlightTag631075">
                    //<![CDATA[
                    var axel = Math.random() + "";
                    var a = axel * 10000000000000;
                    document.write('<iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=flight01;ord=1;num=' + a + '?" width="1" height="1" frameborder="0"></iframe>');
                    //]]>
                </script>
                <noscript>
                <iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=flight01;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
            </div>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
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
                            <table lang="en" cellspacing="0" border="0" cellpadding="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:TDButton ID="commandBack" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="FindAFlight"></a>
                                                        <cc1:TDImage ID="imageFindFlight" runat="server" Width="62" Height="36"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelFindFlightTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <table id="FlightMainTable" lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <uc1:AirportBrowseControl ID="airportBrowseFrom" runat="server"></uc1:AirportBrowseControl>
                                                                    <uc1:AirportDisplayControl ID="airportDisplayFrom" runat="server"></uc1:AirportDisplayControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td class="findafromcolumn">
                                                                    &nbsp;</td>
                                                                <td width="1">
                                                                    <asp:Label ID="labelSRcommandDirectFlightsOnly" associatedcontrolid="commandDirectFlightsOnly" runat="server" CssClass="screenreader"></asp:Label><cc1:TDImageButton
                                                                        ID="commandDirectFlightsOnly" runat="server"></cc1:TDImageButton></td>
                                                                <td>
                                                                    <asp:Label ID="labelFindDirectFlightsTitle" runat="server" CssClass="txtseven">[Find direct flights only]</asp:Label>
                                                                    <asp:Label ID="labelFindDirectFlightsFixedAll" runat="server" CssClass="txtsevenb"
                                                                        Visible="False"></asp:Label>
                                                                    <asp:Label ID="labelFindDirectFlightsFixedDirect" runat="server" CssClass="txtsevenb"
                                                                        Visible="False"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <uc1:AirportBrowseControl ID="airportBrowseTo" runat="server"></uc1:AirportBrowseControl>
                                                                    <uc1:AirportDisplayControl ID="airportDisplayTo" runat="server"></uc1:AirportDisplayControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <uc1:FindLeaveReturnDatesControl ID="dateControl" runat="server"></uc1:FindLeaveReturnDatesControl>
                                                        <uc1:FindPageOptionsControl ID="pageOptionsControl" runat="server"></uc1:FindPageOptionsControl>
                                                    </div>
                                                    
                                                    <!-- Advanced options -->
                                                    <asp:Panel ID="hidePreferencesBar" runat="server" Visible="True">
                                                        <div class="boxtypesixalt">
                                                            <table>
                                                                <tr>
                                                                    <td id="jpthdl" align="left">
                                                                        <asp:Label ID="labelAirTravelPreferencesHeader" runat="server"></asp:Label>&nbsp;&nbsp;
                                                                    </td>
                                                                    <td align="left">
                                                                        <span id="jpt">
                                                                            <uc1:TravelDetailsControl ID="loginSaveOption" runat="server"></uc1:TravelDetailsControl>
                                                                        </span>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelAirTravelPreferences" runat="server" Visible="False">
                                                        <asp:Panel ID="panelOperatorSelection" runat="server" Visible="False">
                                                            <div class="boxtypetwo">
                                                                <uc1:OperatorSelectionControl ID="airOperatorSelection" runat="server"></uc1:OperatorSelectionControl>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="panelCheckInTime" runat="server" Visible="False">
                                                            <div class="boxtypetwo">
                                                                <table lang="en" cellspacing="0" summary="Check in time options" border="0">
                                                                    <tr>
                                                                        <td width="100">
                                                                            <asp:Label ID="labelCheckInTimeTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="labelCheckInTimeExplanation" runat="server" CssClass="txtseven"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="labelCheckInTimeDropTitle" runat="server" CssClass="txtseven"></asp:Label>
                                                                            <asp:Label ID="labelSRdropCheckInTime" runat="server" CssClass="screenreader"></asp:Label>
                                                                            <asp:DropDownList ID="dropCheckInTime" runat="server"></asp:DropDownList>
                                                                            <asp:Label ID="labelCheckInTimeFixed" runat="server" CssClass="txtsevenb" Visible="False"></asp:Label>&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="labelCheckInTimeNote" runat="server" CssClass="txtnote"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <uc1:FindPageOptionsControl ID="pageOptionsControlsInPanelCheckInTime" runat="server"></uc1:FindPageOptionsControl>
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="panelFlyVia" runat="server">
                                                            <div class="boxtypetwo">
                                                                <uc1:AirStopOverControl ID="findFlightInputStopover" runat="server"></uc1:AirStopOverControl>
                                                                <uc1:FindPageOptionsControl ID="pageOptionsControlsBottom" runat="server"></uc1:FindPageOptionsControl>
                                                            </div>
                                                        </asp:Panel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server" CssClass="SoftContentPanel" ScrollBars="None"></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
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
