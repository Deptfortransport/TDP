<%@ Register TagPrefix="uc1" TagName="ZonalAirportOperatorControl" Src="../Controls/ZonalAirportOperatorControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TaxiInformationControl" Src="../Controls/TaxiInformationControl.ascx" %>

<%@ Page Language="c#" Codebehind="LocationInformation.aspx.cs" AutoEventWireup="false"
    Inherits="TransportDirect.UserPortal.Web.Templates.LocationInformation" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AvoidSelectControl" Src="../Controls/AvoidSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ZonalServiceLinksControl" Src="../Controls/ZonalServiceLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="../Controls/HyperlinkPostbackControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ZonalAccessibilityLinksControl" Src="../Controls/ZonalAccessibilityLinksControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateSelectControl" Src="../Controls/DateSelectControl.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css, homepage.css, nifty.css, expandablemenu.css, LocationInformation.aspx.css">
    </cc1:HeadElementControl>
</head>
<body dir="ltr">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#SkipToMain" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
     <div class="CenteredContent">
        <a href="#Summary">
            <img id="summaryPlannersSkipLink" runat="server" class="skiptolinks"></a>
        <form id="LocationInformation" method="post" runat="server">
            <uc1:HeaderControl ID="headerControl" runat="server">
            </uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:ExpandableMenuControl>
                        <uc1:ExpandableMenuControl ID="relatedLinksControl" runat="server" EnableViewState="False"
                            CategoryCssClass="HomePageMenu">
                        </uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <a name="SkipToMain"></a>
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <table lang="en" cellspacing="0" cellpadding="0" class="PageHeight">
                                <tr>
                                    <td valign="top">
                                        <div id="boxtypeeightstd1">
                                            <div class="boxtypeeightstd">
                                                <cc1:TDButton ID="buttonBack" runat="server" EnableViewState="false">
                                                </cc1:TDButton></div>
                                        </div>
                                        <div id="boxtypeeightstd">
                                            <h1>
                                                <asp:Label ID="labelStationInformationTitle" runat="server"></asp:Label><asp:Label
                                                    ID="labelLocationInformationTitle" runat="server"></asp:Label></h1>
                                        </div>
                                        <!-- main content -->
                                        <table>
                                            <tr>
                                                <td valign="top">
                                                    <%--  Removed side menu, links have been moved to within main body of page--%>
                                                    <%-- <div id="butl">
                                            <div>
                                                <uc1:HyperlinkPostbackControl ID="taxiHyperlinkPostbackControl" runat="server"></uc1:HyperlinkPostbackControl>
                                            </div>
                                            <div>
                                                <uc1:HyperlinkPostbackControl ID="faresHyperlinkPostbackControl" runat="server"></uc1:HyperlinkPostbackControl>
                                            </div>
                                            <div>
                                                <uc1:HyperlinkPostbackControl ID="airoperatorsHyperlinkPostbackControl" runat="server">
                                                </uc1:HyperlinkPostbackControl>
                                            </div>
                                            <div>
                                                <asp:Panel ID="panelGoTo" runat="server">
                                                    <hr />
                                                    <div id="heading">
                                                        <asp:Label ID="labelGoTo" runat="server"></asp:Label></div>
                                                   
                                                </asp:Panel>
                                            </div>
                                        </div>--%>
                                                </td>
                                                <td valign="top">
                                                    <div id="primcontentlocationinfo">
                                                        <div id="contentareaw1">
                                                           
                                                            <%--<asp:Panel ID="panelTaxiDetails" runat="server"></asp:Panel>--%>
                                                            <div id="hdtypethree">
                                                                <h2>
                                                                    <asp:Label ID="labelStationName" runat="server"></asp:Label></h2>
                                                            </div>
                                                             <asp:Label ID="labelErrorMessage" runat="server" Visible="False" CssClass="txtsevenb"></asp:Label>
                                                            <div>
                                                                <a name="Summary"></a>
                                                                <asp:Panel ID="localInfo" runat="server">
                                                                    <div id="hdtypefour2">
                                                                        
                                                                        <asp:Label ID="localInformation" runat="server"></asp:Label></div>
                                                                </asp:Panel>
                                                            </div>
                                                            <div>
                                                                <uc1:ZonalAccessibilityLinksControl ID="ZonalAccessibilityLinksControl1" runat="server"
                                                                    Visible="True">
                                                                </uc1:ZonalAccessibilityLinksControl>
                                                            </div>
                                                            <div>
                                                                <uc1:ZonalServiceLinksControl ID="ZonalServiceLinksControl1" runat="server" Visible="True">
                                                                </uc1:ZonalServiceLinksControl>
                                                            </div>
                                                            <asp:Panel ID="realTime" runat="server">
                                                                <div id="hdtypefour4">
                                                                    
                                                                    <asp:Label ID="realTimeInfo" runat="server"></asp:Label></div>
                                                            </asp:Panel>
                                                            <div class="locationInformationLinks" id="DepartBoardDiv" runat="server" visible="false">
                                                                <asp:HyperLink ID="DepartureBoardHyperLink" runat="server">
                                                                    <asp:Label ID="labelDepartureBoardNavigation" runat="server"></asp:Label></asp:HyperLink></div>
                                                            <div class="locationInformationLinks" id="ArrivalBoardDiv" runat="server" visible="false">
                                                                <asp:HyperLink ID="ArrivalsBoardHyperlink" runat="server">
                                                                    <asp:Label ID="labelArrivalsBoardNavigation" runat="server"></asp:Label></asp:HyperLink></div>
                                                            <asp:Panel ID="stationInfo" runat="server">
                                                                <div id="hdtypefour3">
                                                                    
                                                                    <asp:Label ID="stationFacilities" runat="server"></asp:Label></div>
                                                            </asp:Panel>
                                                            <div class="locationInformationLinks" id="StationAccessDiv" runat="server" visible="false">
                                                                <div>
                                                                    <uc1:ZonalAccessibilityLinksControl ID="StationZonalAccessibilityLinks" runat="server"
                                                                        Visible="True">
                                                                    </uc1:ZonalAccessibilityLinksControl>
                                                                </div>

                                                                <asp:HyperLink ID="StationAccessibilityLink" runat="server" Text="Accessibility information"></asp:HyperLink></div>
                                                            <div class="locationInformationLinks" id="FurtherDetailsDiv" runat="server" visible="false">
                                                                <asp:HyperLink ID="FurtherDetailsHyperLink" runat="server">
                                                                    <asp:Label ID="labelFurtherDetailsNavigation" runat="server"></asp:Label></asp:HyperLink></div>
                                                            <asp:Panel ID="panelOperatorDetails" runat="server" Visible="True">
                                                                <asp:Panel ID="operatorInfo" runat="server">
                                                                    <div id="hdtypefour1">
                                                                        
                                                                        <asp:Label ID="labelOperator" runat="server"></asp:Label></div>
                                                                </asp:Panel>
                                                            </asp:Panel>
                                                            <uc1:ZonalAirportOperatorControl ID="ZonalAirportOperatorControl1" runat="server"
                                                                Visible="True">
                                                            </uc1:ZonalAirportOperatorControl>
                                                            <asp:Panel ID="taxi" runat="server">
                                                                <div id="hdtypefour">
                                                                    <a id="Taxis"></a>
                                                                    <asp:Label ID="labelSummaryTitle" runat="server"></asp:Label></div>
                                                            </asp:Panel>
                                                            
                                                            <uc1:TaxiInformationControl ID="TaxiInformationControl1" runat="server">
                                                            </uc1:TaxiInformationControl>
                                                        </div>
                                                    </div>
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
            <uc1:FooterControl ID="FooterControl1" runat="server">
            </uc1:FooterControl>
        </form>
    </div>
</body>
</html>
