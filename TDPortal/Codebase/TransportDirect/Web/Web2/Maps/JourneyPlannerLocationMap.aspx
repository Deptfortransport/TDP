<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="../Controls/TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationControl" Src="../Controls/MapLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapZoomControl" Src="../Controls/MapZoomControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapControl" Src="../Controls/MapControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapLocationSelectControl" Src="../Controls/MapLocationIconsSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping"
    Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="MapDisabledControl" Src="../Controls/MapDisabledControl.ascx" %>

<%@ Page Language="c#" Codebehind="JourneyPlannerLocationMap.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyPlannerLocationMap" %>

<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/Maps/JourneyPlannerLocationMap.aspx" />
    <meta name="description" content="Get local street maps in Great Britain by entering a postcode, address, facility or town/city into Transport Direct's online street map finder. " />
    <meta name="keywords" content="Street Maps, location maps, town map, city maps, street finder, place finder" />
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css, JourneyPlannerLocationMap.aspx.css">
    </cc2:HeadElementControl>
</head>
<body>
<!-- Start of DoubleClick Spotlight Tag: Please do not remove-->
<!-- Activity Name for this tag is:Transport Direct Journey Planner - Maps -->
<!-- Web site URL where tag should be placed: http://www.transportdirect.info/Web2/Maps/JourneyPlannerLocationMap.aspx -->
<!-- This tag must be placed within the opening <body> tag, as close to the beginning of it as possible-->
<!-- Creation Date:1/8/2010 -->
<script type="text/javascript" id="DoubleClickFloodlightTag631079">
//<![CDATA[
var axel = Math.random()+"";
var a = axel * 10000000000000;
var newFrame=document.createElement('iframe');
newFrame.src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=jpmaps01;ord=1;num="+ a + "?";
newFrame.width="1";
newFrame.frameBorder="0";
newFrame.height="1";
var scriptNode=document.getElementById('DoubleClickFloodlightTag631079');
scriptNode.parentNode.insertBefore(newFrame,scriptNode);
//]]>
</script>
<noscript>
<iframe src="http://fls.uk.doubleclick.net/activityi;src=1501791;type=trans304;cat=jpmaps01;ord=1;num=1?" width="1" height="1" frameborder="0"></iframe>
</noscript>
<!-- End of DoubleClick Spotlight Tag: Please do not remove-->
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyPlannerLocationMap" method="post" runat="server">
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
                        <a name="SkipToMain"></a>
                        <cc2:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0" cellpadding="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" width="100%" border="0" cellpadding="0">
                                            <tr id="findaMapRow" runat="server">
                                                <td valign="top">
                                                    <table cellspacing="0" width="100%" cellpadding="0">
                                                        <tr>
                                                            <td valign="top">
                                                                <table lang="en" cellspacing="0" width="617" border="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop" Wrap="False">
                                                                                <cc2:TDButton ID="commandBack" runat="server" EnableViewState="false" Visible="false"></cc2:TDButton>
                                                                            </asp:Panel>
                                                                        </td>
                                                                        <td align="right">
                                                                            <cc2:HelpCustomControl ID="HelpControlLocation" runat="server" HelpLabel="helpLabelLocations"></cc2:HelpCustomControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <div class="toptitlediv">
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <cc2:TDImage ID="imageFindAMap" runat="server" Width="50" Height="40"></cc2:TDImage>
                                                                                        </td>
                                                                                        <td>
                                                                                            <h1>
                                                                                                <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label>
                                                                                            </h1>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                                                                <div class="boxtypeerrormsgfour">
                                                                                    <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                                                </div>
                                                                            </asp:Panel>
                                                                            <div class="boxtypeeightalt">
                                                                                <asp:Label ID="labelFindMapMessage" runat="server" CssClass="txtseven"></asp:Label>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <asp:Panel ID="panelSubHeading" runat="server">
                                                                                <div class="boxtypeeightalt">
                                                                                    <asp:Label ID="labelFromToTitle" runat="server" CssClass="txtseven"></asp:Label></div>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                                                                <div id="boxtypeeightalt">
                                                                                    <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label></div>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <cc2:HelpLabelControl ID="helpLabelLocations" runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl>
                                                                             <cc2:HelpLabelControl ID="helpLabelLocationsAmbig" runat="server" Visible="False"
                                                        CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <div id="mboxtypetwo">
                                                                                <asp:Panel ID="panelLocationSelect" runat="server">
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <uc1:TriStateLocationControl2 ID="triStateLocationControl1" runat="server"></uc1:TriStateLocationControl2>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                    <table width="100%">
                                                                                        <tr>
                                                                                            <td align="left">
                                                                                                <cc2:TDButton ID="previousLocationButton" runat="server"></cc2:TDButton></td>
                                                                                            <td align="right">
                                                                                                <cc2:TDButton ID="resolveLocationButton" runat="server" Text="[Dummy]"></cc2:TDButton></td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center">
                                                                            <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server" CssClass="SoftContentPanel"
                                                                                ScrollBars="None">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <!-- White Space Column -->
                                                            <td class="WhiteSpaceBetweenColumns">
                                                            </td>
                                                            <!-- Information Column -->
                                                            <td class="HomepageMainLayoutColumn4" valign="top">
                                                                <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                                                <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server">
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="findaMapResults" runat="server" visible="false">
                                                <td>
                                                    <div class="boxtypeeightstd">
                                                        <table lang="en" summary="Page controls" width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td id="mlabel">
                                                                    <cc2:TDButton ID="buttonTopBack" runat="server"></cc2:TDButton>
                                                                    <asp:Literal ID="literalSpaceBeforeLocation" runat="server" Visible="False" Text="&nbsp;"></asp:Literal>
                                                                    <cc2:TDButton ID="newSearchButton" runat="server" />
                                                                    <asp:Literal ID="literalSpaceAfterLocation" runat="server" Visible="False" Text="&nbsp;"></asp:Literal>
                                                                    <cc2:TDButton ID="buttonTopNext" runat="server"></cc2:TDButton>
                                                                </td>
                                                                <td id="mprinter">
                                                                    <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server">
                                                                    </uc1:PrinterFriendlyPageButtonControl>
                                                                    <cc2:HelpCustomControl ID="HelpControlLocation2" runat="server" HelpLabel="helpLabelMapIcons"></cc2:HelpCustomControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="boxtypeeightstd">
                                                        <h2 style="display: inline">
                                                            <asp:Label ID="labelMap" runat="server" CssClass="txttenb"></asp:Label>
                                                        </h2>
                                                        <h2>
                                                            <asp:Label ID="labelSelectedLocation" runat="server"></asp:Label></h2>
                                                    </div>
                                                    <cc2:HelpLabelControl ID="helpLabelMapTools" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelMapToolsStart" runat="server" Visible="False"
                                                        CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelMapToolsDestination" runat="server" Visible="False"
                                                        CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelMapToolsVia" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelMapIcons" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                   <cc2:HelpLabelControl ID="helpLabelStartLocation"
                                                            runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl><cc2:HelpLabelControl
                                                                ID="helpLabelStartAmbiguity" runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelDestination" runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl><cc2:HelpLabelControl
                                                        ID="helpLabelVia" runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl>
                                                    <cc2:HelpLabelControl ID="helpLabelDestinationAmbiguity" runat="server" Visible="False"
                                                        CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl><cc2:HelpLabelControl ID="helpLabelViaAmbiguity"
                                                            runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc2:HelpLabelControl>
                                                    <div class="boxtypeeightstd">
                                                        <strong>
                                                            <asp:Label ID="labelHdrSetLocation" runat="server" Visible="False" CssClass="txtseven"></asp:Label></strong><asp:Label
                                                                ID="labelSetLocation" runat="server" Visible="False" CssClass="txtseven"></asp:Label></div>
                                                    <div class="boxtypeeightstd">
                                                        <strong>
                                                            <asp:Label ID="labelInstructions" runat="server" Visible="False" CssClass="txtseven"></asp:Label></strong></div>
                                                    <table lang="en" id="mapcontrol" cellspacing="0" cellpadding="0" summary="Map">
                                                        <tr>
                                                            <td valign="top">
                                                                <div id="mboxtypetwonoborder">
                                                                    <asp:Panel ID="panelMapTools" runat="server">
                                                                        <table lang="en" style="margin: 0px" cellspacing="0" cellpadding="0" width="100%"
                                                                            summary="Map controls" border="0">
                                                                            <tr valign="top">
                                                                                <td>
                                                                                    <uc1:MapLocationControl ID="mapLocationControl" runat="server"></uc1:MapLocationControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>
                                                                <uc1:MapControl ID="theMapControl" runat="server"></uc1:MapControl>
                                                                <uc1:MapDisabledControl ID="disabledMapControl" runat="server"></uc1:MapDisabledControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="mapnav">
                                                                <asp:Panel ID="panelMapLocationSelect" Height="270px" runat="server">
                                                                    <div class="MapLocationSelect">
                                                                        <div id="mapOverviewBox">
                                                                            <div id="mhd">
                                                                                <asp:Label ID="labelOverviewMap" runat="server"></asp:Label></div>
                                                                            <div id="msmlm">
                                                                                <asp:Image ID="imageSummaryMap" runat="server"></asp:Image></div>
                                                                        </div>
                                                                        <div id="findamapbox">
                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <div id="mheSymbols">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <h2><asp:Label ID="labelMapSymbols" runat="server"></asp:Label></h2>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>    
                                                                                        </div>
                                                                                    </td>
                                                                                    <td>
                                                                                        <div class="mapSymbolsDisclaimer">
                                                                                            <asp:Label ID="labelMapSymbolsDisclaimer" runat="server"></asp:Label>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <uc1:MapLocationSelectControl ID="mapLocationIconsSelectControl" runat="server"></uc1:MapLocationSelectControl>
                                                                        </div>
                                                                    </div>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
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
