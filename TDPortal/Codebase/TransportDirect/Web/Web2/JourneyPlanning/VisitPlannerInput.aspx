<%@ Register TagPrefix="uc1" TagName="VisitPlannerLocationControl" Src="../Controls/VisitPlannerLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="DateDisplayControl" Src="../Controls/DateDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="../Controls/FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="CalendarControl" Src="../Controls/CalendarControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>

<%@ Page Language="c#" Codebehind="VisitPlannerInput.aspx.cs" AutoEventWireup="True"
    Inherits="TransportDirect.UserPortal.Web.Templates.VisitPlannerInput" %>

<%@ Register TagPrefix="uc1" TagName="PTPreferencesControl" Src="../Controls/PTPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TransportTypesControl" Src="../Controls/TransportTypesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmbiguousDateSelectControl" Src="../Controls/AmbiguousDateSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapInputControl" Src="../Controls/MapInputControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/JourneyPlanning/VisitPlannerInput.aspx" />
    <meta name="description" content="Plan a journey to two destinations with the Transport Direct day trip travel planner. Map your route online today." />
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,homepage.css,expandablemenu.css,nifty.css,map.css,VisitPlannerInput.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BackButton" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="VisitPlannerInput" method="post" runat="server">
            <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
                <Services>
                    <asp:ServiceReference Path="../webservices/TDMapWebService.asmx" />
                </Services>
            </asp:ScriptManager>
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
                        <cc1:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" CssClass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <!-- Top of Page Controls -->
                                        <table lang="en" cellspacing="0" width="630" border="0">
                                            <tr>
                                                <td>
                                                    <a name="BackButton"></a>
                                                    <asp:Panel ID="panelBackTop" runat="server" CssClass="panelBackTop">
                                                        <cc1:TDButton ID="commandBack" runat="server" EnableViewState="false"></cc1:TDButton>
                                                    </asp:Panel>
                                                </td>
                                                <td align="right">
                                                    <cc1:HelpButtonControl ID="helpControl" runat="server">
                                                    </cc1:HelpButtonControl>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <cc1:TDImage ID="imageVisitPlanner" runat="server" Width="70" Height="36"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label ID="labelVisitPlannerTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:Panel ID="panelErrorDisplayControl" runat="server" Visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControlSession" runat="server" Visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>        
                                        <asp:Panel ID="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label ID="labelFromToTitle" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorMessage" runat="server" Visible="False">
                                            <div id="boxtypeeightalt">
                                                <asp:Label ID="labelErrorMessages" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="panelErrorDisplayControl2" runat="server" Visible="false">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <div class="boxtypeeightalt">
                                            <asp:Label ID="labelVisitPlannerInstructional" runat="server" EnableViewState="False" CssClass="txtseven"></asp:Label>
                                        </div>
                                        <!-- Journey Planning Controls -->
                                        <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <div class="boxtypetwo">
                                                        <uc1:VisitPlannerLocationControl ID="visitPlannerLocationOrigin" runat="server"></uc1:VisitPlannerLocationControl>
                                                    </div>
                                                    <div class="boxtypetwo">
                                                        <uc1:VisitPlannerLocationControl ID="visitPlannerLocationVisitPlace1" runat="server"></uc1:VisitPlannerLocationControl>
                                                    </div>
                                                    <div class="boxtypetwo">
                                                        <uc1:VisitPlannerLocationControl ID="visitPlannerLocationVisitPlace2" runat="server"></uc1:VisitPlannerLocationControl>
                                                    </div>
                                                    <div class="boxtypetwo">
                                                        <uc1:FindLeaveReturnDatesControl ID="dateControl" runat="server" />
                                                        <uc1:FindPageOptionsControl ID="findPageOptionsControl" runat="server" />
                                                    </div>
                                                    <div class="mcMapInputBox">
                                                        <!-- Map -->
                                                        <a id="Map"></a>
                                                        <uc1:MapInputControl id="mapInputControl" runat="server" visible="false"></uc1:MapInputControl>
                                                    </div>
                                                    
                                                    <!-- Advanced options -->
                                                    <div class="boxtypeeightalt">
                                                        <h2>
                                                            <asp:Label ID="labelAdvanced" runat="server" EnableViewState="False"></asp:Label>
                                                        </h2>
                                                    </div>
                                                    <asp:Panel ID="panelTransportTypes" runat="server" Visible="False">
                                                        <div class="boxtypetwo">
                                                            <div class="txtseven">
                                                                <uc1:TransportTypesControl ID="transportTypesControl" runat="server"></uc1:TransportTypesControl>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                    <uc1:PTPreferencesControl ID="ptpreferencesControl" runat="server" />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="TDPageInformationHtmlPlaceHolderDefinition" runat="server" CssClass="SoftContentPanel" ScrollBars="None"></asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <!-- White Space Column -->
                                    <td class="WhiteSpaceBetweenColumns">
                                    </td>
                                    <!-- Information Column -->
                                    <td class="HomepageMainLayoutColumn3" valign="top">
                                        <uc1:PoweredBy ID="PoweredByControl" runat="server" EnableViewState="False"></uc1:PoweredBy>
                                        <asp:Panel ID="TDInformationHtmlPlaceholderDefinition" runat="server"></asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>            
        </form>
    </div>
</body>
</html>
