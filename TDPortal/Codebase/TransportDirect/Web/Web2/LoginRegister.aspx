<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LoginRegister.aspx.cs"
    Inherits="TransportDirect.UserPortal.Web.Templates.LoginRegister" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="RegisterControl" Src="Controls/RegisterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LogoutControl" Src="Controls/LogoutControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LoginControl" Src="Controls/LoginControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta name="keywords" content="Login, Register" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,CalendarSS.css,expandablemenu.css,nifty.css,LoginRegister.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="LoginRegister" method="post" runat="server">
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
                                        <table lang="en" cellspacing="0" width="830" border="0">
                                            <tr>
                                                <td id="backButtonCell">
                                                    <cc1:TDButton ID="buttonBack1" runat="server" >
                                                    </cc1:TDButton>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="toptitlediv">
                                                        <h1><asp:Label runat="server" ID="labelTitleLoginRegister" EnableViewState="false"></asp:Label></h1>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelLogin" runat="server" EnableViewState="false">
                                                        <div class="controlWrap">
                                                            <div class="controlText">
                                                               <h2> <asp:Label ID="loginControlText" EnableViewState="False" runat="server">[Login Text Control]</asp:Label></h2></div>
                                                        </div>
                                                        <uc1:LoginControl ID="loginControl" runat="server"></uc1:LoginControl>
                                                        <uc1:LogoutControl ID="logoutControl" runat="server"></uc1:LogoutControl>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelRegister" runat="server" EnableViewState="false">
                                                        <div class="controlWrap">
                                                            <div class="controlText">
                                                                <h2><asp:Label ID="registerControlText" EnableViewState="False" runat="server">[Register Text Control]</asp:Label></h2></div>
                                                        </div>
                                                        <uc1:RegisterControl ID="registerControl" runat="server"></uc1:RegisterControl>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelWhyRegister" runat="server" EnableViewState="false">
                                                        <div class="controlWrap">
                                                            <div class="controlText">
                                                                <h2><asp:Label ID="whyRegisterControlText" EnableViewState="False" runat="server">[Why Register Text Control]</asp:Label></h2></div>
                                                            <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server">
                                                            </asp:Panel>
                                                        </div>
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
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
