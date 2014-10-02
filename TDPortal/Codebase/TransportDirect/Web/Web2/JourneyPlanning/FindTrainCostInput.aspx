<%@ Page Language="C#" Codebehind="FindTrainCostInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindTrainCostInput" EnableViewState="True"
    ValidateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFarePreferenceControl" Src="../Controls/FindFarePreferenceControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ModeSelectControl" Src="../Controls/ModeSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SearchTypeControl" Src="../Controls/SearchTypeControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta name="description" content="Save on train journeys with the Transport Direct cheap rail fare search." />
    <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/softcontent/en/finda_train.gif" />
    <cc1:HeadElementControl ID="headElementControl" Stylesheets="setup.css, homepage.css, jpstd.css, CalendarSS.css, expandablemenu.css, nifty.css, FindTrainCostInput.aspx.css"
        runat="server"></cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindATrain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindTrainInput" method="post" runat="server">
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
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:TDButton ID="commandBack" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="FindATrain"></a>
                                                        <cc1:TDImage ID="imageFindATrainCost" runat="server" ></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <uc1:SearchTypeControl ID="searchTypeControl" runat="server" EnableViewState="False"></uc1:SearchTypeControl>
                                                    <div class="boxtypetwo">
                                                        <uc1:FindToFromLocationsControl ID="locationsControl" runat="server"></uc1:FindToFromLocationsControl>
                                                        <uc1:FindLeaveReturnDatesControl ID="dateControl" runat="server"></uc1:FindLeaveReturnDatesControl>
                                                        <uc1:findpageoptionscontrol id="pageOptionsControltop" runat="server"></uc1:findpageoptionscontrol>                                                    
                                                    </div>
                                                    <uc1:FindFarePreferenceControl ID="preferencesControl" runat="server"></uc1:FindFarePreferenceControl>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server" CssClass="SoftContentPanel" ScrollBars="None"></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
                                        <asp:Panel ID="TDFindTrainPromoHtmlPlaceholderDefinition" runat="server" Visible="false"></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
