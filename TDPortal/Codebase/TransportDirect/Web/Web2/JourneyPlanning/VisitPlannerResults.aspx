<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="../Controls/MapJourneyControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsTableControl" Src="../Controls/JourneyDetailsTableControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyDetailsControl" Src="../Controls/JourneyDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<%@ Page Language="c#" Codebehind="VisitPlannerResults.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.VisitPlannerResults" %>

<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VisitPlannerRequestDetailsControl" Src="../Controls/VisitPlannerRequestDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="VisitPlannerJourneyOptionsControl" Src="../Controls/VisitPlannerJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,visitplanner.css,VisitPlannerResults.aspx.css,Map.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="VisitPlannerResults" method="post" runat="server">
            <asp:ScriptManager runat="server" ID="scriptManager1">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
            
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:expandablemenucontrol id="expandableMenuControl" runat="server" EnableViewState="False" CategoryCssClass="HomePageMenu" />
                       
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0" cellpadding="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Journey Planning Controls -->
                                        <a name="SkipToMain"></a>
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
                                                    <table id="boxtypeeightalt" cellspacing="0" cellpadding="0" width="100%" summary="Visit Planner Results Page">
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" width="100%" summary="Journey Changes">
                                                                     <tr>
                                                                        <td width="100%">
                                                                            <div id="boxjourneychangesearchcontrolfullwidth">
                                                                                <uc1:JourneyChangeSearchControl ID="journeyChangeSearchVisitResults" runat="server">
                                                                                </uc1:JourneyChangeSearchControl>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <div class="labelVisitResultsTitle">
                                                                                <h1>
                                                                                    <asp:Label ID="labelVisitResultsTitle" runat="server" EnableViewState="False"></asp:Label></h1>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                       
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:VisitPlannerRequestDetailsControl ID="visitPlannerJourneyDetailsControl" runat="server"
                                                                    EnableViewState="false"></uc1:VisitPlannerRequestDetailsControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" EnableViewState="false">
                                                                </uc1:ErrorDisplayControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:VisitPlannerJourneyOptionsControl ID="visitPlannerJourneyOptionsControl" runat="server">
                                                                </uc1:VisitPlannerJourneyOptionsControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="journeyDetailsPanel" runat="server">
                                                                    <table class="boxtyperesultsarea" cellpadding="0" cellspacing="0" summary="Journey Details">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="boxtyperesultstitle">
                                                                                    <asp:Label ID="labelDetailsTitle" runat="server" CssClass="txteightb"></asp:Label>&nbsp;<cc1:TDButton
                                                                                        ID="buttonCompareEmissions" runat="server"></cc1:TDButton></div>
                                                                                <uc1:JourneyDetailsControl ID="journeyDetailsVisitResults" runat="server" EnableViewState="false"
                                                                                    Visible="False"></uc1:JourneyDetailsControl>
                                                                                <uc1:JourneyDetailsTableControl ID="journeyDetailsTableVisitResults" runat="server"
                                                                                    EnableViewState="false" Visible="False"></uc1:JourneyDetailsTableControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                                <asp:Panel ID="journeyMapPanel" runat="server">
                                                                    <table class="boxtyperesultsarea">
                                                                        <tr>
                                                                            <td>
                                                                                <div class="boxtyperesultstitle">
                                                                                    <asp:Label ID="labelMapsTitle" runat="server" CssClass="txteightb"></asp:Label></div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <uc1:MapJourneyControl ID="mapJourneyControlOutward" runat="server"></uc1:MapJourneyControl>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:ResultsFootnotesControl runat="server" ID="footnotesControl" EnableViewState="false">
                                                                </uc1:ResultsFootnotesControl>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <uc1:AmendSaveSendControl ID="visitAmendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br/>
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
            <uc1:FooterControl ID="FooterControl1" runat="server" EnableViewState="false"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
