<%@ Register TagPrefix="uc1" TagName="ResultsSummaryControl2" Src="../Controls/ResultsSummaryControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Page language="c#" Codebehind="JourneyEmissionsCompareJourney.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.JourneyEmissionsCompareJourney" validateRequest="false" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsCompareControl" Src="../Controls/JourneyEmissionsCompareControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css, homepage.css, expandablemenu.css, nifty.css, jpstd.css, emissions.css, JourneyEmissionsCompareJourney.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="JourneyEmissionsCompareJourney" method="post" runat="server">
			<a href="#MainContent">
				<cc1:tdimage id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks" enableviewstate="false"></cc1:tdimage></a>
			<uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
			<table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
				<tr valign="top">
					<% /* Left Hand Navigaion Bar */ %>
					<td class="LeftHandNavigationBar" valign="top">
						<uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False" CategoryCssClass="HomePageMenu"></uc1:expandablemenucontrol>
						<div class="HomepageBookmark">
							<uc1:clientlinkcontrol id="clientLink" runat="server" EnableViewState="False"></uc1:clientlinkcontrol>
						</div>
					</td>
					<% /* Page Content */ %>
					<td>
						<cc1:roundedcornercontrol id="mainbit" runat="server" innercolour="White" outercolour="#CCECFF" corners="TopLeft"
							cssclass="bodyArea"> <% /* Main content control table */ %>
							<table id="mainArea" cellspacing="0" width="100%" border="0">
								<tr valign="top"> <% /* Main page content */ %>
									<td>
										<table cellspacing="0" width="623" border="0">
											<tr>
												<td>
													<div id="boxtypeeightalt">
														<a name="MainContent"></a>
														<table cellspacing="0" cellpadding="0" width="100%" border="0">
														    <tr>
														        <td>
														            <a name="BackButton"></a>
														            <div id="miniHeaderHolder">
															            <div id="backButtonHolder">
															                <cc1:tdbutton id="buttonBack" runat="server"></cc1:tdbutton>
															            </div>
															            <div id="otherButtonHolder">
															                    <uc1:printerfriendlypagebuttoncontrol id="printerFriendlyControl" runat="server"></uc1:printerfriendlypagebuttoncontrol>
																	            <cc1:helpbuttoncontrol id="helpJourneyEmissions" runat="server"></cc1:helpbuttoncontrol>
															            </div>
            															
															            <div id="labelHolder">
															                    <h1>
																		            <asp:label id="labelTitle" runat="server" enableviewstate="false"></asp:label>
																	            </h1>
															            </div>
														            </div>
														        </td>
															</tr>
															<tr>
																<td><img height="5" src="/web2/App_Themes/TransportDirect/images/gifs/spacer.gif" width="5" alt="" /></td>
															</tr>
															<tr>
																<td colspan="2">
																	<asp:panel id="panelSummary" runat="server" cssclass="boxtypelargetwo">
																		<div class="boxtypetwentyfive">
																			<uc1:resultstabletitlecontrol id="resultsTableTitleControl" runat="server"></uc1:resultstabletitlecontrol></div>
																		<div class="boxtypetwentysix">
																			<uc1:resultssummarycontrol2 id="resultsSummaryControl" runat="server"></uc1:resultssummarycontrol2></div>
																	</asp:panel></td>
															</tr>
															<tr>
																<td><img height="5" src="/web2/App_Themes/TransportDirect/images/gifs/spacer.gif" width="5" alt="" /></td>
															</tr>
															<tr>
																<td colspan="2">
																	<uc1:JourneyEmissionsCompareControl id="journeyEmissionsCompareControlOutward" runat="server"></uc1:JourneyEmissionsCompareControl></td>
															</tr>
															<tr>
																<td><img height="5" src="/web2/App_Themes/TransportDirect/images/gifs/spacer.gif" width="5" alt="" /></td>
															</tr>
															<tr>
																<td>
																	<%/* <cc1:tdbutton id="buttonBack" runat="server"></cc1:tdbutton>*/%>
															    </td>
																<td align="right">
																	<cc1:tdbutton id="buttonNext" runat="server"></cc1:tdbutton></td>
															</tr>
														</table>
													</div>
												</td>
											</tr>
										</table>
									</td> <% /* White Space Column */ %>
									<td class="WhiteSpaceBetweenColumns"></td> 
									<% /* Information Right Hand Column */ %>
									<td class="HomepageMainLayoutColumn4" valign="top">
									    <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
										<% /* Information Control */ %>
										<asp:panel id="TDInformationHtmlPlaceholderControl" runat="server" ></asp:panel>  
										<% /* Information Links Control */ %>
										<div class="JourneyEmissionInformationColumn">
										<div class="Column3Header">
											<div class="txtsevenbbl">
												<asp:label id="labelInformationLinks" runat="server" enableviewstate="false"></asp:label></div>
											&nbsp;&nbsp;&nbsp;
										</div>										
										<div class="Column3Content">
											<uc1:expandablemenucontrol id="informationLinksControl" runat="server" EnableViewState="False" CategoryCssClass="JourneyEmissionsPageRelatedLinks"></uc1:expandablemenucontrol></div>
										</div>
										<div style="clear:both;"></div>
									</td>
								</tr>
							</table>
						</cc1:roundedcornercontrol>
					</td>
				</tr>
			</table>
			<uc1:footercontrol id="Footercontrol" runat="server" EnableViewState="False"></uc1:footercontrol>
		</form>
		</div>
	</body>
</html>
