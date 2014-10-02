<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>

<%@ Page Language="c#" Codebehind="ReplanFullItinerarySummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.ReplanFullItinerarySummary" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyLineControl" Src="../Controls/ExtendJourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionOutputNavigationControl" Src="../Controls/ExtensionOutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,ExpandableMenu.css,homepage.css,nifty.css,ReplanFullItinerarySummary.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="ReplanFullItinerarySummary" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
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
                                        <table summary="MainPageContentTable" cellspacing="0" width="624" border="0">
                                            <tr>
                                                <td>
                                                    <div style="float:left;padding-left:5px">
                                                        <cc1:TDButton ID="newJourneyButton" runat="server"></cc1:TDButton>
                                                    </div>


                                                    <div class="ExtendedButtons" style="float:right;padding-right:5px;">
                                                        <cc1:HelpButtonControl ID="helpButton" runat="server">
                                                        </cc1:HelpButtonControl>
                                                    </div>
                                                    <div class="ExtendedButtons" style="float:right;padding-right:5px;">
                                                        <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                        </div>
                                                        
                                            </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="boxtypesixteen">
                                                        <h1>
                                                            <asp:Label ID="label1" CssClass="ExtendedLabels" runat="server"></asp:Label></h1>
                                                        <br/>
                                                        <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
                                                    </div>
                                                    <div class="spacer1">
                                                        &nbsp;</div>
                                                    <asp:Panel ID="errorMessagePanel" runat="server" CssClass="boxtypeerrormsgthree">
                                                        <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server"></uc1:ErrorDisplayControl>
                                                    </asp:Panel>
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
                                                    <asp:Panel ID="summaryPanel" CssClass="boxtypefortyfour" runat="server">
                                                        <asp:Panel ID="combinedResultsSummaryPanel" runat="server">
                                                            <div class="boxtypefortytwo">
                                                                <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                                                            </div>
                                                            <div class="boxtypefortythree">
                                                                <uc1:ResultsSummaryControl ID="combinedResultsSummaryControl" runat="server"></uc1:ResultsSummaryControl>
                                                            </div>
                                                        </asp:Panel>
                                                        
                                                        <uc1:ResultsViewSelectionControl ID="resultsViewSelectionControl" runat="server" Visible="false"></uc1:ResultsViewSelectionControl>
                                                        
                                                    </asp:Panel>
                                                    <br />
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
