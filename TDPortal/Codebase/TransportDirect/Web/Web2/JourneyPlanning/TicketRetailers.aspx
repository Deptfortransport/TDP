<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyFareHeadingControl" Src="../Controls/JourneyFareHeadingControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RetailerMatrixControl" Src="../Controls/RetailerMatrixControl.ascx" %>
<%@ Page language="c#" Codebehind="TicketRetailers.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.TicketRetailers"%>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TicketMatrixControl" Src="../Controls/TicketMatrixControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindFareStepsControl" Src="../Controls/FindFareStepsControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,ticketRetailers.css,homepage.css,expandablemenu.css,nifty.css,TicketRetailers.aspx.css"></cc1:headelementcontrol>
	</head>
	<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
	<div class="CenteredContent">
		<form id="TicketRetailers" method="post" runat="server">
				<a href="#MainContent"><asp:image id="imageMainContentSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a>
				<asp:panel id="panelOutwardJourneyTicketsSkipLink1" runat="server" Visible="False"><a href="#OutwardJourneyTickets"><asp:image id="imageOutwardJourneyTicketsSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
				<asp:panel id="panelOutwardJourneyRetailersSkipLink1" runat="server" Visible="False"><a href="#OutwardJourneyRetailers"><asp:image id="imageOutwardJourneyRetailersSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
				<asp:panel id="panelReturnJourneyTicketsSkipLink1" runat="server" Visible="False"><a href="#ReturnJourneyTickets"><asp:image id="imageReturnJourneyTicketsSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
				<asp:panel id="panelReturnJourneyRetailersSkipLink1" runat="server" Visible="False"><a href="#ReturnJourneyRetailers"><asp:image id="imageReturnJourneyRetailersSkipLink1" runat="server" cssclass="skiptolinks"></asp:image></a></asp:panel>
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
                            <table id="maincontenttable" lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div id="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxjourneychangesearchcontrol">
				                                        <uc1:journeychangesearchcontrol id="theJourneyChangeSearchControl" helplabel="TicketRetailersHelpLabel" runat="server"></uc1:journeychangesearchcontrol>
			                                        </div>

                                                    <uc1:journeyfareheadingcontrol id="journeyFareHeadingControl" runat="server"></uc1:journeyfareheadingcontrol>
				                                    <cc1:helplabelcontrol id="TicketRetailersHelpLabel" runat="server" visible="False" cssmaintemplate="helpboxoutput"></cc1:helplabelcontrol>
				                                    
                                                    <asp:Panel ID="panelFindFareSteps" runat="server" visible="false">
                                                        <uc1:FindFareStepsControl ID="findFareStepsControl" runat="server" visible="false"></uc1:FindFareStepsControl>
                                                    </asp:Panel>
                                                    
			                                        <div class="boxtypeeightstd">
			                                            <uc1:journeyplanneroutputtitlecontrol id="journeyPlannerOutputTitle" runat="server"></uc1:journeyplanneroutputtitlecontrol>
			                                        </div>
			                                        
			                                        <div class="boxtypeeightstd">
			                                            <asp:label id="outwardTicketNotes" runat="server" cssclass="txtseven" width="755" enableviewstate="False"></asp:label>
			                                        </div>
			                                        
			                                        <asp:panel id="panelOutward" runat="server">
				                                        <a name="OutwardJourneyTickets"></a>
				                                        <uc1:ticketmatrixcontrol id="outwardTickets" runat="server"></uc1:ticketmatrixcontrol>
				                                        <asp:panel id="outwardRetailerPanel" runat="server">
					                                        <div class="boxtypeeightstd">
					                                            <asp:label id="outwardRetailerNotes" runat="server" enableviewstate="False" width="755" cssclass="txtseven"></asp:label>
                                                            </div>
					                                        <a name="OutwardJourneyRetailers"></a>
					                                        <uc1:retailermatrixcontrol id="outwardRetailers" runat="server"></uc1:retailermatrixcontrol>
				                                        </asp:panel>
				                                        <br />
			                                        </asp:panel>
			                                        <asp:panel id="panelInward" runat="server">
				                                        <a name="ReturnJourneyTickets"></a>
				                                        <uc1:ticketmatrixcontrol id="inwardTickets" runat="server"></uc1:ticketmatrixcontrol>
				                                        <asp:panel id="inwardRetailerPanel" runat="server">
					                                        <div class="boxtypeeightstd">
                                                                <asp:label id="inwardRetailerNotes" runat="server" enableviewstate="False" width="755" cssclass="txtseven"></asp:label>
						                                    </div>
					                                        <a name="ReturnJourneyRetailers"></a>
					                                        <uc1:retailermatrixcontrol id="inwardRetailers" runat="server"></uc1:retailermatrixcontrol>
				                                        </asp:panel>
			                                        </asp:panel>
			                                        <uc1:amendsavesendcontrol id="AmendSaveSendControl" runat="server"></uc1:amendsavesendcontrol>
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
