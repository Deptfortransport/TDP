<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsViewSelectionControl" Src="../Controls/ResultsViewSelectionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl" Src="../Controls/ResultsSummaryControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExtendJourneyLineControl" Src="../Controls/ExtendJourneyLineControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ExtensionOutputNavigationControl" Src="../Controls/ExtensionOutputNavigationControl.ascx" %>
<%@ Page language="c#" Codebehind="ExtendedFullItinerarySummary.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.ExtendedFullItinerarySummary" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,ExtendedFullItinerarySummary.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="ExtendedFullItinerarySummary" method="post" runat="server">
			<a href="#MainContent">
				<cc1:tdimage id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks"></cc1:tdimage></a>
			<a href="#ViewSelection">
				<cc1:tdimage id="imageMainContentSkipLink2" runat="server" cssclass="skiptolinks"></cc1:tdimage></a>
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                       
                        <div class="HomepageBookmark">
                            <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False" Visible="False"></uc1:ClientLinkControl>
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
                                        <table id="topofpagecontrols" lang="en" cellspacing="0" border="0">
                                            <tr>
                                                <td>
                                                    <div style="float:left;"></div>
                                                </td>
                                                <td align="left">
                                                    <cc1:tdbutton id="newJourneyButton" enableviewstate="false" runat="server"></cc1:tdbutton>                                                    
                                                </td>
                                                <td align="right">
                                                <uc1:printerfriendlypagebuttoncontrol id="printerFriendlyControl" runat="server"></uc1:printerfriendlypagebuttoncontrol>
					                            <cc1:helpbuttoncontrol id="helpButton" runat="server"></cc1:helpbuttoncontrol>
                                                <% /*
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
                                                 */ %>
                                                </td>
                                            </tr>
                                            <tr>
                                              <td>
                                                <h1><asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>  
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
                                                    <a name="MainContent"></a>
			                                        <div class="boxtypesixteen">
				                                        <h1><asp:label id="labelTitle" cssclass="ExtendedLabels" enableviewstate="false" runat="server"></asp:label></h1>
			                                        </div>
			                                        <div class="boxtypesixteen">
			                                            <asp:label id="labelIntroductoryText" runat="server" enableviewstate="false" cssclass="txtseven"></asp:label>
			                                        </div>
			                                        <div class="boxtypewhitebackground">
				                                        <uc1:extendjourneylinecontrol id="extendJourneyLineControl" visible="true" runat="server"></uc1:extendjourneylinecontrol>
			                                        </div>
			                                        <div class="boxtypefortyone">
                                                            <table cellspacing="0" cellpadding="0" width="100%" summary="Journey details menu">
                                                                <tr>
                                                                    <td valign="bottom" align="right">
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td valign="bottom" align="right">
                                                                                    <uc1:ExtensionOutputNavigationControl ID="theOutputNavigationControl" runat="server">
                                                                                    </uc1:ExtensionOutputNavigationControl>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
			                                        <asp:panel id="summaryPanel" cssclass="boxtypefortyfour" runat="server">
				                                        <asp:panel id="combinedResultsSummaryPanel" runat="server">
					                                        <div class="boxtypefortytwo">
					                                           
						                                        <uc1:resultstabletitlecontrol id="resultsTableTitleControl" runat="server"></uc1:resultstabletitlecontrol>
						                                        
						                                    </div>
					                                        <div class="boxtypefortythree">
						                                        <uc1:resultssummarycontrol id="combinedResultsSummaryControl" runat="server"></uc1:resultssummarycontrol></div>
				                                        </asp:panel>
				                                        
					                                        <uc1:resultsviewselectioncontrol id="resultsViewSelectionControl" runat="server" Visible="false"></uc1:resultsviewselectioncontrol>
			                                        </asp:panel>
			                                        <div class="OnwardJourneyBox">
			                                             <cc1:TDButton ID="addAnotherOnwardJourney" runat="server" />
			                                        </div>
			                                        <br />
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
			<uc1:footercontrol id="FooterControl" runat="server"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>
