<%@ Register TagPrefix="uc1" TagName="JourneyEmissionRelatedLinksControl" Src="../Controls/JourneyEmissionRelatedLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsDistanceInputControl" Src="../Controls/JourneyEmissionsDistanceInputControl.ascx" %>

<%@ Page Language="c#" Codebehind="JourneyEmissionsCompare.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyEmissionsCompare" ValidateRequest="false" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx" />
    <meta name="description" content="Check and compare CO2 emissions for your car/public transport journey, with our free online CO2 emissions calculator.  Transport Direct has a range of tools to keep you travelling cheaper and greener." />
    <meta name="keywords" content="carbon calculator, CO2 calculator, CO2, car, public transport,carbon emissions" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css, homepage.css, expandablemenu.css, nifty.css, jpstd.css, Emissions.css, JourneyEmissionsCompare.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyEmissionsCompare" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"
                    EnableViewState="false"></cc1:TDImage></a>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <% /* Left Hand Navigaion Bar */ %>
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <br />
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <% /* Page Content */ %>
                    <td>
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <% /* Main content control table */ %>
                            <table class="mainArea" cellspacing="0" width="100%"
                                border="0">
                                <tr valign="top">
                                    <% /* Main page content */ %>
                                    <td>
                                        <table cellspacing="0" width="585" border="0">
                                            <tr>
                                                <td>
                                                    <div id="boxtypeeightalt">
                                                        <a name="MainContent"></a>
                                                        <table cellspacing="0" cellpadding="0" width="100%"
                                                            border="0">
                                                            <tr>
                                                                <td align="right">
                                                                    <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                                    <cc1:HelpButtonControl ID="helpJourneyEmissions" runat="server">
                                                                    </cc1:HelpButtonControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <cc1:TDImage ID="imageJourneyEmissionsCompare" runat="server" Width="70" Height="36">
                                                                                </cc1:TDImage>
                                                                            </td>
                                                                            <td>
                                                                                <h1>
                                                                                    <asp:Label ID="labelTitle" runat="server" EnableViewState="false"></asp:Label></h1>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img height="5" src="/web2/images/gifs/spacer.gif" width="5" alt='<%# GetSpacerText() %>' /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <div id="boxtypeJEone">
                                                                        <div id="boxtypeJEtwo">
                                                                            <div id="boxtypeJEthree">
                                                                                <table cellspacing="0" cellpadding="0" width="100%"
                                                                                    border="0">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <uc1:JourneyEmissionsDistanceInputControl ID="journeyEmissionsDistanceInputControl"
                                                                                                runat="server"></uc1:JourneyEmissionsDistanceInputControl>
                                                                                        </td>
                                                                                        <td valign="bottom" align="right">
                                                                                            <cc1:TDButton ID="buttonNext" runat="server"></cc1:TDButton></td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img height="5" src="/web2/images/gifs/spacer.gif" width="5" alt='<%# GetSpacerText() %>' /></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    <uc1:JourneyEmissionsCompareControl ID="journeyEmissionsCompareControlOutward" runat="server">
                                                                    </uc1:JourneyEmissionsCompareControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <img height="5" src="/web2/images/gifs/spacer.gif" width="5" alt='<%# GetSpacerText() %>'/></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <div style="padding-left: 5px">
                                                        <asp:Panel ID="EmissionsInformationHtmlPlaceholderControl" runat="server">
                                                        </asp:Panel>
                                                     </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <% /* White Space Column */ %>
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <% /* Information Right Hand Column */ %>
                                    <td class="HomepageMainLayoutColumn4" valign="top">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <% /* Information Control */ %>
                                        <asp:Panel ID="TDInformationHtmlPlaceholderControl" runat="server">
                                        </asp:Panel>
                                        <% /* Information Links Control */ %>
                                        <div class="JourneyEmissionInformationColumn">
                                            <div class="Column3Header">
                                                <div class="txtsevenbbl">
                                                    <asp:Label ID="labelInformationLinks" runat="server" EnableViewState="false"></asp:Label></div>
                                                &nbsp;&nbsp;&nbsp;
                                            </div>
                                            <div class="Column3Content">
                                                <uc1:ExpandableMenuControl ID="informationLinksControl" runat="server" EnableViewState="False"
                                                    CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                                            </div>
                                        </div>
                                        
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="Footercontrol" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
