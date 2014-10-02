<%@ Register TagPrefix="uc1" TagName="ExtendJourneyOptionsControl" Src="../Controls/ExtendJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmendStopoverControl" Src="../Controls/AmendStopoverControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarPreferencesControl" Src="../Controls/FindCarPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PTPreferencesControl" Src="../Controls/PTPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TransportModesControl" Src="../Controls/TransportModesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateLocationControl2" Src="../Controls/TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ItineraryViasControl" Src="../Controls/ItineraryViasControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyLineControl" Src="../Controls/ExtendJourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<%@ Page Language="c#" Codebehind="ExtendJourneyInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.ExtendJourneyInput" %>

<%@ Register TagPrefix="uc1" TagName="TransportTypesControl" Src="../Controls/TransportTypesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>"  xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" Stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,map.css,ExtendJourneyInput.aspx.css"
        runat="server"></cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            <a href="#MainContent">
                <cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
            <a href="#InputForm">
                <cc1:TDImage ID="imageMainContentSkipLink2" runat="server" CssClass="skiptolinks"></cc1:TDImage></a>
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
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:TDButton ID="buttonBack" runat="server" EnableViewState="False"></cc1:TDButton>&nbsp;
                                                        <cc1:TDButton ID="newJourneyButton" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="helpButton" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <cc1:TDImage ID="imageExtendJourney" runat="server" Width="70" Height="30"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelTitle" runat="server" CssClass="ExtendedLabels"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>    
                                            </div>
                                        </asp:Panel>    
                                        <div class="boxtypewhitebackground">
                                            <uc1:ExtendJourneyLineControl ID="extendJourneyLineControl" runat="server"></uc1:ExtendJourneyLineControl>
                                        </div>
                                        <asp:Panel ID="panelErrorMessage" runat="server" CssClass="boxtypeerrormsgthree">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Label ID="labelWarningMessages" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="InputForm"></a>
                                                    <asp:Panel ID="panelOrigin" runat="server">
                                                        <div class="boxtypetwo">
                                                            <table>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <uc1:ItineraryViasControl ID="originViasControl" runat="server" Visible="False"></uc1:ItineraryViasControl>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="originLocationTitleLabel" runat="server" CssClass="txtsevenb" EnableViewState="False"></asp:Label></td>
                                                                    <td>
                                                                        <uc1:TriStateLocationControl2 ID="originLocationControl" runat="server"></uc1:TriStateLocationControl2>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <uc1:ItineraryViasControl ID="destinationViasControl" runat="server" Visible="False">
                                                                        </uc1:ItineraryViasControl>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="destinationLocationTitleLabel" runat="server" CssClass="txtsevenb"
                                                                            EnableViewState="False"></asp:Label></td>
                                                                    <td>
                                                                        <uc1:TriStateLocationControl2 ID="destinationLocationControl" runat="server"></uc1:TriStateLocationControl2>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label ID="labelShow" runat="server" CssClass="txtsevenb" EnableViewState="false"></asp:Label></td>
                                                                    <td valign="bottom" align="left">
                                                                        <asp:CheckBox ID="checkBoxPublicTransport" runat="server" CssClass="txtseven" Checked="true">
                                                                        </asp:CheckBox>&nbsp;&nbsp;
                                                                        <asp:CheckBox ID="checkBoxCarRoute" runat="server" CssClass="txtseven" Checked="true">
                                                                        </asp:CheckBox>
                                                                        <asp:Label ID="labelTransport" runat="server" CssClass="txtseven" EnableViewState="False"
                                                                            Visible="False"></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelStopover" runat="server">
                                                        <div class="boxtypetwo">
                                                            <div>
                                                                <asp:Label ID="labelStopOverErrorMessage" runat="server" CssClass="txtseven"></asp:Label>
                                                            </div>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <uc1:AmendStopoverControl ID="amendStopoverControl" runat="server"></uc1:AmendStopoverControl>
                                                                        <uc1:FindPageOptionsControl ID="findPageOptionsControl" runat="server"></uc1:FindPageOptionsControl>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <div class="mcMapInputBox">
                                                        <!-- Map -->
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </div>
                                                    <asp:Panel ID="panelTransportTypes" runat="server">
                                                        <div class="boxtypesixalt">
                                                            <table>
                                                                <tr>
                                                                    <td class="jpthdl" align="left">
                                                                        <asp:Label ID="labelAdvanced" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>    
                                                        <div class="boxtypetwo">
                                                            <div class="txtseven">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <uc1:TransportTypesControl ID="transportTypesControl" runat="server"></uc1:TransportTypesControl>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="panelAdvancedOptions" runat="server">
                                                        <uc1:PTPreferencesControl ID="ptPreferencesControl" runat="server"></uc1:PTPreferencesControl>
                                                        <uc1:FindCarPreferencesControl ID="carPreferencesControl" runat="server"></uc1:FindCarPreferencesControl>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
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
