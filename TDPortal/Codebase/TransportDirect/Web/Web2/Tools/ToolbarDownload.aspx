<%@ Page Language="c#" Codebehind="ToolbarDownload.aspx.cs" AutoEventWireup="True"
    ValidateRequest="false" Inherits="TransportDirect.UserPortal.Web.Templates.ToolbarDownload" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/Tools/Toolbardownload.aspx" />
    <meta name="description" content="Add a journey planner to your web browser with the Transport Direct Journey Planner toolbar" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,ToolbarDownload.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="ToolbarDownload" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="False"></uc1:ClientLinkControl>
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
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="595" border="0">
                                            <tr>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <a name="SkipToMain"></a>
                                                                <cc1:TDImage ID="imageToolbarDownload" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                            </td>
                                                            <td>
                                                                <h1>
                                                                    <asp:Label ID="labelPageTitle" runat="server" EnableViewState="false"></asp:Label></h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <!--<td align="right">
                                                <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                </cc1:HelpButtonControl>
                                            </td> -->
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
                                                    <table id="maincontenttable" cellpadding="5">
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Panel ID="errorMessagePanel" runat="server" Visible="False">
                                                                    <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="false"></uc1:ErrorDisplayControl>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div>
                                                                    <asp:Panel ID="TDToolbarHeaderHtmlPlaceholderControl" runat="server">
                                                                    </asp:Panel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="375">
                                                                <div id="mboxtypethree">
                                                                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                                        <asp:Label ID="labelDownloadInfo" runat="server" Visible="true" CssClass="txtsevenb"
                                                                            EnableViewState="false"></asp:Label>
                                                                    </div>
                                                                    <div style="padding-right: 0px; padding-left: 0px; padding-bottom: 15px; padding-top: 15px;
                                                                        text-align: center">
                                                                        <cc1:TDButton ID="buttonToolbarDownload" runat="server" CssClass="buttonToolbarDownload"
                                                                            CssClassMouseOver="buttonToolbarDownloadMouseOver" EnableViewState="false"></cc1:TDButton>
                                                                    </div>
                                                                    <div style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px; padding-top: 5px">
                                                                        <asp:Label ID="labelSystemRequirements" runat="server" Visible="true" CssClass="txtseven"
                                                                            EnableViewState="false"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                            <td width="375">
                                                                <div>
                                                                    <asp:Panel ID="TDToolbarAvailableNowHtmlPlaceHolderControl" runat="server">
                                                                    </asp:Panel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Panel ID="TDToolbarComingUpHtmlPlaceholderControl" runat="server">
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <% /* End: Content to be replaced when white labelling */ %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                </tr>
            </table>
            </cc1:RoundedCornerControl> </td> </tr> </table>
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
