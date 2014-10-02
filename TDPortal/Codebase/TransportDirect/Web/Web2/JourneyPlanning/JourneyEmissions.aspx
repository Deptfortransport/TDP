<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsControl" Src="../Controls/JourneyEmissionsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Page Language="c#" Codebehind="JourneyEmissions.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyEmissions" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css, nifty.css,JourneyEmissions.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
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
                    </td>
                    <% /* Page Content */ %>
                    <td>
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <% /* Main content control table */ %>
                            <table summary="MainContentControlTable" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <% /* Main page content */ %>
                                    <td>
                                        <table summary="MainPageContentTable" cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td valign="top">
                                                    <a name="MainContent"></a>
                                                    <table lang="en" cellspacing="0" border="0" style="margin-left: 10px; padding-bottom: 5px" width="100%">
                                                        <tr>
                                                            <td align="left">
                                                                <cc1:TDButton ID="buttonBack" runat="server"></cc1:TDButton>
                                                            </td>
                                                            <td align="right" >
                                                                <cc1:TDButton ID="buttonDashboardView" runat="server" Text="[Test]" />
                                                                <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                            </td>
                                                            <td>
                                                                <cc1:HelpButtonControl Visible="False" ID="helpJourneyEmissions" runat="server">
                                                                </cc1:HelpButtonControl>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table width="100%" cellspacing="0" cellpadding="0" style="margin-left: 5px; padding-bottom: 5px">
                                                        <tr>
                                                            <td>
                                                                <h1>
                                                                    <asp:Label ID="labelTitle" runat="server" CssClass="ExtendedLabels" EnableViewState="false"></asp:Label></h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="spacer1">
                                                        &nbsp;</div>
                                                
                                                    <asp:Panel ID="panelSummary" CssClass="boxtypelargetwo" runat="server">
                                                        <div class="boxtypetwentyfive">
                                                            <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                                                        </div>
                                                        <div class="boxtypetwentysix">
                                                            <uc1:ResultsSummaryControl ID="resultsSummaryControl" runat="server"></uc1:ResultsSummaryControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <uc1:JourneyEmissionsControl ID="journeyEmissionsControl" runat="server"></uc1:JourneyEmissionsControl>
                                                    <br/>
                                                    <a name="AmendSaveSend"></a>
                                                    <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                </td>
                                               <td>
                                                    <div class="WhiteSpaceBetweenColumns"></div>
                                                </td>
                                                <!-- Information Column -->
                                                <td class="HomepageMainLayoutColumn3" valign="top">
                                                    <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                                    <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server">
                                                    </asp:Panel>
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
            <uc1:FooterControl ID="FooterControl" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
