<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AccessibilityLinkForInputControl" Src="../Controls/AccessibilityLinkForInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFarePreferenceControl" Src="../Controls/FindFarePreferenceControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Page Language="c#" Codebehind="FindFareInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindFareInput" EnableViewState="True" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModeSelectControl" Src="../Controls/ModeSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchTypeControl" Src="../Controls/SearchTypeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" >
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,homepage.css,expandablemenu.css,nifty.css">
    </cc1:HeadElementControl>
</head>
<body ms_positioning="FlowLayout" dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <% /* Start: Region to copy to include LH menu when white labelling */ %>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:ExpandableMenuControl>
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False">
                            </uc1:ClientLinkControl>
                        </div>
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
                                        <table lang="en" cellspacing="0" width="595" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" CssClass="panelBackTop" runat="server">
                                                    </asp:Panel>
                                                </td>
                                                <td align="left">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <cc1:TDImage ID="imageFindACoach" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                            </td>
                                                            <td>
                                                                <h1>
                                                                    <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label>
                                                                </h1>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <!-- Journey Planning Controls -->
                                        <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
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
                                                    <div id="Div1">
                                                        <table lang="en" width="100%" cellspacing="0">
                                                            <tr>
                                                                <td colspan="3" align="right" class="txtseven">
                                                                    <uc1:AccessibilityLinkForInputControl runat="server" ID="accessibilityLinkForInputControl">
                                                                    </uc1:AccessibilityLinkForInputControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="panel1" CssClass="panelBackTop" runat="server">
                                                                    </asp:Panel>
                                                                </td>
                                                                <td width="100%">
                                                                    <h1>
                                                                        <asp:Label ID="label1" runat="server" EnableViewState="False"></asp:Label></h1>
                                                                </td>
                                                                <td align="right">
                                                                    <cc1:HelpButtonControl runat="server" ID="Helpbuttoncontrol2">
                                                                    </cc1:HelpButtonControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td colspan="2">
                                                                    <asp:Label ID="label2" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <asp:Panel ID="panel2" runat="server" Visible="False">
                                                        <div id="boxtypeeightstd">
                                                            <asp:Label ID="label3" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                                                        </div>
                                                    </asp:Panel>
                                                    <uc1:SearchTypeControl ID="searchTypeControl" runat="server"></uc1:SearchTypeControl>
                                                    <uc1:ModeSelectControl ID="modeSelectControl" runat="server"></uc1:ModeSelectControl>
                                                    <div id="boxtypetwo">
                                                        <uc1:FindToFromLocationsControl ID="locationsControl" runat="server"></uc1:FindToFromLocationsControl>
                                                        <uc1:FindLeaveReturnDatesControl ID="dateControl" runat="server"></uc1:FindLeaveReturnDatesControl>
                                                    </div>
                                                    <uc1:FindFarePreferenceControl ID="preferencesControl" runat="server"></uc1:FindFarePreferenceControl>
                                                    <% /* End: Content to be replaced when white labelling */ %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn4" valign="top">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <div class="Column3Header">
                                            <div class="txtsevenbbl">
                                                <asp:Label ID="labelRelatedLinks" runat="server" EnableViewState="false" Visible="false"></asp:Label></div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <% /* End: Region to copy to include LH menu when white labelling */ %>
            <div>
                <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
            </div>
        </form>
    </div>
</body>
</html>
