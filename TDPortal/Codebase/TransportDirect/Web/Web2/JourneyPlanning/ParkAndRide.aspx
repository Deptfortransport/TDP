<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapRegionSelectControl" Src="../Controls/MapRegionSelectControl.ascx" %>

<%@ Page Language="c#" Codebehind="ParkAndRide.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.ParkAndRide" %>

<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ParkAndRideTableControl" Src="../Controls/ParkAndRideTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/ParkAndRide.aspx" />
    <meta name="description" content="Find park &amp; ride schemes throughout Britain with Transport Direct's car park &amp; ride finder." />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ParkAndRide.aspx.css,ExpandableMenu.css,homepage.css,nifty.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <% /* Left Hand Navigaion Bar */ %>
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                            
                        <div class="HomepageBookmark">
                            <uc1:clientlinkcontrol id="clientLink" runat="server" EnableViewState="False">
                            </uc1:clientlinkcontrol></div>
                    </td>
                    <% /* Page Content */ %>
                    <td>
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="RoundedCornerControl1" runat="server" InnerColour="White"
                            OuterColour="#CCECFF" Corners="TopLeft" CssClass="bodyArea">
                            <% /* Main content control table */ %>
                            <table id="ColumnLeftPage">
                                <tr valign="top">
                                    <% /* Page Content */ %>
                                    <td align="right">
                                        <div class="boxprinterfriendly">
                                            <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server">
                                            </uc1:PrinterFriendlyPageButtonControl>
                                            <cc1:HelpCustomControl ID="imageButtonHelp" runat="server" HelpLabel="helpLabelControl"
                                                Style="vertical-align: top"></cc1:HelpCustomControl>
                                        </div>
                                    </td>
                                    <%--       <td id="RightFiller">
                                        &nbsp;
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <cc1:TDImage ID="imageFindPage" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelParkAndRideHeading" runat="server" CssClass="parkAndRideHeading"
                                                            EnableViewState="False">Park and ride heading</asp:Label></h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div class="txtseven">
                                            <asp:Label ID="labelParkAndRideSubheading" runat="server" CssClass="parkAndRideSubheading"
                                                EnableViewState="False">Park and ride sub heading</asp:Label></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <table>
                                            <tr>
                                                <td valign="top">
                                                    <uc1:MapRegionSelectControl ID="regionSelector" runat="server"></uc1:MapRegionSelectControl>
                                                </td>
                                                <td>
                                                    <uc1:ParkAndRideTableControl ID="parkAndRideTable" runat="server"></uc1:ParkAndRideTableControl>
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
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
