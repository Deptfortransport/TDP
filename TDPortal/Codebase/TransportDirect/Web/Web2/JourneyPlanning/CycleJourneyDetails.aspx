<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CycleJourneyDetails.aspx.cs" Inherits="TransportDirect.UserPortal.Web.Templates.CycleJourneyDetails" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="JourneyChangeSearchControl" Src="../Controls/JourneyChangeSearchControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneyPlannerOutputTitleControl" Src="../Controls/JourneyPlannerOutputTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="JourneysSearchedForControl" Src="../Controls/JourneysSearchedForControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsFootnotesControl" Src="../Controls/ResultsFootnotesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmendSaveSendControl" Src="../Controls/AmendSaveSendControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="OutputNavigationControl" Src="../Controls/OutputNavigationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ResultsTableTitleControl" Src="../Controls/ResultsTableTitleControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindSummaryResultControl" Src="../Controls/FindSummaryResultControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SocialBookMarkLinkControl" Src="../Controls/SocialBookMarkLinkControl.ascx" %>

<%@ Register TagPrefix="uc1" TagName="CycleAllDetailsControl" Src="../Controls/CycleAllDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapJourneyControl" Src="../Controls/MapJourneyControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="homepage.css,nifty.css,setup.css,jpstd.css,ExpandableMenu.css,CyclePlanner.css,map.css,CycleJourneyDetails.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="CycleJourneyDetails" runat="server">
         <asp:ScriptManager runat="server" ID="scriptManager1" EnablePageMethods ="true">
            <Services>
                <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
             </Services>
        </asp:ScriptManager>
        <div>
            <a href="#MainContent"><cc1:TDImage ID="imageMainContentSkipLink1" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage></a>
            <a href="#OutwardJourneyDetails"><cc1:TDImage ID="imageOutwardJourneySkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage></a>
            <a href="#ReturnJourneyDetails"><cc1:TDImage ID="imageReturnJourneySkipLink" runat="server" CssClass="skiptolinks" EnableViewState="false"></cc1:TDImage></a>
            
            <uc1:HeaderControl ID="headerControl" runat="server"></uc1:HeaderControl>
    
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                        <div class="SocialBookmark ExpandableMenu HomePageMenu">
                            <uc1:SocialBookMarkLinkControl ID="socialBookMarkLinkControl" runat="server" EnableViewState="False"></uc1:SocialBookMarkLinkControl>
                        </div>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        
                                        <table lang="en" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <a name="ChangeJourney"></a>
                                                    <div id="boxjourneychangesearchcontrol">
                                                        <uc1:JourneyChangeSearchControl ID="journeyChangeSearchControl" runat="server"></uc1:JourneyChangeSearchControl>
                                                    </div>
                                                    <div>
                                                        <uc1:JourneysSearchedForControl ID="journeysSearchedControl" runat="server"></uc1:JourneysSearchedForControl>
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <asp:Panel ID="errorMessagePanel" runat="server" Visible="False" EnableViewState="false">
                                                            <div class="boxtypeerrormsgtwo">
                                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                                            </div>
                                                        </asp:Panel>
                                                        
                                                                                                              
                                                        <% /* This is the control that says "Details" */ %>
                                                        <uc1:JourneyPlannerOutputTitleControl ID="journeyPlannerOutputTitleControl" runat="server"></uc1:JourneyPlannerOutputTitleControl>
                                                        
                                                    </div>
                                                    <div class="boxtypesixteen">
                                                        <table cellspacing="0" cellpadding="0" width="100%" summary="Journey details menu">
                                                            <tr>
                                                                <td valign="bottom" align="right">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td valign="bottom" align="right">
                                                                                <uc1:OutputNavigationControl ID="outputNavigationControl" runat="server"></uc1:OutputNavigationControl>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <a id="MainContent"></a>
                                                    <a id="outward"></a>
                                                    <asp:Panel ID="outwardPanel" runat="server">
                                                        <div class="boxtypeseventeen">
                                                            <table id="summaryOutwardTable" runat="server" class="jpsumoutfinda" enableviewstate="false">
                                                                <tr>
                                                                    <td class="titlewidth">
                                                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlOutward" runat="server"></uc1:ResultsTableTitleControl>
                                                                    </td>
                                                                    <td align="right">
                                                                        <asp:Panel id="hyperLinkPanelReturnJourneys" runat="server" visible="false" enableviewstate="false">
                                                                            <a class="jptablelink" href="#return">
                                                                            <asp:Label ID="hyperLinkReturnJourneys" runat="server" enableviewstate="false"></asp:Label>
                                                                            <cc1:TDImage ID="hyperLinkImageReturnJourneys" runat="server"></cc1:TDImage></a>
                                                                        </asp:Panel></td>
                                                                </tr>
                                                            </table>
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlOutward" runat="server" Visible="false"></uc1:FindSummaryResultControl>
                                                        </div>
                                                        <uc1:MapJourneyControl ID="mapJourneyControlOutward" runat="server" Visible="False"></uc1:MapJourneyControl>
                                                        <div class="clearboth"></div>
                                                        <a id="OutwardJourneyDetails"></a>
                                                        <div class="cycleJourneyDetailsPanel">
                                                            <uc1:CycleAllDetailsControl ID="cycleAllDetailsControlOutward" runat="server" Visible="false"></uc1:CycleAllDetailsControl>
                                                        </div>
                                                    </asp:Panel>
                                                    
                                                    <a id="return"></a>
                                                    <asp:Panel ID="returnPanel" runat="server">
                                                        <div class="boxtypeeighteen">
                                                            <table id="summaryReturnTable" runat="server" class="jpsumrtnfinda" enableviewstate="false">
                                                                <tr>
                                                                    <td class="titlewidth">
                                                                        <uc1:ResultsTableTitleControl ID="resultsTableTitleControlReturn" runat="server"></uc1:ResultsTableTitleControl>
                                                                    </td>
                                                                   <td align="right">
                                                                        <a class="jptablelink" href="#outward">
                                                                            <asp:Label ID="hyperLinkOutwardJourneys" runat="server" enableviewstate="false"></asp:Label>
                                                                            <cc1:TDImage ID="hyperLinkImageOutwardJourneys" runat="server"></cc1:TDImage></a>
                                                                   </td>
                                                                </tr>
                                                            </table>    
                                                            <uc1:FindSummaryResultControl ID="findSummaryResultTableControlReturn" runat="server" Visible="false"></uc1:FindSummaryResultControl>
                                                        </div>
                                                        <uc1:MapJourneyControl ID="mapJourneyControlReturn" runat="server" Visible="False"></uc1:MapJourneyControl>
                                                        <div class="clearboth"></div>
                                                        <a id="ReturnJourneyDetails"></a>
                                                        <div class="cycleJourneyDetailsPanel">
                                                            <uc1:CycleAllDetailsControl ID="cycleAllDetailsControlReturn" runat="server" Visible="false"></uc1:CycleAllDetailsControl>
                                                        </div>
                                                    </asp:Panel>

                                                    <uc1:ResultsFootnotesControl runat="server" ID="resultFootnotesControl"></uc1:ResultsFootnotesControl>
                                                    <br/>
                                                    <uc1:AmendSaveSendControl ID="amendSaveSendControl" runat="server"></uc1:AmendSaveSendControl>                                                  
                                                    
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
    
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
    
        </div>
        </form>
    </div>
</body>
</html>
