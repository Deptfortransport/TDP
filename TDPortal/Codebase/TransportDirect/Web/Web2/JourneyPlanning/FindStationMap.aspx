<%@ Register TagPrefix="cc2" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.Presentation.InteractiveMapping" Assembly="td.interactivemapping" %>
<%@ Register TagPrefix="uc1" TagName="MapDisabledControl" Src="../Controls/MapDisabledControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsTable" Src="../Controls/FindStationResultsTable.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindStationResultsLocationControl" Src="../Controls/FindStationResultsLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>

<%@ Page Language="c#" Codebehind="FindStationMap.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.FindStationMap" %>

<%@ Register TagPrefix="uc1" TagName="PrinterFriendlyPageButtonControl" Src="../Controls/PrinterFriendlyPageButtonControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapNearestControl" Src="../Controls/MapNearestControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc2:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css,nifty.css,FindStationMap.aspx.css,Map.css">
    </cc2:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#ChangeJourney" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
    <form id="FindStationMap" method="post" runat="server">
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
                    <uc1:ExpandableMenuControl ID="expandableMenuControl" runat="server" EnableViewState="False"
                        CategoryCssClass="HomePageMenu"></uc1:ExpandableMenuControl>
                </td>
                <!-- Page Content -->
                <td valign="top">
                    <cc2:RoundedCornerControl ID="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                        Corners="TopLeft" CssClass="bodyArea">
                        <!-- Main content control table -->
                        <table lang="en" cellspacing="0" width="100%" border="0">
                            <tr valign="top">
                                <!-- Journey Planning Column -->
                                <td valign="top">
                                    <!-- Top of Page Controls -->
                                    <table lang="en" cellspacing="0" width="100%" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <a name="ChangeJourney"></a>
                                                <div id="boxjourneychangesearchcontrol">
                                                    <div class="boxjourneychangesearchback">
                                                        <cc2:TDButton ID="commandBack" runat="server"></cc2:TDButton>
                                                        <cc2:TDButton ID="commandNewSearch" runat="server" Style="vertical-align: top"></cc2:TDButton>
                                                        <cc2:TDButton ID="commandAmendSearch" runat="server" Style="vertical-align: top"></cc2:TDButton>
                                                    </div>
                                                    <div class="boxjourneychangesearchchange">
                                                        
                                                        <uc1:PrinterFriendlyPageButtonControl ID="printerFriendlyPageButton" runat="server">
                                                        </uc1:PrinterFriendlyPageButtonControl>
                                                         <cc2:helpcustomcontrol id="helpIconSelect" runat="server" ></cc2:helpcustomcontrol>
                                                    </div>
                                                </div>
                                                <div id="boxtypeeightstd">
                                                    <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False" enableviewstate="false">
                                                        <div class="boxtypeerrormsgfour">
                                                            <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <uc1:FindStationResultsLocationControl ID="stationResultsLocationControl" runat="server">
                                                </uc1:FindStationResultsLocationControl>
                                                <div id="errormsg" class="boxtypeerrormsg" visible="false" runat="server">
                                                    <asp:Label ID="labelMessage" runat="server" Visible="False"></asp:Label></div>
                                                <div class="boxtypeeightstd">
                                                    <table summary="Go back or click next" width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                
                                                                <asp:Label ID="labelResultsTableTitle" CssClass="txtnineb" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="right">
                                                                <cc2:TDButton ID="commandNext" runat="server"></cc2:TDButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <asp:Label ID="labelNote" runat="server" CssClass="txtseven"></asp:Label>
                                                </div>
                                                <uc1:FindStationResultsTable ID="stationResultsControl" runat="server"></uc1:FindStationResultsTable>
                                                <div class="boxtypeeightstd">
                                                    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" summary="Command buttons">
                                                        <tr>
                                                            
                                                            <td align="right">
                                                                <cc2:TDButton ID="commandNext2" runat="server"></cc2:TDButton>&nbsp;
                                                                <cc2:TDButton ID="commandTravelFrom" runat="server"></cc2:TDButton>
                                                                <cc2:TDButton ID="commandTravelTo" runat="server"></cc2:TDButton></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <cc2:HelpLabelControl ID="helpLabelMapIcons" runat="server" Visible="False" CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                <cc2:HelpLabelControl ID="helpLabelMapToolsFindStation" runat="server" Visible="False"
                                                    CssMainTemplate="helpboxoutput"></cc2:HelpLabelControl>
                                                <table lang="en" id="mapcontrol" cellspacing="0" cellpadding="0" summary="Map">
                                                    <tr>
                                                        <td>
                                                           <uc1:MapNearestControl ID="mapNearestControl" runat="server"></uc1:MapNearestControl>
                                                        </td>
                                                    </tr>
                                                </table>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </cc2:RoundedCornerControl>
                </td>
            </tr>
        </table>
        <uc1:FooterControl ID="FooterControl1" runat="server"></uc1:FooterControl>
    </form>
    </div>
</body>
</html>
