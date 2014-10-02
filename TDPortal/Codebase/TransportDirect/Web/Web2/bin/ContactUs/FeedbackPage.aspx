<%@ Register TagPrefix="uc1" TagName="FeedbackControl" Src="../Controls/FeedbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="FeedbackPage.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.FeedbackPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,homepage.css,expandablemenu.css,nifty.css,FeedbackPage.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FeedbackPage" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"
                    EnableViewState="false"></cc1:TDImage></a> <a href="#Feedback">
                        <cc1:TDImage ID="imageFeedbackPanelSkipLink1" runat="server" CssClass="skiptolinks"
                            EnableViewState="false"></cc1:TDImage></a>
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
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table id="maincontenttable" lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="595" border="0">
                                            <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <cc1:TDImage ID="imageFeedbackPage" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                        </td>
                                                        <td>
                                                            <h1>
                                                                <asp:Label ID="labelPageTitle" runat="server" EnableViewState="false"></asp:Label>
                                                            </h1>
                                                        </td>
                                                    </tr>
                                                </table></td>
                                            </tr>
                                        </table>
                                        <!-- Journey Planning Controls -->
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" CssClass="txtseven"></asp:Label></div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div id="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label></div>
                                        </asp:Panel>
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <% /* Start: Content to be replaced when white labelling */ %>
                                                    <table id="mainpagecontent" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td align="left" style="padding-left: 15px">
                                                                <h2>
                                                                    <asp:Label ID="labelTitle" runat="server" EnableViewState="false"></asp:Label></h2>
                                                            </td>
                                                        </tr>
                                                        <tr style="height: 10px;"><td></td></tr>
                                                    </table>
                                                    <!--left hand menu-->
                                                    <div id="menuarea">
                                                    </div>
                                                    <div>
                                                    </div>
                                                    <!-- main content-->
                                                    <div id="primcontent">
                                                        <div id="contentareawl" style="padding-left: 10px">
                                                            <table id="centralpagecontent" cellpadding="0px" cellspacing="0px">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        
                                                                            <asp:Literal ID="labelIntroduction" runat="server" EnableViewState="False" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <a name="Feedback"></a>
                                                            <uc1:FeedbackControl ID="feedbackControl" runat="server"></uc1:FeedbackControl>
                                                            <br/>
                                                        </div>
                                                    </div>
                                                    <% /* End: Content to be replaced when white labelling */ %>
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
