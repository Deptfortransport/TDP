<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Page language="c#" Codebehind="SiteMapDefault.aspx.cs" validateRequest = "false" AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.SiteMapDefault" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css,SiteMapDefault.aspx.css"></cc1:headelementcontrol>
		<meta name="ROBOTS" content="NOODP" />
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">	
		<form id="Form1" method="post" runat="server">
			<uc1:headercontrol id="HeaderControl1" runat="server"></uc1:headercontrol>
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Page Content -->
                    <td valign="top">
                        <div id="boxtypeeightsm">
				            <div class="boxtypeeightstd">
				                <a name="SkipToMain"></a>
					            <h1><asp:label id="lblSiteMapTitle" runat="server"></asp:label></h1>
				            </div>
				            <div id="boxtypeeightsmap">
					            <table width="100%" cellpadding="1" cellspacing="1">
						            <tr>
                                        <td width="20%"><div class="smh"><asp:Panel runat="server" id="QuickPlannersTitle"></asp:Panel></div></td>
							            <td width="20%"><div class="smh"><asp:Panel runat="server" id="JourneyPlannerTitle"></asp:Panel></div></td>
							            <td width="20%"><div class="smh"><asp:Panel runat="server" id="MapsTitle"></asp:Panel></div></td>
							            <td width="20%"><div class="smh"><asp:Panel runat="server" id="LiveTravelTitle"></asp:Panel></div></td>
							            <td width="20%"><div class="smh"><asp:Panel runat="server" id="TDOnTheMoveTitle"></asp:Panel></div></td>
						            </tr>
						            <tr>
							            <td valign="top"><div class="smc"><asp:Panel runat="server" id="QuickPlannersBody"></asp:Panel></div></td>
							            <td valign="top"><div class="smc"><asp:Panel runat="server" id="JourneyPlannerBody"></asp:Panel></div></td>
							            <td valign="top"><div class="smc"><asp:Panel runat="server" id="MapsBody"></asp:Panel></div></td>
							            <td valign="top"><div class="smc"><asp:Panel runat="server" id="LiveTravelBody"></asp:Panel></div></td>
							            <td valign="top"><div class="smc"><asp:Panel runat="server" id="TDOnTheMoveBody"></asp:Panel></div></td>
            						</tr>
            						
            					</table>
				            </div>	
			            		
			            </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                    <asp:Panel runat="server" ID="sitemapFooterNote"></asp:Panel>
            	    </td>
            	</tr>
            </table>
            
			<uc1:footercontrol id="FooterControl1" runat="server"></uc1:footercontrol>
        </form>			
    </div>
	</body>
</html>