<%@ Page language="c#" Codebehind="JourneyAdjust.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyAdjust" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AdjustOptionsControl" Src="../Controls/AdjustOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RefinePageOptionsControl" Src="../Controls/RefinePageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyAdjustSegmentControl" Src="../Controls/JourneyAdjustSegmentControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyAdjustTableGridControl" Src="../Controls/JourneyAdjustTableGridControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,JourneyAdjust.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="JourneyAdjust" method="post" runat="server">
			<a href="#MainContent">
				<cc1:tdimage id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks"></cc1:tdimage></a>
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
                                                <td align="left"><div style="width:2px;float:left;"></div>
                                                <cc1:tdbutton id="cancelButton" runat="server" enableviewstate="false"></cc1:tdbutton>&nbsp;
                                                    <cc1:TDButton ID="newJourneyButton" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    <h1>
                                                        <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="Helpbuttoncontrol1" runat="server" EnableViewState="False">
                                                    </cc1:HelpButtonControl>
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
				                                        <h1><asp:label id="labelTitle" runat="server" cssclass="ExtendedLabels" enableviewstate="false"></asp:label></h1>
				                                        <% /*
				                                        <div class="ExtendedButtons">
					                                        <cc1:helpbuttoncontrol id="helpButton" runat="server"></cc1:helpbuttoncontrol>
				                                        </div>
				                                        */ %>
				                                        <br /><asp:label id="labelIntroductoryText" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
			                                        </div>

			                                        <asp:panel id="adjustOptionsControlPanel" runat="server">
				                                        <div class="boxtypelargetwomargintop">
					                                        <uc1:adjustoptionscontrol id="adjustOptionsControl" runat="server"></uc1:adjustoptionscontrol>
				                                        </div>
			                                        </asp:panel>
                                        			
			                                        <div class="boxtypetwelverefine">
				                                        <div id="dmtitle">
					                                        <div class="floatleftonly">
						                                        <span class="txteightb">
							                                        <asp:label id="labelJourneyDirection" runat="server" enableviewstate="false"></asp:label>
						                                        </span>
					                                        </div>
					                                        <div class="floatrightonly">
						                                        <cc1:tdbutton id="buttonShowTableDiagram" runat="server" causesvalidation="false"></cc1:tdbutton>
					                                        </div>&nbsp;
				                                        </div>
				                                        <div id="dmview"><!-- This comment to fix IE DIV bug. Do not remove! -->
					                                        <uc1:journeyadjustsegmentcontrol id="journeyAdjustSegmentControl" runat="server"></uc1:journeyadjustsegmentcontrol>
					                                        <uc1:journeyadjusttablegridcontrol id="journeyAdjustTableGridControl" runat="server"></uc1:journeyadjusttablegridcontrol>
				                                        </div>
			                                        </div>

			                                        <uc1:refinepageoptionscontrol id="pageOptionsControl" runat="server" Visible="false"></uc1:refinepageoptionscontrol>
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
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>
