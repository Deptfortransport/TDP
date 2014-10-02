<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Page language="c#" Codebehind="RetailerInformation.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.RetailerInformation" %>
<%@ Register TagPrefix="uc1" TagName="RetailerInformationControl" Src="../Controls/RetailerInformationControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css, homepage.css, nifty.css, expandablemenu.css, RetailerInformation.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="RetailerInformation" method="post" runat="server">		    
            <uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
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
                            <table class="mainArea" lang="en" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top">
                                        <!-- main content -->
                                        <a name="MainContent"></a>
                                        <div class="boxtypeeightstd">
			                                <cc1:tdbutton id="buttonBack" runat="server" enableviewstate="False"></cc1:tdbutton>
                                            </div>
                                        <div id="primcontentlocationinfo">
										<div><a name="Summary"></a>
														<uc1:retailerinformationcontrol id="retailerInformationControl" runat="server"></uc1:retailerinformationcontrol>
								</div>
							</div>
							 
                                            
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
