<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareTicketSelectionControl" Src="../Controls/FindFareTicketSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<%@ Page Language="c#" Codebehind="FindFareTicketSelectionReturn.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindFareTicketSelectionReturn" %>

<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="nifty.css,expandablemenu.css,homepage.css,setup.css,jpstd.css,FindAFare.css,FindFareTicketSelectionReturn.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindFareTicketSelection" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
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
                                        <!-- Top of Page Controls -->
                                        
                                        <!-- Journey Planning Controls -->
                                        
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                               
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="journeyChangeSearchControl" HelpLabel="helpLabelControl"
                                                            runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <uc1:JourneysSearchedForControl ID="journeysSearchedForControl" runat="server"></uc1:JourneysSearchedForControl>
                                                    <br />
                                                    <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server"></uc1:FindFareStepsControl>
                                                    <cc1:HelpLabelControl ID="helpLabelControl" runat="server" CssMainTemplate="helpboxoutput"
                                                        Visible="false"></cc1:HelpLabelControl>
                                                    <div class="boxtypeeightstd">
                                                        <uc1:JourneyPlannerOutputTitleControl ID="pageTitleControl" runat="server"></uc1:JourneyPlannerOutputTitleControl>
                                                    </div>
                                                    <asp:Panel ID="messagePanel" runat="server">
                                                        <div class="boxtypeerrormsg">
                                                            <asp:Label ID="messagesLabel" runat="server" EnableViewState="False"></asp:Label></div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="instructionPanel" runat="server">
                                                        <div class="boxtypeeightstd">
                                                            <asp:Label ID="instructionLabel" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="viewSinglesPanel" runat="server">
                                                        <div class="boxtypeeightstd">
                                                            <asp:Label ID="viewSinglesPart1Label" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                                            <asp:HyperLink ID="viewSinglesHyperLink" runat="server" EnableViewState="False" CssClass="txtseven"></asp:HyperLink>
                                                            <asp:Label ID="viewSinglesPart2Label" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label></div>
                                                    </asp:Panel>
                                                    <br/>
                                                    <uc1:FindFareTicketSelectionControl ID="ticketSelectionControl" runat="server"></uc1:FindFareTicketSelectionControl>
                                                    <div class="boxtypeeightstd">
                                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="right">
                                                                    <cc1:TDButton ID="nextButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="boxtypeeightstd">
                                                        <asp:Label ID="noteLabel" runat="server" EnableViewState="False" CssClass="txtnote"></asp:Label></div>
                                                    <br/>
                                                    <uc1:AmendSaveSendControl ID="amendControl" runat="server"></uc1:AmendSaveSendControl>
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
