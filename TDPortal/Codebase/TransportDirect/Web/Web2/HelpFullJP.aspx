<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>

<%@ Page Language="c#" Codebehind="HelpFullJP.aspx.cs" ValidateRequest="false" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.HelpFullJP" %>

<%@ Register TagPrefix="uc1" TagName="FooterControlMcms" Src="Controls/FooterControlMcms.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="Controls/ExpandableMenuControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="Controls/PrinterFriendlyPageButtonControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css, FooterPages.css,HelpFull.aspx.css">
    </cc1:HeadElementControl>
    <asp:literal id="basehelpliteral" runat="server"></asp:literal>
    <meta name="ROBOTS" content="NOODP" />
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
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
                        <div id="mainbit" class="bodyArea" style="padding-top: 0pt;">
                            <!-- Main content control table -->
                            <table class="mainArea" lang="en" cellspacing="0" cellpadding="0" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <div class="boxtypeStaticPages">
                                            <div class="floatleftonly">
                                                <cc1:TDButton ID="buttonBack1" runat="server" ></cc1:TDButton>
                                            </div>
                                            <div class="floatrightonly">
                                            <asp:HyperLink ID="PrinterFriendlyLink" runat="server" Target="_blank">
                                                <asp:Label ID="hyperlinkText" CssClass="TDButtonHyperlink" runat="server"></asp:Label>
                                            </asp:HyperLink>
                                            </div>
                                        <br />
                                        </div>
                                        
                                        <div class="titleLabelPad">
                                            <h1><asp:Label ID="HelpLabelTitle" runat="server"></asp:Label></h1>
                                        </div>
                                        
                                        <div id="boxtypenine">
                                            <asp:Panel ID="HelpBodyText" runat="server">
                                            </asp:Panel>
                                        </div>
                                        
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="false"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>

