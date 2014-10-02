<%@ Page Language="c#" Codebehind="JourneyPlannerInput.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyPlannerInput" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="FavouriteLoadOptionsControl" Src="../Controls/FavouriteLoadOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="../Controls/LocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="AccessibleOptionsControl" Src="../Controls/AccessibleOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DTransportTypesControl" Src="../Controls/D2DTransportTypesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DCarPreferencesControl" Src="../Controls/D2DCarPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DPTPreferencesControl" Src="../Controls/D2DPTPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DPageOptionsControl" Src="../Controls/D2DPageOptionsControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/JourneyPlannerInput.aspx" />
    <meta name="description" content="Plan your journey by public transport or car with the door to door route planner from Transport Direct." />
    <meta name="keywords" content="route planner, route planner UK, journey planner, public transport planner, route planner England, car route, car planner, timetable, train timetable, bus routes"/>
        <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeJourneyPlannerBlueBackground.gif" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,Homepage.css,expandablemenu.css,nifty.css,map.css,JourneyPlannerInput.aspx.css,jquery.qtip.min.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#DoorToDoor" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyPlannerInput" method="post" runat="server">
            <div class="spotlighttag">
                <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Transport Direct Journey Planner -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/web2/JourneyPlanning/JourneyPlannerInput.aspx -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:05/22/09 -->
                <script type="text/javascript" id="DoubleClickFloodlightTag543047">
                //<![CDATA[
                var axel = Math.random()+"";
                var a = axel * 10000000000000;
                document.write('<iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=trans398;ord=1;num=' + a + '?" width="1" height="1" frameborder="0"></iframe>');
                //]]>
                </script>
                <noscript>
                <iframe title="Not user content" style="display:none" src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=trans398;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
            </div>
            <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <!-- header -->
            <div class="floatrightonly">
                <asp:Panel ID="panelPTJourneyDetailsSkipLink1" runat="server" Visible="False">
                    <a href="#PTJourneyDetails"><asp:Image ID="imagePTJourneyDetailsSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
                </asp:Panel>
                <asp:Panel ID="panelCarJourneyDetailsSkipLink1" runat="server" Visible="False">
                    <a href="#CarJourneyDetails"><asp:Image ID="imageCarJourneyDetailsSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
                </asp:Panel>
            </div>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False" CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl id="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF" Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop"></asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False"></cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="DoorToDoor"></a>
                                                        <cc1:TDImage ID="imageJourneyPlanner" runat="server" Width="50" Height="65"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelJourneyPlannerTitle" runat="server"></asp:Label>
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
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelFavouriteLoadOptions" runat="server">
                                                        <uc1:FavouriteLoadOptionsControl ID="FavouriteLoadOptions" runat="server"></uc1:FavouriteLoadOptionsControl>
                                                    </asp:Panel>
                                                    <div class="boxtypetwo boxOptions1">
                                                        <!-- From and To locations -->
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
                                                        
                                                        <!-- Dates -->
                                                        <uc1:FindLeaveReturnDatesControl ID="dateControl" runat="server"></uc1:FindLeaveReturnDatesControl>
                                                        
                                                        <!-- PT and Car check boxes -->
                                                        <table cellspacing="0" width="100%">
                                                            <tr>
                                                                <td class="findafromcolumn" align="right">
                                                                    <asp:Label ID="labelShow" runat="server" EnableViewState="false" CssClass="txtsevenb"></asp:Label>
                                                                </td>
                                                                <td align="left" valign="bottom">
                                                                    <asp:CheckBox ID="checkBoxPublicTransport" runat="server" Checked="true" CssClass="txtseven"></asp:CheckBox>
                                                                    &nbsp;&nbsp;
                                                                    <asp:CheckBox ID="checkBoxCarRoute" runat="server" Checked="true" CssClass="txtseven"></asp:CheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    
                                                    <!-- Map -->
                                                    <asp:Panel ID="pnlMap" runat="server" CssClass="mcMapInputBox" Visible="false">
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </asp:Panel>
                                                                                                        
                                                    <!-- Advanced options -->
                                                    <asp:Label ID="labelAdvanced" runat="server" CssClass="screenreader"></asp:Label>
                                                    <a name="AccessibleOptions"></a>
                                                    <uc1:AccessibleOptionsControl ID="accessibleOptionsControl" runat="server"></uc1:AccessibleOptionsControl>
                                                    <uc1:D2DTransportTypesControl ID="transportTypesControl" runat="server"></uc1:D2DTransportTypesControl>
                                                    <a name="PTJourneyDetails"></a>
                                                    <uc1:D2DPTPreferencesControl ID="ptPreferencesControl" runat="server"></uc1:D2DPTPreferencesControl>
                                                    <a name="CarJourneyDetails"></a>
                                                    <uc1:D2DCarPreferencesControl ID="carPreferencesControl" runat="server"></uc1:D2DCarPreferencesControl>    
		                                            
		                                            <div class="boxtypetwo boxNavigationOptions">
		                                                <uc1:D2DPageOptionsControl id="pageOptionsControlBottom" runat="server"></uc1:D2DPageOptionsControl>
		                                            </div>
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
