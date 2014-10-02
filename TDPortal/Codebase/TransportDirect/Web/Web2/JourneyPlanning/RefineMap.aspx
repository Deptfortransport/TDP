<%@ Page Language="c#" Codebehind="RefineMap.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.RefineMap" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyBuilderControl" Src="../Controls/JourneyBuilderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="../Controls/MapJourneyControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,Map.css,RefineMap.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="RefineMap" method="post" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" >
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <a href="#MainContent">
                <asp:Image ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <a name="SkipToMain"></a>
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:ExpandableMenuControl>
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
                                        
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="MainContent"></a>
                                                    <div class="boxtypelargeeight">
                                                        <div>
                                                            <div style="float:left">
                                                                <cc1:TDButton ID="backButton" runat="server"></cc1:TDButton>
                                                            </div>
                                                            <div style="float:right">
                                                                <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                                <cc1:HelpCustomControl ID="HelpControlRefineMap" runat="server" HelpLabel="helpLabelRefineMap"></cc1:HelpCustomControl>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <cc1:HelpLabelControl ID="helpLabelRefineMap" runat="server" Visible="False" CssMainTemplate="helpboxlocations"></cc1:HelpLabelControl>
                                                    <div class="boxtypelargeeight">
                                                        <h1>
                                                            <asp:Label ID="labelTitle"  runat="server"></asp:Label>
                                                        </h1>
                                                        
                                                        <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
                                                    </div>
                                                    <div class="spacer1">
                                                        &nbsp;</div>
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <uc1:MapJourneyControl ID="mapJourneyControlOutward" runat="server"></uc1:MapJourneyControl>
                                                    </asp:Panel>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <uc1:MapJourneyControl ID="mapJourneyControlReturn" runat="server"></uc1:MapJourneyControl>
                                                    </asp:Panel>
                                                    <div class="boxtypeJourneyBuilderWithPadding">
                                                        <uc1:JourneyBuilderControl ID="addExtensionControl" runat="server"></uc1:JourneyBuilderControl>
                                                    </div>
                                                    <br />
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
            <uc1:FooterControl ID="FooterControl" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
