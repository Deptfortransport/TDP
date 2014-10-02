<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StopServiceDetails.aspx.cs" Inherits="TransportDirect.UserPortal.Web.JourneyPlanning.StopServiceDetails" %>

<%@ Register TagPrefix="uc1" TagName="ServiceHeaderControl" Src="../Controls/ServiceHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CallingPointsControl" Src="../Controls/CallingPointsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,homepage.css,expandablemenu.css,nifty.css,ServiceDetails.aspx.css"></cc1:headelementcontrol>
	</head>
	<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="ServiceDetails" method="post" runat="server">
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
                            <table lang="en" id="maincontenttable" cellspacing="0" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table id="topofpagecontrols" lang="en" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left">
                                                <div style="float:left;width:3px"></div> 
                                                <cc1:tdbutton id="buttonBack" runat="server" EnableViewState="False" style="vertical-align: top"></cc1:tdbutton>
                                                    <h1>
                                                        <asp:Label ID="labelFindPageTitle" runat="server"></asp:Label></h1>
                                                </td>
                                                <td align="right">                                    
					                                <uc1:printerfriendlypagebuttoncontrol id="Printerfriendlypagebuttoncontrol2" runat="server" EnableViewState="False"></uc1:printerfriendlypagebuttoncontrol>
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
                                        <div id="StopServiceDetailDiv" runat="server">
                                            <table lang="en" cellspacing="0" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <% /* Start: Content to be replaced when white labelling */ %>
                                                        <div id="boxtypeeightstd">
				                                        <h2>
					                                        <asp:label id="labelServiceDetailsTitle" cssclass="HeaderLabels" runat="server" EnableViewState="False"></asp:label>
				                                        </h2>
				                                        <br />
				                                        </div>
			                                        <div id="boxtypetwelve">
				                                        <div id="dmtitle"><h3><asp:Label id="serviceHeaderLabel" runat="server" EnableViewState="False">Label</asp:Label></h3></div>
				                                        <div id="dmview"><br/>
					                                        <uc1:callingpointscontrol id="callingPointsControlBefore" runat="server" EnableViewState="False"></uc1:callingpointscontrol><br/>
					                                        <uc1:callingpointscontrol id="callingPointsControlLeg" runat="server" EnableViewState="False"></uc1:callingpointscontrol><br/>
					                                        <uc1:callingpointscontrol id="callingPointsControlAfter" runat="server" EnableViewState="False"></uc1:callingpointscontrol><br/>
    					                                   
				                                        </div>
			                                        </div>
                                                        <% /* End: Content to be replaced when white labelling */ %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
				<uc1:footercontrol id="FooterControl1" runat="server" EnableViewState="False"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>

