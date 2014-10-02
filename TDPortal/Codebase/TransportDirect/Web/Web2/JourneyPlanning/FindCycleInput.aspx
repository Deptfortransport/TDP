<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FindCycleInput.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.FindCycleInput" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="FavouriteLoadOptionsControl" Src="../Controls/FavouriteLoadOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="../Controls/LocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCycleJourneyTypeControl" Src="../Controls/FindCycleJourneyTypeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCyclePreferencesControl" Src="../Controls/FindCyclePreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/FindCycleInput.aspx" />
    <meta name="description" content="Plan your bike journey with the cycle route planner from Transport Direct. Discover cycle paths and local bike routes today." />
    <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/journeyplanning/cyclejourney.gif" />
    <cc1:HeadElementControl id="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,nifty.css,ExpandableMenu.css,CalendarSS.css,map.css,FindCycleInput.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindCycle" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindCycleInput" runat="server">
            <div class="spotlighttag">
                <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Find a cycle route -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/JourneyPlanning/FindCycleInput.aspx -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:1/22/2010 -->
                <script type="text/javaScript" id="DoubleClickFloodlightTag546465">
                    var axel = Math.random() + "";
                    var a = axel * 10000000000000;
                    document.write('<iframe title="Not user content" style="display:none" src="http://fls.doubleclick.net/activityi;src=1501791;type=trans304;cat=finda245;ord=1;num=' + a + '?" width="1" height="1" frameborder="0"></iframe>');
                </script>
                <noscript>
                    <iframe title="Not user content" style="display:none" src="http://fls.doubleclick.net/activityi;src=1501791;type=trans304;cat=finda245;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
            </div>
            <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <a href="#InputForm">
                <cc1:TDImage ID="imageInputFormSkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage>
            </a>
            <uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>          
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl id="clientLink" runat="server" enableviewstate="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl id="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" cssclass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop" >
                                                        <cc1:TDButton id="commandBack" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>                                      
                                                <td align="right">
                                                    <cc1:HelpButtonControl id="Helpbuttoncontrol1" runat="server" enableviewstate="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="FindCycle"></a>
                                                        <cc1:TDImage id="imageFindACycle" runat="server"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label id="labelFindPageTitle" runat="server" enableviewstate="false"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False" enableviewstate="false">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelSubHeading" runat="server" enableviewstate="false">
                                            <div class="boxtypeeightalt">
                                                <asp:Label id="labelFromToTitle" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelErrorMessage" runat="server" visible="False" enableviewstate="false">
                                            <div id="boxtypeeightalt">
                                                <asp:Label id="labelErrorMessages" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <a id="InputForm"></a>
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div>
                                                        <uc1:FavouriteLoadOptionsControl ID="favouriteLoadOptions" runat="server"></uc1:FavouriteLoadOptionsControl>
                                                    </div>
                                                    <div class="boxtypetwo">
                                                        <table cellspacing="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="findafromcolumn" align="right">
                                                                                <asp:Label ID="labelOriginTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <uc1:LocationControl ID="originLocationControl" runat="server"></uc1:LocationControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td class="findafromcolumn" align="right">
                                                                                <asp:Label ID="labelDestinationTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <uc1:LocationControl ID="destinationLocationControl" runat="server"></uc1:LocationControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        
                                                        <uc1:FindLeaveReturnDatesControl id="dateControl" runat="server"></uc1:FindLeaveReturnDatesControl>
                                                        <uc1:FindCycleJourneyTypeControl id="cycleJourneyTypeControl" runat="server"></uc1:FindCycleJourneyTypeControl>
                                                        <uc1:FindPageOptionsControl id="pageOptionsControltop" runat="server"></uc1:FindPageOptionsControl>
                                                    </div>
                                                    <div class="mcMapInputBox">
                                                        <!-- Map -->
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </div>
                                                    <uc1:FindCyclePreferencesControl id="preferencesControl" runat="server"></uc1:FindCyclePreferencesControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel id="TDPageInformationHtmlPlaceHolderDefinition" runat="server" cssclass="SoftContentPanel" ScrollBars="None" enableviewstate="false"></asp:Panel>
									            </td>
                                            </tr>
                                        </table>                                    
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
                                        <uc1:PoweredBy id="PoweredByControl" runat="server" enableviewstate="False"></uc1:PoweredBy>
                                        <asp:Panel id="TDInformationHtmlPlaceholderDefinition" runat="server" enableviewstate="false"></asp:Panel>
                                        <asp:Panel ID="TDFindCyclePromoHtmlPlaceholderDefinition" runat="server" enableviewstate="false"></asp:Panel>
                                        <asp:Panel ID="TDAdditionalInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>

                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl id="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
