<%@ Register TagPrefix="uc1" TagName="CarAllDetailsControl" Src="../Controls/CarAllDetailsControl.ascx" %>
<%@ Page language="c#" Codebehind="CarDetails.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.CarDetails" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ExtendAdjustReplan.css,expandablemenu.css,nifty.css,homepage.css,CarDetails.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="CarDetails" method="post" runat="server">
			<!-- header -->
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			
			 <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                                                CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table class="mainArea" lang="en" cellspacing="0" width="100%" border="0">
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
                                                   <div id="boxtypelargeeight">
				                                    
				                                    <div class="HeaderButtons" align="right">
					                                    
					                                    <uc1:printerfriendlypagebuttoncontrol id="printerFriendlyControl" runat="server"></uc1:printerfriendlypagebuttoncontrol>
					                                    <cc1:helpbuttoncontrol id="helpButton" runat="server"></cc1:helpbuttoncontrol>
				                                    </div>
				                                    <div style="float:left">
				                                    <cc1:tdbutton id="backButton" runat="server"></cc1:tdbutton>
				                                    </div>
				                                    <br /><br />
				                                    
				                                    <h1>
					                                    <asp:label id="labelTitle" CssClass="HeaderLabels" runat="server" EnableViewState="False"></asp:label>
				                                    </h1>
				                                    
				                                    <div class="clearText">
				                                    <asp:label id="labelIntroductoryText" runat="server" cssclass="txtseven" EnableViewState="False"></asp:label>
				                                    </div>
			                                        </div>
			                                        <div class="boxtypetwelverefine">
				                                        <div id="dmtitle">
					                                    <table cellpadding="0" cellspacing="0" width="100%">
						                                    <tr>
							                                <td><asp:label id="labelDirection" runat="server" cssclass="txteightb" EnableViewState="False"></asp:label>
								                            <asp:label id="labelSummary" runat="server" cssclass="txteight" EnableViewState="False"></asp:label></td>
							                                <td align="right"><cc1:tdbutton id="buttonCompareEmissions" runat="server"></cc1:tdbutton></td>
						                                    </tr>
					                                        </table>			
				                                    </div>
				                                    <uc1:caralldetailscontrol id="carAllDetailsControlOutward" runat="server"></uc1:caralldetailscontrol>
			                                        </div>
                                                    <% /* End: Content to be replaced when white labelling */ %>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <!--<td class="WhiteSpaceBetweenColumns">
                                    </td>-->
                                    <!-- Information Column -->
                                    <!--<td class="HomepageMainLayoutColumn4" valign="top">
                                        <div class="Column3Header">
                                            <div class="txtsevenbbl">
                                                <asp:Label ID="labelRelatedLinks" runat="server" EnableViewState="false"></asp:Label></div>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </div>
                                    </td>-->
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
               
            </table>
			<div>
                <uc1:footercontrol id="footerControl" runat="server" EnableViewState="False"></uc1:footercontrol>
			</div>
		</form>
		</div>
	</body>
</html>
