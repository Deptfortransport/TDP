<%@ Page Language="c#" Codebehind="JourneySummary.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneySummary" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionSummaryControl" Src="../Controls/ExtensionSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareGotoTicketRetailerControl" Src="../Controls/FindFareGotoTicketRetailerControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareSelectedTicketLabelControl" Src="../Controls/FindFareSelectedTicketLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AnalyticsControl" Src="../Controls/AnalyticsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyExtensionControl" Src="../Controls/JourneyExtensionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="OutputNavigationControl" Src="../Controls/OutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SummaryResultTableControl" Src="../Controls/SummaryResultTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindOverviewResultControl" Src="../Controls/FindOverviewResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CycleWalkLinksControl" Src="../Controls/CycleWalkLinks.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="homepage.css,setup.css,jpstd.css,ExtendAdjustReplan.css,expandablemenu.css,nifty.css,JourneySummary.aspx.css,ModalPopupMessage.css">
    </cc1:HeadElementControl>
    <uc1:AnalyticsControl id="analyticsControl" runat="server"></uc1:AnalyticsControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneySummary" method="post" runat="server">
            <a href="#MainContent">
                <asp:Image ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
            <a href="#JourneyButtons">
                <asp:Image ID="imageJourneyButtonsSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
            <asp:Panel ID="panelOutwardJourneysSkipLink1" runat="server" Visible="False">
                <a href="#OutwardJourneys">
                    <asp:Image ID="imageOutwardJourneysSkipLink1" runat="server" CssClass="skiptolinks">
                    </asp:Image></a></asp:Panel>
            <asp:Panel ID="panelFindTransportToStartLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportToStartButton">
                    <asp:Image ID="imageFindTransportToStartLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </asp:Image></a></asp:Panel>
            <asp:Panel ID="panelFindTransportFromEndLocationSkipLink" runat="server" Visible="False">
                <a href="#FindTransportFromEndButton">
                    <asp:Image ID="imageFindTransportFromEndLocationSkipLink" runat="server" CssClass="skiptolinks">
                    </asp:Image></a></asp:Panel>
            <asp:Panel ID="panelReturnJourneysSkipLink1" runat="server" Visible="False">
                <a href="#ReturnJourneys">
                    <asp:Image ID="imageReturnJourneysSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a></asp:Panel>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <!-- Homepage Outline Table -->
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                       
                        <div class="SocialBookmark ExpandableMenu HomePageMenu">
                            <uc1:SocialBookMarkLinkControl ID="socialBookMarkLinkControl" runat="server" EnableViewState="False"></uc1:SocialBookMarkLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <table lang="en" cellspacing="0" width="830px" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="theJourneyChangeSearchControl" runat="server"
                                                            HelpLabel="journeySummaryPageHelpLabel"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <div>
                                                        <uc1:JourneysSearchedForControl ID="theJourneysSearchedForControl1" runat="server"></uc1:JourneysSearchedForControl>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <asp:Panel ID="errorMessagePanel" runat="server" Visible="False">
                                                            <div class="boxtypeerrormsgtwo">
                                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                            </div>
                                                        </asp:Panel>
                                                    
                                                        <asp:Panel ID="panelFindFareSteps" runat="server" visible="false">
                                                            <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server" visible="false"></uc1:FindFareStepsControl>
                                                        </asp:Panel>
                                                        
                                                        <uc1:JourneyPlannerOutputTitleControl ID="JourneyPlannerOutputTitleControl1" runat="server"></uc1:JourneyPlannerOutputTitleControl>
                                                        
                                                        <cc1:HelpLabelControl ID="journeySummaryPageHelpLabel" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc1:HelpLabelControl>
                                                        
                                                        <div id="panelInstructions" style="margin-bottom: 5px" runat="server">
                                                            <asp:Label ID="labelInstructions" runat="server" CssClass="txtseven"></asp:Label>
                                                            <asp:Label ID="labelInstructions2" runat="server" CssClass="txtseven" visible="false"></asp:Label></div>
                                                        <asp:Label ID="labelJourneyOptionsTableDescription" runat="server" CssClass="screenreader"></asp:Label>
                                                        <table cellspacing="0" cellpadding="0" width="100%" summary="Journey details menu">
                                                            <tr id="selectedTicketRow" runat="server">
                                                                <td id="selectedTicketCell" valign="bottom" width="100%" runat="server">
                                                                    <uc1:FindFareSelectedTicketLabelControl ID="findFareSelectedTicketLabelControl" runat="server">
                                                                    </uc1:FindFareSelectedTicketLabelControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" align="right">
                                                                    <table cellspacing="0" cellpadding="0" width="827px">
                                                                        <tr>
                                                                            <td valign="bottom" align="right">
                                                                                <uc1:OutputNavigationControl ID="theOutputNavigationControl" runat="server"></uc1:OutputNavigationControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <a name="OutwardJourneys"></a>
                                                        <div class="boxtypeseventeen">
                                                            <table id="summaryOutwardTable" runat="server">
                                                                <tr>
                                                                    <td width="610">
                                                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                                                                    </td>
                                                                    <td align="right">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <uc1:SummaryResultTableControl ID="summaryResultTableControlOutward" runat="server">
                                                            </uc1:SummaryResultTableControl>
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server"
                                                                Visible="true"></uc1:FindSummaryResultControl>
                                                        </div>
                                                        <uc1:JourneyExtensionControl ID="theJourneyExtensionControlOutward" runat="server"></uc1:JourneyExtensionControl>
                                                    </asp:Panel>
                                                    <div class="cyclewalkcontainer">
                                                        <uc1:CycleWalkLinksControl id="outwardCycleWalkLinks" runat="server" />
                                                    </div>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <a name="ReturnJourneys"></a>
                                                        <asp:Table ID="summaryReturnTable" runat="server">
                                                            <asp:TableRow>
                                                                <asp:TableCell ID="cellButtonSummary" width="610">
                                                                    <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                                                </asp:TableCell>
                                                            </asp:TableRow>
                                                        </asp:Table>
                                                        <uc1:SummaryResultTableControl ID="summaryResultTableControlReturn" runat="server"></uc1:SummaryResultTableControl>
                                                        <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server"
                                                            Visible="true"></uc1:FindSummaryResultControl>
                                                    </asp:Panel>
                                                    <div class="cyclewalkcontainer">
                                                        <uc1:CycleWalkLinksControl id="returnCycleWalkLinks" runat="server" />
                                                    </div>
                                                    <a name="amend"></a>
                                                    <div class="boxtypesixteen">
                                                        <table lang="en" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <uc1:FindFareGotoTicketRetailerControl ID="findFareGotoTicketRetailerControl" runat="server">
                                                                    </uc1:FindFareGotoTicketRetailerControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <uc1:ResultsFootnotesControl ID="footnotesControl" runat="server"></uc1:ResultsFootnotesControl>
                                                    <br />
                                                    <p>
                                                        <a href="#JourneyButtons">
                                                        <asp:Image ID="imageJourneyButtonsSkipLink2" runat="server" CssClass="skiptolinks"></asp:Image></a>
                                                    </p>
                                                    <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                        &nbsp;&nbsp;&nbsp;
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
