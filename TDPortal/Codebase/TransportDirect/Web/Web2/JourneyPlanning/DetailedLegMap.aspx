<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Page Language="c#" Codebehind="DetailedLegMap.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.DetailedLegMap" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping" Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css">
    </cc2:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="DetailedLegMap" method="post" runat="server">
            <!-- header -->
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
                        <cc2:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
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
                                                                <cc2:TDImage ID="imageFindACoach" runat="server" Width="70" Height="36">
                                                                </cc2:TDImage>
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
                                                    <cc2:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc2:HelpButtonControl>
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
                                                    <div id="boxtypeeightstd">
                                                        <h1>
                                                            <asp:Label ID="labelHeader" runat="server"></asp:Label></h1>
                                                    </div>
                                                    <div id="Div1">
                                                        <h1>
                                                            <asp:Label ID="labelLegLocations" runat="server"></asp:Label></h1>
                                                    </div>
                                                    <div id="picbox">
                                                        <asp:Image ID="modeImage" runat="server"></asp:Image></div>
                                                    <div id="boxtypethreemdlm">
                                                        <cc1:Map ID="Map" runat="server" ClickMode="Query" Height="390px" Width="543px" Visible="False">
                                                        </cc1:Map><asp:Image ID="DisplayMap" runat="server"></asp:Image></div>
                                                    <div id="Div2">
                                                        <cc2:TDButton ID="buttonBack" runat="server"></cc2:TDButton>
                                                    </div>
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
                                        <div class="Column3Header">
                                            <div class="txtsevenbbl">
                                                <asp:Label ID="labelRelatedLinks" runat="server" EnableViewState="false" Visible="false"></asp:Label></div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </cc2:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <% /* End: Region to copy to include LH menu when white labelling */ %>
            <p>
                <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
            </p>
        </form>
    </div>
</body>
</html>
