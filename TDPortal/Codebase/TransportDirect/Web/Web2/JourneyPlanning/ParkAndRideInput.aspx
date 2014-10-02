<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarPreferencesControl" Src="../Controls/FindCarPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<%@ Page language="c#" Codebehind="ParkAndRideInput.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.ParkAndRideInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>"  xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/ParkAndRideInput.aspx" />
	<meta name="description" content="Car route planner for park &amp; ride schemes across England, Scotland and Wales.  Brought to you by Transport Direct." />
    <meta name="keywords" content="directions to park and ride, park and ride locations, park and ride schemes, park and ride costs" />
	<link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/softcontent/parknride3_1.gif" />
    <cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="Homepage.css,setup.css,jpstd.css,ExpandableMenu.css,CalendarSS.css, nifty.css,map.css,ParkAndRideInput.aspx.css"></cc1:headelementcontrol>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="ParkAndRideInput" method="post" runat="server">
		    <div class="spotlighttag">
		        <!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
                <!-- Activity Name for this tag is:Transport Direct - Plan to Park and Ride -->
                <!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/JourneyPlanning/ParkAndRideInput.aspx -->
                <!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
                <!-- Creation Date:1/8/2010 -->
                <script type="text/javascript" id="DoubleClickFloodlightTag631085">
                //<![CDATA[
                var axel = Math.random()+"";
                var a = axel * 10000000000000;
                var newFrame=document.createElement('iframe');
                newFrame.src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=pkride01;ord=1;num="+ a + "?";
                newFrame.width="1";
                newFrame.frameBorder="0";
                newFrame.height="1";
                var scriptNode=document.getElementById('DoubleClickFloodlightTag631085');
                scriptNode.parentNode.insertBefore(newFrame,scriptNode);
                //]]>
                </script>
                <noscript>
                <iframe src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=pkride01;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
                </noscript>
                <!-- End of DoubleClick Spotlight Tag: Please do not remove-->
            </div> 
		    <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
			<uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl id="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">                                    
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <a name="BackButton"></a>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop" >
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
                                                        <cc1:TDImage ID="imageFindPage" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label>
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
                                                <asp:Label ID="labelFromToTitle" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo">
								                        <uc1:findtofromlocationscontrol id="locationsControl" runat="server"></uc1:findtofromlocationscontrol><uc1:findleavereturndatescontrol id="dateControl" runat="server"></uc1:findleavereturndatescontrol>
							                            <uc1:FindPageOptionsControl ID="pageOptionsControltop" runat="server"></uc1:FindPageOptionsControl>
                                                    </div>
                                                    <div class="mcMapInputBox">
                                                        <!-- Map -->
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </div>
                                                    <uc1:findcarpreferencescontrol id="preferencesControl" runat="server"></uc1:findcarpreferencescontrol>
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
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
