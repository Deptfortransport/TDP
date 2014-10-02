<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>

<%@ Page Language="c#" Codebehind="RefineDetails.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.RefineDetails" %>

<%@ Register TagPrefix="uc1" TagName="JourneyBuilderControl" Src="../Controls/JourneyBuilderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css, homepage.css, expandablemenu.css,nifty.css,RefineDetails.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="RefineDetails" method="post" runat="server">
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
            <!-- header -->
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <% /* Start: Region to copy to include LH menu when white labelling */ %>
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
                            <table lang="en" cellspacing="0" width="100%" border="0" style="height: 595px">
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
                                                    <h1>
                                                        <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>
                                                </td>
                                                <td align="right">
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
                                                     <div class="ExtendedButtons">
                                                        <div class="backButtonPad">
                                                            <cc1:TDButton ID="backButton" runat="server"></cc1:TDButton>
                                                        </div>
                                                        <div class="buttonsPad">
                                                            <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                            <cc1:HelpButtonControl ID="helpButton" runat="server">
                                                            </cc1:HelpButtonControl>
                                                        </div>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <h1>
                                                            <asp:Label ID="labelTitle" CssClass="ExtendedLabels" runat="server"></asp:Label></h1>
                                                        
                                                        <br />
                                                        <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                       
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <div class="boxtypetwelverefine">
                                                            <div class="dmtitle">
                                                                <div class="floatright">
                                                                    &nbsp;
                                                                    <cc1:TDButton ID="buttonShowTableOutward" runat="server" CausesValidation="False"></cc1:TDButton></div>
                                                                <div>
                                                                    <span class="txteightb">
                                                                        <asp:Label ID="labelOutwardDirection" runat="server"></asp:Label>
                                                                    </span><span class="txteight">
                                                                        <asp:Label ID="labelOutwardSummary" runat="server"></asp:Label>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="dmview">
                                                                <!-- This comment to fix IE DIV bug. Do not remove! -->
                                                                <uc1:JourneyDetailsControl ID="journeyDetailsControlOutward" runat="server"></uc1:JourneyDetailsControl>
                                                                <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlOutward" runat="server">
                                                                </uc1:JourneyDetailsTableControl>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <div id="boxtypetwelve">
                                                            <div class="dmtitle">
                                                                <div class="floatright">
                                                                    &nbsp;
                                                                    <cc1:TDButton ID="buttonShowTableReturn" runat="server" CausesValidation="False"></cc1:TDButton></div>
                                                                <div>
                                                                    <span class="txteightb">
                                                                        <asp:Label ID="labelReturnDirection" runat="server"></asp:Label>
                                                                    </span><span class="txteight">
                                                                        <asp:Label ID="labelReturnSummary" runat="server"></asp:Label>
                                                                    </span>
                                                                </div>
                                                            </div>
                                                            <div class="dmview">
                                                                <!-- This comment to fix IE DIV bug. Do not remove! -->
                                                                <uc1:JourneyDetailsControl ID="journeyDetailsControlReturn" runat="server"></uc1:JourneyDetailsControl>
                                                                <uc1:JourneyDetailsTableControl ID="journeyDetailsTableControlReturn" runat="server">
                                                                </uc1:JourneyDetailsTableControl>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    </div>
                                                    <!--<div class="boxtypeJourneyBuilderWithPadding">-->
                                                    <uc1:JourneyBuilderControl ID="addExtensionControl" runat="server"></uc1:JourneyBuilderControl>
                                                    <uc1:ResultsFootnotesControl id="ResultsFootnotesControl1" runat="server"></uc1:ResultsFootnotesControl>
                                                    <% /* End: Content to be replaced when white labelling */ %>
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
            <% /* End: Region to copy to include LH menu when white labelling */ %>
            <uc1:FooterControl ID="FooterControl" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
