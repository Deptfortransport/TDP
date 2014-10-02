<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="~/Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="~/Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>

<%@ Page Language="c#" Codebehind="StaticWithoutPrint.aspx.cs" ValidateRequest="false"
    AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.StaticWithoutPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css, FooterPages.css,StaticWithoutPrint.aspx.css">
    </cc1:HeadElementControl>
    <meta name="ROBOTS" content="NOODP" />
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <!-- header -->
            <uc1:HeaderControl ID="HeaderControl1" runat="server" Visible="True"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <div id="mainbit" class="bodyArea" style="padding-top: 0pt;">
                            <!-- Main content control table -->
                            <table class="mainArea" lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <a name="SkipToMain"></a>
                                        <!-- Top of Page Controls -->
                                        <div class="boxtypeStaticPages">
                                            <table lang="en" cellspacing="2" cellpadding="2" width="100%" border="0">
                                                <tr>
                                                    <td align="right">
                                                        <font size="2">
                                                            <asp:HyperLink ID="PrinterFriendlyLink" runat="server" Target="_blank">
                                                                <asp:Label ID="hyperlinkText" CssClass="TDButtonHyperlink" runat="server">
                                                                </asp:Label>
                                                            </asp:HyperLink>
                                                        </font>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <!--page title bar version 1-->
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <asp:Panel ID="TitleText" runat="server">
                                                        </asp:Panel>
                                                    </div>
                                                    <!-- main content-->
                                                    <asp:Panel ID="Body_Text" runat="server">
                                                    </asp:Panel>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControlMcms1" runat="server"></uc1:FooterControl>
        </form>
    </div>
    <script language="javascript" type="text/javascript"> 
        if(NiftyCheck()) Rounded( "div.bodyArea", "tl", "#ccecff", "#ffffff", "smooth"); 
    </script>
</body>
</html>
