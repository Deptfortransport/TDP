<%@ Page Language="c#" Codebehind="ExtensionResultsSummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.ExtensionResultsSummary" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyLineControl" Src="../Controls/ExtendJourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyBuilderControl" Src="../Controls/JourneyBuilderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionOutputNavigationControl" Src="../Controls/ExtensionOutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,ExtensionResultSummary.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="ExtensionResultsSummary" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"
                    EnableViewState="false"></cc1:TDImage></a> <a href="#ViewSelection">
                        <cc1:TDImage ID="imageMainContentSkipLink2" runat="server" CssClass="skiptolinks"
                            EnableViewState="false"></cc1:TDImage></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <a name="MainContent"></a>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="false"></uc1:ClientLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypesixteen">
                                                         <div class="ExtendedLabels">
                                                            
                                                            <cc1:TDButton ID="newJourneyButton" runat="server" EnableViewState="false"></cc1:TDButton>
                                                            <cc1:TDButton ID="amendJourneyButton" runat="server" EnableViewState="false"></cc1:TDButton>
                                                        </div>
                                                        <div class="ExtendedButtons">
                                                            <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"
                                                                EnableViewState="false" />
                                                            <cc1:HelpButtonControl ID="helpButton" runat="server" EnableViewState="false">
                                                            </cc1:HelpButtonControl>
                                                        </div>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <cc1:TDImage ID="imageExtensionResultSummary" runat="server" Width="70" Height="30"></cc1:TDImage>
                                                                </td>
                                                                <td>
                                                                    <h1><asp:Label ID="labelTitle" runat="server" CssClass="ExtendedLabels" EnableViewState="false"></asp:Label></h1>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                        </table>   
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven" EnableViewState="false"></asp:Label>
                                                    </div>
                                                    <div class="boxtypewhitebackground">
                                                        <uc1:ExtendJourneyLineControl ID="extendJourneyLineControl" runat="server" EnableViewState="false" />
                                                    </div>
                                                    <asp:Panel ID="errorMessagePanel" runat="server" CssClass="boxtypeerrormsgthree">
                                                        <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" EnableViewState="false" />
                                                    </asp:Panel>
                                                    <asp:Panel ID="summaryPanel" runat="server">
                                                        <div class="boxtypefortyone">
                                                            <table cellspacing="0" cellpadding="0" width="100%" summary="Journey details menu">
                                                                <tr>
                                                                    <td valign="bottom" align="right">
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td valign="bottom" align="right">
                                                                                    <uc1:ExtensionOutputNavigationControl ID="theOutputNavigationControl" runat="server">
                                                                                    </uc1:ExtensionOutputNavigationControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div class="boxtypefortyone">
                                                            <asp:Panel ID="outwardSummaryPanel" runat="server">
                                                                <div>
                                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="boxtypefortytwo">
                                                                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"
                                                                                        EnableViewState="false" />
                                                                                </div>
                                                                            </td>
                                                                            <td class="ejseleet">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <div class="boxtypefortythree">
                                                                    <uc1:ResultsSummaryControl ID="outwardResultsSummaryControl" runat="server" EnableViewState="false" />
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="returnSummaryPanel" runat="server">
                                                                <div class="boxtypefortytwo">
                                                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"
                                                                        EnableViewState="false" />
                                                                </div>
                                                                <div class="boxtypefortythree">
                                                                    <uc1:ResultsSummaryControl ID="returnResultsSummaryControl" runat="server" EnableViewState="false" />
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <a name="ViewSelection"></a>
                                                        <div class="ResultsViewDisplay">
                                                            <uc1:ResultsViewSelectionControl ID="resultsViewSelectionControl" runat="server"
                                                                Visible="false" />
                                                        </div>
                                                    </asp:Panel>
                                                    <uc1:JourneyBuilderControl ID="journeyBuilderControl" runat="server" EnableViewState="false" />
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
