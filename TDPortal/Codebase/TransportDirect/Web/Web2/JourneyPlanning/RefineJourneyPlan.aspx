<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyOptionsControl" Src="../Controls/ExtendJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<%@ Page Language="c#" Codebehind="RefineJourneyPlan.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.RefineJourneyPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="homepage.css,nifty.css,expandablemenu.css,setup.css,jpstd.css,ExtendAdjustReplan.css,RefineJourneyPlan.aspx.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <a href="#MainContent">
                <asp:Image ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image>
            </a>
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="false"></uc1:ClientLinkControl>
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
                                                    <a name="ChangeJourney"></a>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:TDButton ID="buttonBack" runat="server"></cc1:TDButton>
                                                        <cc1:TDButton ID="buttonNewJourney" runat="server"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="buttonHelp" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="boxtypeeightalt">
                                            <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
                                        </div>
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="errorMessagePanel" runat="server" CssClass="boxtypeerrormsgthree">
                                            <div class="boxtypeeightalt">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl1" runat="server"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="MainContent"></a>
                                                    <asp:Panel ID="panelSummary" CssClass="boxtypetwo" runat="server">
                                                        <div class="boxtypetwentyfive">
                                                            <uc1:ResultsTableTitleControl ID="resultsTableTitleControl" runat="server"></uc1:ResultsTableTitleControl>
                                                        </div>
                                                        <div class="boxtypetwentysix">
                                                            <uc1:ResultsSummaryControl ID="ResultsSummaryControl1" runat="server"></uc1:ResultsSummaryControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelAdvancedPlanningOptionsPanel" runat="server">
                                                        <div class="boxtypetwo">
                                                            <uc1:ExtendJourneyOptionsControl ID="extendJourneyOptionsControl" runat="server"></uc1:ExtendJourneyOptionsControl>
                                                            <asp:Panel ID="replanPanel" runat="server">
                                                                <fieldset>
                                                                    <table>
                                                                        <tr>
                                                                            <td align="center" width="100" rowspan="3">
                                                                                <cc1:TDImage ID="replanImage" runat="server" alternatetext=" "></cc1:TDImage></td>
                                                                            <td colspan="5">
                                                                                <asp:Label ID="labelReplanTitle" runat="server" CssClass="txtsevenb"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <cc1:ScriptableGroupRadioButton ID="scriptableRadioButtonReplanOutward" runat="server"
                                                                                    GroupName="direction"></cc1:ScriptableGroupRadioButton></td>
                                                                            <td>
                                                                                <asp:Label ID="labelReplanOptionOne" runat="server" CssClass="txtseven"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <cc1:ScriptableGroupRadioButton ID="scriptableRadioButtonReplanReturn" runat="server"
                                                                                    GroupName="direction"></cc1:ScriptableGroupRadioButton></td>
                                                                            <td>
                                                                                <asp:Label ID="labelReplanOptionTwo" runat="server" CssClass="txtseven"></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </asp:Panel>
                                                            <p>&nbsp;</p>
                                                            <asp:Panel ID="adjustPanel" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center" width="100" rowspan="3">
                                                                            <cc1:TDImage ID="adjustImage" runat="server" alternatetext=" "></cc1:TDImage></td>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="labelAdjustTitle" runat="server" CssClass="txtsevenb"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:ScriptableGroupRadioButton ID="scriptableRadioButtonAdjustOutward" runat="server"
                                                                                GroupName="direction"></cc1:ScriptableGroupRadioButton></td>
                                                                        <td>
                                                                            <asp:Label ID="labelAdjustOptionOne" runat="server" CssClass="txtseven"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:ScriptableGroupRadioButton ID="scriptableRadioButtonAdjustReturn" runat="server"
                                                                                GroupName="direction"></cc1:ScriptableGroupRadioButton></td>
                                                                        <td>
                                                                            <asp:Label ID="labelAdjustOptionTwo" runat="server" CssClass="txtseven"></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <asp:Panel ID="amendPanel" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td align="center" width="100" rowspan="3">
                                                                            <cc1:TDImage ID="amendImage" runat="server" alternatetext=" "></cc1:TDImage></td>
                                                                        <td colspan="4">
                                                                            <asp:Label ID="labelAmendTitle" runat="server" CssClass="txtsevenb"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <cc1:ScriptableGroupRadioButton ID="scriptableRadioButtonAmend" runat="server" GroupName="direction">
                                                                            </cc1:ScriptableGroupRadioButton></td>
                                                                        <td>
                                                                            <asp:Label ID="labelAmendOptionOne" runat="server" CssClass="txtseven"></asp:Label></td>
                                                                    </tr>
                                                                    <tr id="rowAccessibleNote" runat="server" enableviewstate="false" visible="false">
                                                                        <td></td>
                                                                        <td>
                                                                            <asp:Label ID="labelAccessibleNote" runat="server" CssClass="txtseven" EnableViewState="false"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                            <div class="boxtypesevenalt">
                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <cc1:TDButton ID="buttonNext" runat="server"></cc1:TDButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <br />
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
