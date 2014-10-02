<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyReplanTableGridControl" Src="../Controls/JourneyReplanTableGridControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyReplanSegmentControl" Src="../Controls/JourneyReplanSegmentControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefinePageOptionsControl" Src="../Controls/RefinePageOptionsControl.ascx" %>

<%@ Page Language="c#" Codebehind="JourneyReplanInputPage.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.JourneyReplanInputPage" %>

<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css, JourneyReplanInputPage.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="JourneyReplanInputPage" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <% /* Start: Region to copy to include LH menu when white labelling */ %>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                       
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
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="595" border="0">
                                            <tr>
                                                <td>
                                                    <a name="BackButton"></a>
                                                    <asp:Panel ID="panelBackTop" CssClass="panelBackTop" runat="server">
                                                    </asp:Panel>
                                                </td>
                                                <td align="left">
                                                    <h1>
                                                        <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>
                                                </td>
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
                                                    <a name="MainContent"></a>
                                                    <table class="JourneyChangeSearchControl" cellspacing="0" cellpadding="0" width="100%">
                                                        <tr>
                                                            
                                                            <td style="padding-bottom: 5px; padding-left:5px">
                                                            <uc1:JourneyChangeSearchControl ID="journeyChangeSearchControl1" runat="server"></uc1:JourneyChangeSearchControl>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                        <table cellspacing="0" cellpadding="0" width="100%"  style="margin-left:5px">
                                                        <tr>
                                                            <td>
                                                                <h1>
                                                                    <asp:Label ID="labelReplanInputTitle" runat="server" EnableViewState="False"></asp:Label></h1>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="labelReplanInputSubheading" runat="server" EnableViewState="False"
                                                                    CssClass="txtseven"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Panel ID="errorMessagePanel" runat="server" CssClass="boxtypeerrormsgthree">
                                                                    <uc1:ErrorDisplayControl ID="errorDisplayControl1" runat="server"></uc1:ErrorDisplayControl>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div class="boxtypetwelverefine">
                                                        <div id="dmtitle">
                                                            <div class="HeaderButtons">
                                                                <cc1:TDButton ID="buttonShowTableDiagram" runat="server" CausesValidation="False"></cc1:TDButton>
                                                            </div>
                                                            <div>
                                                                <span class="txteightb">
                                                                    <asp:Label ID="labelJourneyDirection" runat="server"></asp:Label>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div id="dmview">
                                                            <!-- This comment to fix IE DIV bug. Do not remove! -->
                                                            <uc1:JourneyReplanSegmentControl ID="journeyReplanSegmentControl1" runat="server"></uc1:JourneyReplanSegmentControl>
                                                            <uc1:JourneyReplanTableGridControl ID="journeyReplanTableGridControl1" runat="server">
                                                            </uc1:JourneyReplanTableGridControl>
                                                        </div>
                                                    </div>
                                                    <uc1:RefinePageOptionsControl ID="pageOptionsControl" runat="server"></uc1:RefinePageOptionsControl>
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
            <% /* End: Region to copy to include LH menu when white labelling */ %>
            <uc1:FooterControl ID="footerControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
