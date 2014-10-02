<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Page language="c#" Codebehind="HelpToolbar.aspx.cs" validateRequest="false" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.HelpToolbar" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css, HelpToolbar.aspx.css"></cc1:headelementcontrol>
			<asp:literal id="basehelpliteral" runat="server"></asp:literal>
	</head>
	<body>
	<div class="CenteredContent">
		<form id="HelpToolbar" method="post" runat="server">
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
                                        <table lang="en" cellspacing="0" width="100%" border="0" >
                                            <tr>
                                                <td>
                                                    <% /* Start: Content to be replaced when white labelling */ %>
                                                    <div id="boxtypeeight">
				                                        <table class="topRow" cellspacing="0" cellpadding="0" width="755">
					                                    <tr>
						                                <td>
							                            <div>
								                            <h1>Help</h1>
							                            </div>
						                                </td>
						                                <td align="right">
							                            <div class="boxprinterfriendly">
								                           <asp:HyperLink ID="PrinterFriendlyLink" runat="server" Target="_blank">
                                                                <asp:Label ID="hyperlinkText" CssClass="TDButtonHyperlink" runat="server">
                                                                </asp:Label>
                                                            </asp:HyperLink>
							                            </div>
						                                </td>
					                                    </tr>
				                                        </table>
			                                            </div>
			                                            <!--help -->
			                                            <div id="boxtypenine">
				                                            <asp:Panel ID="HelpBodyText" runat="server"></asp:Panel>
			                                            </div>
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
			<uc1:footercontrol id="FooterControl1" runat="server" enableviewstate="false"></uc1:footercontrol> 
			</form>
			</div>
	</body>
</html>
