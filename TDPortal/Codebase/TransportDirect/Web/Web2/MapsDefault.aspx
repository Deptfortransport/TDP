<%@ Page language="c#" Codebehind="MapsDefault.aspx.cs" validateRequest = "false" AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.MapsDefault" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="~/Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="~/Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <link rel="canonical" href="http://www.transportdirect.info/Web2/mapsdefault.aspx" />
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css, FooterPages.css,MapsDefault.aspx.css"></cc1:headelementcontrol>
		<meta name="ROBOTS" content="NOODP" />
	    <meta name="description" content="Maps of rail, bus, coach and other transport networks throughout England, Scotland and Wales, including London Underground maps." />

	</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:HeaderControl ID="HeaderControl1" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <div id="mainbit" class="bodyArea" style="padding-top: 0pt;">
                            <!-- Main content control table -->
                            <table class="mainArea" lang="en" cellspacing="0" cellpadding="0" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <!--page title bar version 1-->
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <asp:Panel ID="Title_Text" runat="server">
                                                        </asp:Panel>
                                                    </div>
                                                    <!-- main content-->
                                                    <asp:Panel ID="Body_Text" runat="server">
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl" runat="server" EnableViewState="false"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
