<%@ Page language="c#" Codebehind="Home.aspx.cs" AutoEventWireup="True" validateRequest="false" Inherits="TransportDirect.UserPortal.Web.Templates.Tools.Home" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="~/Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="~/Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="~/Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="~/Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<link rel="canonical" href="http://www.transportdirect.info/Web2/Tools/Home.aspx" />
	    <meta name="description" content="Transport Direct has a range of free travel planning tools, plus tips and resources. Try our useful tools today, like the CO2 journey checker and toolbar." />
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,nifty.css,expandablemenu.css,ToolsHome.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	    <div id="SkipToMainContentArea" class="SkipToMainContentArea">
	        <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	    </div>
       <div class="CenteredContent">
		<form id="HomeTipsTools" method="post" runat="server">
			<a href="#SideNavigation"><cc1:tdimage id="imageSideNavigationSkipLink1" runat="server" class="skiptolinks" /></a>
			<a href="#LinkToUs"><cc1:tdimage id="imageLinkToUsSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#ToolBarDownload"><cc1:tdimage id="imageToolBarDownloadSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#TDOnTheMove"><cc1:tdimage id="imageTDOnTheMoveSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#JourneyEmissionsPT"><cc1:tdimage id="imageJourneyEmissionsPTSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#BatchJourneyPlanner"><cc1:tdimage id="imageBatchJourneyPlannerSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#RelatedSites"><cc1:tdimage id="imageRelatedSitesSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#Faq"><cc1:tdimage id="imageFaqSkipLink" runat="server" class="skiptolinks" /></a>
			<a href="#DigiTv"><cc1:tdimage id="imageDigiTvSkipLink" runat="server" class="skiptolinks" /></a>
			<!-- header -->
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0">
				<tr>
					<td class="LeftHandNavigationBar" align="left" valign="top">
						<a name="SideNavigation"></a>
						<uc1:expandablemenucontrol id="expandableMenuControl" runat="server" CategoryCssClass="HomePageMenu"></uc1:expandablemenucontrol>
						<uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
						<div class="HomepageBookmark">
							<uc1:clientlinkcontrol id="clientLink" runat="server"></uc1:clientlinkcontrol>
						</div>
					</td>
					<td height="100%">
						<cc1:roundedcornercontrol id="bodyArea" cssclass="bodyArea" runat="server" corners="TopLeft" outercolour="#CCECFF" innercolour="White">
							<table class="HomepageLayoutTable" cellspacing="0" cellpadding="0">
							    <tr>
							        <td>
							            <table cellpadding="0" cellspacing="0">
							                <tr>
							                    <td colspan="4">
    							                    <a name="SkipToMain"></a>
							                        <div id="boxtypeeightstd"><h1><asp:literal id="literalPageHeading" runat="server" enableviewstate="False"></asp:literal></h1></div>
							                    </td>
							                </tr>
							                <tr>
							                    <td class="HyperlinkTableCell">
										            <a name="LinkToUs"></a>
               										<asp:hyperlink id="hyperlinkLinkToUs" runat="server" enableviewstate="false">
			    	    							    <cc1:tdimage id="imageLinkToUs" runat="server" width="70" height="36"></cc1:tdimage><br />
				    	    						    <asp:literal id="literalLinkToUs" runat="server" enableviewstate="false"></asp:literal>
							            			</asp:hyperlink>
            									</td>
            									<td class="HyperlinkTableCell">
            										<a name="ToolBarDownload"></a>
			            							<asp:hyperlink id="hyperlinkToolBarDownload" runat="server" enableviewstate="false">
						            					<cc1:tdimage id="imageToolBarDownload" runat="server" width="70" height="36"></cc1:tdimage><br />
									            		<asp:literal id="literalToolBarDownload" runat="server" enableviewstate="false"></asp:literal>
            										</asp:hyperlink>
            									</td>
			            						<td class="HyperlinkTableCell">
						            				<a name="TDOnTheMove"></a>
									            	<asp:hyperlink id="hyperlinkTDOnTheMove" runat="server" enableviewstate="false">
											            <cc1:tdimage id="imageTDOnTheMove" runat="server" width="70" height="36"></cc1:tdimage><br />
            											<asp:literal id="literalTDOnTheMove" runat="server" enableviewstate="false"></asp:literal>
			            							</asp:hyperlink>
						            			</td>
									            <td class="HyperlinkTableCell"><a name="JourneyEmissionsPT"></a>
            										<asp:hyperlink id="hyperlinkJourneyEmissionsPT" runat="server" enableviewstate="false">
			            								<cc1:tdimage id="imageJourneyEmissionsPT" runat="server" width="70" height="36"></cc1:tdimage><br />
						            					<asp:literal id="literalJourneyEmissionsPT" runat="server" enableviewstate="false"></asp:literal>
									            	</asp:hyperlink>
            									</td>
			  				                </tr>
							                <tr>
							                    <td class="HyperlinkTableCell">
            										<a name="BatchJourneyPlanner"></a>
			                							<asp:hyperlink id="hyperlinkBatchJourneyPlanner" runat="server" enableviewstate="false">
                											<cc1:tdimage id="imageBatchJourneyPlanner" runat="server" width="70" height="36"></cc1:tdimage><br />
				            			    				<asp:literal id="literalBatchJourneyPlanner" runat="server" enableviewstate="false"></asp:literal>
							            	    		</asp:hyperlink>
												</td>
												<td class="HyperlinkTableCell">
													<a name="RelatedSites"></a>
													<asp:hyperlink id="hyperlinkRelatedSites" runat="server" enableviewstate="false">
														<cc1:tdimage id="imageRelatedSites" runat="server" width="70" height="36"></cc1:tdimage><br />
														<asp:literal id="literalRelatedSites" runat="server" enableviewstate="false"></asp:literal>
													</asp:hyperlink>
												</td>
												<td class="HyperlinkTableCell">
													<a name="Faq"></a>
													<asp:hyperlink id="hyperlinkFaq" runat="server" enableviewstate="false">
														<cc1:tdimage id="imageFaq" runat="server" width="70" height="36"></cc1:tdimage><br />
														<asp:literal id="literalFaq" runat="server" enableviewstate="false"></asp:literal>
													</asp:hyperlink>
												</td>																	
												<td class="HyperlinkTableCell">
													<a name="DigiTv"></a>
													<asp:hyperlink id="hyperlinkDigiTv" runat="server" enableviewstate="false">
														<cc1:tdimage id="imageDigiTv" runat="server" width="36" height="36"></cc1:tdimage><br />
														<asp:literal id="literalDigiTv" runat="server" enableviewstate="false"></asp:literal>
													</asp:hyperlink>
												</td>
											</tr>
											<tr>
												<td align="center" colspan="4">
													<asp:Panel ID="TipsToolsInformationHtmlPlaceholderDefinition" CssClass="panelHeight" runat="server"></asp:Panel> <br />
												</td>
							                </tr>
							            </table>
							        </td>
							        <td class="HomepageMainLayoutColumn3">
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
