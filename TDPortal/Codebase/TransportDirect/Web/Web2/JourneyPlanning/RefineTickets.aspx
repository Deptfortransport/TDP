<%@ Page Language="c#" Codebehind="RefineTickets.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.RefineTickets" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyTicketCostsControl" Src="../Controls/JourneyTicketCostsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyBuilderControl" Src="../Controls/JourneyBuilderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,RefineTickets.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="RefineTickets" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
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
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        
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
                                                    <a href="#MainContent">
                                                        <asp:Image ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks"></asp:Image></a>
                                                   <a name="MainContent"></a>
                                                   
                                                  
                                                    <div class="boxtypelargeeight">
                                                     <div style="float:left">
                                                        <cc1:TDButton ID="backButton" runat="server"></cc1:TDButton>
                                                   </div>
                                                         <div class="ExtendedButtons" style="float:right">
                                                            <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyControl" runat="server"></uc1:PrinterFriendlyPageButtonControl>
                                                            <cc1:HelpButtonControl ID="helpButton" runat="server">
                                                            </cc1:HelpButtonControl>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="boxtypelargeeight">
                                                    <br />
                                                        <asp:Label ID="labelTitle" CssClass="ExtendedLabels" runat="server"></asp:Label>
                                                       
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="labelIntroductoryText" runat="server" CssClass="txtseven"></asp:Label>
                                                    </div>
                                                    <div class="RefineTicketsOuterControl">
                                                        <div class="RefineTicketsOuterControlHeading">
                                                            <asp:Label ID="labelOutward" runat="server"></asp:Label>
                                                            <asp:Label ID="labelOutwardLocations" CssClass="LocationsTitleLabel" runat="server"></asp:Label>
                                                        </div>
                                                        <uc1:JourneyTicketCostsControl ID="outwardJourneyTicketCostsControl" runat="server">
                                                        </uc1:JourneyTicketCostsControl>
                                                    </div>
                                                    <asp:Panel ID="returnTicketsPanel" runat="server">
                                                        <div class="RefineTicketsOuterControl">
                                                            <div class="RefineTicketsOuterControlHeading">
                                                                <asp:Label ID="labelReturn" runat="server"></asp:Label>
                                                                <asp:Label ID="labelReturnLocations" CssClass="LocationsTitleLabel" runat="server"></asp:Label>
                                                            </div>
                                                            <uc1:JourneyTicketCostsControl ID="returnJourneyTicketCostsControl" runat="server"></uc1:JourneyTicketCostsControl>
                                                        </div>
                                                    </asp:Panel>
                                                    <!-- Start of Footnote Code -->
                                                    <table runat="server" id="tableFootnotes" class="txtseven">
                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="labelFootnote" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                •</td>
                                                            <td>
                                                                <asp:Label ID="labelThroughTicketingInfo" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div align="right" class="boxtypeJourneyBuilderWithPadding">
                                                        <uc1:JourneyBuilderControl ID="addExtensionControl" runat="server"></uc1:JourneyBuilderControl>
                                                    </div>
                                                    <!-- End of Footnote Code -->
                                                    <uc1:AmendSaveSendControl ID="AmendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
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
            <uc1:FooterControl ID="FooterControl" runat="server"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
