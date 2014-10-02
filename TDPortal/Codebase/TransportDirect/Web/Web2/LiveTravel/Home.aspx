<%@ Page Language="c#" CodeBehind="Home.aspx.cs" AutoEventWireup="True" ValidateRequest="false"
    Inherits="TransportDirect.UserPortal.Web.Templates.LiveTravel.Home" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelNewsHeadlineControl" Src="../Controls/TravelNewsHeadlineControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/LiveTravel/Home.aspx" />
    <meta name="description" content="Live travel news from Transport Direct. Get the latest traffic updates, travel news, incident reports and departure information." />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,LiveTravelHome.aspx.css,nifty.css,expandablemenu.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#TravelNews" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="HomeTravelInfo" method="post" runat="server">
        <a href="#SideNavigation">
            <cc1:TDImage ID="imageSideNavigationSkipLink1" runat="server" class="skiptolinks" /></a>
        <a href="#TravelNews">
            <cc1:TDImage ID="imageTravelNewsSkipLink" runat="server" class="skiptolinks" /></a>
        <a href="#DepartureBoards">
            <cc1:TDImage ID="imageDepartureBoardsSkipLink" runat="server" class="skiptolinks" /></a>
        <!-- header -->
        <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
        <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0">
            <tr>
                <td class="LeftHandNavigationBar" align="left" valign="top">
                    <a name="SideNavigation"></a>
                    <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" CategoryCssClass="HomePageMenu">
                    </uc1:ExpandableMenuControl>
                    <div class="HomepageBookmark">
                        <uc1:ClientLinkControl ID="clientLink" runat="server"></uc1:ClientLinkControl>
                    </div>
                </td>
                <td valign="top">
                    <cc1:RoundedCornerControl ID="bodyArea" CssClass="bodyArea" runat="server" Corners="TopLeft"
                        OuterColour="#CCECFF" InnerColour="White">
                        <table class="HomepageLayoutTable" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="3">
                                                <div id="boxtypeeightstd">
                                                    <h1>
                                                        <asp:Literal ID="literalPageHeading" runat="server" EnableViewState="False"></asp:Literal></h1>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                           <td colspan="3">
                                           
                                           <table cellpadding="0" cellspacing="0" >
                                           <tr>
                                            <td valign="middle" width="50%">
                                                <table cellspacing="10">
                                                        <tr>
                                                        <td>
                                                        <div class="HomepageMainLayoutColumn2">
                                                            <a name="TravelNews"></a>
                                                            <div class="Column2Header" id="TravelNewsHeader" runat="server">
                                                                <h2>
                                                                    <asp:HyperLink CssClass="Column2HeaderLink" ID="hyperLinkLiveTravel" runat="server"
                                                                        EnableViewState="false">
                                                                        <asp:Label CssClass="txtsevenbwl" ID="labelLiveTravel" runat="server" EnableViewState="false"></asp:Label>
                                                                    </asp:HyperLink>
                                                                </h2>
                                                                <asp:HyperLink class="txtsevenbwrlink" ID="hyperLinkLiveTravelMore" runat="server"
                                                                    EnableViewState="false"></asp:HyperLink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </div>
                                                            <div class="clearboth">
                                                            </div>
                                                            <div class="Column2Content1" id="TravelNewsDetail" runat="server">
                                                                <div class="TravelNewsHeadlines">
                                                                    <uc1:TravelNewsHeadlineControl ID="travelNewsHeadlines" runat="server" EnableViewState="false">
                                                                    </uc1:TravelNewsHeadlineControl>
                                                                </div>
                                                                <div class="TextAlignRight">
                                                                    <asp:Label class="txtsevenbb" ID="labelStatusAt" runat="server" EnableViewState="false"></asp:Label>
                                                                    <asp:Label class="txtsevenbn" ID="labelCurrentTime" runat="server" EnableViewState="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        </td>
                                                        </tr>
                                                </table>
                                            </td>
                                            <td align="center" valign="middle" width="50%">
                                                <table>
                                                <tr>
                                                    <td>
                                                        <div class="HyperlinkTableCell">
                                                        <a name="DepartureBoards"></a>
                                                        <asp:HyperLink ID="hyperlinkDepartureBoards" runat="server" EnableViewState="false">
                                                            <cc1:TDImage ID="imageDepartureBoards" runat="server" Width="70" Height="36"></cc1:TDImage><br />
                                                            <asp:Literal ID="literalDepartureBoards" runat="server" EnableViewState="false"></asp:Literal>
                                                        </asp:HyperLink>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            </tr>
                                            </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center">
                                                <asp:Panel ID="HomeTravelInformationHtmlPlaceholderDefinition" runat="server">
                                                </asp:Panel>
                                                <br />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="HomepageMainLayoutColumn3">
                                    <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                    <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server">
                                    </asp:Panel>
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
