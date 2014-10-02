<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="SeasonalNoticeBoardControl" Src="Controls/SeasonalNoticeBoardControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>

<%@ Page Language="c#" Codebehind="SeasonalNoticeBoard.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.SeasonalNoticeBoard" %>

<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExpandableMenu.css,homepage.css,nifty.css,SeasonalNoticeBoard.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="SeasonalNoticeBoard" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <% /* Left Hand Navigaion Bar */ %>
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl1" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                            <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>

                    </td>
                    <% /* Page Content */ %>
                    <td>
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="RoundedCornerControl1" runat="server" InnerColour="White"
                            OuterColour="#CCECFF" Corners="TopLeft" CssClass="bodyArea">
                            <% /* Main content control table */ %>
                            <div style="margin-top:5px">
                                &nbsp;<cc2:TDButton ID="backButton" runat="server"></cc2:TDButton>
                            </div>
                            <table id="mapcontrol" cellspacing="0">
                                <tr>
                                    <td>
                                        <div id="SeasonalNoticeboardTopHeader">
                                            <h1>
                                                <asp:Label ID="lblSeasonalTopHeader" runat="server" EnableViewState="false"></asp:Label></h1>
                                        </div>
                                        <div class="txtseven">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <uc1:SeasonalNoticeBoardControl ID="ctrlSeasonalNoticeBoardControl" runat="server"></uc1:SeasonalNoticeBoardControl>
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
