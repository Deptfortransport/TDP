<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="True" validateRequest="false" Inherits="TransportDirect.UserPortal.Web.Templates.Maps.Home" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="~/Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="~/Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="~/Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="~/Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindAPlaceControl" Src="../Controls/FindAPlaceControl.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <meta name="description" content="Find your nearest transport links including airports, train stations &amp; coach stations. You can also find the nearest car parks." />
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,nifty.css,expandablemenu.css,MapsHome.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#FindAPlace" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="HomeFindAPlace" method="post" runat="server">
			<a href="#SideNavigation"><cc1:tdimage id="imageSideNavigationSkipLink1" runat="server" class="skiptolinks" /></a>
			<a href="#FindAPlace"><cc1:tdimage id="imageFindAPlaceSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#FindAStation"><cc1:tdimage id="imageFindAStationSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#TrafficMaps"><cc1:tdimage id="imageTrafficMapsSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#NetworkMaps"><cc1:tdimage id="imageNetworkMapsSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#FindCarPark"><cc1:tdimage id="imageFindCarParkSkipLink" runat="server" class="skiptolinks" /></a>
			<!-- header -->
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0">
				<tr>
					<td class="LeftHandNavigationBar" align="left" valign="top">
						<a name="SideNavigation"></a>
						<uc1:expandablemenucontrol id="expandableMenuControl" runat="server" CategoryCssClass="HomePageMenu"></uc1:expandablemenucontrol>
						<div class="HomepageBookmark">
							<uc1:clientlinkcontrol id="clientLink" runat="server"></uc1:clientlinkcontrol>
						</div>
					</td>
					<td valign="top">
						<cc1:roundedcornercontrol id="bodyArea" cssclass="bodyArea" runat="server" corners="TopLeft" outercolour="#CCECFF" innercolour="White">
							<table class="HomepageLayoutTable" cellspacing="0" cellpadding="0">
								<tr>
								    <td>
						                <table cellpadding="0" cellspacing="0">
						                    <tr>
						                        <td colspan="3">
						                            <div id="boxtypeeightstd"><h1><asp:literal id="literalPageHeading" runat="server" enableviewstate="False"></asp:literal></h1></div>
						                        </td>
						                    </tr>
							                    
							                    
                                    <tr valign="top">
                                        <td>
                                        <table>
                                        <tr>
		                                <td>
		                                    <div class="WhiteSpaceBetweenColumns"></div>
		                                </td>
		                                <td class="HomepageMainLayoutColumn1"><a name="FindAPlace"></a>
			                                <a name="FindAPlace"></a>
			                                <div class="Column1Header" id="FindAPlaceHeader" runat="server">
				                                <h2>
				                                <asp:hyperlink cssclass="Column1HeaderLink" id="hyperLink1" runat="server" enableviewstate="false">
				                                <asp:label cssclass="txtsevenbwl" id="labelFindAPlace" runat="server" enableviewstate="false"></asp:label>
				                                </asp:hyperlink>
				                                </h2>
				                                <asp:hyperlink class="txtsevenbwrlink" id="hyperLinkFindAPlaceMore" runat="server" enableviewstate="false"></asp:hyperlink><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size --> 
				                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			                                </div>
			                                <div class="clearboth"></div>
			                                <div class="Column1Content2" id="FindAPlaceDetail" runat="server">
				                                <uc1:findaplacecontrol id="findAPlaceControl1" runat="server"></uc1:findaplacecontrol>
			                                </div>
                                         </td>
                                         </tr>
                                         </table>
                                         </td>
                                            <td>
                                            <table>
                                            <tr valign="top">
                                                    <td class="HyperlinkTableCell">
					                                    <a name="FindAPlace"></a>
					                                    <asp:hyperlink id="hyperlinkFindAPlace" runat="server" enableviewstate="false">
						                                    <cc1:tdimage id="imageFindAPlace" runat="server" width="50" height="40"></cc1:tdimage><br />
						                                    <asp:literal id="literalFindAPlace" runat="server" enableviewstate="false"></asp:literal>
            		                                    </asp:hyperlink>
				                                    </td>
			            							<td class="HyperlinkTableCell">
						            					<a name="FindAStation"></a>
									            		<asp:hyperlink id="hyperlinkFindAStation" runat="server" enableviewstate="false">
												            <cc1:tdimage id="imageFindAStation" runat="server" width="70" height="36"></cc1:tdimage><br />
            												<asp:literal id="literalFindAStation" runat="server" enableviewstate="false"></asp:literal>
			            								</asp:hyperlink>
						            				</td>
									            	<td class="HyperlinkTableCell">
            											<a name="FindCarPark"></a>
			            								<asp:hyperlink id="hyperlinkFindCarPark" runat="server" enableviewstate="false">
						            						<cc1:tdimage id="imageFindCarPark" runat="server" width="36" height="36"></cc1:tdimage><br />
									            			<asp:literal id="literalFindCarPark" runat="server" enableviewstate="false"></asp:literal>
            											</asp:hyperlink>
			            							</td>
            									</tr>
			            						<tr valign="top">
						            				<td class="HyperlinkTableCell">
									            		<a name="TrafficMaps"></a>
            											<asp:hyperlink id="hyperlinkTrafficMaps" runat="server" enableviewstate="false">
			            									<cc1:tdimage id="imageTrafficMaps" runat="server" width="70" height="36"></cc1:tdimage><br />
						            						<asp:literal id="literalTrafficMaps" runat="server" enableviewstate="false"></asp:literal>
									            		</asp:hyperlink>
            										</td>
			            							<td class="HyperlinkTableCell">
						            					<a name="NetworkMaps"></a>
									            		<asp:hyperlink id="hyperlinkNetworkMaps" runat="server" enableviewstate="false">
												            <cc1:tdimage id="imageNetworkMaps" runat="server" width="70" height="36"></cc1:tdimage><br />
            												<asp:literal id="literalNetworkMaps" runat="server" enableviewstate="false"></asp:literal>
			            								</asp:hyperlink>
						            				</td>
									            	<td class="HyperlinkTableCell">
            										</td>
			            							<td class="HyperlinkTableCell">
						            				</td>
									            </tr>
									            </table>
									            </td>
									            </tr>
            									<tr>
			            							<td colspan="4" align="center">
						            			    	<asp:Panel ID="FindAPlaceInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
						            			    	<br />
									            	</td>
            									</tr>
			            					</table>
			            				</td>
			            			    <td class="HomepageMainLayoutColumn3" rowspan="3">
										    <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
											<asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
										</td>
                                    </tr>
                            </table>
						</cc1:roundedcornercontrol>
					</td>
				</tr>
			</table>	
			<uc1:footercontrol id="FooterControl1" runat="server" enableviewstate="false"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>