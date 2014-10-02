<%@ Page Language="c#" Codebehind="FindNearestAccessibleStop.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.FindNearestAccessibleStop" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AccessibleTransportTypesControl" Src="../Controls/AccessibleTransportTypesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindTDANControl" Src="../Controls/FindTDANControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapNearestControl" Src="../Controls/MapNearestControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/FindNearestAccessibleStop.aspx" />
    <meta name="description" content="Find the nearest accessible stop." />
    <meta name="keywords" content="route planner, accessible, accessibility, route planner UK, journey planner, public transport planner, route planner England, car route, car planner, timetable, train timetable, bus routes"/>
        <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeJourneyPlannerBlueBackground.gif" />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,Homepage.css,expandablemenu.css,nifty.css,map.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindTDAN" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="FindNearestAccessibleStop" method="post" runat="server">

            <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
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
                                                    <div class="panelBackTop">
                                                        <cc1:TDButton ID="commandBack" runat="server"></cc1:TDButton>
                                                    </div>
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
                                                        <a name="FindTDAN"></a>
                                                        <cc1:TDImage ID="imageJourneyPlanner" runat="server" Width="50" Height="65"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelFindNearestAccessibleStopTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
    
                                        <asp:UpdatePanel ID="updateErrorPanel" runat="server" UpdateMode="Always" >
                                            <ContentTemplate>
                                                <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                                    <div class="boxtypeerrormsgfour">
                                                        <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt boxmarginbottom5">
                                                <asp:Label ID="labelSubHeading" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo boxpaddingright5">
                                                        <!-- From and To locations -->
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:UpdatePanel ID="updateInputPanel" runat="server" UpdateMode="Always" >
                                                                        <ContentTemplate>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr aria-live="assertive" aria-atomic="true">
                                                                                    <td class="findafromcolumn" align="right">
                                                                                        <asp:Label ID="labelOriginTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <uc1:FindTDANControl ID="originTDANControl" runat="server"></uc1:FindTDANControl>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr aria-live="assertive" aria-atomic="true">
                                                                                    <td class="findafromcolumn" align="right">
                                                                                        <asp:Label ID="labelDestinationTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <uc1:FindTDANControl ID="destinationTDANControl" runat="server"></uc1:FindTDANControl>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr aria-live="assertive" aria-atomic="true">
                                                                                    <td class="findafromcolumn" align="right">
                                                                                        <asp:Label ID="labelViaTitle" runat="server" CssClass="txtsevenb"></asp:Label>
                                                                                    </td>
                                                                                    <td colspan="2">
                                                                                        <uc1:FindTDANControl ID="viaTDANControl" runat="server"></uc1:FindTDANControl>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <uc1:AccessibleTransportTypesControl ID="accessibleTransportTypesControl" runat="server"></uc1:AccessibleTransportTypesControl>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <asp:Panel ID="pnlCJPUser" runat="server" Visible="false">
                                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td class="findafromcolumn" align="right">
                                                                                     </td>
                                                                                    <td colspan="2">
                                                                                        <asp:Label id="lblDistance" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                                                                                        <asp:TextBox ID="txtDistance" runat="server" Columns="10"></asp:TextBox>
                                                                                        <cc1:TDButton ID="btnUpdate" runat="server" />
                                                                                    </td>
                                                                                </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    
                                                                    <div class="floatright floatrightPadding4">
                                                                        <cc1:TDButton ID="commandSubmit" runat="server"></cc1:TDButton>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <asp:UpdatePanel ID="updateMapPanel" runat="server" UpdateMode="Always" >
                                                        <ContentTemplate>
                                                            <div id="fnAccessibleStopMap" runat="server" class="fnAccessibleStopMap">
                                                                <a id="Map"></a>
                                                                <uc1:MapNearestControl ID="mapNearestControl" runat="server" Visible="false"></uc1:MapNearestControl>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
                                        <asp:Panel ID="TDAdditionalInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
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
